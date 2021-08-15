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
    public partial class M1100_Warehouses : X210_ExampleForm_Normal
    {
        public M1100_Warehouses()
        {
            InitializeComponent();
        }

        private void M1100_Warehouses_Shown(object sender, EventArgs e)
        {
            //if (Stack.UserLevel_Type==1)
            //{
            //    //btnDeleteAll.Visible = true;
            //    //tsmiDelete.Visible = true;
            //}

            cmbST_Name.SelectedIndex = 0;
            cmbST_Address.SelectedIndex = 0;
            cmbST_Phone.SelectedIndex = 0;
            comboBox1.SelectedIndex = 0;

            dgvData.DataSource = GetData();
            ShowData();

            Application.DoEvents();
            panel1.Enabled = true;
        }

        private List<Models.Warehouse> GetData()
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0: return Program.dbOperations.GetAllWarehousesAsync(Stack.Company_Index, true);
                default:
                    return Program.dbOperations.GetAllWarehousesAsync(Stack.Company_Index, false);
            }
        }

        private void ShowData()
        {
            #region ترجمه سر ستونها و مخفی کردن بعضی ستونها
            foreach (DataGridViewColumn col in dgvData.Columns)
            {
                switch (col.Name)
                {
                    case "Active":
                        col.HeaderText = "فعال؟";
                        col.Width = 50;
                        break;
                    case "Name":
                        col.HeaderText = "نام انبار";
                        col.Width = 150;
                        break;
                    case "Phone":
                        col.HeaderText = "تلفن";
                        col.Width = 100;
                        break;
                    case "Address":
                        col.HeaderText = "آدرس";
                        col.Width = 100;
                        break;
                    case "Description":
                        col.HeaderText = "توضیحات";
                        col.Width = 200;
                        break;

                    default: col.Visible = false; break;
                }

                if (col.ReadOnly) col.DefaultCellStyle.BackColor = Color.WhiteSmoke;
                else col.DefaultCellStyle.BackColor = Color.White;
            }
            #endregion
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtST_Name.Text)
                && string.IsNullOrWhiteSpace(txtST_Phone.Text)
                && string.IsNullOrWhiteSpace(txtST_Address.Text))
                return;

            panel1.Enabled = false;
            //dgvData.Visible = false;
            Application.DoEvents();

            //ShowData(false);
            List<Models.Warehouse> lstWHs = (List<Models.Warehouse>)dgvData.DataSource;
            //MessageBox.Show(lstItems.Count.ToString());

            foreach (Control c in groupBox1.Controls)
            {
                //MessageBox.Show(c.Text);
                if (c.Name.Length > 4)
                {
                    if (c.Name.Substring(0, 5).Equals("txtST"))
                        if (!string.IsNullOrWhiteSpace(c.Text))
                        {
                            lstWHs = SearchThis(lstWHs, c.Name);
                            if ((lstWHs == null) || !lstWHs.Any()) break;
                        }
                }
            }

            dgvData.DataSource = lstWHs;

            //System.Threading.Thread.Sleep(500);
            Application.DoEvents();
            panel1.Enabled = true;
            //dgvData.Visible = true;

        }

        // جستجوی موردی
        private List<Models.Warehouse> SearchThis(List<Models.Warehouse> lstWHs1, string TextBoxName)
        {
            switch (TextBoxName)
            {
                case "txtST_Name":
                    switch (cmbST_Name.SelectedIndex)
                    {
                        case 0:
                            return lstWHs1.Where(d => d.Name.ToLower().Contains(txtST_Name.Text.ToLower())).ToList();
                        case 1:
                            return lstWHs1.Where(d => d.Name.ToLower().StartsWith(txtST_Name.Text.ToLower())).ToList();
                        case 2:
                            return lstWHs1.Where(d => d.Name.ToLower().Equals(txtST_Name.Text.ToLower())).ToList();
                        default: return lstWHs1;
                    }
                case "txtST_Phone":
                    switch (cmbST_Phone.SelectedIndex)
                    {
                        case 0:
                            return lstWHs1.Where(d => d.Phone.ToLower().Contains(txtST_Phone.Text.ToLower())).ToList();
                        case 1:
                            return lstWHs1.Where(d => d.Phone.ToLower().StartsWith(txtST_Phone.Text.ToLower())).ToList();
                        case 2:
                            return lstWHs1.Where(d => d.Phone.ToLower().Equals(txtST_Phone.Text.ToLower())).ToList();
                        default: return lstWHs1;
                    }
                case "txtST_Address":
                    switch (cmbST_Address.SelectedIndex)
                    {
                        case 0:
                            return lstWHs1.Where(d => d.Address.ToLower().Contains(txtST_Address.Text.ToLower())).ToList();
                        case 1:
                            return lstWHs1.Where(d => d.Address.ToLower().StartsWith(txtST_Address.Text.ToLower())).ToList();
                        case 2:
                            return lstWHs1.Where(d => d.Address.ToLower().Equals(txtST_Address.Text.ToLower())).ToList();
                        default: return lstWHs1;
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

            int index = Convert.ToInt32(dgvData["Index", e.RowIndex].Value);

            Models.Warehouse wh = Program.dbOperations.GetWarehouseAsync(index);
            switch (dgvData.Columns[e.ColumnIndex].Name)
            {
                case "Active":
                    wh.Active = Convert.ToBoolean(dgvData["Active", e.RowIndex].Value);
                    break;
                case "Name":
                    wh.Name = Convert.ToString(dgvData["Name", e.RowIndex].Value);
                    if (string.IsNullOrWhiteSpace(wh.Name))
                        return;
                    #region اگر انبار دیگرس با این نام تعریف شده باشد
                    else if (Program.dbOperations.GetAllWarehousesAsync(Stack.Company_Index,false)
                        .Where(d => d.Index != index).Any(j => j.Name.ToLower().Equals(wh.Name.ToLower())))
                    {
                        bSaveChange = false;
                        MessageBox.Show("نام انبار تکراری بوده و قبلا استفاده شده است.", "خطا");
                        #endregion
                    }
                    break;
                case "Phone":
                    wh.Phone = Convert.ToString(dgvData["Phone", e.RowIndex].Value);
                    break;
                case "Address":
                    wh.Address = Convert.ToString(dgvData["Address", e.RowIndex].Value);
                    break;
                case "Description":
                    wh.Description = Convert.ToString(dgvData["Description", e.RowIndex].Value);
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
                Program.dbOperations.UpdateItemAsync(wh);
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

        private void DgvData_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // Don't throw an exception when we're done.
            e.ThrowException = false;
            if (dgvData.Columns[dgvData.CurrentCell.ColumnIndex].Name.Equals("Wh_Quantity_Real")
                || dgvData.Columns[dgvData.CurrentCell.ColumnIndex].Name.Equals("Wh_Quantity_x"))
            {
                MessageBox.Show("تعداد را به صورت عدد وارد نمایید", "خطای نوع داده", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("خطای نوع داده", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // If this is true, then the user is trapped in this cell.
            e.Cancel = false;
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
