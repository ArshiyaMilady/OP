using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EXCEL = Microsoft.Office.Interop.Excel;


namespace OrdersProgress
{
    public partial class M1120_WarehouseItems : X210_ExampleForm_Normal
    {
        public M1120_WarehouseItems()
        {
            InitializeComponent();

            if(Stack.UserLevel <= Stack.UserLevel_Supervisor3)
            {
                panel2.Visible = true;
            }

            if(Stack.UserLevel <= Stack.UserLevel_Supervisor1)
            {
                //panel2.Visible = true;
                btnImportDataFromExcel.Visible = true;
                //btnAddNew.Visible = true;
                btnDeleteAll.Visible = true;
            }

        }

        private void M1120_WarehouseItems_Shown(object sender, EventArgs e)
        {
            if (Stack.UserLevel <= Stack.UserLevel_Supervisor1)
            {
                btnDeleteAll.Visible = true;
                tsmiDelete.Visible = true;
            }

            cmbST_Name.SelectedIndex = 0;
            cmbST_SmallCode.SelectedIndex = 0;
            comboBox1.SelectedIndex = 0;

            cmbWarehouses.Items.Add("تمام انبارها");
            cmbWarehouses.Items.AddRange(Program.dbOperations.GetAllWarehousesAsync(Stack.Company_Index, true).Select(d => d.Name).ToArray());
            cmbWarehouses.SelectedIndex = 1;

            dgvData.DataSource = GetData();
            ShowData();
        }


        private List<Models.Warehouse_Item> GetData()
        {
            switch (cmbWarehouses.SelectedIndex)
            {
                case 0: return Program.dbOperations.GetAllWarehouse_InventorysAsync(Stack.Company_Index);
                default: return Program.dbOperations.GetAllWarehouse_InventorysAsync(Stack.Company_Index,cmbWarehouses.Text);
            }
        }

        private void ShowData()
        {
            #region ترجمه سر ستونها و مخفی کردن بعضی ستونها
            foreach (DataGridViewColumn col in dgvData.Columns)
            {
                switch (col.Name)
                {
                    //case "Index":
                    //    col.HeaderText = "index";
                    //    break;
                    case "Item_Code":
                        col.HeaderText = "کد کالا";
                        col.ReadOnly = Stack.UserLevel > Stack.UserLevel_Supervisor1;
                        col.Width = 100;
                        break;
                    case "Item_Name":
                        col.HeaderText = "نام کالا";
                        col.ReadOnly = Stack.UserLevel > Stack.UserLevel_Supervisor3;
                        col.Width = 200;
                        break;
                    case "Item_Unit":
                        col.HeaderText = "واحد";
                        col.ReadOnly = Stack.UserLevel > Stack.UserLevel_Supervisor3;
                        col.Width = 100;
                        break;
                    case "Quantity_Real":
                        col.HeaderText = "تعداد";
                        col.ReadOnly = Stack.UserLevel > Stack.UserLevel_Supervisor1;
                        col.Width = 100;
                        break;
                    case "Quantity_x":
                        if (Stack.UserLevel_Type == 1)  // فقط برای برنامه نویس قابل مشاهده باشد
                        {
                            col.HeaderText = "تعداد غیر واقعی";
                            col.ReadOnly = Stack.UserLevel > Stack.UserLevel_Supervisor1;
                            col.Width = 100;
                        }
                        else col.Visible = false;
                        break;

                    default: col.Visible = false; break;
                }

                if (col.ReadOnly) col.DefaultCellStyle.BackColor = Color.WhiteSmoke;
                else col.DefaultCellStyle.BackColor = Color.White;
            }
            #endregion
        }

        private void BtnImportDataFromExcel_Click(object sender, EventArgs e)
        {
            MessageBox.Show("دقت نمایید برای کدهایی که قبلا در جدول وارد شده اند،نام و تعداد آنها از فایل اکسل به روز می شوند", "مهم");

            GetDataFromExcel_OneSheet();
        }

        private void GetDataFromExcel_OneSheet()
        {
            if (MessageBox.Show(
                "لطفا در فایل اکسل باز شده، و در شیت " + "Inventory" + " اطلاعات خود را وارد نموده و سپس آنرا ذخیره نمایید."
                + "\n" + "دقت نمایید فایل اکسل نباید ببندید"
                , "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No) return;

            int warehouse_index = Program.dbOperations.GetWarehouseAsync(cmbWarehouses.Text).Index;

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
                bool bIsBOM_sheetExists = wb.Worksheets.OfType<EXCEL.Worksheet>().Any(ws => ws.Name.ToLower().Equals("inventory"));
                //MessageBox.Show("", "200");
                if (bIsBOM_sheetExists)
                {
                    //progressBar1.Maximum = 303;
                    EXCEL.Worksheet ws = wb.Worksheets["Inventory"];
                    ws.Activate();
                    if (MessageBox.Show("آیا اطلاعات وارد شوند؟", "", MessageBoxButtons.YesNo)
                        == DialogResult.Yes)
                    {
                        #region بررسی اینکه آیا کاربر فایل اکسل را بسته است یا خیر. در صورت بسته بودن، آنرا باز میکند
                        try
                        {
                            ws = wb.Worksheets["Inventory"];
                        }
                        catch
                        {
                            excelApp = new EXCEL.Application();
                            excelApp.DisplayAlerts = false;
                            excelApp.Visible = false;
                            excelApp.WindowState = EXCEL.XlWindowState.xlMaximized;
                            wb = excelApp.Workbooks.Open(Application.StartupPath + @"\_Requirements\MainData.xlsx");
                            ws = wb.Worksheets["Inventory"];
                        }
                        #endregion

                        #region Get Items
                        int n = 1;
                        while ((ws.Cells[n, 1].Value != null) && ws.Cells[n, 2].Value != null) n++;

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

                            if (b1) b1 = ws.Cells[i, 1].Value.ToString().Length > 0;
                            if (b2) b2 = ws.Cells[i, 2].Value.ToString().Length > 0;

                            bool bStop = !b1 && !b2;
                            if (bStop) break;
                            #endregion

                            #region ثبت کالا در انبار
                            string item_Small_code = null;
                            double quantity = Convert.ToDouble(ws.Cells[i, 3].Value);

                            item_Small_code = ws.Cells[i, 2].Value.ToString();
                            if (Program.dbOperations.GetWarehouse_InventoryAsync(item_Small_code) == null)
                            {
                                Models.Warehouse_Item wi = new Models.Warehouse_Item
                                {
                                    Company_Index = Stack.Company_Index,
                                    Warehouse_Index = warehouse_index,
                                    Item_Name = ws.Cells[i, 1].Value.ToString(),
                                    Item_Code = item_Small_code,
                                    Quantity_Real = quantity,
                                    Item_Unit = Convert.ToString(ws.Cells[i, 4].Value),
                                };

                                Models.Item item3 = Program.dbOperations.GetItemAsync(item_Small_code, true);
                                if (item3 != null) wi.Item_Index = item3.Index;

                                Program.dbOperations.AddWarehouse_Inventory(wi);
                            }
                            else  // اگر کد کالا در انبار موجود باشد ، نام و تعداد آنرا بروز رسانی می کند
                            {
                                Models.Warehouse_Item wi = Program.dbOperations.GetWarehouse_InventoryAsync(item_Small_code);
                                wi.Item_Name = ws.Cells[i, 1].Value.ToString();
                                wi.Quantity_Real = quantity;
                                wi.Item_Unit = Convert.ToString(ws.Cells[i, 4].Value);
                                Program.dbOperations.UpdateWarehouse_InventoryAsync(wi);
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
                    MessageBox.Show("شیت Inventory یافت نشد!", "خطا");
                }
            }
            catch
            {
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

                    dgvData.DataSource = Program.dbOperations.GetAllWarehouse_InventorysAsync(warehouse_index);
                    Application.DoEvents();
                    //panel1.Enabled = true;
                }
                catch { }
            }

            panel1.Enabled = true;
            progressBar1.Visible = false;
            //pictureBox1.Visible = false;

        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtST_Name.Text)
                && string.IsNullOrWhiteSpace(txtST_SmallCode.Text))
            {
                //ShowData(false);
                return;
            }

            panel1.Enabled = false;
            //dgvData.Visible = false;
            Application.DoEvents();

            //ShowData(false);
            List<Models.Warehouse_Item> lstWI = (List<Models.Warehouse_Item>)dgvData.DataSource;
            //MessageBox.Show(lstItems.Count.ToString());

            if (!string.IsNullOrWhiteSpace(txtST_Name.Text)
               || !string.IsNullOrWhiteSpace(txtST_SmallCode.Text))
            {
                foreach (Control c in groupBox1.Controls)
                {
                    //MessageBox.Show(c.Text);
                    if (c.Name.Length > 4)
                    {
                        if (c.Name.Substring(0, 5).Equals("txtST"))
                            if (!string.IsNullOrWhiteSpace(c.Text))
                            {
                                lstWI = SearchThis(lstWI, c.Name);
                                if ((lstWI == null) || !lstWI.Any()) break;
                            }
                    }
                }
            }

            dgvData.DataSource = lstWI;

            //System.Threading.Thread.Sleep(500);
            Application.DoEvents();
            panel1.Enabled = true;
            //dgvData.Visible = true;

        }

        // جستجوی موردی
        private List<Models.Warehouse_Item> SearchThis(List<Models.Warehouse_Item> lstWI1, string TextBoxName)
        {
            switch (TextBoxName)
            {
                case "txtST_SmallCode":
                    switch (cmbST_SmallCode.SelectedIndex)
                    {
                        case 0:
                            return lstWI1.Where(d => d.Item_Code.ToLower().Contains(txtST_SmallCode.Text.ToLower())).ToList();
                        case 1:
                            return lstWI1.Where(d => d.Item_Code.ToLower().StartsWith(txtST_SmallCode.Text.ToLower())).ToList();
                        case 2:
                            return lstWI1.Where(d => d.Item_Code.ToLower().Equals(txtST_SmallCode.Text.ToLower())).ToList();
                        default: return lstWI1;
                    }
                //break;
                case "txtST_Name":
                    switch (cmbST_Name.SelectedIndex)
                    {
                        case 0:
                            return lstWI1.Where(d => d.Item_Name.ToLower().Contains(txtST_Name.Text.ToLower())).ToList();
                        case 1:
                            return lstWI1.Where(d => d.Item_Name.ToLower().StartsWith(txtST_Name.Text.ToLower())).ToList();
                        case 2:
                            return lstWI1.Where(d => d.Item_Name.ToLower().Equals(txtST_Name.Text.ToLower())).ToList();
                        default: return lstWI1;
                    }
            }

            return null;
        }

        private void BtnShowAll_Click(object sender, EventArgs e)
        {
            dgvData.DataSource = GetData();
        }

        private void ChkCanEdit_CheckedChanged(object sender, EventArgs e)
        {
            dgvData.ReadOnly = !chkCanEdit.Checked;
            chkShowUpdateMessage.Enabled = chkCanEdit.Checked;
            ShowData();
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

            Models.Warehouse_Item wi = Program.dbOperations.GetWarehouse_InventoryAsync(index);
            switch (dgvData.Columns[e.ColumnIndex].Name)
            {
                case "Item_Code":
                    wi.Item_Code = Convert.ToString(dgvData["Item_Code", e.RowIndex].Value);
                    if (string.IsNullOrWhiteSpace(wi.Item_Code))
                        return;
                    #region اگر کالایی دیگر با این کد تعریف شده باشد
                    else if (Program.dbOperations.GetAllWarehouse_InventorysAsync(Stack.Company_Index).Where(d => d.Index != index)
                        .Any(j => j.Item_Code.ToLower().Equals(wi.Item_Code.ToLower())))
                    {
                        bSaveChange = false;
                        MessageBox.Show("کد قبلا استفاده شده است.", "خطا");
                        #endregion
                    }
                    break;
                case "Item_Name":
                    wi.Item_Name = Convert.ToString(dgvData["Item_Name", e.RowIndex].Value);
                    break;
                case "Quantity_Real":
                    wi.Quantity_Real = Convert.ToDouble(dgvData["Quantity_Real", e.RowIndex].Value);
                    break;
                case "Item_Unit":
                    bSaveChange = false;
                    wi.Item_Unit = Convert.ToString(dgvData["Item_Unit", e.RowIndex].Value);
                    break;
            }

            if (bSaveChange)
            {
                // برای ذخیره تغییرات در ردیف جدید ، پیغامی نمایش داده نشود
                if (chkCanEdit.Checked)
                {
                    if (chkShowUpdateMessage.Checked)
                    {
                        bSaveChange = MessageBox.Show("آیا از ثبت تغییرات اطمینان دارید؟"
                            , "", MessageBoxButtons.YesNo) == DialogResult.Yes;
                    }
                }
            }


            if (bSaveChange)
                Program.dbOperations.UpdateWarehouse_InventoryAsync(wi);
            else dgvData[e.ColumnIndex, e.RowIndex].Value = InitailValue;
        }

        private void CmbWarehouses_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvData.DataSource = GetData();
        }

        private void BtnDeleteAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("آیا از حذف همه کالاها اطمینان دارید؟"
                , "اخطار", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                == DialogResult.No) return;

            Program.dbOperations.DeleteAllWarehouse_InventorysAsync();
        }

        private void TsmiDelete_Click(object sender, EventArgs e)
        {
            long index = Convert.ToInt64(dgvData.CurrentRow.Cells["Index"].Value);
            Models.Warehouse_Item wi = Program.dbOperations.GetWarehouse_InventoryAsync(index);

            if (MessageBox.Show("آیا از حذف کالا اطمینان دارید؟"
                , wi.Item_Name, MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                == DialogResult.No) return;
            int row = dgvData.CurrentRow.Index;
            Program.dbOperations.DeleteWarehouse_InventoryAsync(wi);

            dgvData.DataSource = GetData();
            try { dgvData.CurrentCell = dgvData["Item_Code", row]; }
            catch { try { dgvData.CurrentCell = dgvData["Item_Code", row-1]; } catch { } }
        }

        int iX = 0, iY = 0;
        private void dgvData_MouseDown(object sender, MouseEventArgs e)
        {
            if (!btnDeleteAll.Visible) return;

            iX = e.X;
            iY = e.Y;
        }

        private void dgvData_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (!btnDeleteAll.Visible) return;

            if (e.ColumnIndex < 0 || e.RowIndex < 0) return;
            if (e.Button == MouseButtons.Right)
            {
                /////// Do something ///////
                // انتخاب سلولی که روی آن کلیک راست شده است
                dgvData.CurrentCell = dgvData[e.ColumnIndex, e.RowIndex];
                contextMenuStrip1.Show(dgvData, new Point(iX, iY));
            }
        }

        private void TxtST_SmallCode_Enter(object sender, EventArgs e)
        {
            AcceptButton = btnSearch;
        }

        private void TxtST_SmallCode_Leave(object sender, EventArgs e)
        {
            AcceptButton = null;
        }

        private void BtnReturn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnAddNew_Click(object sender, EventArgs e)
        {

        }

    }
}
