using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrdersProgress
{
    public partial class M1120_WarehouseRequest_AddNew : X210_ExampleForm_Normal
    {
        List<Models.Item> lstItems = new List<Models.Item>();
        List<long> lstCostCenter_Code = new List<long>();

        public M1120_WarehouseRequest_AddNew()
        {
            InitializeComponent();

            // آیا کاربر درخواست(های) جدیدی را ثبت کرده است
            Stack.bx = false;
        }

        private void M1120_WarehouseRequest_AddNew_Load(object sender, EventArgs e)
        {
            if (!backgroundWorker1.IsBusy)
                backgroundWorker1.RunWorkerAsync();
            //dgvWarehouseItems.DataSource = Program.dbOperations.GetAllItemsAsync(Stack.Company_Index)
        }

        private void M1120_WarehouseRequest_AddNew_Shown(object sender, EventArgs e)
        {
            if (Stack.UserLevel_Type == 0)
            {
                if (!Program.dbOperations.GetAllUL_Request_CategoriesAsync(Stack.Company_Index, Stack.UserLevel_Index).Any())
                {
                    MessageBox.Show("عدم امکان ثبت درخواست", "خطا");
                    Close();
                    return;
                }
            }

            #region دسته کالاهایی که کاربر می تواند کالاهای آن را انتخاب نماید
            List<long> lstCatsIndex = Program.dbOperations.GetAllUL_Request_CategoriesAsync
                (Stack.Company_Index, Stack.UserLevel_Index).Select(d=>d.Category_Index).ToList();

            foreach (long l in lstCatsIndex)
                cmbCategories.Items.Add(Program.dbOperations.GetCategoryAsync(l).Name);
            if (cmbCategories.Items.Count > 0) cmbCategories.SelectedIndex = 0;
            #endregion

            foreach (Models.CostCenter cc in Program.dbOperations.GetAllCostCentersAsync(Stack.Company_Index, 1)
               .Where(d=>!d.Description.Equals("?") && !d.Description.Equals("؟"))
               .OrderBy(d => d.Index_in_Company).ToList())
            {
                lstCostCenter_Code.Add(cc.Index_in_Company);
                cmbCostCenters.Items.Add(cc.Index_in_Company + " - " + cc.Description);
            }
            if (cmbCostCenters.Items.Count > 0) cmbCostCenters.SelectedIndex = 0;


            cmbST_Name.SelectedIndex = 0;
            cmbST_SmallCode.SelectedIndex = 0;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            #region خطایابی
            if (dgvRequestItems.Rows.Count == 0)
            {
                MessageBox.Show("حداقل یک کالا را برای ثبت درخواست وارد نمایید", "خطا");
                return;
            }

            #endregion

            if (MessageBox.Show("پس از ثبت درخواست، امکان تغییر در درخواست نخواهد بود. آیا از ثبت درخواست اطمینان دارید؟"
                , "", MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            panel1.Enabled = false;
            progressBar1.Visible = true;
            Application.DoEvents();
            bool bNeed_Supervisor_Confirmation = dgvRequestItems.Rows.Cast<DataGridViewRow>()
                .Any(d => Convert.ToBoolean(d.Cells["colNeed_Supervisor_Confirmation"].Value));

            #region ثبت درخواست
            Models.Warehouse_Request wr = new Models.Warehouse_Request
            {
                Company_Index = Stack.Company_Index,
                UserLevel_Index = Stack.UserLevel_Index,
                Unit_Name = Program.dbOperations.GetUser_LevelAsync(Stack.UserLevel_Index).Unit_Name,
                User_Index = Stack.UserIndex,
                User_Name = Stack.UserName,
                DateTime_mi = DateTime.Now,
                DateTime_sh = Stack_Methods.DateTimeNow_Shamsi(),
                Need_Supervisor_Confirmation = bNeed_Supervisor_Confirmation,

                // برای شروع کار باید سرپرست با درخواست دهنده یکسان باشد
                //Supervisor_Confirmer_Index = Stack.UserIndex,
                //Supervisor_Confirmer_LevelIndex = Stack.UserLevel_Index
            };
            #endregion

            Application.DoEvents();

            #region تهیه ردیف های درخواست
            long wr_index = Program.dbOperations.AddWarehouse_RequestAsync(wr,Stack.Company_Index);

            for (int i = 0; i < dgvRequestItems.Rows.Count; i++)
            {
                DataGridViewRow row = dgvRequestItems.Rows[i];

                long cost_center_index = 0;
                if (lstCostCenter_Code.Any())
                    cost_center_index = lstCostCenter_Code[cmbCostCenters.SelectedIndex];
                Models.Item item = (Models.Item)row.Tag;
                Program.dbOperations.AddWarehouse_Request_RowAsync(
                    new Models.Warehouse_Request_Row
                    {
                        Company_Index = Stack.Company_Index,
                        Warehouse_Request_Index = wr_index,
                        Warehouse_Request_Index_in_Company = wr.Index_in_Company,
                        CostCenter_Index = cost_center_index,
                        Item_Index = item.Index, // Convert.ToInt64(row.Cells["colItem_Index"].Value),
                        Item_SmallCode =item.Code_Small, // Convert.ToString(row.Cells["colItem_Index"].Value),
                        Item_Name = item.Name_Samll,    // Convert.ToString(row.Cells["colItem_Name"].Value),
                        Item_Unit =item.Unit,   // Convert.ToString(row.Cells["colItem_Unit"].Value),
                        Item_Category_Index = item.Category_Index,
                        Quantity = Convert.ToDouble(row.Cells["colQuantity"].Value),
                        Need_Supervisor_Confirmation = Convert.ToBoolean(row.Cells["colNeed_Supervisor_Confirmation"].Value),
                        //Supervisor_Confirmer_LevelIndex = Convert.ToInt64(row.Cells["colSupervisor_Confirmer_LevelIndex"].Value),
                    });
                Application.DoEvents();
            }
            #endregion

            Application.DoEvents();

            // ثبت درخواست در تاریخچه
            new ThisProject().Create_RequestHistory(wr, "درخواست ثبت گردید");

            progressBar1.Visible = false;
            panel1.Enabled = true;
            dgvRequestItems.Rows.Clear();
            Stack.bx = true;
        }

        private void BtnReturn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void M1120_WarehouseRequest_AddNew_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(dgvRequestItems.Rows.Count>0)
            {
                if (MessageBox.Show("آیا مایل به بستن صفحه می باشید؟", "", MessageBoxButtons.YesNo)
                    != DialogResult.Yes) { e.Cancel = true; return; }
            }

            if(backgroundWorker1.IsBusy)
                backgroundWorker1.CancelAsync();
        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            lstItems = Program.dbOperations.GetAllItemsAsync(Stack.Company_Index, 1, 100);
            if (Stack.UserLevel_Type == 0)
            {
                // دسته کالاهایی که کاربر جاری می تواند به انبار درخواست دهد
                List<long> lstURC_Categories = Program.dbOperations
                    .GetAllUL_Request_CategoriesAsync(Stack.Company_Index
                    , Stack.UserLevel_Index).Select(j=>j.Category_Index).ToList();
                lstItems = lstItems.Where(d => lstURC_Categories.Contains(d.Category_Index)).ToList();

                // تعداد قابل درخواست از هر کالا
                foreach (Models.Item item in lstItems)
                    item.C_D1 = item.Wh_Quantity_Real - item.Wh_Quantity_Booking;
            }
        }

        private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            dgvWarehouseItems.DataSource = lstItems;
            ShowData();

            if (dgvWarehouseItems.Rows.Count > 0)
                dgvWarehouseItems.CurrentCell = dgvWarehouseItems["Name_Samll", 0];

            progressBar1.Visible = false;
            panel1.Enabled = true;
        }

        private void ShowData()
        {
            #region ترجمه سر ستونها و مخفی کردن بعضی ستونها
            //if (ChangeHeaderTexts)
            {
                foreach (DataGridViewColumn col in dgvWarehouseItems.Columns)
                {
                    switch (col.Name)
                    {
                        case "Code_Small":
                            col.HeaderText = "کد";
                            col.Width = 100;
                            break;
                        case "Name_Samll":
                            col.HeaderText = "نام کالا";
                            //col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                            col.Width = 400;
                            break;

                        case "C_D1":
                            col.HeaderText = "موجودی";
                            col.Width = 100;
                            break;
                        default: col.Visible = false; break;
                    }
                }
            }
            #endregion
        }

        private void BtnShowAll_Click(object sender, EventArgs e)
        {
            dgvWarehouseItems.DataSource = lstItems;
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtST_Name.Text)
                && string.IsNullOrWhiteSpace(txtST_SmallCode.Text))
                return;

            panel1.Enabled = false;
            Application.DoEvents();

            List<Models.Item> lstItems1 = (List<Models.Item>)dgvWarehouseItems.DataSource;
            foreach (Control c in groupBox2.Controls)
            {
                //MessageBox.Show(c.Text);
                if (c.Name.Length > 4)
                {
                    if (c.Name.Substring(0, 5).Equals("txtST"))
                        if (!string.IsNullOrWhiteSpace(c.Text))
                        {
                            lstItems1 = SearchThis(lstItems1, c.Name);
                            if ((lstItems1 == null) || !lstItems1.Any()) break;
                        }
                }
            }

            long category_index = Program.dbOperations.GetCategoryAsync(cmbCategories.Text, Stack.Company_Index).Index;
            dgvWarehouseItems.DataSource = lstItems1.Where(d=>d.Category_Index == category_index).ToList();

            //System.Threading.Thread.Sleep(500);
            Application.DoEvents();
            panel1.Enabled = true;
            //dgvData.Visible = true;

        }

        // جستجوی موردی
        private List<Models.Item> SearchThis(List<Models.Item> lstItems2, string TextBoxName)
        {
            switch (TextBoxName)
            {
                case "txtST_SmallCode":
                    switch (cmbST_SmallCode.SelectedIndex)
                    {
                        case 0:
                            return lstItems2.Where(d => d.Code_Small.ToLower().Contains(txtST_SmallCode.Text.ToLower())).ToList();
                        case 1:
                            return lstItems2.Where(d => d.Code_Small.ToLower().StartsWith(txtST_SmallCode.Text.ToLower())).ToList();
                        case 2:
                            return lstItems2.Where(d => d.Code_Small.ToLower().Equals(txtST_SmallCode.Text.ToLower())).ToList();
                        default: return lstItems2;
                    }
                //break;
                case "txtST_Name":
                    switch (cmbST_Name.SelectedIndex)
                    {
                        case 0:
                            return lstItems2.Where(d => d.Name_Samll.ToLower().Contains(txtST_Name.Text.ToLower())).ToList();
                        case 1:
                            return lstItems2.Where(d => d.Name_Samll.ToLower().StartsWith(txtST_Name.Text.ToLower())).ToList();
                        case 2:
                            return lstItems2.Where(d => d.Name_Samll.ToLower().Equals(txtST_Name.Text.ToLower())).ToList();
                        default: return lstItems2;
                    }
            }

            return null;
        }

        private void DgvWarehouseItems_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            try
            {
                //dgvWarehouseItems.CurrentCell = dgvWarehouseItems["Name_Samll", 0];
                textBox1.Text = dgvWarehouseItems["Name_Samll",e.RowIndex].Value.ToString();
                textBox2.Text = dgvWarehouseItems["Code_Small", e.RowIndex].Value.ToString();
                label2.Text = dgvWarehouseItems["Unit", e.RowIndex].Value.ToString();
                numericUpDown1.Value = 0;
            }
            catch { }
        }

        private void BtnAddItem_to_Request_Click(object sender, EventArgs e)
        {
            #region خطایابی
            if (numericUpDown1.Value == 0)
            {
                MessageBox.Show("تعداد درخواست را مشخص نمایید", "خطا");
                return;
            }

            // موجودی قابل برداشت = موجودی انبار - رزرو شده ها
            decimal qExistence = Convert.ToDecimal(dgvWarehouseItems.CurrentRow.Cells["C_D1"].Value);
            if (numericUpDown1.Value > qExistence)
            {
                if(MessageBox.Show("تعداد درخواست شده از تعداد موجودی بیشتر است. آیا درخواست انجام شود؟"
                    , "اخطار",MessageBoxButtons.YesNo) != DialogResult.Yes)
                    return;
            }

            #endregion

            long item_index = Convert.ToInt64(dgvWarehouseItems.CurrentRow.Cells["Index"].Value);
            Models.Item item = Program.dbOperations.GetItem(item_index);
            int iRow = dgvRequestItems.Rows.Add();
            DataGridViewRow row = dgvRequestItems.Rows[iRow];
            row.Tag = item;
            row.Cells["colRow"].Value = iRow+1;
            row.Cells["colItem_Index"].Value = item.Index;
            row.Cells["colItem_SmallCode"].Value = item.Code_Small;
            row.Cells["colItem_Name"].Value = item.Name_Samll;
            row.Cells["colQuantity"].Value = numericUpDown1.Value;
            row.Cells["colItem_Unit"].Value = item.Unit;
            row.Cells["colItem_Category_Index"].Value = item.Category_Index;
            if(lstCostCenter_Code.Any())    // کد مرکز هزینه
                row.Cells["colCostCenter_Index"].Value = lstCostCenter_Code[cmbCostCenters.SelectedIndex];
            // رابطه بین دسته کالا و سطح کاربری در هنگام درخواست کالا از انبار
            Models.UL_Request_Category urc = Program.dbOperations.GetAllUL_Request_CategoriesAsync
                (Stack.Company_Index, Stack.UserLevel_Index).FirstOrDefault(d => d.Category_Index == item.Category_Index);
            if (urc != null)
            {
                row.Cells["colNeed_Supervisor_Confirmation"].Value = urc.Supervisor_UL_Index > 0;
                //row.Cells["colSupervisor_Confirmer_LevelIndex"].Value = urc.Supervisor_UL_Index;
            }
        }

        private void DgvRequestItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            if(dgvRequestItems.Columns[e.ColumnIndex].Name.Equals("colRemove"))
            {
                dgvRequestItems.CurrentCell = null;
                dgvRequestItems.Rows.RemoveAt(e.RowIndex);
            }
        }
    }
}
