using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using EXCEL = Microsoft.Office.Interop.Excel;


namespace OrdersProgress
{
    public partial class K1300_Items : X210_ExampleForm_Normal
    {
        public K1300_Items()
        {
            InitializeComponent();

            btnDeleteAllItems.Visible = Stack.UserLevel_Type == 1;
            btnDeleteAllImages.Visible = Stack.UserLevel_Type == 1;

            panel2.Visible = Stack.lstUser_ULF_UniquePhrase.Contains("jn2110"); // امکان تغییر
            btnAddNew.Visible = Stack.lstUser_ULF_UniquePhrase.Contains("jn2120"); // امکان افزودن
            btnGetImages.Visible= Stack.lstUser_ULF_UniquePhrase.Contains("jn2110"); // امکان افزودن تصویر
        }

        private void K1300_Items_Shown(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Maximized;
            //Application.DoEvents();

            cmbST_Name.SelectedIndex = 0;
            cmbST_SmallCode.SelectedIndex = 0;
            //dataGridView1.DataSource = Program.dbOperations.GetAllModulesAsync();
            //dgvData.DataSource = Program.dbOperations.GetAllItemsAsync(Stack.Company_Index);
            comboBox1.SelectedIndex = 0;
            ShowData();
        }

        private List<Models.Item> GetData()
        {
            int enableType = 0;
            switch (comboBox1.SelectedIndex)
            {
                case 0: enableType = 1; break;
                case 1: enableType = -1; break;
                case 2: enableType = 0; break;
            }

            if (radModule.Checked) return Program.dbOperations.GetAllItemsAsync(Stack.Company_Index, enableType).Where(d => d.Module).ToList();
            else if (radNotModule.Checked) return Program.dbOperations.GetAllItemsAsync(Stack.Company_Index, enableType).Where(d => !d.Module).ToList();
            else return Program.dbOperations.GetAllItemsAsync(Stack.Company_Index, enableType);

        }

        private void ShowData(bool ChangeHeaderTexts = true)
        {
            dgvData.DataSource = GetData();

            #region ترجمه سر ستونها و مخفی کردن بعضی ستونها
            if (ChangeHeaderTexts)
            {
                foreach (DataGridViewColumn col in dgvData.Columns)
                {
                    switch (col.Name)
                    {
                        //case "Index":
                        //    col.HeaderText = "index";
                        //    break;
                        case "Enable":
                            col.HeaderText = "فعال؟";
                            col.Width = 50;
                            break;
                        case "Code_Small":
                            col.HeaderText = "کد";
                            //col.DefaultCellStyle.BackColor = Color.LightGray;
                            break;
                        case "Name_Samll":
                            col.HeaderText = "نام کالا";
                            //col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                            col.Width = 200;
                            break;
                      case "Weight":
                            col.HeaderText = "وزن(کیلوگرم)";
                            break;
                        case "FixedPrice":
                            col.HeaderText = "بهای تمام شده (ریال)";
                            //col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                            col.Width = 100;
                            break;
                        case "SalesPrice":
                            col.HeaderText = "قیمت فروش (ریال)";
                            //col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                            col.Width = 100;
                            break;
                        case "Code_Full":
                            col.HeaderText = "کد کامل";
                            col.Width = 100;
                            break;
                        case "Name_Full":
                            col.HeaderText = "نام کامل";
                            col.Width = 100;
                            break;
                        default: col.Visible = false; break;
                    }
                }
            }
            #endregion
        }

        private void BtnReturn_Click(object sender, EventArgs e)
        {
            Close();
        }

        int iNewRow = -1;
        bool JustEdit = true;
        private void BtnAddNew_Click(object sender, EventArgs e)
        {
            new K1304_Item_ChooseWarehouse().ShowDialog();
            if (Stack.ix < 0) return;

            radAll.Checked = true;
            RadModule_CheckedChanged(null, null);
            Application.DoEvents();

            long index = Program.dbOperations.GetNewIndex_Item();

            if (Program.dbOperations.AddItem(new Models.Item
            {
                Company_Index = Stack.Company_Index,
                Index = index,
                Code_Small = "کد " + (index % 100000),
                Name_Samll = "نام " + (index % 100000),
                Warehouse_Index = Stack.ix,
                Enable = true,
            }) > 0)
            {
                chkCanEdit.Checked = false;
                ShowData(false);
                iNewRow = dgvData.Rows.Count - 1;
                dgvData.CurrentCell = dgvData["Name_Samll", iNewRow];
                dgvData.Focus();
            }

            JustEdit = false;
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("آیا از حذف همه کالاها اطمینان دارید؟"
                , "اخطار", MessageBoxButtons.YesNo,MessageBoxIcon.Warning)
                == DialogResult.No) return;

            if (MessageBox.Show("با انجام این عمل ، تمام روابط کالاها و جداول دیگر از بین خواهد رفت"
                + "\n" + "آیا از حذف تمام کالا اطمینان دارید؟", "اخطار 2"
                , MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

            Program.dbOperations.DeleteAllModulesAsync();
            Program.dbOperations.DeleteAllItemsAsync();
            dgvData.DataSource = Program.dbOperations.GetAllItemsAsync(Stack.Company_Index);
        }

        private void TsmiDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("آیا از حذف این مورد اطمینان دارید؟", "", MessageBoxButtons.YesNo)
                == DialogResult.No) return;

            try
            {
                string sCode_Small = Convert.ToString(dgvData.CurrentRow.Cells["Code_Small"].Value);
                Models.Item item = Program.dbOperations.GetItemAsync(Stack.Company_Index,sCode_Small);
                if(item.Module)
                {
                    //MessageBox.Show(item.Code_Small,"module = true");
                    if (Program.dbOperations.GetAllModulesAsync(Stack.Company_Index,0, sCode_Small).Any())
                    {
                        //MessageBox.Show(item.Code_Small);
                        List<Models.Module> lst = Program.dbOperations.GetAllModulesAsync(Stack.Company_Index,0, sCode_Small);
                        foreach (var m in lst) Program.dbOperations.DeleteModule(m);
                    }
                }
                Program.dbOperations.DeleteItemAsync(item);
                dgvData.DataSource = Program.dbOperations.GetAllItemsAsync(Stack.Company_Index);
                //dataGridView1.DataSource =  Program.dbOperations.GetAllModulesAsync();
            }
            catch { MessageBox.Show("خطا در اجرای عملیات"); }
        }

        int iX = 0, iY = 0;
        private void dgvData_MouseDown(object sender, MouseEventArgs e)
        {
            iX = e.X;
            iY = e.Y;
        }

        private void dgvData_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0) return;
            string sc = Convert.ToString(dgvData["Code_Small", e.RowIndex].Value);
            //#region نمایش تصویر کالا در صورت وجود
            //Models.Item_File item_file = Program.dbOperations.GetItem_FileAsync(sc, 1,true);
            //if (item_file != null)
            //    pictureBox2.Image = new ThisProject().ByteToImage
            //        (Program.dbOperations.GetFileAsync(item_file.File_Index).Content);
            //else pictureBox2.Image = null;
            //#endregion

            if (e.Button == MouseButtons.Right)
            {
                /////// Do something ///////
                Models.Item item2 = Program.dbOperations.GetItemAsync(Stack.Company_Index,sc);
                tsmiShowModuleItems.Visible = item2.Module;
                tsmiDiagram.Visible = item2.Module;
                //if (Program.dbOperations.GetWarehouse_InventoryAsync(sc) == null)
                //    tsmiItem_Warehouse.Visible = true;

                // انتخاب سلولی که روی آن کلیک راست شده است
                dgvData.CurrentCell = dgvData[e.ColumnIndex, e.RowIndex];
                contextMenuStrip1.Show(dgvData, new Point(iX, iY));
            }
        }

        object InitailValue = null;
        private void DgvData_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            InitailValue = dgvData[e.ColumnIndex, e.RowIndex].Value;//.ToString();
        }

        private void DgvData_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            if (dgvData[e.ColumnIndex, e.RowIndex].Value == InitailValue) return;

            bool bSaveChange = true;   // آیا تغییر ذخیره شود؟

            long index = Convert.ToInt64(dgvData["Index", e.RowIndex].Value);

            Models.Item item = Program.dbOperations.GetItemAsync(index);
            switch (dgvData.Columns[e.ColumnIndex].Name)
            {
                case "Code_Small":
                    item.Code_Small = Convert.ToString(dgvData["Code_Small", e.RowIndex].Value);
                    if (string.IsNullOrWhiteSpace(item.Code_Small))
                        return;
                    #region اگر کالایی دیگر با این کد تعریف شده باشد
                    else if (Program.dbOperations.GetAllItemsAsync(Stack.Company_Index).Where(d => d.Index != index)
                        .Any(j => j.Code_Small.ToLower().Equals(item.Code_Small.ToLower())))
                    {
                        bSaveChange = MessageBox.Show("کد قبلا استفاده شده است. آیا مایل به تعریف ورژن جدیدی از این کد می باشید؟"
                            + "\n" + "دقت نمایید با تأیید این عمل، کدهای قبلی غیر فعال شده، و تمام ارتباطات از این پس از ورژن جدید استفاده می شوند"
                            , "اخطار", MessageBoxButtons.YesNo) == DialogResult.Yes;
                        if (bSaveChange)
                        {
                            foreach (Models.Item it1 in Program.dbOperations.GetAllItemsAsync(Stack.Company_Index).Where(d => d.Index != index)
                                .Where(j => j.Code_Small.ToLower().Equals(item.Code_Small.ToLower())).ToList())
                            {
                                it1.Enable = false;
                                Program.dbOperations.UpdateItemAsync(it1);
                            }

                        }
                        //bSaveChange=false;
                        #endregion
                    }
                    break;
                case "Name_Samll":
                    item.Name_Samll = Convert.ToString(dgvData["Name_Samll", e.RowIndex].Value);
                    break;
                case "Code_Full":
                    item.Code_Full = Convert.ToString(dgvData["Code_Full", e.RowIndex].Value);
                    if (string.IsNullOrWhiteSpace(item.Code_Full))
                        return;
                    #region اگر کالایی دیگر با این کد تعریف شده باشد
                    else if (Program.dbOperations.GetAllItemsAsync(Stack.Company_Index).Where(d => d.Index != index)
                        .Any(j => j.Code_Full.ToLower().Equals(item.Code_Full.ToLower())))
                    {
                        MessageBox.Show("کد قبلا استفاده شده است" , "خطا");
                        bSaveChange = false;
                    }
                    #endregion
                    break;
                case "Name_Full":
                    item.Name_Full = Convert.ToString(dgvData["Name_Full", e.RowIndex].Value);
                    break;
                case "Module":
                    bSaveChange = false;
                    //item.Module = Convert.ToBoolean(dgvData["Module", e.RowIndex].Value);
                    break;
                case "Weight":
                    item.Weight = Convert.ToDouble(dgvData["Weight", e.RowIndex].Value);
                    break;
                case "FixedPrice":
                    item.FixedPrice = Convert.ToInt64(dgvData["FixedPrice", e.RowIndex].Value);
                    break;
                case "SalesPrice":
                    item.SalesPrice = Convert.ToInt64(dgvData["SalesPrice", e.RowIndex].Value);
                    break;
                case "Enable":
                    item.Enable = Convert.ToBoolean(dgvData["Enable", e.RowIndex].Value);
                    #region if 'item.Enable = false', remove this from <Module> table 
                    if (item.Enable)
                    {
                        if (Program.dbOperations.GetAllItemsAsync(Stack.Company_Index, 1,100)
                            .Where(j => j.Code_Small.ToLower().Equals(item.Code_Small.ToLower()))
                            .Any(d => d.Index != index))
                        {
                            MessageBox.Show("امکان فعال سازی این کالا وجود ندارد، زیرا ورژن دیگری از این کالا فعال است","خطا");
                            bSaveChange = false;
                        }
                    }
                    else
                    {
                        List<Models.Module> lstModules = Program.dbOperations.GetAllModulesAsync(Stack.Company_Index,1);
                        bool b1 = lstModules.Any(d => d.Module_Code_Small.ToLower().Equals(item.Code_Small));
                        bool b2 = lstModules.Any(d => d.Item_Code_Small.ToLower().Equals(item.Code_Small));
                        if (b1 || b2)
                        {
                            if (MessageBox.Show("غیر فعال کردن این کالا باعث حذف رابطه های این کالا "
                                + "با سایر کالاها خواهد شد. آیا از انجام عمل اطمینان دارید؟"
                                , "اخطار", MessageBoxButtons.YesNo,MessageBoxIcon.Warning) == DialogResult.Yes)
                            {
                                if (b1)
                                    foreach (Models.Module md in lstModules.Where(d => d.Module_Code_Small.ToLower().Equals(item.Code_Small)).ToList())
                                        Program.dbOperations.DeleteModule(md);
                                if (b2)
                                    foreach (Models.Module md in lstModules.Where(d => d.Item_Code_Small.ToLower().Equals(item.Code_Small)).ToList())
                                        Program.dbOperations.DeleteModule(md);

                                Program.dbOperations.UpdateItemAsync(item);
                                return;
                            }
                            else bSaveChange = false;
                        }
                    }
                    #endregion
                    break;
            }

            if (bSaveChange)
            {
                // برای ذخیره تغییرات در ردیف جدید ، پیغامی نمایش داده نشود
                if ((e.RowIndex == iNewRow))
                {
                    Program.dbOperations.UpdateItemAsync(item);
                    //AddUpdateItem_to_WarehouseInventory(item, true,JustEdit);
                    JustEdit = true;
                }
                else
                {
                    if (chkCanEdit.Checked)
                    {
                        if (chkShowUpdateMessage.Checked)
                        {
                            bSaveChange = MessageBox.Show("آیا از ثبت تغییرات اطمینان دارید؟"
                                , "", MessageBoxButtons.YesNo) == DialogResult.Yes;
                        }
                    }
                }
            }


            if (bSaveChange)
            {
                Program.dbOperations.UpdateItemAsync(item);

                //AddUpdateItem_to_WarehouseInventory(item, false,true);
            }
            else dgvData[e.ColumnIndex, e.RowIndex].Value = InitailValue;
        }

        private void DgvData_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            DataGridViewCell cell = dgvData[e.ColumnIndex, e.RowIndex];

            if (dgvData.Columns[e.ColumnIndex].Name.Equals("Module"))
            { cell.ReadOnly = true; return; }

            if (!chkCanEdit.Checked)
            {
                if (iNewRow >= 0)
                {
                    //MessageBox.Show(iNewRow.ToString());
                    dgvData.ReadOnly = false;
                    if (e.RowIndex == iNewRow)
                        cell.ReadOnly = false;
                    else cell.ReadOnly = true;
                }
            }

        }

        private void BtnImportDataFromExcel_Click(object sender, EventArgs e)
        {
            if(Program.dbOperations.GetAllModulesAsync(Stack.Company_Index,0).Any())
                MessageBox.Show("دقت نمایید که کدهایی که قبلا در جدول وارد شده اند، مجددا وارد نمی شوند", "مهم");

            GetDataFromExcel_OneSheet2(Application.StartupPath + @"\_Requirements\MainData.xlsx", "Modules_Items");
        }

        // ---
        private void GetDataFromExcel_OneSheet()
        {
            if (MessageBox.Show(
                "لطفا در فایل اکسل باز شده، و در شیت " +"items" +" اطلاعات خود را وارد نموده و سپس آنرا ذخیره نمایید."
                +"\n"+"دقت نمایید فایل اکسل نباید ببندید"
                , "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No) return;

            panel1.Enabled = false;
            progressBar1.Style = ProgressBarStyle.Marquee;
            progressBar1.Visible = true;
            Application.DoEvents();
            //pictureBox1.Visible = true;

            EXCEL.Application excelApp = new EXCEL.Application();
            excelApp.DisplayAlerts = false;
            excelApp.Visible = true;
            excelApp.WindowState = EXCEL.XlWindowState.xlMaximized;
            EXCEL.Workbook wb = excelApp.Workbooks.Open(Application.StartupPath + @"\_Requirements\MainData.xlsx");
            try
            {
                //MessageBox.Show(openFileDialog1.FileName,"100");
                //wb = excelApp.Workbooks.Open(openFileDialog1.FileName);
                bool bIsBOM_sheetExists = wb.Worksheets.OfType<EXCEL.Worksheet>().Any(ws => ws.Name.ToLower().Equals("items"));
                //MessageBox.Show("", "200");
                if (bIsBOM_sheetExists)
                {
                    //progressBar1.Maximum = 303;
                    EXCEL.Worksheet ws = wb.Worksheets["items"];
                    ws.Activate();
                    if (MessageBox.Show("آیا اطلاعات وارد شوند؟", "", MessageBoxButtons.YesNo)
                        == DialogResult.Yes)
                    {
                        #region بررسی اینکه آیا کاربر فایل اکسل را بسته است یا خیر. در صورت بسته بودن، آنرا باز میکند
                        try
                        {
                            ws = wb.Worksheets["items"];
                        }
                        catch
                        {
                            excelApp = new EXCEL.Application();
                            excelApp.DisplayAlerts = false;
                            excelApp.Visible = false;
                            excelApp.WindowState = EXCEL.XlWindowState.xlMaximized;
                            wb = excelApp.Workbooks.Open(Application.StartupPath + @"\_Requirements\MainData.xlsx");
                            ws = wb.Worksheets["items"];
                        }
                        #endregion

                        #region Get Items
                        int n = 1;
                        while ((ws.Cells[n, 1].Value != null) || ws.Cells[n, 3].Value != null) n++;

                        progressBar1.Style = ProgressBarStyle.Blocks;
                        progressBar1.Maximum = n;
                        progressBar1.Value = 0;

                        int i = 1;
                        while (true)
                        {
                            i++;
                            #region بررسی وضعیت سلولهای یک ردیف و شرط پایان حلقه
                            bool b1 = ws.Cells[i, 1].Value != null;
                            bool b2 = ws.Cells[i, 2].Value != null;
                            bool b3 = ws.Cells[i, 3].Value != null;
                            bool b4 = ws.Cells[i, 4].Value != null;

                            if (b1) b1 = ws.Cells[i, 1].Value.ToString().Length > 0;
                            if (b2) b2 = ws.Cells[i, 2].Value.ToString().Length > 0;
                            if (b3) b3 = ws.Cells[i, 3].Value.ToString().Length > 0;
                            if (b4) b4 = ws.Cells[i, 4].Value.ToString().Length > 0;

                            bool bStop = !b1 && !b2 && !b3 && !b4;
                            if (bStop) break;

                            bool bContinue = (!b1 || !b2) && (!b3 || !b4);
                            if (bContinue) continue;
                            #endregion

                            #region تغییرات در جدول کالاها
                            string module_Small_code = null;
                            if (b1 & b2)
                            {
                                module_Small_code = ws.Cells[i, 2].Value.ToString();
                                if (Program.dbOperations.GetItemAsync(Stack.Company_Index,module_Small_code) == null)
                                {
                                    Models.Item module = new Models.Item
                                    {
                                        Company_Index = Stack.Company_Index,
                                        Index = Program.dbOperations.GetNewIndex_Item(),
                                        Name_Samll = ws.Cells[i, 1].Value.ToString(),
                                        Code_Small = module_Small_code,
                                        Enable = true,
                                        Module = true
                                    };

                                    Program.dbOperations.AddItem(module);
                                }
                            }

                            string item_Small_code = null;
                            double quantity = Convert.ToDouble(ws.Cells[i, 5].Value);

                            if (b3 & b4)
                            {
                                item_Small_code = ws.Cells[i, 4].Value.ToString();
                                if (Program.dbOperations.GetItemAsync(Stack.Company_Index,item_Small_code) == null)
                                {
                                    Models.Item item = new Models.Item
                                    {
                                        Company_Index = Stack.Company_Index,
                                        Index = Program.dbOperations.GetNewIndex_Item(),
                                        Name_Samll = ws.Cells[i, 3].Value.ToString(),
                                        Code_Small = item_Small_code,
                                        Enable = true
                                    };

                                    Program.dbOperations.AddItem(item);
                                }
                            }
                            #endregion

                            #region رابطه بین ماژول و کالاها
                            // در جدول ماژول ها نباید رکوردی وجود داشته باشد که کد ماژول و کد کالای آن 
                            // با کد ماژول و کد کالای وارد شده یکسان باشد
                            if (!string.IsNullOrEmpty(module_Small_code) && !string.IsNullOrEmpty(item_Small_code))
                            {
                                if (Program.dbOperations.GetModuleAsync(module_Small_code, item_Small_code) == null)
                                {
                                    Program.dbOperations.AddModuleAsync(new Models.Module
                                    {
                                        Company_Index = Stack.Company_Index,
                                        Module_Code_Small = module_Small_code,
                                        Item_Code_Small = item_Small_code,
                                        Quantity = quantity,
                                        Enable = true,
                                    });
                                }
                            }
                            #endregion

                            if(progressBar1.Value<progressBar1.Maximum)
                                progressBar1.Value++;
                            Application.DoEvents();
                        }
                        #endregion
                    }

                }
                else
                {
                    MessageBox.Show("شیت Items یافت نشد!", "خطا");
                }
            }
            catch {
                //if (wb == null) wb = excelApp.Workbooks.Add();
            }
            finally
            {
                try
                {
                    wb.Close(SaveChanges: false);
                    excelApp.Workbooks.Close();
                    excelApp.Quit();

                    while (Marshal.ReleaseComObject(wb) != 0) { }
                    while (Marshal.ReleaseComObject(excelApp.Workbooks) != 0) { }
                    while (Marshal.ReleaseComObject(excelApp) != 0) { }
                    GC.Collect();
                    GC.WaitForPendingFinalizers();

                    dgvData.DataSource = Program.dbOperations.GetAllItemsAsync(Stack.Company_Index);
                    Application.DoEvents();
                    //panel1.Enabled = true;
                }
                catch { }
            }

            panel1.Enabled = true;
            progressBar1.Visible = false;
            //pictureBox1.Visible = false;

        }

        // امکان استفاده از حالت مِرژ ماژولها در اکسل
        private void GetDataFromExcel_OneSheet2(string ExcelFilePath, string SheetName)
        {
            if (MessageBox.Show(
                "لطفا در فایل اکسل باز شده، و در شیت " + SheetName + " اطلاعات خود را وارد نموده و سپس آنرا ذخیره نمایید."
                +"\n"+"دقت نمایید فایل اکسل نباید ببندید"
                , "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No) return;

            panel1.Enabled = false;
            progressBar1.Style = ProgressBarStyle.Marquee;
            progressBar1.Visible = true;
            Application.DoEvents();
            //pictureBox1.Visible = true;

            EXCEL.Application excelApp = new EXCEL.Application();
            excelApp.DisplayAlerts = false;
            excelApp.Visible = true;
            excelApp.WindowState = EXCEL.XlWindowState.xlMaximized;
            EXCEL.Workbook wb = excelApp.Workbooks.Open(ExcelFilePath);
            try
            {
                //MessageBox.Show(openFileDialog1.FileName,"100");
                //wb = excelApp.Workbooks.Open(openFileDialog1.FileName);
                bool bIsBOM_sheetExists = wb.Worksheets.OfType<EXCEL.Worksheet>()
                    .Any(ws => ws.Name.ToLower().Equals(SheetName.ToLower()));
                //MessageBox.Show("", "200");
                if (bIsBOM_sheetExists)
                {
                    //progressBar1.Maximum = 303;
                    EXCEL.Worksheet ws = wb.Worksheets[SheetName];
                    ws.Activate();
                    if (MessageBox.Show("آیا اطلاعات وارد شوند؟", "", MessageBoxButtons.YesNo)
                        == DialogResult.Yes)
                    {
                        #region بررسی اینکه آیا کاربر فایل اکسل را بسته است یا خیر. در صورت بسته بودن، آنرا باز میکند
                        try
                        {
                            ws = wb.Worksheets[SheetName];
                        }
                        catch
                        {
                            excelApp = new EXCEL.Application();
                            excelApp.DisplayAlerts = false;
                            excelApp.Visible = false;
                            excelApp.WindowState = EXCEL.XlWindowState.xlMaximized;
                            wb = excelApp.Workbooks.Open(ExcelFilePath);
                            ws = wb.Worksheets[SheetName];
                        }
                        #endregion

                        #region Get Items
                        int n = 1;
                        while (ws.Cells[n, 1].Value != null) n++;
                        //while ((ws.Cells[n, 1].Value != null) || ws.Cells[n, 3].Value != null) n++;

                        progressBar1.Style = ProgressBarStyle.Blocks;
                        progressBar1.Maximum = n;
                        progressBar1.Value = 0;

                        string module_name = null;
                        string module_Small_code = null;
                        int i = 1;
                        while (++i < n)
                        {
                            #region بررسی وضعیت سلولهای یک ردیف و شرط پایان حلقه
                            bool b4 = ws.Cells[i, 4].Value != null;
                            bool b5 = ws.Cells[i, 5].Value != null;

                            //if (b1) b1 = ws.Cells[i, 1].Value.ToString().Length > 0;
                            if (b4) b4 = ws.Cells[i, 4].Value.ToString().Length > 0;
                            if (b5) b5 = ws.Cells[i, 5].Value.ToString().Length > 0;
                            if (!b4 || !b5) continue;

                            bool b2 = ws.Cells[i, 2].Value != null;
                            bool b3 = ws.Cells[i, 3].Value != null;
                            if (b2) b2 = ws.Cells[i, 2].Value.ToString().Length > 0;
                            if (b3) b3 = ws.Cells[i, 3].Value.ToString().Length > 0;
                            //bool bStop = !b1 && !b2 && !b3 && !b4;
                            //if (bStop) break;

                            //bool bContinue = (!b1 || !b2) && (!b3 || !b4);
                            //if (bContinue) continue;
                            #endregion

                            #region تغییرات در جدول کالاها

                            #region تعریف ماژول
                            if (b2) module_name = ws.Cells[i, 2].Value.ToString();

                            if (b3)
                            {
                                // کد ماژول
                                module_Small_code = ws.Cells[i, 3].Value.ToString();
                                if (Program.dbOperations.GetItemAsync(Stack.Company_Index,module_Small_code,true) == null)
                                {
                                    Program.dbOperations.AddItem(new Models.Item
                                    {
                                        Index = Program.dbOperations.GetNewIndex_Item(),
                                        Name_Samll = module_name,// + " - " + module_Small_code,
                                        Code_Small = module_Small_code,
                                        Enable = true,
                                        Module = true
                                    });
                                }
                            }
                            #endregion

                            #region تعریف کالا
                            string item_Small_code = ws.Cells[i, 5].Value.ToString();
                            double quantity = Convert.ToDouble(ws.Cells[i, 7].Value);

                            Models.Item item = Program.dbOperations.GetItemAsync(Stack.Company_Index,item_Small_code, true);
                            if (item == null)
                            {
                                long index = Program.dbOperations.GetNewIndex_Item();
                                item = new Models.Item
                                {
                                    Index = index,
                                    Name_Samll = ws.Cells[i, 4].Value.ToString(),
                                    Code_Small = item_Small_code,
                                    Enable = true
                                };
                                Program.dbOperations.AddItem(item,index);
                            }
                            #endregion
                            #endregion

                            #region رابطه بین ماژول و کالاها
                            // در جدول ماژول ها نباید رکوردی وجود داشته باشد که کد ماژول و کد کالای آن 
                            // با کد ماژول و کد کالای وارد شده یکسان باشد
                            if (!string.IsNullOrEmpty(module_Small_code) && !string.IsNullOrEmpty(item_Small_code))
                            {
                                // غیر فعال کردن رابطه قبلی در صورت وجود
                                if (Program.dbOperations.GetModuleAsync(module_Small_code, item_Small_code) != null)
                                {
                                    Models.Module module = Program.dbOperations.GetModuleAsync(module_Small_code, item_Small_code);
                                    module.Enable = false;
                                    Program.dbOperations.UpdateModuleAsync(module);
                                }

                                Program.dbOperations.AddModuleAsync(new Models.Module
                                {
                                    Module_Code_Small = module_Small_code,
                                    Item_Index = item.Index,
                                    Item_Code_Small = item_Small_code,
                                    Quantity = quantity,
                                    Enable = true,
                                });
                            }
                            #endregion

                            if (progressBar1.Value < progressBar1.Maximum)
                                progressBar1.Value++;
                            Application.DoEvents();
                        }
                        #endregion
                    }

                }
                else
                {
                    MessageBox.Show("شیت " + SheetName + " یافت نشد!", "خطا");
                }
            }
            catch {
                //if (wb == null) wb = excelApp.Workbooks.Add();
            }
            finally
            {
                try
                {
                    wb.Close(SaveChanges: false);
                    excelApp.Workbooks.Close();
                    excelApp.Quit();

                    while (Marshal.ReleaseComObject(wb) != 0) { }
                    while (Marshal.ReleaseComObject(excelApp.Workbooks) != 0) { }
                    while (Marshal.ReleaseComObject(excelApp) != 0) { }
                    GC.Collect();
                    GC.WaitForPendingFinalizers();

                    dgvData.DataSource = GetData();// Program.dbOperations.GetAllItemsAsync(Stack.Company_Index);
                    Application.DoEvents();
                    //panel1.Enabled = true;
                }
                catch { }
            }

            panel1.Enabled = true;
            progressBar1.Visible = false;
            //pictureBox1.Visible = false;

        }

        private void ChkCanEdit_CheckedChanged(object sender, EventArgs e)
        {
            dgvData.SelectionMode = chkCanEdit.Checked ? DataGridViewSelectionMode.RowHeaderSelect
                : DataGridViewSelectionMode.FullRowSelect;
            dgvData.ReadOnly = !chkCanEdit.Checked;
            //dgvData.Columns["Code_Small"].ReadOnly = true;  // ستون کدها در هر حالتی باید غیرقابل تغییر باشد
            dgvData.Columns["Module"].ReadOnly = true;  // ستون ماژول در هر حالتی باید غیرقابل تغییر باشد

            chkShowUpdateMessage.Enabled = chkCanEdit.Checked;
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowData(false);
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtST_Name.Text)
                && string.IsNullOrWhiteSpace(txtST_SmallCode.Text)
                && string.IsNullOrWhiteSpace(txtST_FullName.Text)
                && string.IsNullOrWhiteSpace(txtST_SmallCode.Text))
            {
                //ShowData(false);
                return;
            }

            panel1.Enabled = false;
            //dgvData.Visible = false;
            Application.DoEvents();

            //ShowData(false);
            List<Models.Item> lstItems = (List<Models.Item>)dgvData.DataSource;
            //MessageBox.Show(lstItems.Count.ToString());

            //if (!string.IsNullOrWhiteSpace(txtST_Name.Text)
            //   || !string.IsNullOrWhiteSpace(txtST_SmallCode.Text))
            {
                foreach (Control c in groupBox1.Controls)
                {
                    //MessageBox.Show(c.Text);
                    if (c.Name.Length > 4)
                    {
                        if (c.Name.Substring(0, 5).Equals("txtST"))
                            if (!string.IsNullOrWhiteSpace(c.Text))
                            {
                                lstItems = SearchThis(lstItems, c.Name);
                                if ((lstItems == null) || !lstItems.Any()) break;
                            }
                    }
                }
            }

            dgvData.DataSource = lstItems;

            //System.Threading.Thread.Sleep(500);
            Application.DoEvents();
            panel1.Enabled = true;
            //dgvData.Visible = true;
            
        }

        // جستجوی موردی
        private List<Models.Item> SearchThis(List<Models.Item> lstItems1, string TextBoxName)
        {
            switch (TextBoxName)
            {
                case "txtST_SmallCode":
                    switch (cmbST_SmallCode.SelectedIndex)
                    {
                        case 0:
                            return lstItems1.Where(d => d.Code_Small.ToLower().Contains(txtST_SmallCode.Text.ToLower())).ToList();
                        case 1:
                            return lstItems1.Where(d => d.Code_Small.ToLower().StartsWith(txtST_SmallCode.Text.ToLower())).ToList();
                        case 2:
                            return lstItems1.Where(d => d.Code_Small.ToLower().Equals(txtST_SmallCode.Text.ToLower())).ToList();
                        default: return lstItems1;
                    }
                //break;
                case "txtST_Name":
                    switch (cmbST_Name.SelectedIndex)
                    {
                        case 0:
                            return lstItems1.Where(d => d.Name_Samll.ToLower().Contains(txtST_Name.Text.ToLower())).ToList();
                        case 1:
                            return lstItems1.Where(d => d.Name_Samll.ToLower().StartsWith(txtST_Name.Text.ToLower())).ToList();
                        case 2:
                            return lstItems1.Where(d => d.Name_Samll.ToLower().Equals(txtST_Name.Text.ToLower())).ToList();
                        default: return lstItems1;
                    }
                case "txtST_FullCode":
                    switch (cmbST_FullCode.SelectedIndex)
                    {
                        case 0:
                            return lstItems1.Where(d => d.Code_Full.ToLower().Contains(txtST_FullCode.Text.ToLower())).ToList();
                        case 1:
                            return lstItems1.Where(d => d.Code_Full.ToLower().StartsWith(txtST_FullCode.Text.ToLower())).ToList();
                        case 2:
                            return lstItems1.Where(d => d.Code_Full.ToLower().Equals(txtST_FullCode.Text.ToLower())).ToList();
                        default: return lstItems1;
                    }
                //break;
                case "txtST_FullName":
                    switch (cmbST_FullName.SelectedIndex)
                    {
                        case 0:
                            return lstItems1.Where(d => d.Name_Full.ToLower().Contains(txtST_FullName.Text.ToLower())).ToList();
                        case 1:
                            return lstItems1.Where(d => d.Name_Full.ToLower().StartsWith(txtST_FullName.Text.ToLower())).ToList();
                        case 2:
                            return lstItems1.Where(d => d.Name_Full.ToLower().Equals(txtST_FullName.Text.ToLower())).ToList();
                        default: return lstItems1;
                    }
            }

            return null;
        }

        private void tsmiItemStructure_Click(object sender, EventArgs e)
        {
            bool enable = Convert.ToBoolean(dgvData.CurrentRow.Cells["Enable"].Value);
            if (!enable)
            {
                MessageBox.Show("این کالا فعال نمی باشد، در نتیجه امکان تعریف یا تغییر زیر ساخت برای آن وجود ندارد"
                    , "خطا");
            }
            else
            {
                Stack.bx = false;   // آیا تغییری انجام شده است؟
                long index = Convert.ToInt64(dgvData.CurrentRow.Cells["Index"].Value);
                //Models.Item item = Program.dbOperations.GetItemAsync(index);
                new K1320_Modules(Program.dbOperations.GetItemAsync(index)).ShowDialog();

                if (Stack.bx)
                    ShowData(false);
            }
        }

        private void DgvData_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // Don't throw an exception when we're done.
            e.ThrowException = false;
            if (dgvData.Columns[dgvData.CurrentCell.ColumnIndex].Name.Equals("FixedPrice")
                || dgvData.Columns[dgvData.CurrentCell.ColumnIndex].Name.Equals("SalesPrice"))
            {
                MessageBox.Show("لطفا «بهای تمام شده» را به صورت عدد وارد نمایید.", "خطای نوع داده", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("خطای نوع داده", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // If this is true, then the user is trapped in this cell.
            e.Cancel = false;
        }

        private void TsmiShowModuleItems_Click(object sender, EventArgs e)
        {
            long index = Convert.ToInt64(dgvData.CurrentRow.Cells["Index"].Value);
            new K1320_Modules(Program.dbOperations.GetItemAsync(index)).ShowDialog();
            //new zForm1().ShowDialog();
        }

        private void RadModule_CheckedChanged(object sender, EventArgs e)
        {
            if (radModule.Checked) dgvData.DataSource = GetData().Where(d => d.Module).ToList();
            else if (radNotModule.Checked) dgvData.DataSource = GetData().Where(d => !d.Module).ToList();
            else dgvData.DataSource = GetData();
        }

        private void TxtST_SmallCode_Enter(object sender, EventArgs e)
        {
            AcceptButton = btnSearch;
        }

        private void TxtST_SmallCode_Leave(object sender, EventArgs e)
        {
            AcceptButton = null;
        }

        private void BtnShowAll_Click(object sender, EventArgs e)
        {
            RadModule_CheckedChanged(null, null);
        }

        private void TsmiProperties_Click(object sender, EventArgs e)
        {
            long index = Convert.ToInt64(dgvData.CurrentRow.Cells["Index"].Value);
            new K1330_Item_Properties(Program.dbOperations.GetItemAsync(index)).ShowDialog();
        }

        private void TsmiItem_Warehouse_Click(object sender, EventArgs e)
        {
            long index = Convert.ToInt64(dgvData.CurrentRow.Cells["Index"].Value);
            Models.Item item = Program.dbOperations.GetItemAsync(index);
            new K1304_Item_ChooseWarehouse(item).ShowDialog();

            if (Stack.ix > 0)
            {
                item.Warehouse_Index = Stack.ix;
                Program.dbOperations.UpdateItemAsync(item);
                //Application.DoEvents();
                //pictureBox1.Visible = true;
                //timer1.Enabled = true;
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            timer1.Enabled = false;
        }

        private void TsmiDiagram_Click(object sender, EventArgs e)
        {
            new K1322_Module_Diagram(dgvData.CurrentRow.Cells["Code_Small"].Value.ToString()).ShowDialog();
        }

        private void K1300_Items_Load(object sender, EventArgs e)
        {

        }

        private void DgvData_Enter(object sender, EventArgs e)
        {
            CancelButton = null;
        }

        private void DgvData_Leave(object sender, EventArgs e)
        {
            CancelButton = btnReturn;
        }

        private void TsmiAdd_ChangeItemImage_Click(object sender, EventArgs e)
        {
            MessageBox.Show("حجم فایل نباید بیشتر از 50 کیلو بایت باشد", "توجه نمایید");

            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string file = openFileDialog1.FileName;
                if(new FileInfo(file).Length> 51200000)
                {
                    MessageBox.Show("حجم فایل انتخاب شده، بیشتر از 50 کیلو باید می باشد", "خطا");
                    return;
                }

                string item_code= Convert.ToString(dgvData.CurrentRow.Cells["Code_Small"].Value);
                Add_ChangeImage(item_code,file);
                Application.DoEvents();

                pictureBox2.Image =new Bitmap(file);
                pictureBox1.Visible = true;
                timer1.Enabled = true;
            }
        }

        private void TsmiDeleteItemImage_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("آیا از حذف تصویر اطمینان دارید؟", "اخطار"
                , MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            string code_cmall = Convert.ToString(dgvData.CurrentRow.Cells["Code_Small"].Value);
            Models.Item_File item_file = Program.dbOperations.GetItem_FileAsync(code_cmall, 1, true);
            item_file.Enable = false;
            Program.dbOperations.UpdateItem_FileAsync(item_file);

            pictureBox1.Visible = true;
            timer1.Enabled = true;
            pictureBox2.Image = null;
        }

        private void BtnGetImages_Click(object sender, EventArgs e)
        {
            string ImageDirectoryPath = Application.StartupPath + @"\_Requirements\Images\";
            foreach (string file in Directory.EnumerateFiles(
                           ImageDirectoryPath, "*.jpg", SearchOption.AllDirectories))
            {
                string item_code = Path.GetFileNameWithoutExtension(file);
                Add_ChangeImage(item_code,file);
            }
        }

        private void btnDeleteAllImages_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("آیا از حذف تمام تصاویر اطمینان دارید؟", "اخطار"
               , MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

            //if (MessageBox.Show("با انجام این عمل ، تمام روابط مشخصه ها و کالاها از بین خواهد رفت"
            //    + "\n" + "آیا از حذف تمام مشخصات اطمینان دارید؟", "اخطار 2"
            //    , MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

            Program.dbOperations.DeleteAllFilesAsync();
            Program.dbOperations.DeleteAllItem_FilesAsync();
            pictureBox2.Image = null;
        }

        private void DgvData_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            string sc = Convert.ToString(dgvData["Code_Small", e.RowIndex].Value);
            #region نمایش تصویر کالا در صورت وجود
            Models.Item_File item_file = Program.dbOperations.GetItem_FileAsync(sc, 1, true);
            if (item_file != null)
                pictureBox2.Image = new ThisProject().ByteToImage
                    (Program.dbOperations.GetFileAsync(item_file.File_Index).Content);
            else pictureBox2.Image = null;
            #endregion
        }

        private void TsmiItem_Details_Click(object sender, EventArgs e)
        {
            long index = Convert.ToInt64(dgvData.CurrentRow.Cells["Index"].Value);
            Models.Item item = Program.dbOperations.GetItemAsync(index);
            new K1302_Item_Details(0,item).ShowDialog();
        }

        private void Add_ChangeImage(string item_code,string file)
        {
            // اگر کالا قبلا تصویری داشته باشد، ابتدا آنرا غیرفعال می کند
            if (Program.dbOperations.GetItem_FileAsync(item_code, 1, true) != null)
            {
                Models.Item_File item_file = Program.dbOperations.GetItem_FileAsync(item_code, 1, true);
                item_file.Enable = false;
                Program.dbOperations.UpdateItem_FileAsync(item_file);
            }

            long file_index = Program.dbOperations.AddFileAsync(new Models.File
            {
                Company_Index = Stack.Company_Index,
                Content = new ThisProject().ConvertImageToByteArray(file),
                OriginalFileName = Path.GetFileName(file),
                Description = "تصویر کالای " + item_code,
                DateTime_mi = DateTime.Now,
                DateTime_sh = Stack_Methods.DateTimeNow_Shamsi(),
                Enable = true,
            }) ;

            Program.dbOperations.AddItem_FileAsync(new Models.Item_File
            {
                Company_Index = Stack.Company_Index,
                File_Index = file_index,
                Item_Code_Small = item_code,
                Type = 1,   // مربوط به تصویر یک کالا
                Enable = true,
            });
        }


    }
}
