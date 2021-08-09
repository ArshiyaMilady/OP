using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EXCEL = Microsoft.Office.Interop.Excel;
using OrdersProgress.Models;
using System.IO;
using System.Runtime.InteropServices;
using File = System.IO.File;

namespace OrdersProgress
{
    public partial class Mm110_Progress_by_UserPriorities : X210_ExampleForm_Normal
    {
        EXCEL.Application excelApp;
        EXCEL.Workbook wb;
        int nItemsQuantity = -1;    // تعداد کل کالاهای تعریف شده در انبار

        public Mm110_Progress_by_UserPriorities()
        {
            InitializeComponent();
        }

        private void M110_Progress_by_UserPriorities_Shown(object sender, EventArgs e)
        {
            //MessageBox.Show("d");
            UP();
        }

        // UP : User Priorities
        private void UP()
        {
            string ExcelFilePath = Application.StartupPath + @"\Link.xlsx";
            if (!File.Exists(ExcelFilePath))
            {
                MessageBox.Show("فایل " + "Link.xlsx" + " یافت نشد!"
                    , "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                return;
            }

            bool bCloseExcel = false;

            excelApp = new EXCEL.Application();
            excelApp.WindowState = EXCEL.XlWindowState.xlMaximized;
            excelApp.DisplayAlerts = false;
            excelApp.Visible = false;

            wb = excelApp.Workbooks.Open(ExcelFilePath);

            #region خطایابی
            #region بررسی وجود شیت ها
            if (!wb.Worksheets.Cast<EXCEL.Worksheet>().Any(d => d.Name.Equals("UserPriorities")))
            {
                MessageBox.Show("شیت " + "UserPriorities" + " یافت نشد!"
                    , "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                bCloseExcel = true;
            }

            if (!bCloseExcel)
            {
                if (!wb.Worksheets.Cast<EXCEL.Worksheet>().Any(d => d.Name.Equals("Data")))
                {
                    MessageBox.Show("شیت " + "Data" + " یافت نشد!"
                        , "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    bCloseExcel = true;
                }
                else
                {
                    EXCEL.Worksheet wsData = wb.Worksheets["Data"];
                    if (wsData.Range["E2"].Value != null)
                    {
                        if (wsData.Range["E2"].Value.ToString().Length > 0)
                        {
                            if(int.TryParse(wsData.Range["E2"].Value.ToString(),out nItemsQuantity))
                                nItemsQuantity = Convert.ToInt32(wsData.Range["E2"].Value);
                        }
                    }

                    bCloseExcel = nItemsQuantity <= 0;
                }
            }

            if (!bCloseExcel)
            {
                if (!wb.Worksheets.Cast<EXCEL.Worksheet>().Any(d => d.Name.Equals("Priorities")))
                {
                    MessageBox.Show("شیت " + "Priorities" + " یافت نشد!"
                        , "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    bCloseExcel = true;
                }
            }
            #endregion
            #endregion

            if (bCloseExcel)
            {
                wb.Close(SaveChanges: true);
                excelApp.Workbooks.Close();
                excelApp.Quit();

                while (Marshal.ReleaseComObject(wb) != 0) { }
                while (Marshal.ReleaseComObject(excelApp.Workbooks) != 0) { }
                while (Marshal.ReleaseComObject(excelApp) != 0) { }
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            else
            {
                //  Priorities دریافت اطلاعات از شیت
                EXCEL.Worksheet wsPr = wb.Worksheets["Priorities"];
                EXCEL.Worksheet wsUP = wb.Worksheets["UserPriorities"];
                EXCEL.Worksheet wsOrders = wb.Worksheets["لیست سفارش کل فروشگاه ها"];
                EXCEL.Worksheet wsProgress_UP = wb.Worksheets["Progress_UP"];

                wsUP.Range["E2:BB"+nItemsQuantity].Value = wsOrders.Range["E2:BB" + nItemsQuantity].Value;
                wsUP.Range["D2:D1000"].Value = wsUP.Range["B2:B1000"].Value;

                List<OrderPriority> lstOP = GetPriorities(wsPr);
                //excelApp.Visible = true;
                //return;

                foreach (OrderPriority op in lstOP.OrderBy(d => d.Priority).ToList())
                {
                    // پیدا کردن شماره ستون مربوط به سفارش
                    int j = OrderColumn(wsUP, op.Order_Title);
                    int nTotalRemainedQuantity = 0;  // جمیع مقادیر باقیمانده بعد از تفاضل با موجودی انبار
                    #region بررسی پیشرفت سفارش با توجه به اولویت آن
                    int k = 1;
                    while (++k<=nItemsQuantity)    // نام سفارش نباید خالی باشد
                    {
                        if (wsUP.Cells[k, j].Value != null)
                        {
                            if (wsUP.Cells[k, j].Value.ToString().Length > 0)
                            {
                                // تعداد کالای مورد نیاز در سفارش
                                if (int.TryParse(wsUP.Cells[k, j].Value.ToString(), out int qOrder))
                                {
                                    if (qOrder > 0)
                                    {
                                        // تعداد کالای باقیماده در انبار
                                        int qStoreInventory = Convert.ToInt32(wsUP.Cells[k, 4].Value);
                                        if (qStoreInventory > 0)
                                        {
                                            // اگر تعداد مورد نیاز بیشتر از تعداد در انبار باشد
                                            if (qOrder >= qStoreInventory)
                                            {
                                                if (qOrder == qStoreInventory)
                                                    wsUP.Cells[k, j].Value = null;
                                                else
                                                    wsUP.Cells[k, j].Value = qOrder - qStoreInventory;

                                                wsUP.Cells[k, 4].Value = 0; // صفر کردن موجودی

                                                nTotalRemainedQuantity += qOrder - qStoreInventory;
                                            }
                                            else
                                            {
                                                wsUP.Cells[k, j].Value = null;
                                                wsUP.Cells[k, 4].Value = qStoreInventory - qOrder; // صفر کردن موجودی
                                            }
                                        }
                                        else nTotalRemainedQuantity += qOrder;
                                    }

                                }
                            }
                        }
                        Application.DoEvents();
                    }
                    //Application.DoEvents();
                    #endregion

                    op.RemainedQuantity = nTotalRemainedQuantity;
                    if (nTotalRemainedQuantity == 0) op.ProgressPercent = 0;
                    else op.ProgressPercent = 100 - (100 * nTotalRemainedQuantity / (int)op.TotalQuantity);

                    int i2 = OrderRow(wsProgress_UP, op.Order_Title,3);
                    if(i2>0)
                        wsProgress_UP.Cells[i2, 5].Value = op.ProgressPercent;

                    //System.Threading.Thread.Sleep(5);
                    Application.DoEvents();
                }

                dgvData.DataSource = lstOP.OrderBy(d => d.Priority).ToList();
                ArrangeColumns();

                Application.DoEvents();
                wsUP.Activate();
                wsUP.Range["A1"].Select();
                wb.Save();
                excelApp.Visible = true;
                panel1.Enabled = true; 
                pictureBox1.Visible = false;
                progressBar1.Visible = false;
            }

        }

        private void ArrangeColumns()
        {
            #region ترجمه سر ستونها و مخفی کردن بعضی ستونها
            foreach (DataGridViewColumn col in dgvData.Columns)
            {
                switch (col.Name)
                {
                    //case "Id":
                    //    col.HeaderText = "Id";
                    //    break;
                    case "OrderName":
                        col.HeaderText = "نام سفارش";
                        col.ReadOnly = true;
                        col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        col.DefaultCellStyle.BackColor = Color.WhiteSmoke;
                        break;
                    case "Priority":
                        col.HeaderText = "اولویت";
                        break;
                    case "TotalQuantity":
                        col.HeaderText = "مجموع تعداد اقلام";
                        break;
                    case "ProgressPercent":
                        col.HeaderText = "درصد پیشرفت";
                        col.DefaultCellStyle.ForeColor = Color.ForestGreen;
                        break;
                    case "TotalRemainedQuantity":
                        col.HeaderText = "جمع باقیمانده ها";
                        break;
                    default: col.Visible = false; break;
                }
            }

            #endregion
        }

        // پیدا کردن شماره ستون با توجه به نام سفارش
        private int OrderColumn(EXCEL.Worksheet ws, string Order_Name)
        {
            int j = 4;
            while(++j < 60)
            {
                if(ws.Cells[1,j].Value.ToString().Equals(Order_Name))
                {
                    return j;
                }
            }

            return -1;
        }

        private int OrderRow(EXCEL.Worksheet ws, string Order_Name,int colNo,int InitialRow=1)
        {
            int i = InitialRow;
            while(++i < 60)
            {
                if (ws.Cells[i, colNo].Value != null)
                {
                    if (ws.Cells[i, colNo].Value.ToString().Equals(Order_Name))
                        return i;
                }
            }

            return -1;
        }

        //  Priorities دریافت اطلاعات از شیت
        private List<OrderPriority> GetPriorities(EXCEL.Worksheet wsPr)
        {
            List<OrderPriority> lstOP = new List<OrderPriority>();
            int i = 1;
            while (wsPr.Cells[++i, 3].Value != null)
            {
                if (wsPr.Cells[i, 3].Value.ToString().Length > 0)
                {
                    if (int.TryParse(wsPr.Cells[i, 4].Value.ToString(), out int priority))
                    {
                        if (priority > 0)
                        {
                            lstOP.Add(new OrderPriority
                            {
                                Id = Convert.ToInt32(wsPr.Cells[i, 1].Value),
                                TotalQuantity = Convert.ToInt32(wsPr.Cells[i, 2].Value),
                                Order_Title = Convert.ToString(wsPr.Cells[i, 3].Value),
                                Priority = priority,
                            });
                        }
                    }
                }
            }
            return lstOP;
        }

        private void BtnReturn_Click(object sender, EventArgs e)
        {
            Close();
        }

        // ---
        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            if(nItemsQuantity<=0)
            {
                MessageBox.Show("آخرین سطر کالاها در شیت «لیست سفارش کل فروشگاه ها» مشخص نمی باشد"
                    , "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            panel1.Enabled = false;
            Application.DoEvents();

            //EXCEL.Worksheet wsUserPriorities = wb.Worksheets["UserPriorities"];
            EXCEL.Worksheet wsProgress_UP = wb.Worksheets["Progress_UP"];
            //EXCEL.Worksheet wsInventory = wb.Worksheets["Inventory"];
            EXCEL.Worksheet wsPriorities = wb.Worksheets["Priorities"];
            //EXCEL.Worksheet wsOrders = wb.Worksheets["لیست سفارش کل فروشگاه ها"];

            // کوپی کردن اطلاعات جدید در شیت سفارشها
            //wsOrders.Range["E2:BB" + nItemsQuantity].Value = wsUserPriorities.Range["E2:BB" + nItemsQuantity].Value;
            // بروزرسانی موجودی انبار
            //wsInventory.Range["B2:B1000"].Value= wsUserPriorities.Range["D2:D1000"].Value ;
            // بروزرسانی پیشرفت سفارشها
            wsPriorities.Range["E2:E1000"].Value = wsProgress_UP.Range["E2:E1000"].Value;
            wb.Save();

            Application.DoEvents();
            panel1.Enabled = true;
        }

        private void TsmiMakeChecklist_Click(object sender, EventArgs e)
        {
            if (!wb.Worksheets.Cast<EXCEL.Worksheet>().Any(d => d.Name.Equals("Checklist_temp")))
            {
                MessageBox.Show("شیت " + "Checklist_temp" + " یافت نشد!"
                    , "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            panel1.Enabled = false;
            excelApp.Visible = false;

            if (wb.Worksheets.Cast<EXCEL.Worksheet>().Any(d => d.Name.Equals("Checklist")))
                wb.Worksheets["Checklist"].Delete();

            #region ایجاد شیت چک لیست
            EXCEL.Worksheet wsChecklist_temp = wb.Worksheets["Checklist_temp"];
            int index = wsChecklist_temp.Index;
            wsChecklist_temp.Copy(wsChecklist_temp, Type.Missing);
            EXCEL.Worksheet wsChecklist = (EXCEL.Worksheet)wb.Worksheets["Checklist_temp (2)"];
            wsChecklist.Name = "Checklist";
            wsChecklist.Visible = EXCEL.XlSheetVisibility.xlSheetVisible;
            #endregion

            EXCEL.Worksheet wsUP = wb.Worksheets["UserPriorities"];
            EXCEL.Worksheet wsOrders = wb.Worksheets["لیست سفارش کل فروشگاه ها"];
            EXCEL.Worksheet wsInventory = wb.Worksheets["Inventory"];
            string OrderName = dgvData.CurrentRow.Cells["OrderName"].Value.ToString();
            wsChecklist.PageSetup.RightHeader = OrderName;
            int j = OrderColumn(wsUP, OrderName);

            int m = 1;
            int k = 1;
            while (++k <= nItemsQuantity)    
            {
                //bool bIsOrdersEmpty = wsOrders.Cells[k, j].Value == null;
                //if (!bIsOrdersEmpty) bIsOrdersEmpty = wsOrders.Cells[k, j].Value.ToString().Length == 0;

                //bool bIsUP_Empty = wsUP.Cells[k, j].Value == null;
                //if (!bIsUP_Empty) bIsUP_Empty = wsUP.Cells[k, j].Value.ToString().Length == 0;

                //if(!bIsOrdersEmpty)
                {
                    int qO = Convert.ToInt32(wsOrders.Cells[k, j].Value);
                    int qUP = 0;
                    //if (!bIsUP_Empty)
                        qUP = Convert.ToInt32(wsUP.Cells[k, j].Value);

                    if (qO - qUP > 0)
                    {
                        m++;
                        wsChecklist.Cells[m, 3].Value = wsOrders.Cells[k, 3].Value; // کد ماژول
                        wsChecklist.Cells[m, 4].Value = wsOrders.Cells[k, 4].Value; // نام قطعه
                        wsChecklist.Cells[m, 6].Value = qO - qUP;
                        //wsInventory.Cells[k, 5].Value = qI - (qO - qUP);
                    }

                    int qI = Convert.ToInt32(wsInventory.Cells[k, 2].Value);
                    wsInventory.Cells[k, 5].Value = qI - (qO - qUP); ;
                }
            }

            int lastRow = m;
            while (wsChecklist.Cells[++lastRow, 6].Value == null);
            int qA = Convert.ToInt32(wsChecklist.Cells[lastRow, 6].Value);
            wsChecklist.Cells[lastRow, 6].Value = qA;

            //MessageBox.Show(lastRow.ToString());
            for (int d = lastRow - 1; d > m; d--)
                wsChecklist.Rows[d].delete();

           
            wsChecklist.Activate();
            Application.DoEvents();
            panel1.Enabled = true;
            excelApp.Visible = true;

            if (MessageBox.Show("آیا با توجه به اطلاعات شیت " + "Checklist" + " شیتهای «لیست سفارش کل فروشگاه ها» و "
                + "Inventory" + " (موجودی انبار) تغییر کنند؟", "خیلی مهم"
                , MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            wsOrders.Range[wsOrders.Cells[1, j], wsOrders.Cells[nItemsQuantity, j]].Value =
            wsUP.Range[wsUP.Cells[1, j], wsUP.Cells[nItemsQuantity, j]].Value;

            wsInventory.Range["B2:B1000"].Value = wsInventory.Range["E2:E1000"].Value;
            wsInventory.Range["E2:E1000"].Value = null;

            if (MessageBox.Show("پس از تأیید این پیام، امکان بازگشت به داده های قبل موجود نمی باشد. "
                +"آیا مایل به ذخیره اطلاعات می باشید؟", "خیلی مهم"
                , MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            wb.Save();
        }

        int iX = 0, iY = 0;
        private void dgvOrders_MouseDown(object sender, MouseEventArgs e)
        {
            iX = e.X;
            iY = e.Y;
        }

        private void dgvData_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0) return;
            if (e.Button == MouseButtons.Right)
            {
                /////// Do something ///////
                // انتخاب سلولی که روی آن کلیک راست شده است
                dgvData.CurrentCell = dgvData[e.ColumnIndex, e.RowIndex];
                contextMenuStrip1.Show(dgvData, new Point(iX, iY));
            }
        }
    }
}
