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
    public partial class M1110_WarehouseItems : X210_ExampleForm_Normal
    {
        bool bUserCanEdit = false;
        bool bUserCanEdit_Quantity = false;
        bool bUserCanAdd = false;

        public M1110_WarehouseItems()
        {
            InitializeComponent();

            // امکان تغییر فقط مقادیر کالاها
            panel2.Visible =  Stack.lstUser_ULF_UniquePhrase.Contains("jq3121");

            btnAddNew.Visible = Stack.lstUser_ULF_UniquePhrase.Contains("jn2120"); // امکان افزودن
            //if (Stack.lstUser_ULF_UniquePhrase.Contains("jn2110")) // امکان تغییر
            //    tsmiChange.Visible = true;
            //else tsmiDetails.Visible = true;
        }

        private void M1110_WarehouseItems_Shown(object sender, EventArgs e)
        {
            //if (Stack.UserLevel_Type==1)
            //{
            //    //btnDeleteAll.Visible = true;
            //    //tsmiDelete.Visible = true;
            //}

            cmbST_Name.SelectedIndex = 0;
            cmbST_SmallCode.SelectedIndex = 0;
            comboBox1.SelectedIndex = 0;

            cmbWarehouses.Items.Add("تمام انبارها");
            cmbWarehouses.Items.AddRange(Program.dbOperations.GetAllWarehousesAsync
                (Stack.Company_Index, true).Select(d => d.Name).ToArray());
            cmbWarehouses.SelectedIndex = 0;

            //dgvData.DataSource = GetData();
            ShowData();

            Application.DoEvents();
            panel1.Enabled = true;
        }

        private List<Models.Item> GetData()
        {
            switch (cmbWarehouses.SelectedIndex)
            {
                case 0: return Program.dbOperations.GetAllItemsAsync(Stack.Company_Index)
                        .OrderBy(d => d.Code_Small).ToList();
                default:
                    long wh_index = Program.dbOperations.GetWarehouseAsync(Stack.Company_Index, cmbWarehouses.Text).Index;
                    return Program.dbOperations.GetAllItems_in_WarehouseAsync
                        (Stack.Company_Index,wh_index).OrderBy(d=>d.Code_Small).ToList();
            }
        }

        private void ShowData()
        {
            #region ترجمه سر ستونها و مخفی کردن بعضی ستونها
            foreach (DataGridViewColumn col in dgvData.Columns)
            {
                switch (col.Name)
                {
                    case "Enable":
                        col.HeaderText = "فعال؟";
                        col.Visible = bUserCanEdit;
                        break;
                    case "Code_Small":
                        col.HeaderText = "کد کالا";
                        col.ReadOnly = !(bUserCanEdit && chkCanEdit.Checked);
                        col.Width = 100;
                        break;
                    case "Name_Samll":
                        col.HeaderText = "نام کالا";
                        col.ReadOnly = !(bUserCanEdit && chkCanEdit.Checked);
                        col.Width = 200;
                        break;
                    case "Unit":
                        col.HeaderText = "واحد";
                        col.ReadOnly = !(bUserCanEdit && chkCanEdit.Checked);
                        col.Width = 100;
                        break;
                    case "Wh_Quantity_Real":
                        col.HeaderText = "موجودی";
                        col.ReadOnly = !chkCanEdit.Checked;
                        //col.ReadOnly = !( bUserCanEdit_Quantity && chkCanEdit.Checked);
                        col.Width = 100;
                        break;
                    case "Wh_Quantity_x":
                        if (Stack.UserLevel_Type == 1)  // فقط برای برنامه نویس قابل مشاهده باشد
                        {
                            col.HeaderText = "موجودی غیر واقعی";
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
            List<Models.Item> lstItems = (List<Models.Item>)dgvData.DataSource;
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

            Models.Item item = Program.dbOperations.GetItem(index);
            switch (dgvData.Columns[e.ColumnIndex].Name)
            {
                case "Code_Small":
                    item.Code_Small = Convert.ToString(dgvData["Code_Small", e.RowIndex].Value);
                    if (string.IsNullOrWhiteSpace(item.Code_Small))
                        return;
                    #region اگر کالایی دیگر با این کد تعریف شده باشد
                    else if (Program.dbOperations.GetAllItemsAsync(Stack.Company_Index)
                        .Where(d => d.Index != index).Any(j => j.Code_Small.ToLower().Equals(item.Code_Small.ToLower())))
                    {
                        bSaveChange = false;
                        MessageBox.Show("کد قبلا استفاده شده است.", "خطا");
                        #endregion
                    }
                    break;
                case "Name_Samll":
                    item.Name_Samll = Convert.ToString(dgvData["Name_Samll", e.RowIndex].Value);
                    break;
                case "Wh_Quantity_Real":
                    item.Wh_Quantity_Real = Convert.ToDouble(dgvData["Wh_Quantity_Real", e.RowIndex].Value);
                    break;
                case "Unit":
                    bSaveChange = false;
                    item.Unit = Convert.ToString(dgvData["Unit", e.RowIndex].Value);
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
                Program.dbOperations.UpdateItemAsync(item);
            else dgvData[e.ColumnIndex, e.RowIndex].Value = InitailValue;
        }

        private void CmbWarehouses_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvData.DataSource = GetData();
        }

        private void BtnDeleteAll_Click(object sender, EventArgs e)
        {
            //if (MessageBox.Show("آیا از حذف همه کالاها اطمینان دارید؟"
            //    , "اخطار", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
            //    == DialogResult.No) return;

            //Program.dbOperations.DeleteAllWarehouse_InventorysAsync();
        }

        private void TsmiDelete_Click(object sender, EventArgs e)
        {

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
                contextMenuStrip1.Show(dgvData, new Point(iX, iY));
            }
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

        private void CmbWarehouses_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            dgvData.DataSource = GetData();
        }

        private void TsmiDetails_Click(object sender, EventArgs e)
        {
            long index = Convert.ToInt64(dgvData.CurrentRow.Cells["Index"].Value);
            Models.Item item = Program.dbOperations.GetItemAsync(index);

            int type = Stack.lstUser_ULF_UniquePhrase.Contains("jn2110") ? 1 : 0;
            new K1302_Item_Details(type, item).ShowDialog();

            if (Stack.bx)
            {
                BtnShowAll_Click(null, null);
                DataGridViewRow row = null;
                if ((row = dgvData.Rows.Cast<DataGridViewRow>().ToList().FirstOrDefault
                    (d => d.Cells["Index"].Value.ToString().Equals(index.ToString()))) != null)
                {
                    dgvData.CurrentCell = row.Cells["Name_Samll"];
                }
            }

        }

        private void M1110_WarehouseItems_Load(object sender, EventArgs e)
        {

        }

        private void BtnAddNew_Click(object sender, EventArgs e)
        {
            new K1302_Item_Details(2).ShowDialog();

            if(Stack.bx)
            {
                dgvData.DataSource = GetData();
                dgvData.CurrentCell = dgvData["Name_Samll", dgvData.Rows.Count - 1];
            }
        }





    }
}
