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
    public partial class L2100_OneOrder : X210_ExampleForm_Normal
    {
        string OrderIndex = null;   // درصورتیکه سفارش جدید باشد، مقدار ندارد
        Models.Order order = new Models.Order();
        bool bOrderReadOnly = false;    // آیا سفارش فقط خواندنی است؟
        long first_level_index = 0;   // مرحله ای سفارش الان در آن قرار دارد
        long current_level_index = 0;   // مرحله ای سفارش الان در آن قرار دارد
        List<Models.Item> lstResult = new List<Models.Item>();


        public L2100_OneOrder(string _OrderIndex = null, bool _bOrderReadOnly = false)
        {
            InitializeComponent();

            if (!string.IsNullOrEmpty(_OrderIndex))
            {
                OrderIndex = _OrderIndex;
                order = Program.dbOperations.GetOrderAsync(OrderIndex);
                // آیا سفارش قابل تغییر است؟
                bOrderReadOnly = _bOrderReadOnly || !Program.dbOperations.GetOrder_LevelAsync
                    (order.CurrentLevel_Index).OrderCanChange;
            //}
            //if (order != null)
            //{
                current_level_index = order.CurrentLevel_Index;
                // اگر سفارش حذف شده یا کنسل شده بود، امکان مجدد سفارش زدن با شماره و تاریخ جدید را فراهم کنم
                Models.Order_Level ol = Program.dbOperations.GetOrder_LevelAsync(current_level_index);
                bOrderReadOnly = bOrderReadOnly && !ol.CancelingLevel && !ol.RemovingLevel;

                txtOrderTitle.Text = order.Title;
                btnCustomer.Text = order.Customer_Name;
                btnCustomer.Tag = order.Customer_Index;
            }

            if (bOrderReadOnly)
            {
                label4.Text = "زمان ثبت سفارش : " + order.Date_sh;
                btnSave.Text = "مشاهده کالاهای سفارش";
                panel2.Enabled = false;
                Text = "مشاهده سفارش";
                btnSave.Visible = false;
            }
            else
            {
                label4.Text = Stack_Methods.Miladi_to_Shamsi_YYYYMMDD(DateTime.Now);
                first_level_index = Program.dbOperations.GetAllOrder_LevelsAsync
                    (Stack.Company_Index).First(d => d.FirstLevel).Index;
                current_level_index = first_level_index;

                //if (Program.dbOperations.GetOrder_LevelAsync(first_level_index).MessageText != null)
                //    btnSave.Text = Program.dbOperations.GetOrder_LevelAsync(first_level_index).MessageText;
                //else btnSave.Visible = false;
            }

            //Program.dbOperations.Items_Reset_Values();
        }

        private void L2100_OneOrder_Shown(object sender, EventArgs e)
        {
            //if (bOrderReadOnly)
            //{
            //    btnSave.Visible = false;
            //}

            cmbST_Name.SelectedIndex = 0;
            cmbST_SmallCode.SelectedIndex = 0;

            Program.dbOperations.Items_Reset_Values(Stack.Company_Index);
            dgvData.DataSource = GetData();
            ShowData();
            //BtnSearch_Click(null, null);

            Application.DoEvents();
            panel1.Enabled = true;
        }

        // NeedToCorrect_C_B1 : آیا نیاز به بروز رسانی پارامتر «سی-بی 1» می باشد؟
        private List<Models.Item> GetData()
        {
            if (!lstResult.Any())
            {
                dgvData.ReadOnly = bOrderReadOnly;

                if (!string.IsNullOrEmpty(OrderIndex))   // اگر سفارش قبلا ثبت شده باشد
                {
                    #region بروز رسانی پارامتر «سی-بی 1»
                    //if (NeedToCorrect_C_B1)
                    {
                        List<Models.Order_Item> lstOI = Program.dbOperations.GetAllOrder_ItemsAsync(Stack.Company_Index, OrderIndex);
                        if (lstOI.Any())
                        {
                            #region کالاهای سفارش
                            foreach (Models.Order_Item oi in lstOI)
                            {
                                Models.Item item = Program.dbOperations.GetItemAsync(oi.Item_Index);
                                item.C_B1 = true;
                                item.Quantity = oi.Quantity;
                                Program.dbOperations.UpdateItemAsync(item);

                            }
                            #endregion
                        }
                        else
                        {

                            MessageBox.Show("?");
                            return null;// Program.dbOperations.GetAllItemsAsync(Stack.Company_Index,1);
                        }
                    }
                    #endregion

                    if (bOrderReadOnly)
                        lstResult = Program.dbOperations.GetAllItemsAsync
                            (Stack.Company_Index, 1).Where(b=>b.Salable).Where(d => d.C_B1).ToList();
                }

                if (!lstResult.Any())
                    lstResult = Program.dbOperations.GetAllItemsAsync
                        (Stack.Company_Index, 1).Where(b => b.Salable).ToList();
            }

            // نمایش قطعات ، ماژولها یا همگی
            if (radModule.Checked) return lstResult.Where(d => d.Module)
                    .OrderByDescending(d => d.C_B1).ThenBy(j=>j.Code_Small).ToList();
            else if (radNotModule.Checked) return lstResult.Where(d => !d.Module)
                    .OrderByDescending(d => d.C_B1).ThenBy(j => j.Code_Small).ToList();
            else return lstResult.OrderByDescending(d => d.C_B1).ThenBy(j => j.Code_Small).ToList();
        }

        private void ShowData()
        {

            #region ترجمه سر ستونها و مخفی کردن بعضی ستونها
            foreach (DataGridViewColumn col in dgvData.Columns)
            {
                switch (col.Name)
                {
                    case "Code_Small":
                        col.HeaderText = "کد";
                        col.ReadOnly = true;
                        col.DefaultCellStyle.BackColor = Color.LightGray;
                        col.DefaultCellStyle.BackColor = Color.LightGray;
                        break;
                    case "Name_Samll":
                        col.HeaderText = "نام کالا";
                        col.ReadOnly = true;
                        col.DefaultCellStyle.BackColor = Color.LightGray;
                        col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        break;
                    case "C_B1":
                        col.HeaderText = "انتخاب";
                        col.Width = 75;
                        break;
                    case "Quantity":
                        col.HeaderText = "تعداد";
                        break;
                    case "SalesPrice":
                        col.HeaderText = "قیمت (ریال)";
                        col.ReadOnly = true;
                        col.DefaultCellStyle.BackColor = Color.LightGray;
                        col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        break;

                    //case "Module":
                    //    col.HeaderText = "ماژول؟";
                    //    col.ReadOnly = true;
                    //    col.DefaultCellStyle.BackColor = Color.LightGray;
                    //    col.Width = 50;
                    //    break;
                    //case "Weight":
                    //    col.HeaderText = "وزن(کیلوگرم)";
                    //    break;
                    default: col.Visible = false; break;
                }
            }
            #endregion
        }

        private void BtnCustomer_Click(object sender, EventArgs e)
        {
            Stack.sx = null;    // index of customer
            new L3100_Customers(true).ShowDialog(); // از فرم برای انتخاب خریدار استفاده شود

            if (!string.IsNullOrEmpty(Stack.sx))
            {
                Models.Customer customer = Program.dbOperations.GetCustomerAsync(Stack.sx);
                btnCustomer.Text = customer.Name;
                btnCustomer.Tag = customer.Index;
            }
        }


        private void BtnSearch_Click(object sender, EventArgs e)
        {
            panel1.Enabled = false;
            Application.DoEvents();

            //ShowData(false);
            List<Models.Item> lstItems = GetData();// (List<Models.Item>)dgvData.DataSource;
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

        private void DgvData_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            //if (dgvData[e.ColumnIndex, e.RowIndex].Value == InitailValue) return;

            long index = Convert.ToInt64(dgvData["Index", e.RowIndex].Value);

            Models.Item item = Program.dbOperations.GetItemAsync(index);
            switch (dgvData.Columns[e.ColumnIndex].Name)
            {
                case "C_B1":
                    item.C_B1 = Convert.ToBoolean(dgvData["C_B1", e.RowIndex].Value);
                    break;
                case "Quantity":
                    item.Quantity = Convert.ToDouble(dgvData["Quantity", e.RowIndex].Value);
                    break;
            }
            Program.dbOperations.UpdateItemAsync(item);
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            #region خطایابی
            if (string.IsNullOrEmpty(txtOrderTitle.Text))
            {
                MessageBox.Show("لطفا عنوان سفارش را وارد نمایید", "خطا");
                return;
            }

            #region خطای تکراری بودن نام سفارش
            if (string.IsNullOrEmpty(OrderIndex))
            {
                if (Program.dbOperations.GetAllOrdersAsync(Stack.UserIndex)
                    .Any(d => d.Title.ToLower().Equals(txtOrderTitle.Text.ToLower())))
                {
                    MessageBox.Show("نام سفارش قبلا استفاده شده است", "خطا");
                    return;
                }
            }
            else
            {
                if (Program.dbOperations.GetAllOrdersAsync(Stack.UserIndex)
                    .Where(j=>j.Index != OrderIndex)
                    .Any(d => d.Title.ToLower().Equals(txtOrderTitle.Text.ToLower())))
                {
                    MessageBox.Show("نام سفارش قبلا استفاده شده است", "خطا");
                    return;
                }
            }
            #endregion

            if (string.IsNullOrEmpty(btnCustomer.Text) || (btnCustomer.Text.Length<2))
            {
                MessageBox.Show("لطفا خریدار را مشخص نمایید. حداق باید دو کاراکتر (حرف ، عدد یا ...) باشد", "خطا");
                return;
            }

            if(!Program.dbOperations.GetAllItemsAsync(Stack.Company_Index, 1).Any(d=>d.C_B1))
            {
                MessageBox.Show("لطفا حداقل یک کالا را برای ثبت سفارش انتخاب نمایید", "خطا");
                return;
            }

            if(Program.dbOperations.GetAllItemsAsync(Stack.Company_Index, 1).Where(d=>d.C_B1).Any(j=>j.Quantity<=0))
            {
                MessageBox.Show("تمام موارد انتخاب شده باید دارای تعدادی بیشتر از صفر باشند", "خطا");
                return;
            }
            #endregion

            #region پیغام های اطمینان از ثبت
            if (string.IsNullOrEmpty(OrderIndex))
            {
                if (MessageBox.Show("آیا از ثبت اطلاعات این سفارش اطمینان دارید؟", ""
                    , MessageBoxButtons.YesNo) == DialogResult.No) return;
            }
            else
            {
                if (MessageBox.Show("آیا از ثبت تغییرات سفارش اطمینان دارید؟", ""
                    , MessageBoxButtons.YesNo) == DialogResult.No) return;
            }
            #endregion

            panel1.Enabled = false;
            progressBar1.Visible = true;
            Application.DoEvents();

            bool bSaveItems_to_Order = false;   // آیا اطلاعات کالاها در سفارش ذخیره شود؟

            if (!bOrderReadOnly)
            {
                if (string.IsNullOrEmpty(OrderIndex))
                    order.Index = Stack.UserIndex + Stack_Methods.DateTimeNow_Shamsi("/", ":", true);
                else order.Index = OrderIndex;

                #region مشخصات سفارش
                string sDateTime_sh = Stack_Methods.DateTimeNow_Shamsi();
                order.Company_Index = Stack.Company_Index;
                order.Title = txtOrderTitle.Text;
                order.User_Index = Stack.UserIndex;
                order.Customer_Index = btnCustomer.Tag.ToString();
                order.Customer_Name = btnCustomer.Text;
                order.PreviousLevel_Index = 0;
                order.CurrentLevel_Index = first_level_index; // new ThisProject().Next_OrderLevel_Indexes(OrderIndex).First();
                order.Level_Description = Program.dbOperations.GetOrder_LevelAsync(first_level_index).Description2;
                order.DateTime_mi = DateTime.Now;
                order.Date_sh = sDateTime_sh.Substring(0, 10);
                order.Time = sDateTime_sh.Substring(11, 5);
                #endregion

                if (string.IsNullOrEmpty(OrderIndex))
                {
                    Program.dbOperations.AddOrderAsync(order);
                    OrderIndex = order.Index;
                }
                else
                {
                    Program.dbOperations.UpdateOrderAsync(order);

                    #region حذف کالاهای قبلی سفارش در صورت وجود
                    if (Program.dbOperations.GetAllOrder_ItemsAsync(Stack.Company_Index, OrderIndex).Any())
                    {
                        foreach (Models.Order_Item oi in Program.dbOperations.GetAllOrder_ItemsAsync(Stack.Company_Index, OrderIndex))
                            Program.dbOperations.DeleteOrder_Item(oi);

                        foreach (Models.Order_Item_Property oip in Program.dbOperations.GetAllOrder_Item_PropertiesAsync(Stack.Company_Index, OrderIndex))
                            Program.dbOperations.DeleteOrder_Item_PropertyAsync(oip);
                    }
                    #endregion

                    #region حذف مراحل گذراندۀ قبلی
                    if(Program.dbOperations.GetAllOrder_OLsAsync(Stack.Company_Index,OrderIndex).Any())
                    {
                        foreach (Models.Order_OL ool in Program.dbOperations.GetAllOrder_OLsAsync(Stack.Company_Index, OrderIndex))
                            Program.dbOperations.DeleteOrder_OLAsync(ool);
                    }
                    #endregion
                }

                bSaveItems_to_Order = true;
            }

  
            bSaveItems_to_Order = bSaveItems_to_Order && !bOrderReadOnly && (order != null);
            if (bSaveItems_to_Order)
            {
                #region وارد کردن کالاها و مشخصات آنها در سفارش
                foreach (Models.Item item in Program.dbOperations.GetAllItemsAsync(Stack.Company_Index, 1)
                    .Where(d => d.C_B1).ToList())
                {
                    Program.dbOperations.AddOrder_Item(new Models.Order_Item
                    {
                        Company_Index = Stack.Company_Index,
                        Order_Index = order.Index,
                        Item_Index = item.Index,
                        Item_Name_Samll = item.Name_Samll,
                        Item_SmallCode = item.Code_Small,
                        Item_Module = item.Module,
                        FixedPrice = item.FixedPrice,
                        SalesPrice = item.SalesPrice,
                        Quantity = item.Quantity,
                    });
                    Application.DoEvents();
                }

                //MessageBox.Show(Program.dbOperations.GetAllItemsAsync(Stack.Company_Index,1)
                //    .Where(d => d.C_B1).ToList().Count().ToString(),"تعداد کالاها");

                int itemOrder_Counter = 0;   // شمارنده کالا در کل سفارش
                foreach (Models.Order_Item oi in Program.dbOperations.GetAllOrder_ItemsAsync(Stack.Company_Index,order.Index))
                {
                    //MessageBox.Show(oi.Item_SmallCode, "کد کالا");

                    for (int i = 0; i < oi.Quantity; i++)
                    {
                        itemOrder_Counter++;
                        // تمام مشخصه های کالا
                        foreach (Models.Item_Property ip in Program.dbOperations.GetAllItem_PropertiesAsync(Stack.Company_Index,oi.Item_Index))
                        {
                            Models.Property property = Program.dbOperations.GetPropertyAsync(ip.Property_Index);
                            Program.dbOperations.AddOrder_Item_Property(new Models.Order_Item_Property
                            {
                                Company_Index = Stack.Company_Index,
                                OI_Index = oi.Index,
                                Order_Index = order.Index,
                                Item_Index = oi.Item_Index,
                                Item_SmallCode = oi.Item_SmallCode,
                                ItemBatch_Counter = i+1, // شمارنده کالا در یک دسته
                                ItemOrder_Counter = itemOrder_Counter,
                                Property_Index = ip.Property_Index,
                                Property_Name = property.Name,
                                Property_Description = property.Description,
                                Property_Value = property.DefaultValue,
                                Property_ChangingValue = ip.ChangingValue,
                            });
                            Application.DoEvents();
                            //MessageBox.Show(property.Name, (i+1).ToString());
                        }
                    }
                }
                #endregion

                // ثبت در تاریخچه
                ThisProject this_project = new ThisProject();
                this_project.Create_OrderHistory(order);
                // ثبت جدول مراحل گذرانده سفارش
                if (this_project.AddOrder_OrderLevel(order))
                {
                    // تعیین مرحله بعدی
                    if (this_project.Next_OrderLevel_Indexes(order.Index).Any())
                    {
                        order.NextLevel_Index = this_project.Next_OrderLevel_Indexes(order.Index).First();
                        Program.dbOperations.UpdateOrderAsync(order);
                    }
                }
            }

            //Hide();
            new L2120_OneOrder_Items(OrderIndex, bOrderReadOnly).ShowDialog();
            ForceClose = Stack.bx;
            Close();

            Application.DoEvents();
            panel1.Enabled = true;
            progressBar1.Visible = false;
        }

        int iX = 0, iY = 0;
        private void dgvData_MouseDown(object sender, MouseEventArgs e)
        {
            return;

            iX = e.X;
            iY = e.Y;
        }

        private void dgvData_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            return;

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
            if (dgvData.Columns[dgvData.CurrentCell.ColumnIndex].Name.Equals("Quantity"))
            {
                MessageBox.Show("لطفا «تعداد» را به صورت عدد وارد نمایید.", "خطای نوع داده", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("خطای نوع داده", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // If this is true, then the user is trapped in this cell.
            e.Cancel = false;
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

        private void TsmiChangePropertiesValue_Click(object sender, EventArgs e)
        {
            //new L1120_Order_Item_Properties().ShowDialog();
        }

        private void RadModule_CheckedChanged(object sender, EventArgs e)
        {
            //MessageBox.Show(bOrderReadOnly.ToString());
            if(radModule.Checked)
                dgvData.DataSource = GetData();
        }

        private void RadNotModule_CheckedChanged(object sender, EventArgs e)
        {
            if (radNotModule.Checked)
                dgvData.DataSource = GetData();
        }

        private void RadAll_CheckedChanged(object sender, EventArgs e)
        {
            if (radAll.Checked)
                dgvData.DataSource = GetData();
        }

        private void TxtST_SmallCode_Enter(object sender, EventArgs e)
        {
            AcceptButton = btnSearch;
        }

        bool ForceClose = false;
        private void L2100_OneOrder_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!ForceClose)
            {
                bool bNotAsking = false;
                bNotAsking = string.IsNullOrWhiteSpace(txtOrderTitle.Text)
                    && (string.IsNullOrWhiteSpace(btnCustomer.Text) || (btnCustomer.Text.Length==1))
                    && !dgvData.Rows.Cast<DataGridViewRow>().Any(d => Convert.ToBoolean(d.Cells["C_B1"].Value));

                if(!bNotAsking)
                    if (MessageBox.Show("آیا مایل به بستن صفحه می باشید؟", ""
                        , MessageBoxButtons.YesNo) != DialogResult.Yes) e.Cancel = true;
            }
        }

        private void TxtST_SmallCode_Leave(object sender, EventArgs e)
        {
            AcceptButton = null;
        }


    }
}
