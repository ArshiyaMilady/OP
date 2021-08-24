using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EXCEL = Microsoft.Office.Interop.Excel;

namespace OrdersProgress
{
    public class ThisProject
    {
        // ثبت جزییات سفارش در تاریخچه
        public void Create_OrderHistory(Models.Order order, string ol_description = null
            ,long user_index=-1,long user_level_index = -1)
        {
            if (user_index < 0) user_index = Stack.UserIndex;
            if (user_level_index < 0) user_level_index = Stack.UserLevel_Index;
            if (string.IsNullOrEmpty(ol_description))
                ol_description = Program.dbOperations.GetOrder_LevelAsync(order.CurrentLevel_Index).Description;

            Program.dbOperations.AddOrder_HistoryAsync(new Models.Order_History
            {
                Company_Index = Stack.Company_Index,
                User_Index = user_index,
                User_Level_Index=user_level_index,
                User_Name = Program.dbOperations.GetUserAsync(user_index).Real_Name,
                Order_Index = order.Index,
                OrderLevel_Index = order.CurrentLevel_Index,
                OrderLevel_Description = ol_description,
                DateTime_mi = DateTime.Now,
                DateTime_sh = Stack_Methods.DateTimeNow_Shamsi().Substring(0, 19),
            }); ;
        }

        // شناسه سفارشهایی که پیش نیازهای آنها گذرانده شده و سفارش می تواند وارد مرحله آنها شود
        public List<long> Next_OrderLevel_Indexes(string order_index)
        {
            // مراحلی که سفارش گذرانده است
            List<Models.Order_OL> lstOOL = Program.dbOperations
                .GetAllOrder_OLsAsync(Stack.Company_Index, order_index);
            if (!lstOOL.Any()) return new List<long>();

            // شناسه های مراحل فعال 
            List<long> lstEnabled_OL = Program.dbOperations.GetAllOrder_LevelsAsync(Stack.Company_Index).Select(d => d.Index).ToList();
            List<long> lstPassed_OL_Indexes = Program.dbOperations
                .GetAllOrder_OLsAsync(Stack.Company_Index, order_index)
                .Where(b => lstEnabled_OL.Contains(b.OrderLevel_Index))
                .Select(d => d.OrderLevel_Index).ToList();

            List<long> lstResult = new List<long>();
            // تمام مراحلی که آخرین مرحله گذرانده شده، پیش نیاز آنهاست
            foreach (Models.OL_Prerequisite olp in Program.dbOperations
                .GetAllOL_PrerequisitesAsync(Stack.Company_Index)
                .Where(b => lstEnabled_OL.Contains(b.Prerequisite_Index))
                .Where(d => lstEnabled_OL.Contains(d.OL_Index))
                //.Where(d => d.Prerequisite_Index == lstPassed_OL_Indexes.Last()
                .Where(j => lstPassed_OL_Indexes.Contains(j.Prerequisite_Index))
                .Where(n => !lstPassed_OL_Indexes.Contains(n.OL_Index))
                .ToList())
            {
                // آیا تمام پیش نیازهای این مرحله توسط سفارش گذرانده شده است
                bool b = false;
                foreach (long olp_i in Program.dbOperations.GetAllOL_PrerequisitesAsync
                    (Stack.Company_Index, olp.OL_Index)
                    //.Where(d => lstEnabled_OL.Contains(d.Prerequisite_Index))
                    .Select(j => j.Prerequisite_Index).ToList())
                    if (!(b = lstPassed_OL_Indexes.Contains(olp_i))) break;

                if (b) lstResult.Add(olp.OL_Index);
            }

            return lstResult;
        }

        // ثبت مراحل گذرانده
        // تفاوت با تاریخچه: در صورت یک یا چند برگشت سفارش ، مراحل را می توان از این جدول حذف نمود
        public bool AddOrder_OrderLevel(Models.Order order,long order_level_index = 0)
        {
            if (order_level_index == 0) order_level_index = order.CurrentLevel_Index;
            // اگر مرحله سفارش در لیست مراحل این سفارش نباشد، آنرا وارد کن
            if (!Program.dbOperations.GetAllOrder_OLsAsync(Stack.Company_Index, order.Index)
                .Any(d => d.OrderLevel_Index == order_level_index))
            {
                // ثبت جدول مراحل گذرانده سفارش
                Program.dbOperations.AddOrder_OL(new Models.Order_OL
                {
                    Company_Index = Stack.Company_Index,
                    Order_Index = order.Index,
                    OrderLevel_Index = order_level_index,
                });
                return true;
            }
            else return false;
        }

        // سفارش را کنسل کن
        public bool CancelOrder(Models.Order order,string cancel_description = null)
        {
            if (!Program.dbOperations.GetAllOrder_LevelsAsync(Stack.Company_Index).Any(d => d.CancelingLevel))
                return false;

            Models.Order_Level order_level = Program.dbOperations.GetAllOrder_LevelsAsync
                (Stack.Company_Index).First(d => d.CancelingLevel);

            foreach (Models.Order_OL order_ol in Program.dbOperations.GetAllOrder_OLsAsync
                                        (Stack.Company_Index, order.Index))
                Program.dbOperations.DeleteOrder_OLAsync(order_ol);

            if (Program.dbOperations.GetAllOrder_LevelsAsync
                (Stack.Company_Index).Any(d => d.CancelingLevel))
            {
                order.CurrentLevel_Index = Program.dbOperations.GetAllOrder_LevelsAsync
                    (Stack.Company_Index).First(d => d.CancelingLevel).Index;
            }
            else order.CurrentLevel_Index = 0;
            order.Level_Description = order_level.Description2;
            Program.dbOperations.UpdateOrderAsync(order);

            #region ثبت در تاریخچه
            order.CurrentLevel_Index = Program.dbOperations.GetAllOrder_LevelsAsync
                (Stack.Company_Index).First(d => d.CancelingLevel).Index;
            Create_OrderHistory(order, cancel_description);
            #endregion
            return true;
        }

        // سفارش را برگشت بده
        public bool ReturnOrder(Models.Order order,long ol_returned_index,string cause_of_returning)
        {
            if (!Program.dbOperations.GetAllOrder_LevelsAsync(Stack.Company_Index).Any(d => d.ReturningLevel))
                return false;

            //Models.Order_Level order_level = Program.dbOperations.GetAllOrder_LevelsAsync
            //    (Stack.Company_Index).First(d => d.ReturningLevel);

            // حذف آخرین مرحله انجام شده از مراحل سفارش
            Models.Order_OL order_ol = Program.dbOperations.GetAllOrder_OLsAsync
                (Stack.Company_Index, order.Index).OrderBy(d => d.Id).ToList().Last();
            Program.dbOperations.DeleteOrder_OL(order_ol);
            #region حذف تمام مراحل گذرانده ای که پیش نیاز آنها مرحله برگشت شده می باشد
            //foreach(Models.Order_OL ool in Program.dbOperations.GetAllOrder_OLsAsync
            //    (Stack.Company_Index, order.Index).OrderBy(d => d.Id).ToList())
            //{
                foreach (Models.OL_Prerequisite olp in Program.dbOperations.GetAllOL_PrerequisitesAsync
                    (Stack.Company_Index).Where(d => d.Prerequisite_Index == ol_returned_index).ToList())
                {
                    if(Program.dbOperations.GetAllOrder_OLsAsync
                        (Stack.Company_Index, order.Index).Any(d=>d.OrderLevel_Index == olp.OL_Index))
                    {
                        Models.Order_OL ool = Program.dbOperations.GetAllOrder_OLsAsync(Stack.Company_Index
                            , order.Index).First(d => d.OrderLevel_Index == olp.OL_Index);
                        Program.dbOperations.DeleteOrder_OL(ool);
                    }
                }
            //}
            #endregion

            // پیدا کردن مرحله قبلی از جدول مراحل گذرانده سفارش
            order.PreviousLevel_Index = Program.dbOperations.GetAllOrder_OLsAsync
                (Stack.Company_Index, order.Index)
                .Where(d => (d.OrderLevel_Index != order.CurrentLevel_Index)
                && (d.OrderLevel_Index != ol_returned_index))
                .OrderBy(d => d.Id).ToList().Last().OrderLevel_Index;

            order.CurrentLevel_Index = ol_returned_index;
            order.Level_Description = "سفارش برگشت شد";
            Program.dbOperations.UpdateOrder(order);
            order.NextLevel_Index = Next_OrderLevel_Indexes(order.Index).First();// order.CurrentLevel_Index;
            Program.dbOperations.UpdateOrder(order);

            #region ثبت در تاریخچه
            order.CurrentLevel_Index = Program.dbOperations.GetAllOrder_LevelsAsync
                (Stack.Company_Index).First(d => d.ReturningLevel).Index;
            Create_OrderHistory(order, "سفارش به مرحله «"
                + Program.dbOperations.GetOrder_LevelAsync(ol_returned_index).Description2
                        + "» برگشت شد." + " علت برگشت : " + cause_of_returning);
            #endregion

            return true;
        }

        // تغییر مرحله سفارش
        public bool Change_Order_Level(Models.Order order,long order_level_index)
        {
            order.PreviousLevel_Index = order.CurrentLevel_Index;
            order.CurrentLevel_Index = order_level_index;
            order.Level_Description = Program.dbOperations.GetOrder_LevelAsync(order_level_index).Description2;
            Program.dbOperations.UpdateOrder(order);

            Create_OrderHistory(order);
            if (AddOrder_OrderLevel(order))
            {                
                // تعیین مرحله بعدی
                if (Next_OrderLevel_Indexes(order.Index).Any())
                {
                    order.NextLevel_Index = Next_OrderLevel_Indexes(order.Index).First();
                    Program.dbOperations.UpdateOrderAsync(order);
                }
            }

            return true;
        }

        #region تمام زیرشاخه های یک کالا به همراه سطح آنها
        public List<Relation_by_Level> AllSubRelations(string code)
        {
            lstRL = new List<Relation_by_Level>();
            SubRelations(code);
            return lstRL;
        }

        private List<Relation_by_Level> lstRL = new List<Relation_by_Level>();
        private long index = 0;
        private void SubRelations(string code, int level = 0, string top_code = null,long top_index=1,double quantity=1)
        {
            //MessageBox.Show(level.ToString());

            Models.Item item = Program.dbOperations.GetItemAsync(Stack.Company_Index,code, true);
            if (item == null) return;

            Relation_by_Level rl = new Relation_by_Level();
            rl.Index = ++index;
            rl.Code = code;
            rl.Level = level;
            rl.Name = item.Name_Samll;
            rl.IsModule = item.Module;
            rl.Top_Code = top_code;
            rl.Quantity = quantity;
            if(!string.IsNullOrEmpty(top_code))
                rl.Top_Index = top_index;
            lstRL.Add(rl);

            if (item.Module)
            {
                top_index = index;
                //foreach (string sCode in Program.dbOperations.GetAllModulesAsync(1, code)
                //    .Select(d => d.Item_Code_Small).ToList())
                foreach (Models.Module module in Program.dbOperations.GetAllModulesAsync(Stack.Company_Index,1, code).ToList())
                {
                    SubRelations(module.Item_Code_Small, level + 1, code, top_index,module.Quantity * quantity);
                }
            }
        }
        #endregion

        public void Create_RequestHistory(Models.Warehouse_Request wr,string description=null)
        {
            #region ثبت درخواست در تاریخچه
            Program.dbOperations.AddWarehouse_Request_HistoryAsync(
                new Models.Warehouse_Request_History
                {
                    Company_Index = Stack.Company_Index,// wr.Company_Index,
                    Warehouse_Request_Index = wr.Index_in_Company,
                    User_Index = Stack.UserIndex,// wr.User_Index,
                    User_Level_Index = Stack.UserLevel_Index, // wr.UserLevel_Index,
                    User_Name = Stack.UserName, //  wr.User_Name,
                    DateTime_mi = DateTime.Now,
                    Date_sh = Stack_Methods.Miladi_to_Shamsi_YYYYMMDD(DateTime.Now),
                    Time = Stack_Methods.NowTime_HHMMSSFFF(":",false),
                    Description = description,
                }) ;
            #endregion

        }


        // ایجاد یک فایل اکسل از یک دیتاگرید- در این تابع فقط ستونها 
        // و ردیفهای مرئی و قابل مشاهده در فایل اکسل می آیند
        // ضمنا سلولهای اکسل همرنگ سلولهای دیتاگرید خواهند بود
        // ضمنا از سطر مشخصی هم شروع به انجام این عمل می کند
        // متن سربرگ را در سلول یک و یک وارد می کند
        public bool DGV_to_Excel_for_ThisProject(DataGridView dgv, string sExcelFileName = null
            , string SheetName = null, ProgressBar progressBar1 = null
            , bool bMerge_in_ThisProject = false, int base_empty_column = -1
            , string sRightHeader = null, bool bClose_Without_Save = false
            , string sExceptionFilter = "", bool bCloseExcelAfterSave = false
            , int ciFirstExcelRow = 1, string sSpecialText = null)
        {
            if (dgv.Rows.Count < 1) return false;
            if (dgv.Columns.Count < 1) return false;

            try
            {
                EXCEL.Application excelApp = new EXCEL.Application();
                excelApp.Visible = false;

                EXCEL._Workbook wbExport = null;
                if (!string.IsNullOrEmpty(sExcelFileName) && File.Exists(sExcelFileName))
                    wbExport = excelApp.Workbooks.Open(sExcelFileName);
                else
                    wbExport = excelApp.Workbooks.Add();

                if (string.IsNullOrEmpty(SheetName))
                    SheetName = "Sheet1";

                bool bIsSheetExists = wbExport.Worksheets.OfType<EXCEL.Worksheet>()
                    .Any(ws => ws.Name.ToLower().Equals(SheetName.ToLower()));
                if(!bIsSheetExists)
                {
                    MessageBox.Show("شیت " + SheetName +" یافت نشد!","خطا");
                    excelApp.WindowState = EXCEL.XlWindowState.xlMaximized;
                    excelApp.Visible = true;
                    return false;
                }

                EXCEL.Worksheet wsExportSheet = wbExport.Worksheets[SheetName];

                wsExportSheet.DisplayRightToLeft = true;
                wsExportSheet.Cells.NumberFormat = "@";
                // عنوان شیت که می تواند شامل تاریخ یا ... باشد
                if (!string.IsNullOrWhiteSpace(sSpecialText))
                {
                    if (ciFirstExcelRow > 1)
                    {
                        wsExportSheet.Range["A1"].Value = sSpecialText;
                        //ciFirstExcelRow++;
                    }
                }

                // ثبت عنوان سر ستونها
                int iExcelColumn = 1;
                int iExcelRow = ciFirstExcelRow;

                //bool bIsItFirstColumn = true;

                for (int j = 0; j < dgv.Columns.Count; j++)
                {
                    if (!string.IsNullOrWhiteSpace(sExceptionFilter))
                        if (dgv.Columns[j].HeaderText.Contains(sExceptionFilter)) continue;

                    if (!dgv.Columns[j].Visible) continue;

                    iExcelRow = ciFirstExcelRow;
                    wsExportSheet.Cells[ciFirstExcelRow, iExcelColumn].Value = dgv.Columns[j].HeaderText;
                    for (int i = 0; i < dgv.Rows.Count; i++)
                    {
                        //try 
                        {
                            if (dgv.Rows[i].Visible)
                            {
                                iExcelRow++;
                                if (dgv[j, i].Value != null)
                                {
                                    try
                                    { wsExportSheet.Cells[iExcelRow, iExcelColumn].Value = dgv[j, i].Value; }
                                    catch { }
                                }
                                Color back_color = dgv[j, i].InheritedStyle.BackColor;
                                if (back_color.Name.ToLower().Equals("window")) back_color = Color.White;
                                wsExportSheet.Cells[iExcelRow, iExcelColumn].Interior.Color = back_color;

                                //if (MessageBox.Show(dgv[j, i].InheritedStyle.BackColor.Name,"",MessageBoxButtons.OKCancel)
                                //    == DialogResult.Cancel) return false;

                            }
                        }
                        //catch { continue; }
                    }
                    iExcelColumn++;
                    //bIsItFirstColumn = false;
                    if (progressBar1 != null)
                    {
                        if (progressBar1.Value < progressBar1.Maximum)
                        {
                            progressBar1.Value++;
                            Application.DoEvents();
                        }
                    }
                }
                //MessageBox.Show("iExcelColumn = " + iExcelColumn);

                wsExportSheet.Columns.AutoFit();
                wsExportSheet.Rows[RowIndex: 1].Font.Bold = true;
                wsExportSheet.Rows[RowIndex: ciFirstExcelRow].Font.Bold = true;
                if (iExcelColumn > 1)
                {
                    wsExportSheet.Range[wsExportSheet.Cells[ciFirstExcelRow, 1], wsExportSheet
                        .Cells[iExcelRow, iExcelColumn - 1]].Borders.LineStyle
                        = EXCEL.XlLineStyle.xlContinuous;
                    //wsExportSheet.Range[wsExportSheet.Cells[1, 1], wsExportSheet
                    //   .Cells[dgv.Rows.Count + 1, iColCounter - 1]].WrapText = false;
                    wsExportSheet.Range[wsExportSheet.Cells[ciFirstExcelRow, 1], wsExportSheet
                       .Cells[iExcelRow, iExcelColumn - 1]].ShrinkToFit = false;
                }

                if(bMerge_in_ThisProject)
                {
                    Merge_in_ThisProject(wsExportSheet,2, dgv.Rows.Count);
                    Merge_in_ThisProject(wsExportSheet,3, dgv.Rows.Count);
                }

                // حذف سطرهای خالی
                if(base_empty_column>0)
                {
                    while (wsExportSheet.Cells[iExcelRow+1, base_empty_column].Value == null)
                        wsExportSheet.Rows[iExcelRow+1].delete();
                }

                if (!string.IsNullOrEmpty(sRightHeader))
                    wsExportSheet.PageSetup.RightHeader = sRightHeader;

                if (!string.IsNullOrWhiteSpace(sExcelFileName))
                {
                    if (File.Exists(sExcelFileName))
                    {
                        wbExport.Save();
                    }
                    else
                    {
                        wbExport.SaveAs(sExcelFileName);
                        if (bCloseExcelAfterSave)
                        {
                            excelApp.Quit();
                            return true;
                        }
                    }
                }
                //else
                {
                    excelApp.WindowState = EXCEL.XlWindowState.xlMaximized;
                    excelApp.Visible = true;
                }

                return true;
            }
            catch { return false; }
        }


        public void Merge_in_ThisProject(EXCEL.Worksheet ws,int column_number,int last_row_number)
        {
            if (last_row_number < 3) return;

            int i1 = 2;
            for (int i = 3; i <= last_row_number; i++)
            {
                if (ws.Cells[i, column_number].Value != null)
                {
                    if (Convert.ToString(ws.Cells[i1, column_number].Value).Equals(Convert.ToString(ws.Cells[i, column_number].Value)))
                        ws.Cells[i, column_number].Value = null;
                    else
                    {
                        if(i-i1 > 1)
                            ws.Range[ws.Cells[i1, column_number], ws.Cells[i - 1, column_number]].Merge();
                        i1 = i;
                    }
                }
            }
        }

        // کل فایل اکسل را به پی دی اف تبدیل میکند
        // امکان انتخاب شیت وجود ندارد
        public void Excel_to_Pdf(string ExcelFileName, string PDF_FileNameSaved, string WorkSheetName = null)
        {
            EXCEL.Application excelApp = new EXCEL.Application();
            excelApp.Visible = false;
            EXCEL._Workbook wbExport = excelApp.Workbooks.Open(ExcelFileName);
            // اگر شیت خاصی انتخاب نشده باشد
            if (string.IsNullOrEmpty(WorkSheetName))
                wbExport.ExportAsFixedFormat(EXCEL.XlFixedFormatType.xlTypePDF, PDF_FileNameSaved);
            else   // اگر شیت خاصی مد نظر باشد
            {
                if (wbExport.Worksheets.OfType<EXCEL.Worksheet>()
                    .Any(ws => ws.Name.ToLower().Equals(WorkSheetName.ToLower())))
                {
                    EXCEL.Worksheet ws = wbExport.Worksheets[WorkSheetName];
                    ws.ExportAsFixedFormat(EXCEL.XlFixedFormatType.xlTypePDF, PDF_FileNameSaved);
                }
            }

            Application.DoEvents();
            wbExport.Close(false);
            excelApp.Quit();
        }


        // تبدیل تصویر به آرایه باینری
        public byte[] ConvertImageToByteArray(string sPathFileName)
        {
            // now open the file ..
            FileStream fs = new FileStream(sPathFileName, FileMode.Open, FileAccess.Read);

            // or you may use 
            // FileStream fs = (FileStream)openFileDialog.OpenFile();

            BinaryReader br = new BinaryReader(fs);
            byte[] buffer = br.ReadBytes((int)fs.Length);
            br.Close();
            fs.Close();
            return buffer;
        }

        public Bitmap ByteToImage(byte[] blob)
        {
            MemoryStream mStream = new MemoryStream();
            byte[] pData = blob;
            mStream.Write(pData, 0, Convert.ToInt32(pData.Length));
            Bitmap bm = new Bitmap(mStream, false);
            mStream.Dispose();
            return bm;
        }

    }
}
