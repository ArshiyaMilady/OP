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
    public partial class J2210_UL_Features : X210_ExampleForm_Normal
    {
        public J2210_UL_Features()
        {
            InitializeComponent();
        }

        private void J2210_UL_Features_Shown(object sender, EventArgs e)
        {
            cmbST_Unique_Phrase.SelectedIndex = 0;
            cmbST_Description.SelectedIndex = 0;

            dgvData.DataSource = GetData();
            ShowData();
            ColorDgv();
        }

        private List<Models.UL_Feature> GetData()
        {
            int nEnableType = 1;
            if (radDisabled.Checked) nEnableType = -1;
            if (radAll.Checked) nEnableType = 0;

            return Program.dbOperations.GetAllUL_FeaturesAsync
                (Stack.Company_Index, nEnableType).OrderBy(d=>d.Unique_Phrase).ToList();
        }

        private void ShowData()
        {
            #region ترجمه سر ستونها و مخفی کردن بعضی ستونها
            foreach (DataGridViewColumn col in dgvData.Columns)
            {
                switch (col.Name)
                {
                    //case "Index":
                    //    col.HeaderText = "شناسه";
                    //    col.Width = 50;
                    //    break;
                    case "Description":
                        col.HeaderText = "شرح";
                        col.Width = 500;
                        break;
                    case "Unique_Phrase":
                        col.HeaderText = "عبارت شاخص";
                        //col.ReadOnly = true;
                        col.Width = 100;
                        break;
                    case "Enabled":
                        col.HeaderText = "فعال؟";
                        col.Width = 50;
                        //col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        break;

                    default: col.Visible = false; break;
                }

                if (col.ReadOnly) col.DefaultCellStyle.BackColor = Color.WhiteSmoke;
                else col.DefaultCellStyle.BackColor = Color.White;
            }
            #endregion
        }

        // رنگ بندی امکانات
        private void ColorDgv()
        {
            foreach (DataGridViewRow row in dgvData.Rows.Cast<DataGridViewRow>()
                .Where(d => d.Cells["Unique_Phrase"].Value.ToString().Substring(2).Equals("0000")).ToList())
                row.DefaultCellStyle.BackColor = Color.Yellow;
        }

        private void BtnAddNew_Click(object sender, EventArgs e)
        {
            if (!chkCanEdit.Checked)
                chkCanEdit.Checked = true;

            bool b2 = true;
            if (chkShowUpdateMessage.Checked)
                b2 = chkShowUpdateMessage.Checked = false;

            long index = Program.dbOperations.GetNewIndex_UL_Feature();
                
            Program.dbOperations.AddUL_Feature(new Models.UL_Feature
            {
                Company_Index = Stack.Company_Index,
                Index = index,
                Unique_Phrase = "xx-" + index,
                Description = "؟",
                Enabled=true,
            });

            if (index > 0)
            {
                dgvData.DataSource = GetData();
                ColorDgv();
                ShowData();
                int iNewRow = dgvData.Rows.Count - 1;
                dgvData.CurrentCell = dgvData["Unique_Phrase", iNewRow];
                dgvData.Focus();
            }

            Application.DoEvents();
            if (!b2) chkShowUpdateMessage.Checked = true;
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
            if (e.Button == MouseButtons.Right)
            {
                /////// Do something ///////
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
            CancelButton = null;

            InitailValue = dgvData[e.ColumnIndex, e.RowIndex].Value;//.ToString();
        }

        private void DgvData_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            bool bSaveChange = true;   // آیا تغییر ذخیره شود؟
            bool bEnbaled_Changed = false;

            if (e.RowIndex < 0 || e.ColumnIndex < 0) bSaveChange = false;
            if (dgvData[e.ColumnIndex, e.RowIndex].Value == InitailValue) bSaveChange = false;

            if (!bSaveChange)
            {
                CancelButton = btnReturn;
                return;
            }

            long index = Convert.ToInt64(dgvData["Index", e.RowIndex].Value);

            Models.UL_Feature ul_feature = Program.dbOperations.GetUL_FeatureAsync(index);
            switch (dgvData.Columns[e.ColumnIndex].Name)
            {
                case "Unique_Phrase":
                    ul_feature.Unique_Phrase = Convert.ToString(dgvData["Unique_Phrase", e.RowIndex].Value);
                    break;
                case "Description":
                    ul_feature.Description = Convert.ToString(dgvData["Description", e.RowIndex].Value);
                    break;
                case "Enabled":
                    ul_feature.Enabled = Convert.ToBoolean(dgvData["Enabled", e.RowIndex].Value);
                    bEnbaled_Changed = true;
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
            {
                Program.dbOperations.UpdateUL_FeatureAsync(ul_feature);

                #region اگر فعال بودن امکانی تغییر نماید
                if (bEnbaled_Changed)
                {
                    panel1.Enabled = false;
                    Application.DoEvents();
                    foreach(Models.User_Level_UL_Feature ul_ulf in Program
                        .dbOperations.GetAllUser_Level_UL_FeaturesAsync(Stack.Company_Index,0,0)
                        .Where(d=>d.UL_Feature_Index == ul_feature.Index).ToList())
                    {
                        ul_ulf.UL_Feature_Enabled = ul_feature.Enabled;
                        Program.dbOperations.UpdateUser_Level_UL_FeatureAsync(ul_ulf);
                    }
                    Application.DoEvents();
                    panel1.Enabled = true;

                }
                #endregion
            }
            else dgvData[e.ColumnIndex, e.RowIndex].Value = InitailValue;

            CancelButton = btnReturn;
        }

        private void BtnDeleteAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("آیا از حذف همه سطوح اطمینان دارید؟"
                , "اخطار", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                == DialogResult.No) return;

            if (MessageBox.Show("با انجام این عمل ، تمام روابط سطوح کاربری و جداول دیگر از بین خواهد رفت"
                + "\n" + "آیا از حذف تمام سطوح اطمینان دارید؟", "اخطار 2"
                , MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

            Program.dbOperations.DeleteAllUL_FeaturesAsync();
            Program.dbOperations.DeleteAllUser_Level_UL_FeaturesAsync();
            dgvData.DataSource = GetData();
            ColorDgv();
        }

        private void BtnReturn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtST_Unique_Phrase.Text)
                && string.IsNullOrWhiteSpace(txtST_Description.Text))
                return;

            panel1.Enabled = false;
            Application.DoEvents();

            List<Models.UL_Feature> lstUL_Features = (List<Models.UL_Feature>)dgvData.DataSource;
            {
                foreach (Control c in groupBox1.Controls)
                {
                    //MessageBox.Show(c.Text);
                    if (c.Name.Length > 4)
                    {
                        if (c.Name.Substring(0, 5).Equals("txtST"))
                            if (!string.IsNullOrWhiteSpace(c.Text))
                            {
                                lstUL_Features = SearchThis(lstUL_Features, c.Name);
                                if ((lstUL_Features == null) || !lstUL_Features.Any()) break;
                            }
                    }
                }
            }

            dgvData.DataSource = lstUL_Features;

            Application.DoEvents();
            panel1.Enabled = true;
        }

        // جستجوی موردی
        private List<Models.UL_Feature> SearchThis(List<Models.UL_Feature> lstUL_Feature1, string TextBoxName)
        {
            switch (TextBoxName)
            {
                case "txtST_Unique_Phrase":
                    switch (cmbST_Unique_Phrase.SelectedIndex)
                    {
                        case 0:
                            return lstUL_Feature1.Where(d => d.Unique_Phrase.ToLower().Contains(txtST_Unique_Phrase.Text.ToLower())).ToList();
                        case 1:
                            return lstUL_Feature1.Where(d => d.Unique_Phrase.ToLower().StartsWith(txtST_Unique_Phrase.Text.ToLower())).ToList();
                        case 2:
                            return lstUL_Feature1.Where(d => d.Unique_Phrase.ToLower().Equals(txtST_Unique_Phrase.Text.ToLower())).ToList();
                        default: return lstUL_Feature1;
                    }
                case "txtST_Description":
                    switch (cmbST_Description.SelectedIndex)
                    {
                        case 0:
                            return lstUL_Feature1.Where(d => d.Description.ToLower().Contains(txtST_Description.Text.ToLower())).ToList();
                        case 1:
                            return lstUL_Feature1.Where(d => d.Description.ToLower().StartsWith(txtST_Description.Text.ToLower())).ToList();
                        case 2:
                            return lstUL_Feature1.Where(d => d.Description.ToLower().Equals(txtST_Description.Text.ToLower())).ToList();
                        default: return lstUL_Feature1;
                    }
            }

            return null;
        }

        private void tsmiDelete_Click(object sender, EventArgs e)
        {
            long index = Convert.ToInt64(dgvData.CurrentRow.Cells["Index"].Value);
            Models.UL_Feature uL_Feature = Program.dbOperations.GetUL_FeatureAsync(index);

            if (MessageBox.Show("آیا از حذف این قابلیت اطمینان دارید؟"
               , uL_Feature.Unique_Phrase, MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
               == DialogResult.No) return;

            Program.dbOperations.DeleteUL_FeatureAsync(uL_Feature);
            dgvData.DataSource = GetData();
            ColorDgv();

            pictureBox1.Visible = true;
            Application.DoEvents();
            timer1.Enabled = true;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            timer1.Enabled = false;
        }

        private void J2210_UL_Features_FormClosing(object sender, FormClosingEventArgs e)
        {
            btnReturn.Focus();
        }

        private void RadEnabled_CheckedChanged(object sender, EventArgs e)
        {
            dgvData.DataSource = GetData();
            ColorDgv();
        }

        private void BtnShowAll_Click(object sender, EventArgs e)
        {
            dgvData.DataSource = GetData();
            ColorDgv();
        }

    }
}
