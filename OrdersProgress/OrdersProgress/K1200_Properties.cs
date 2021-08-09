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
    public partial class K1200_Properties : X210_ExampleForm_Normal
    {
        public K1200_Properties()
        {
            InitializeComponent();
        }

        private void K1200_Properties_Shown(object sender, EventArgs e)
        {
            btnDeleteAll.Visible = Stack.UserLevel <= Stack.UserLevel_Supervisor1;

            cmbST_Name.SelectedIndex = 0;
            cmbST_Description.SelectedIndex = 0;
            comboBox1.SelectedIndex = 0;
            dgvData.DataSource = Program.dbOperations.GetAllPropertiesAsync(1);
            ShowData();
        }

        private void ShowData(bool ChangeHeaderTexts = true)
        {
            int enableType = 0;
            switch (comboBox1.SelectedIndex)
            {
                case 0: enableType = 1; break;
                case 1: enableType = -1; break;
                case 2: enableType = 0; break;
            }
            dgvData.DataSource = Program.dbOperations.GetAllPropertiesAsync(enableType);


            #region ترجمه سر ستونها و مخفی کردن بعضی ستونها
            if (ChangeHeaderTexts)
            {
                foreach (DataGridViewColumn col in dgvData.Columns)
                {
                    switch (col.Name)
                    {
                        case "Name":
                            col.HeaderText = "نام مشخصه";
                            col.Width = 200;
                            break;
                        case "Description":
                            col.HeaderText = "شرح";
                            col.Width = 100;
                            break;
                        case "Enable":
                            col.HeaderText = "فعال؟";
                            col.Width = 50;
                            break;
                        case "ChangingValue":
                            col.HeaderText = "آیا مشخصه در هنگام سفارش قابل تغییر باشد؟";
                            //col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                            col.Width = 150;
                            break;
                        default: col.Visible = false; break;
                    }
                }
            }
            #endregion
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            long index = Program.dbOperations.GetNewIndex_Property();

            if (Program.dbOperations.AddPropertyAsync(new Models.Property
            {
                Company_Index = Stack.Company_Index,
                Index = index,
                Enable = true,
                Name = "نام " + (index%100000),
            }) >0)
                dgvData.DataSource = Program.dbOperations.GetAllPropertiesAsync(Stack.Company_Index);
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("آیا از حذف تمام مشخصات اطمینان دارید؟", "اخطار 1"
                , MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

            if (MessageBox.Show("با انجام این عمل ، تمام روابط مشخصه ها و کالاها از بین خواهد رفت"
                +"\n" +"آیا از حذف تمام مشخصات اطمینان دارید؟", "اخطار 2"
                , MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

            Program.dbOperations.DeleteAllPropertiesAsync();
            Program.dbOperations.DeleteAllItem_PropertiesAsync();
            dgvData.DataSource = Program.dbOperations.GetAllPropertiesAsync(Stack.Company_Index);
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
            object value = dgvData[e.ColumnIndex, e.RowIndex].Value;
            if (value == InitailValue) return;

            bool bEnableChanged = false;
            bool bSaveChange = true;   // آیا تغییر ذخیره شود؟
            long index = Convert.ToInt64(dgvData["Index", e.RowIndex].Value);

            Models.Property property = Program.dbOperations.GetPropertyAsync(index);
            switch (dgvData.Columns[e.ColumnIndex].Name)
            {
                case "Name":
                    property.Name = Convert.ToString(dgvData["Name", e.RowIndex].Value);
                    if (string.IsNullOrWhiteSpace(property.Name)) return;
                    else if (Program.dbOperations.GetAllPropertiesAsync(Stack.Company_Index).Where(d => d.Index != index)
                        .Any(j => j.Name.ToLower().Equals(property.Name.ToLower())))
                    {
                        MessageBox.Show("نام مشخصه قبلا استفاده شده است", "خطا");
                        bSaveChange = false;
                    }
                    break;
                case "Description":
                    property.Description = Convert.ToString(dgvData["Description", e.RowIndex].Value);
                    break;
                case "Enable":
                    property.Enable = Convert.ToBoolean(dgvData["Enable", e.RowIndex].Value);
                    bEnableChanged = !property.Enable && Program.dbOperations.GetAllItem_PropertiesAsync(Stack.Company_Index, index).Any();
                    break;
                case "ChangingValue":
                    property.ChangingValue = Convert.ToBoolean(dgvData["ChangingValue", e.RowIndex].Value);
                    break;
            }

            if (bSaveChange)
            {
                string msg = null;
                string title = property.Name;
                if (bEnableChanged)
                    msg = "با انجام این عمل ، مشخصه انتخاب شده از کالاهایی که دارای آن می باشند، حذف می گردد. آیا تغییرات ثبت شود؟";
                else if (chkShowUpdateMessage.Checked)
                    msg = "آیا از ثبت تغییرات اطمینان دارید؟";

                if (!string.IsNullOrEmpty(msg))
                {
                    msg = dgvData.Columns[e.ColumnIndex].HeaderText + " = " + value + "\n" + msg;
                    bSaveChange = MessageBox.Show(msg, title, MessageBoxButtons.YesNo) == DialogResult.Yes;
                }

                if (bSaveChange)
                {
                    Program.dbOperations.UpdatePropertyAsync(property);

                    #region اگر مشخصه غیرفعال شود تمام ارتباطات آن با کالاهای دیگر حذف می گردد
                    if (bEnableChanged)
                    {
                        panel1.Enabled = false;
                        progressBar1.Value = 0;
                        progressBar1.Visible = true;
                        progressBar1.Maximum = Program.dbOperations.GetAllItem_PropertiesAsync(Stack.Company_Index, index).Count;
                        Application.DoEvents();

                        foreach (Models.Item_Property ip in Program.dbOperations.GetAllItem_PropertiesAsync(Stack.Company_Index, index))
                        {
                            Program.dbOperations.DeleteItem_PropertyAsync(ip);
                            progressBar1.Value++;
                            Application.DoEvents();
                        }

                        Application.DoEvents();
                        panel1.Enabled = true;
                        progressBar1.Visible = false;
                    }
                    #endregion
                }
            }

            if(!bSaveChange)
                dgvData[e.ColumnIndex, e.RowIndex].Value = InitailValue;
        }

        private void K1200_Properties_FormClosing(object sender, FormClosingEventArgs e)
        {
            panel1.Focus();
        }

        private void BtnReturn_Click(object sender, EventArgs e)
        {
            Close();
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

        private void ChkCanEdit_CheckedChanged(object sender, EventArgs e)
        {
            dgvData.ReadOnly = !chkCanEdit.Checked;
            chkShowUpdateMessage.Enabled = chkCanEdit.Checked;
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowData(false);
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            //List<Models.Property> lstPr = Program.dbOperations.GetAllPropertiesAsync(1);

            if (string.IsNullOrWhiteSpace(txtST_Name.Text)
                && string.IsNullOrWhiteSpace(txtST_Description.Text))
            {
                ShowData(false);
                return;
            }


            panel1.Enabled = false;
            dgvData.Visible = false;
            Application.DoEvents();
            ShowData(false);

            List<Models.Property> lstPr = (List<Models.Property>)dgvData.DataSource;
            //MessageBox.Show(lstItems.Count.ToString());

            foreach (Control c in groupBox1.Controls)
            {
                //MessageBox.Show(c.Text);
                if (c.Name.Length > 4)
                {
                    if (c.Name.Substring(0, 5).Equals("txtST"))
                        if (!string.IsNullOrWhiteSpace(c.Text))
                        {
                            lstPr = SearchThis(lstPr, c.Name);
                            //MessageBox.Show(c.Text, c.Name);
                            //MessageBox.Show(lstItems.Count.ToString(), c.Name);
                            if ((lstPr == null) || !lstPr.Any()) break;
                        }
                }
            }

            //ShowData(false);
            dgvData.DataSource = lstPr;

            Application.DoEvents();
            panel1.Enabled = true;
            dgvData.Visible = true;

        }

        // جستجوی موردی
        private List<Models.Property> SearchThis(List<Models.Property> lstPr1, string TextBoxName)
        {
            switch (TextBoxName)
            {
                case "txtST_Description":
                    switch (cmbST_Description.SelectedIndex)
                    {
                        case 0:
                            return lstPr1.Where(d => d.Description.ToLower().Contains(txtST_Description.Text.ToLower())).ToList();
                        case 1:
                            return lstPr1.Where(d => d.Description.ToLower().StartsWith(txtST_Description.Text.ToLower())).ToList();
                        case 2:
                            return lstPr1.Where(d => d.Description.ToLower().Equals(txtST_Description.Text.ToLower())).ToList();
                        default: return lstPr1;
                    }
                //break;
                case "txtST_Name":
                    switch (cmbST_Name.SelectedIndex)
                    {
                        case 0:
                            return lstPr1.Where(d => d.Name.ToLower().Contains(txtST_Name.Text.ToLower())).ToList();
                        case 1:
                            return lstPr1.Where(d => d.Name.ToLower().StartsWith(txtST_Name.Text.ToLower())).ToList();
                        case 2:
                            return lstPr1.Where(d => d.Name.ToLower().Equals(txtST_Name.Text.ToLower())).ToList();
                        default: return lstPr1;
                    }
            }

            return null;
        }


        // مشخصه انتخابی را به تمام کالاها (در صورتیکه آن کالا ، مشخصه را نداشت) اضافه میکند
        private void TsmiAddToAllItems_Click(object sender, EventArgs e)
        {
            long index = Convert.ToInt64(dgvData.CurrentRow.Cells["Index"].Value);
            Models.Property pr = Program.dbOperations.GetPropertyAsync(index);

            if(!pr.Enable)
            {
                MessageBox.Show("امکان اضافه کردن مشخصه های غیرفعال وجود ندارد", "خطا");
                return;
            }

            progressBar1.Value = 0;
            panel1.Enabled=false;
            progressBar1.Visible = true;
            Application.DoEvents();

            List<Models.Item> lstItems = Program.dbOperations.GetAllItemsAsync(Stack.Company_Index, 0);
            progressBar1.Maximum = lstItems.Count;
            foreach(Models.Item item in lstItems)
            //foreach(string sCode_Small in lstItems.Select(d=>d.Code_Small))
            {
                // اگر کالا با مشخصه در ارتباط نبود
                if(!Program.dbOperations.GetAllItem_PropertiesAsync(Stack.Company_Index, item.Code_Small)
                    .Any(d=>d.Property_Index == index))
                {
                    Program.dbOperations.AddItem_PropertyAsync(new Models.Item_Property
                    {
                        Item_Code_Small = item.Code_Small,
                        //Item_Enable = item.Enable,
                        Property_Index=index,
                        //Property_Enable = pr.Enable,
                    });
                }
                progressBar1.Value++;
                Application.DoEvents();
            }

            Application.DoEvents();
            panel1.Enabled = true;
            progressBar1.Visible = false;
        }

        // مشخصه انتخابی را از تمام کالاها (در صورتیکه آن کالا ، مشخصه را داشت) حذف میکند
        private void TsmiDeleteFromAllItems_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            panel1.Enabled = false;
            progressBar1.Visible = true;
            Application.DoEvents();

            long index = Convert.ToInt64(dgvData.CurrentRow.Cells["Index"].Value);
            List<Models.Item> lstItems = Program.dbOperations.GetAllItemsAsync(Stack.Company_Index, 0);
            progressBar1.Maximum = lstItems.Count;
            foreach (string sCode_Small in lstItems.Select(d => d.Code_Small))
            {
                // اگر کالا با مشخصه در ارتباط نبود
                if (Program.dbOperations.GetAllItem_PropertiesAsync(Stack.Company_Index, sCode_Small)
                    .Any(d => d.Property_Index == index))
                {
                    Program.dbOperations.DeleteItem_PropertyAsync
                        (Program.dbOperations.GetItem_PropertyAsync(Stack.Company_Index, sCode_Small,index));
                }
                progressBar1.Value++;
                Application.DoEvents();
            }

            Application.DoEvents();
            panel1.Enabled = true;
            progressBar1.Visible = false;
        }

        private void TsmiDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("آیا از حذف این مورد اطمینان دارید؟", "", MessageBoxButtons.YesNo)
                == DialogResult.No) return;

            try
            {
                long index = Convert.ToInt64(dgvData.CurrentRow.Cells["Index"].Value);
                Models.Property property = Program.dbOperations.GetPropertyAsync(index);
                Program.dbOperations.DeletePropertyAsync(property);
                dgvData.DataSource = Program.dbOperations.GetAllPropertiesAsync(Stack.Company_Index);
            }
            catch { MessageBox.Show("خطا در اجرای عملیات"); }
        }



    }
}
