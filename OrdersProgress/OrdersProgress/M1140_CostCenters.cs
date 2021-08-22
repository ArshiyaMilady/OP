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
    public partial class M1140_CostCenters : X210_ExampleForm_Normal
    {
        public M1140_CostCenters()
        {
            InitializeComponent();

            tsmiDelete.Visible = Stack.UserLevel_Type == 1;
        }

        private void M1140_CostCenters_Shown(object sender, EventArgs e)
        {
            cmbST_Description.SelectedIndex = 0;

            dgvData.DataSource = GetData();
            ShowData();
        }

        private List<Models.CostCenter> GetData()
        {
            return Program.dbOperations.GetAllCostCentersAsync(Stack.Company_Index);
        }

        private void ShowData()
        {
            #region ترجمه سر ستونها و مخفی کردن بعضی ستونها
            foreach (DataGridViewColumn col in dgvData.Columns)
            {
                switch (col.Name)
                {
                    case "Index_in_Company":
                        if (Stack.UserLevel_Type == 1)
                        {
                            col.HeaderText = "کد مرکز هزینه";
                            col.ReadOnly = true;
                            col.Width = 120;
                        }
                        else col.Visible = false;
                        break;
                    case "Description":
                        col.HeaderText = "شرح";
                        col.Width = 200;
                        break;
                    case "Type":
                        if ((Stack.UserLevel_Type == 1) || (Stack.UserLevel_Type == 2))
                        {
                            col.HeaderText = "نوع";
                            col.Width = 50;
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

        private void BtnReturn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnAddNew_Click(object sender, EventArgs e)
        {
            long index = Program.dbOperations.AddCostCenter(Stack.Company_Index
                ,new Models.CostCenter
            {
                Company_Index = Stack.Company_Index,
                Description = "؟",
                Type = 1,   // مربوط به درخواست کالا از انبار
            });

            if (index > 0)
            {
                dgvData.DataSource = GetData();
                ShowData();
                int iNewRow = dgvData.Rows.Count - 1;
                dgvData.CurrentCell = dgvData["Description", iNewRow];
                dgvData.Focus();
            }
        }

        private void ChkCanEdit_CheckedChanged(object sender, EventArgs e)
        {
            dgvData.SelectionMode = chkCanEdit.Checked ? DataGridViewSelectionMode.RowHeaderSelect
                : DataGridViewSelectionMode.FullRowSelect;
            dgvData.ReadOnly = !chkCanEdit.Checked;

            chkShowUpdateMessage.Enabled = chkCanEdit.Checked;
            ShowData();
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
            dgvData.CurrentCell = dgvData[e.ColumnIndex, e.RowIndex];
            if (e.Button == MouseButtons.Right)
            {
                /////// Do something ///////
                long ul_index = Convert.ToInt64(dgvData.CurrentRow.Cells["Index"].Value);
                Models.User_Level user_level = Program.dbOperations.GetUser_LevelAsync(ul_index);
                tsmiDelete.Visible = (Stack.UserLevel_Type == 1) || (user_level.Type == 0);

                // انتخاب سلولی که روی آن کلیک راست شده است
                dgvData.CurrentCell = dgvData[e.ColumnIndex, e.RowIndex];
                Application.DoEvents();
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

            Models.CostCenter cc = Program.dbOperations.GetCostCenterAsync(index);
            switch (dgvData.Columns[e.ColumnIndex].Name)
            {
                case "Description":
                    cc.Description = Convert.ToString(dgvData["Description", e.RowIndex].Value);
                    if (string.IsNullOrWhiteSpace(cc.Description))
                        return;
                    #region اگر شرح دیگری با این شرح تعریف شده باشد
                    else if (Program.dbOperations.GetAllCostCentersAsync(Stack.Company_Index).Where(d => d.Index != index)
                        .Any(j => j.Description.ToLower().Equals(cc.Description.ToLower())))
                    {
                        MessageBox.Show("شرک کد هزینه قبلا استفاده شده است", "خطا");
                        bSaveChange = false;
                    }
                    #endregion
                    break;
                case "Type":
                    if (dgvData["Type", e.RowIndex].Value == null)
                        return;
                     cc.Type = Convert.ToInt32(dgvData["Type", e.RowIndex].Value);
                    break;
            }

            if (bSaveChange)
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

            if (bSaveChange)
            {
                Program.dbOperations.UpdateCostCenterAsync(cc);
            }
            else dgvData[e.ColumnIndex, e.RowIndex].Value = InitailValue;
        }

        private void DgvData_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // Don't throw an exception when we're done.
            e.ThrowException = false;
            if (dgvData.Columns[dgvData.CurrentCell.ColumnIndex].Name.Equals("Type"))
            {
                MessageBox.Show("لطفا «نوع» را به صورت عدد وارد نمایید.", "خطای نوع داده", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("خطای نوع داده", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // If this is true, then the user is trapped in this cell.
            e.Cancel = false;
        }

        private void BtnShowAll_Click(object sender, EventArgs e)
        {
            dgvData.DataSource = GetData();
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtST_Description.Text))
                return;

            panel1.Enabled = false;
            Application.DoEvents();

            List<Models.CostCenter> lstCC = (List<Models.CostCenter>)dgvData.DataSource;
            {
                foreach (Control c in groupBox1.Controls)
                {
                    //MessageBox.Show(c.Text);
                    if (c.Name.Length > 4)
                    {
                        if (c.Name.Substring(0, 5).Equals("txtST"))
                            if (!string.IsNullOrWhiteSpace(c.Text))
                            {
                                lstCC = SearchThis(lstCC, c.Name);
                                if ((lstCC == null) || !lstCC.Any()) break;
                            }
                    }
                }
            }

            dgvData.DataSource = lstCC;

            Application.DoEvents();
            panel1.Enabled = true;
        }

        // جستجوی موردی
        private List<Models.CostCenter> SearchThis(List<Models.CostCenter> lstCC1, string TextBoxName)
        {
            switch (TextBoxName)
            {
                case "txtST_Description":
                    switch (cmbST_Description.SelectedIndex)
                    {
                        case 0:
                            return lstCC1.Where(d => d.Description.ToLower().Contains(txtST_Description.Text.ToLower())).ToList();
                        case 1:
                            return lstCC1.Where(d => d.Description.ToLower().StartsWith(txtST_Description.Text.ToLower())).ToList();
                        case 2:
                            return lstCC1.Where(d => d.Description.ToLower().Equals(txtST_Description.Text.ToLower())).ToList();
                        default: return lstCC1;
                    }
            }

            return null;
        }

        private void TsmiDelete_Click(object sender, EventArgs e)
        {
            long cc_index = Convert.ToInt64(dgvData.CurrentRow.Cells["Index"].Value);
            Models.CostCenter cc = Program.dbOperations.GetCostCenterAsync(cc_index);

            if (MessageBox.Show("آیا از حذف این مرکز هزینه اطمینان دارید؟"
               , cc.Description, MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
               != DialogResult.Yes) return;
            panel1.Enabled = false;

            // حذف سطح کاربری
            Program.dbOperations.DeleteCostCenterAsync(cc);

            dgvData.DataSource = GetData();

            //pictureBox1.Visible = true;
            Application.DoEvents();
            //timer1.Enabled = true;
            panel1.Enabled = true;
        }


    }
}
