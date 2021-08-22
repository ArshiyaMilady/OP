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

        public M1120_WarehouseRequest_AddNew()
        {
            InitializeComponent();
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
        }

        private void BtnCancel_Click(object sender, EventArgs e)
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
                            col.Width = 300;
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
            foreach (Control c in groupBox1.Controls)
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

            dgvWarehouseItems.DataSource = lstItems1;

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
            }

            return null;
        }

        private void DgvWarehouseItems_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dgvWarehouseItems.CurrentRow.Cells["Name_Samll"].Value.ToString();
            textBox2.Text = dgvWarehouseItems.CurrentRow.Cells["Code_Small"].Value.ToString();
            label2.Text = dgvWarehouseItems.CurrentRow.Cells["Unit"].Value.ToString();
            numericUpDown1.Value = 0;
        }

        private void BtnAddItem_to_Request_Click(object sender, EventArgs e)
        {
            #region خطایابی
            if (numericUpDown1.Value == 0)
            {
                MessageBox.Show("تعداد درخواست را مشخص نمایید", "خطا");
                return;
            }

            #endregion

            long item_index = Convert.ToInt64(dgvWarehouseItems.CurrentRow.Cells["Index"].Value);
            Models.Item item = progr
            int iRow = dgvRequestItems.Rows.Add();
            dgvRequestItems["colItem_Index"].Value = 
        }
    }
}
