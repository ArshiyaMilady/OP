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
    public partial class L1110_Order_Levels : X210_ExampleForm_Normal
    {
        public L1110_Order_Levels()
        {
            InitializeComponent();
        }

        private void L1110_Order_Levels_Shown(object sender, EventArgs e)
        {
            if ((Stack.UserLevel_Type == 1) || (Stack.UserLevel_Type == 2))
            {
                panel2.Visible = true;
                btnAddNew.Visible = true;
                panel3.Visible = true;

                if (Stack.UserLevel_Type == 1)
                {
                    tsmiDelete.Visible = true;
                    tsmiDeleteAllOL_Prerequisites.Visible = true;
                }
            }


            dgvData.DataSource = GetData();
            ShowData();
        }

        private List<Models.Order_Level> GetData()
        {
            if(radEnabledLevel.Checked)
                return Program.dbOperations.GetAllOrder_LevelsAsync(Stack.Company_Index, 1).Where(d=>d.Enabled).ToList();
            else if(radDisabledLevel.Checked)
                return Program.dbOperations.GetAllOrder_LevelsAsync(Stack.Company_Index, -1).Where(d=>!d.Enabled).ToList();

            return Program.dbOperations.GetAllOrder_LevelsAsync(Stack.Company_Index, 0);
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
                    case "Sequence":
                        //if ((Stack.UserLevel_Type == 1) || (Stack.UserLevel_Type == 2))
                        if (Stack.UserLevel_Type == 1)
                        {
                            col.HeaderText = "ترتیب";
                            col.Width = 50;
                        }
                        else col.Visible = false;
                        break;
                    case "Enabled":
                        if (Stack.UserLevel_Type == 1)
                        {
                            col.HeaderText = "فعال؟";
                            col.Width = 50;
                        }
                        else col.Visible = false;
                        //col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        break;
                    case "Description":
                        col.HeaderText = "شرح";
                        col.Width = 250;
                        break;
                    case "Description2":
                        col.HeaderText = "توضیح تکمیلی";
                        col.Width = 200;
                        break;
                    case "OrderCanChange":
                        if (Stack.UserLevel_Type == 1)
                        {
                            col.HeaderText = "امکان تغییر؟";
                            col.Width = 50;
                        }
                        else col.Visible = false;
                        break;
                    case "FirstLevel":
                        col.HeaderText = "مرحله شروع؟";
                        col.Width = 50;
                        //col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        break;
                    case "ReturningLevel":
                        col.HeaderText = "مرحله برگشت؟";
                        col.Width = 50;
                        //col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        break;
                    case "CancelingLevel":
                        col.HeaderText = "مرحله لغو؟";
                        col.Width = 50;
                        //col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        break;
                    case "RemovingLevel":
                        col.HeaderText = "مرحله حذف؟";
                        col.Width = 50;
                        //col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        break;
                    case "LastLevel":
                        col.HeaderText = "مرحله آخر؟";
                        col.Width = 50;
                        //col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        break;
                    case "MessageText":
                        col.HeaderText = "متن پیام";
                        col.Width = 200;
                        break;

                    default: col.Visible = false; break;
                }

                if (col.ReadOnly) col.DefaultCellStyle.BackColor = Color.WhiteSmoke;
                else col.DefaultCellStyle.BackColor = Color.White;
            }
            #endregion
        }

        private void RadEnabledLevel_CheckedChanged(object sender, EventArgs e)
        {
            dgvData.DataSource = GetData();
        }

        private void BtnAddNew_Click(object sender, EventArgs e)
        {
            long sequence = Program.dbOperations.GetAllOrder_LevelsAsync(Stack.Company_Index).Max(d => d.Sequence) + 100;
            long index = Program.dbOperations.AddOrder_Level(new Models.Order_Level
            {
                Company_Index = Stack.Company_Index,
                Description = "؟",
                Enabled = true,
                Type = 0,
                Type_Description = "معمولی",
                Sequence = sequence,
            }) ;

            if (index > 0)
            {
                dgvData.DataSource = GetData();
                Application.DoEvents();
                //ShowData();
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
            //bool bEnable_is_false = false;    

            if (e.RowIndex < 0 || e.ColumnIndex < 0) bSaveChange = false;
            if (dgvData[e.ColumnIndex, e.RowIndex].Value == InitailValue) bSaveChange = false;

            if(!bSaveChange)
            {
                CancelButton = btnReturn;
                return;
            }

            long index = Convert.ToInt64(dgvData["Index", e.RowIndex].Value);

            Models.Order_Level order_level = Program.dbOperations.GetOrder_LevelAsync(index);
            switch (dgvData.Columns[e.ColumnIndex].Name)
            {
                case "Sequence":
                    order_level.Sequence = Convert.ToInt64(dgvData["Sequence", e.RowIndex].Value);
                    break;
                case "Enabled":
                    order_level.Enabled = Convert.ToBoolean(dgvData["Enabled", e.RowIndex].Value);
                    //if(!order_level.Enabled)
                    //{
                    //    if (MessageBox.Show("با غیر فعال کردن این مرحله ، ارتباط این مرحله با سایر جداول از بین خواهد رفت"
                    //        + "\n" + "آیا از ثبت تغییرات اطمینان دارید؟", "", MessageBoxButtons.YesNo)
                    //        != DialogResult.Yes) bSaveChange = false;
                    //    else bEnable_is_false = true;
                    //}
                    break;
                case "Description":
                    order_level.Description = Convert.ToString(dgvData["Description", e.RowIndex].Value);
                    if(string.IsNullOrEmpty(order_level.Description))
                    {
                        MessageBox.Show("شرح مرحله نباید خالی باشد", "خطا");
                        bSaveChange = false;
                    }
                    break;
                case "OrderCanChange":
                    order_level.OrderCanChange = Convert.ToBoolean(dgvData["OrderCanChange", e.RowIndex].Value);
                    break;
                case "ReturningLevel":
                    order_level.ReturningLevel = Convert.ToBoolean(dgvData["ReturningLevel", e.RowIndex].Value);
                    break;
                case "CancelingLevel":
                    order_level.CancelingLevel = Convert.ToBoolean(dgvData["CancelingLevel", e.RowIndex].Value);
                    break;
                case "RemovingLevel":
                    order_level.RemovingLevel = Convert.ToBoolean(dgvData["RemovingLevel", e.RowIndex].Value);
                    break;
                case "FirstLevel":
                    order_level.FirstLevel = Convert.ToBoolean(dgvData["FirstLevel", e.RowIndex].Value);
                    break;
                case "LastLevel":
                    order_level.LastLevel = Convert.ToBoolean(dgvData["LastLevel", e.RowIndex].Value);
                    break;
                case "MessageText":
                    order_level.MessageText = Convert.ToString(dgvData["MessageText", e.RowIndex].Value);
                    break;
                case "Description2":
                    order_level.Description2 = Convert.ToString(dgvData["Description2", e.RowIndex].Value);
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
                Program.dbOperations.UpdateOrder_LevelAsync(order_level);

                //if(bEnable_is_false)
                //{
                //    foreach(Models.OL_Prerequisite olp in Program.dbOperations.GetAllOL_PrerequisitesAsync(Stack.Company_Index))
                //    {
                //        if ((olp.OL_Index == order_level.Index) || (olp.Prerequisite_Index == order_level.Index))
                //            Program.dbOperations.DeleteOL_Prerequisite(olp);
                //    }
                //}
            }
            else dgvData[e.ColumnIndex, e.RowIndex].Value = InitailValue;

            CancelButton = btnReturn;
        }

        private void BtnDeleteAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("آیا از حذف همه مراحل اطمینان دارید؟"
                , "اخطار", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                == DialogResult.No) return;

            if (MessageBox.Show("با انجام این عمل ، تمام روابط مراحل سفارش و جداول دیگر از بین خواهد رفت"
                + "\n" + "آیا از حذف تمام سطوح اطمینان دارید؟", "اخطار 2"
                , MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

            Program.dbOperations.DeleteAllOrder_LevelsAsync();
            Program.dbOperations.DeleteAllOL_PrerequisitesAsync();
            Program.dbOperations.DeleteAllOL_ULsAsync();
            dgvData.DataSource = GetData();

            pictureBox1.Visible = true;
            Application.DoEvents();
            timer1.Enabled = true;

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

            List<Models.Order_Level> lstOLs = (List<Models.Order_Level>)dgvData.DataSource;
            {
                foreach (Control c in groupBox1.Controls)
                {
                    //MessageBox.Show(c.Text);
                    if (c.Name.Length > 4)
                    {
                        if (c.Name.Substring(0, 5).Equals("txtST"))
                            if (!string.IsNullOrWhiteSpace(c.Text))
                            {
                                lstOLs = SearchThis(lstOLs, c.Name);
                                if ((lstOLs == null) || !lstOLs.Any()) break;
                            }
                    }
                }
            }

            dgvData.DataSource = lstOLs;

            Application.DoEvents();
            panel1.Enabled = true;
        }

        // جستجوی موردی
        private List<Models.Order_Level> SearchThis(List<Models.Order_Level> lstULs1, string TextBoxName)
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
            long  ol_index = Convert.ToInt64(dgvData.CurrentRow.Cells["Index"].Value);
            Models.Order_Level order_level = Program.dbOperations.GetOrder_LevelAsync(ol_index);

            if (MessageBox.Show("آیا از حذف این مرحله اطمینان دارید؟"
               , order_level.Description, MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
               != DialogResult.Yes) return;
            panel1.Enabled = false;

            // حذف رابطه این مرحله1 با تمام کاربران دارای این سطح
            foreach (Models.OL_Prerequisite olp in Program.dbOperations.GetAllOL_PrerequisitesAsync(Stack.Company_Index)
                .Where(d => d.OL_Index == ol_index).ToList())
                    Program.dbOperations.DeleteOL_PrerequisiteAsync(olp);
            // حذف تمام رابطه های این سطح با امکانات آن
            foreach (Models.OL_UL ol_ul in Program.dbOperations.GetAllOL_ULsAsync(Stack.Company_Index, ol_index))
                Program.dbOperations.DeleteOL_UL(ol_ul);
            Program.dbOperations.DeleteOrder_LevelAsync(order_level);

            dgvData.DataSource = GetData();
            Application.DoEvents();

            //pictureBox1.Visible = true;
            //timer1.Enabled = true;
            panel1.Enabled = true;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            timer1.Enabled = false;
        }

        private void tsmiOL_Prerequisites_Click(object sender, EventArgs e)
        {
            long order_level_index = Convert.ToInt64(dgvData.CurrentRow.Cells["Index"].Value);
            new L1120_OL_Prerequisites(order_level_index).ShowDialog();
        }

        private void tsmiVerifyingUsers_Click(object sender, EventArgs e)
        {
            long order_level_index = Convert.ToInt64(dgvData.CurrentRow.Cells["Index"].Value);
            new L1130_OL_UL(order_level_index).ShowDialog();
        }

        private void TsmiDeleteAllOL_Prerequisites_Click(object sender, EventArgs e)
        {
            long ol_index = Convert.ToInt64(dgvData.CurrentRow.Cells["Index"].Value);
            Models.Order_Level order_level = Program.dbOperations.GetOrder_LevelAsync(ol_index);

            if (MessageBox.Show("آیا از حذف پیش نیازها اطمینان دارید؟"
               , order_level.Description, MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
               != DialogResult.Yes) return;

            Program.dbOperations.DeleteAllOL_Prerequisites(ol_index);

            pictureBox1.Visible = true;
            Application.DoEvents();
            timer1.Enabled = true;
        }

        private void L1110_Order_Levels_FormClosing(object sender, FormClosingEventArgs e)
        {
            btnReturn.Focus();
        }

        private void BtnImportDataFromExcel_Click(object sender, EventArgs e)
        {

        }

        private void L1110_Order_Levels_Load(object sender, EventArgs e)
        {

        }

        private void TsmiReturningOrderLevels_Click(object sender, EventArgs e)
        {
            long order_level_index = Convert.ToInt64(dgvData.CurrentRow.Cells["Index"].Value);
            new L1140_OL_on_Returning(order_level_index).ShowDialog();
        }

        private void BtnShowAll_Click(object sender, EventArgs e)
        {
            dgvData.DataSource = GetData();
        }

    }
}
