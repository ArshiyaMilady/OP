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
    public partial class J2200_Users_Levels : X210_ExampleForm_Normal
    {
        // دسترسی هایی که کاربر بنا به سطح کاربری خود می تواند داشته باشد
        //List<Models.User_Level> lstUL_Result = new List<Models.User_Level>();
        List<Models.User_Level> lstUL = new List<Models.User_Level>();

        public J2200_Users_Levels()
        {
            InitializeComponent();

            Height = 500;
        }

        private void J2200_Users_Levels_Shown(object sender, EventArgs e)
        {
            cmbST_Description.SelectedIndex = 0;

            dgvData.DataSource = GetData();
            ShowData();
        }

        private List<Models.User_Level> GetData()
        {
            if (Stack.UserLevel_Type == 0)
            {
                MessageBox.Show(Stack.UserLevel_Type.ToString());

                foreach (Models.UL_See_UL ul_see_ul in Program.dbOperations
                    .GetAllUL_See_ULsAsync(Stack.Company_Index, Stack.UserLevel_Index))
                {
                    Models.User_Level ul = Program.dbOperations.GetUser_LevelAsync(ul_see_ul.UL_Index);
                    lstUL.Add(ul);
                }
            }
            else
            {
                if (Stack.UserLevel_Type == 1)
                    lstUL = Program.dbOperations.GetAllUser_LevelsAsync(Stack.Company_Index,0).ToList();
                else if (Stack.UserLevel_Type == 2)
                    lstUL = Program.dbOperations.GetAllUser_LevelsAsync(Stack.Company_Index, 0)
                        .Where(d => (d.Type != 1)&&(d.Type != 2)).ToList();
                else 
                    lstUL = Program.dbOperations.GetAllUser_LevelsAsync(Stack.Company_Index, 0)
                        .Where(d => d.Type == 0).ToList();

            }

            if (radEnabledLevel.Checked) return lstUL.Where(d => d.Enabled).OrderByDescending(d => d.C_B1).ToList();
            else if (radDisabledLevel.Checked) return lstUL.Where(d => !d.Enabled).OrderByDescending(d => d.C_B1).ToList();
            else return lstUL.OrderByDescending(d => d.C_B1).ToList();
        }

        private void ShowData()
        {
            #region ترجمه سر ستونها و مخفی کردن بعضی ستونها
            foreach (DataGridViewColumn col in dgvData.Columns)
            {
                switch (col.Name)
                {
                    case "Index":
                        if (Stack.UserLevel_Type == 1)
                        {
                            col.HeaderText = "شناسه";
                            col.ReadOnly = true;
                            col.Width = 50;
                        }
                        else col.Visible = false;
                        break;
                    case "Description":
                        col.HeaderText = "شرح";
                        col.Width = 200;
                        break;
                    case "Enabled":
                        col.HeaderText = "فعال؟";
                        col.Width = 50;
                        //col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        break;
                    case "Type":
                        if (Stack.UserLevel_Type == 1)
                        {
                            col.HeaderText = "Type";
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

        private void BtnAddNew_Click(object sender, EventArgs e)
        {
            long index = Program.dbOperations.AddUser_Level(new Models.User_Level
            {
                Company_Index = Stack.Company_Index,
                Description = "؟",
                Enabled = true,
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

            Models.User_Level user_level = Program.dbOperations.GetUser_LevelAsync(index);
            switch (dgvData.Columns[e.ColumnIndex].Name)
            {
                case "Enabled":
                    user_level.Enabled = Convert.ToBoolean(dgvData["Enabled", e.RowIndex].Value);
                    break;
                case "Description":
                    user_level.Description = Convert.ToString(dgvData["Description", e.RowIndex].Value);
                    break;
                case "Type":
                    user_level.Type = Convert.ToInt32(dgvData["Type", e.RowIndex].Value);
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
                Program.dbOperations.UpdateUser_LevelAsync(user_level);
            }
            else dgvData[e.ColumnIndex, e.RowIndex].Value = InitailValue;
        }

        private void BtnDeleteAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("آیا از حذف همه سطوح اطمینان دارید؟"
                , "اخطار", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                == DialogResult.No) return;

            if (MessageBox.Show("با انجام این عمل ، تمام روابط سطوح کاربری و جداول دیگر از بین خواهد رفت"
                + "\n" + "آیا از حذف تمام سطوح اطمینان دارید؟", "اخطار 2"
                , MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

            Program.dbOperations.DeleteAllUser_LevelsAsync();
            Program.dbOperations.DeleteAllUser_Level_UL_FeaturesAsync();
            Program.dbOperations.DeleteAllUser_ULsAsync();
            dgvData.DataSource = GetData();
        }

        private void RadAll_CheckedChanged(object sender, EventArgs e)
        {
            dgvData.DataSource = GetData();
        }

        private void BtnReturn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtST_Description.Text))
                return;

            panel1.Enabled = false;
            Application.DoEvents();

            List<Models.User_Level> lstULs = (List<Models.User_Level>)dgvData.DataSource;
            {
                foreach (Control c in groupBox1.Controls)
                {
                    //MessageBox.Show(c.Text);
                    if (c.Name.Length > 4)
                    {
                        if (c.Name.Substring(0, 5).Equals("txtST"))
                            if (!string.IsNullOrWhiteSpace(c.Text))
                            {
                                lstULs = SearchThis(lstULs, c.Name);
                                if ((lstULs == null) || !lstULs.Any()) break;
                            }
                    }
                }
            }

            dgvData.DataSource = lstULs;

            Application.DoEvents();
            panel1.Enabled = true;
        }

        // جستجوی موردی
        private List<Models.User_Level> SearchThis(List<Models.User_Level> lstULs1, string TextBoxName)
        {
            switch (TextBoxName)
            {
                case "txtST_Description":
                    switch (cmbST_Description.SelectedIndex)
                    {
                        case 0:
                            return lstULs1.Where(d => d.Description.ToLower().Contains(txtST_Description.Text.ToLower())).ToList();
                        case 1:
                            return lstULs1.Where(d => d.Description.ToLower().StartsWith(txtST_Description.Text.ToLower())).ToList();
                        case 2:
                            return lstULs1.Where(d => d.Description.ToLower().Equals(txtST_Description.Text.ToLower())).ToList();
                        default: return lstULs1;
                    }
            }

            return null;
        }

        private void TsmiDelete_Click(object sender, EventArgs e)
        {
            long ul_index = Convert.ToInt64(dgvData.CurrentRow.Cells["Index"].Value);
            Models.User_Level user_level = Program.dbOperations.GetUser_LevelAsync(ul_index);

            if (MessageBox.Show("آیا از حذف این سطح اطمینان دارید؟"
               , user_level.Description, MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
               != DialogResult.Yes) return;
            panel1.Enabled = false;

            // حذف رابطه این سطح کاربری با تمام کاربران دارای این سطح
            foreach (Models.User_UL user_ul in Program.dbOperations.GetAllUser_ULsAsync(Stack.Company_Index,0,ul_index).ToList())
                    Program.dbOperations.DeleteUser_ULAsync(user_ul);
            // حذف تمام رابطه های این سطح با امکانات آن
            foreach (Models.User_Level_UL_Feature ul_ulf in Program.dbOperations
                .GetAllUser_Level_UL_FeaturesAsync(Stack.Company_Index, ul_index,0))
                    Program.dbOperations.DeleteUser_Level_UL_Feature(ul_ulf);
            // حذف سطح کاربری
            Program.dbOperations.DeleteUser_LevelAsync(user_level);

            dgvData.DataSource = GetData();

            pictureBox1.Visible = true;
            Application.DoEvents();
            timer1.Enabled = true;
            panel1.Enabled = true;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            timer1.Enabled = false;
        }

        private void TsmiUL_Features_Edit_Click(object sender, EventArgs e)
        {
            long user_level_index = Convert.ToInt64(dgvData.CurrentRow.Cells["Index"].Value);
            new J2220_User_Level_UL_Feature(user_level_index).ShowDialog();
        }

        private void TsmiDeleteAllFeatures_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("آیا از حذف همه امکانات این سطح اطمینان دارید؟"
                , "اخطار", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                == DialogResult.No) return;

            long user_level_index = Convert.ToInt64(dgvData.CurrentRow.Cells["Index"].Value);

            Program.dbOperations.DeleteAllUser_Level_UL_Features
                (Program.dbOperations.GetAllUser_Level_UL_FeaturesAsync(Stack.Company_Index, user_level_index));
            pictureBox1.Visible = true;
            Application.DoEvents();
            timer1.Enabled = true;
        }

        private void tsmiUL_See_ULs_Click(object sender, EventArgs e)
        {
            long user_level_index = Convert.ToInt64(dgvData.CurrentRow.Cells["Index"].Value);
            new J2202_UL_See_UL_Orders(user_level_index).ShowDialog();
        }

        private void TsmiUL_See_OL_Click(object sender, EventArgs e)
        {
            long user_level_index = Convert.ToInt64(dgvData.CurrentRow.Cells["Index"].Value);
            new J2230_UL_See_OL(user_level_index).ShowDialog();
        }

        private void TsmiSetOL_UL_Click(object sender, EventArgs e)
        {
            long user_level_index = Convert.ToInt64(dgvData.CurrentRow.Cells["Index"].Value);
            new J2240_UL_Confirm_OLs(user_level_index).ShowDialog();
        }

        private void TsmiUL_Request_Categories_Click(object sender, EventArgs e)
        {
            long user_level_index = Convert.ToInt64(dgvData.CurrentRow.Cells["Index"].Value);
            new J2310_UL_Request_Categories(user_level_index).ShowDialog();
        }

        private void TsmiUL_Confirm_UL_Request_Click(object sender, EventArgs e)
        {
            long user_level_index = Convert.ToInt64(dgvData.CurrentRow.Cells["Index"].Value);
            new J2320_UL_Confirm_UL_Requests(user_level_index).ShowDialog();
        }

        private void BtnShowAll_Click(object sender, EventArgs e)
        {
            dgvData.DataSource = GetData();
        }


    }
}
