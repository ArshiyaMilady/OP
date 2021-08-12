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
    public partial class L3100_Customers : X210_ExampleForm_Normal
    {
        bool ChooseCustomer;
        //Models.Customer customer;

        // ChooseCustomer = true : از این فرم، خریداری انتخاب می شود
        public L3100_Customers(bool _ChooseCustomer = false)
        {
            InitializeComponent();

            ChooseCustomer = _ChooseCustomer;

            if (ChooseCustomer)
            {
                Text = "   انتخاب خریدار";
                //panel3.Visible = true;
                tsmiConfirmSelection.Visible = true;
                chkCanEdit.Checked = true;
                //btnAddNew.Enabled = true;
                dgvData.ReadOnly = false;
            }

            if (Stack.UserLevel <= Stack.UserLevel_Supervisor1)
                btnDeleteAll.Visible = true;

            if (Stack.UserLevel <= Stack.UserLevel_Supervisor3)
                panel2.Enabled = true;
        }

        private void L3100_Customers_Shown(object sender, EventArgs e)
        {
            cmbST_Name.SelectedIndex = 0;
            cmbST_Mobile.SelectedIndex = 0;
            cmbST_Phone.SelectedIndex = 0;

            dgvData.DataSource = GetData();
            ShowData();
        }


        private List<Models.Customer> GetData()
        {
            if(Stack.UserLevel <= Stack.UserLevel_SaleUnit)
                return Program.dbOperations.GetAllCustomersAsync(Stack.Company_Index);
            else return Program.dbOperations.GetAllCustomersAsync(Stack.Company_Index, Stack.UserIndex);
        }

        private void ShowData()//bool ChangeHeaderTexts = true)
        {
            #region ترجمه سر ستونها و مخفی کردن بعضی ستونها
            //if (ChangeHeaderTexts)
            {
                foreach (DataGridViewColumn col in dgvData.Columns)
                {
                    switch (col.Name)
                    {
                        case "colChoose":
                            if (ChooseCustomer)
                            {
                                col.HeaderText = "-";
                                col.Width = 75;
                            }
                            else col.Visible = false;
                            break;
                        case "Index":
                            //if (Stack.UserLevel <= Stack.UserLevel_Admin)
                            if (Stack.UserLevel_Type == 1)
                            {
                                col.HeaderText = "شناسه";
                                col.ReadOnly = true;
                                col.Width = 60;
                            }
                            else col.Visible = false;
                            break;
                        case "Name":
                            col.HeaderText = "نام";
                            col.Width = 200;
                            break;
                        case "Mobile":
                            col.HeaderText = "شماره همراه";
                            col.Width = 100;
                            break;
                        case "Phone":   
                            col.HeaderText = "تلفن ثابت";
                            //col.DefaultCellStyle.BackColor = Color.LightGray;
                            col.Width = 100;
                            break;
                        case "Address":
                            col.HeaderText = "آدرس";
                            col.Width = 200;
                            //col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                            break;
                        case "Description":
                            col.HeaderText = "توضیحات";
                            //col.ReadOnly = true;
                            //col.DefaultCellStyle.BackColor = Color.LightGray;
                            //col.Width = 50;
                            break;
                        //case "Weight":
                        //    col.HeaderText = "وزن(کیلوگرم)";
                        //    break;
                        default: col.Visible = false; break;
                    }
                }
            }
            #endregion
        }


        private void BtnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtST_Name.Text)
                && string.IsNullOrWhiteSpace(txtST_Mobile.Text)
                && string.IsNullOrWhiteSpace(txtST_Phone.Text))
            {
                ShowData();
                return;
            }


            panel1.Enabled = false;
            //dgvData.Visible = false;
            Application.DoEvents();

            //ShowData(false);
            List<Models.Customer> lstCustomers = GetData();
            //List<Models.Customer> lstCustomers = (List<Models.Customer>)dgvData.DataSource;

            foreach (Control c in groupBox1.Controls)
            {
                //MessageBox.Show(c.Text);
                if (c.Name.Length > 4)
                {
                    if (c.Name.Substring(0, 5).Equals("txtST"))
                        if (!string.IsNullOrWhiteSpace(c.Text))
                        {
                            lstCustomers = SearchThis(lstCustomers, c.Name);
                            if ((lstCustomers == null) || !lstCustomers.Any()) break;
                        }
                }
            }

            dgvData.DataSource = lstCustomers;

            //System.Threading.Thread.Sleep(500);
            Application.DoEvents();
            panel1.Enabled = true;
            //dgvData.Visible = true;

        }

        // جستجوی موردی
        private List<Models.Customer> SearchThis(List<Models.Customer> lstItems1, string TextBoxName)
        {
            switch (TextBoxName)
            {
                case "txtST_Name":
                    switch (cmbST_Name.SelectedIndex)
                    {
                        case 0:
                            return lstItems1.Where(d => d.Name.ToLower().Contains(txtST_Name.Text.ToLower())).ToList();
                        case 1:
                            return lstItems1.Where(d => d.Name.ToLower().StartsWith(txtST_Name.Text.ToLower())).ToList();
                        case 2:
                            return lstItems1.Where(d => d.Name.ToLower().Equals(txtST_Name.Text.ToLower())).ToList();
                        default: return lstItems1;
                    }
                //break;
                case "txtST_Mobile":
                    switch (cmbST_Mobile.SelectedIndex)
                    {
                        case 0:
                            return lstItems1.Where(d => d.Mobile.Contains(txtST_Mobile.Text)).ToList();
                        case 1:
                            return lstItems1.Where(d => d.Mobile.StartsWith(txtST_Mobile.Text)).ToList();
                        case 2:
                            return lstItems1.Where(d => d.Mobile.Equals(txtST_Mobile.Text)).ToList();
                        default: return lstItems1;
                    }
                case "txtST_Phone":
                    switch (cmbST_Phone.SelectedIndex)
                    {
                        case 0:
                            return lstItems1.Where(d => d.Phone.Contains(txtST_Phone.Text)).ToList();
                        case 1:
                            return lstItems1.Where(d => d.Phone.StartsWith(txtST_Phone.Text)).ToList();
                        case 2:
                            return lstItems1.Where(d => d.Phone.Equals(txtST_Phone.Text)).ToList();
                        default: return lstItems1;
                    }
            }

            return null;
        }

        private void BtnReturn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void TsmiOtherOrders_Click(object sender, EventArgs e)
        {
            string customer_index = Convert.ToString(dgvData.CurrentRow.Cells["Index"].Value);
            new L1000_Orders(customer_index).ShowDialog();
        }

        int iNewRow = -1;
        private void BtnAddNew_Click(object sender, EventArgs e)
        {
            long id = 1;
            if (Program.dbOperations.GetAllCustomersAsync(Stack.Company_Index).Any())
                id = Program.dbOperations.GetAllCustomersAsync(Stack.Company_Index).Max(d => d.Id) + 1;

            if (Program.dbOperations.AddCustomer(new Models.Customer
            {
                Company_Index = Stack.Company_Index,
                Name = "نام " + id,
                Mobile = "همراه " + id,
                Phone = "ثابت " + id,
                User_Index = Stack.UserIndex,
                Index = Stack.UserIndex + Stack_Methods.DateTimeNow_Shamsi("/", ":", true),
            }) > 0)
            {
                dgvData.DataSource = GetData();
                //ShowData();
                iNewRow = dgvData.Rows.Count - 1;
                dgvData.CurrentCell = dgvData["Name", iNewRow];
                dgvData.Focus();
            }

        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("آیا از حذف تمام موارد اطمینان دارید؟"
                , "اخطار", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                == DialogResult.No) return;

            Program.dbOperations.DeleteAllCustomersAsync();
            Program.dbOperations.DeleteAllOrder_CustomersAsync();
            dgvData.DataSource = Program.dbOperations.GetAllCustomersAsync(Stack.Company_Index);
        }

        private void ChkCanEdit_CheckedChanged(object sender, EventArgs e)
        {
            dgvData.ReadOnly = !chkCanEdit.Checked;
            chkShowUpdateMessage.Enabled = chkCanEdit.Checked;
            btnAddNew.Enabled = chkCanEdit.Checked;
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

            bool bChangeName = false;   // آیا نام خریدار باید تغییر کند. این تغییر باید در جدول سفارشها نیز ااعمال گردد
            bool bSaveChange = true;   // آیا تغییر ذخیره شود؟

            string index = Convert.ToString(dgvData["Index", e.RowIndex].Value);

            Models.Customer customer = Program.dbOperations.GetCustomerAsync(index);
            switch (dgvData.Columns[e.ColumnIndex].Name)
            {
                case "Name":
                    customer.Name = Convert.ToString(dgvData["Name", e.RowIndex].Value);
                    if (string.IsNullOrWhiteSpace(customer.Name))  // return;
                    {
                        MessageBox.Show(dgvData.Columns["Name"].HeaderText + " نمی تواند خالی باشد", "خطا");
                        bSaveChange = false;
                    }
                    else
                    {
                        if (GetData().Where(d => !d.Index.Equals(index))
                        .Any(j => j.Name.ToLower().Equals(customer.Name.ToLower())))
                        {
                            if (MessageBox.Show("این نام قبلا استفاده شده است"
                                + "\n" + customer.Name + "آیا مایل به ثبت خریدار با همین نام می باشید؟"
                                , "", MessageBoxButtons.YesNo) == DialogResult.No) bSaveChange = false;
                        }

                        bChangeName = bSaveChange;
                    }
                    break;
                case "Mobile":
                    customer.Mobile = Convert.ToString(dgvData["Mobile", e.RowIndex].Value);
                    if (string.IsNullOrWhiteSpace(customer.Mobile))
                    {
                        MessageBox.Show(dgvData.Columns["Mobile"].HeaderText + " نمی تواند خالی باشد", "خطا");
                        bSaveChange = false;
                    }
                    else if (GetData().Where(d => !d.Index.Equals(index))
                        .Any(j => j.Mobile.ToLower().Equals(customer.Mobile.ToLower())))
                    {
                        MessageBox.Show("شماره همراه قبلا استفاده شده است"
                            + "\n" + customer.Mobile, "خطا در ثبت تغییرات");
                        bSaveChange = false;
                    }
                    break;
                case "Phone":
                    //bSaveChange = false;
                    customer.Phone = Convert.ToString(dgvData["Phone", e.RowIndex].Value);
                    break;
                case "Address":
                    customer.Address = Convert.ToString(dgvData["Address", e.RowIndex].Value);
                    break;
                case "Description":
                    customer.Description = Convert.ToString(dgvData["Description", e.RowIndex].Value);
                    break;
            }

            if (bSaveChange)
            {
                // برای ذخیره تغییرات در ردیف جدید ، پیغامی نمایش داده نشود
                if ((e.RowIndex == iNewRow))
                    Program.dbOperations.UpdateCustomerAsync(customer);
                else
                {
                    if (chkCanEdit.Checked)
                    {
                        if (chkShowUpdateMessage.Checked)
                        {
                            bSaveChange = MessageBox.Show("آیا از ثبت تغییرات اطمینان دارید؟"
                                , "", MessageBoxButtons.YesNo) == DialogResult.Yes;
                            //Program.dbOperations.UpdateCustomerAsync(customer);
                        }
                    }
                }
            }

            if (bSaveChange)
            {
                Program.dbOperations.UpdateCustomerAsync(customer);
                #region اعمال تغییر نام در جدول سفارشها
                if (bChangeName)
                {
                    foreach (Models.Order order in Program.dbOperations.GetAllOrders(Stack.Company_Index)
                        .Where(d => d.Customer_Index == index).ToList())
                    {
                        order.Customer_Name = customer.Name;
                        Program.dbOperations.UpdateOrderAsync(order);

                    }
                }
                #endregion
            }
            else dgvData[e.ColumnIndex, e.RowIndex].Value = InitailValue;
        }

        private void TsmiConfirmSelection_Click(object sender, EventArgs e)
        {
            try
            {
                Stack.sx = Convert.ToString(dgvData.CurrentRow.Cells["Index"].Value);
                //MessageBox.Show(index, "Index");
                //customer = Program.dbOperations.GetCustomerAsync(index);
                //MessageBox.Show(customer.Name, "customer.Name");
                Close();
            }
            catch { MessageBox.Show("لطفا یک مورد را انتخاب نمایید"); }
        }

        private void DgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0) return;
            if (dgvData.Columns[e.ColumnIndex].Name.Equals("colChoose"))
                TsmiConfirmSelection_Click(null, null);
        }

        int iX = 0, iY = 0;
        private void dgvData_MouseDown(object sender, MouseEventArgs e)
        {
            iX = e.X;
            iY = e.Y;
        }

        private void BtnShowAll_Click(object sender, EventArgs e)
        {
            dgvData.DataSource = GetData();
        }

        private void BtnImportDataFromExcel_Click(object sender, EventArgs e)
        {

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



    }
}
