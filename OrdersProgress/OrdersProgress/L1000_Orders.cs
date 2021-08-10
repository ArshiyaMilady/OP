using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EXCEL = Microsoft.Office.Interop.Excel;


namespace OrdersProgress
{
    public partial class L1000_Orders : X210_ExampleForm_Normal
    {
        string CustumerIndex = null;
        List<Models.Order> lstOrders = new List<Models.Order>();
        //int ShowWhichOrders = 0;

        public L1000_Orders(string _CustumerIndex = null)
        {
            InitializeComponent();
            CustumerIndex = _CustumerIndex;

            //if (Stack.UserLevel <= Stack.UserLevel_Admin) ShowWhichOrders = 10;  // نمایش تمام سفارشها
            //else if (Stack.UserLevel <= Stack.UserLevel_Supervisor2) ShowWhichOrders = 20;  // حذف شده ها نمایش داده نشوند  
            //else if (Stack.UserLevel <= Stack.UserLevel_Supervisor3) ShowWhichOrders = 30;  // کنسل شده ها نمایش داده نشوند  
            //else if (Stack.UserLevel <= Stack.UserLevel_SaleUnit) ShowWhichOrders = 40;  // فقط ارسال شده ها به شرکت نمایش داده شوند 
            //if(Stack.UserIndex <=0) Stack
        }

        private void L1000_Orders_Shown(object sender, EventArgs e)
        {
            #region دسترسی ها
            if (Stack.UserLevel_Type == 1)
            {
                btnImportOrdersFromExcel.Visible = true;
                btnDeleteAll.Visible = true;
                tsmiDelete.Visible = true;
            }

            if (Stack.UserLevel_Type != 0)
            {
                tsmiCancel.Visible = true;
                tsmiWarehouseChecklist.Visible = true;
                //tsmiChangeOrder.Visible = true;
            }
            #endregion

            cmbST_CustomerName.SelectedIndex = 0;
            cmbST_OrderTitle.SelectedIndex = 0;

            dgvData.DataSource = GetData();
            //else
            //    dgvData.DataSource = GetData();// Program.dbOperations.GetUserAsync("کاربر اتوماتیک").Index);

            ShowData();

            Application.DoEvents();
            progressBar1.Visible = false;
            panel1.Enabled = true;
        }

        private List<Models.Order> GetData(long user_index = 0,bool bShowWhichOrders = false)
        {
            #region چه سفارشهایی توسط کاربر قابل مشاهده می باشد
            if (!lstOrders.Any())
            {
                // شناسه کاربران ادمین و کاربران ارشد
                List<long> lstStandardUserIndex = Stack_Methods.GetStandardUsersIndex(1);

                if (Stack.UserLevel_Type == 0)
                {
                    // مراحلی از سفارش که کاربر وارد شونده می تواند آنها را «مشاهده» کند

                    // تمام سفارشهای کاربران ، به جز ادمین ها و کاربران ارشد
                    if (Stack.lstUser_ULF_UniquePhrase.Contains("jm2220"))
                    {
                        List<long> lstOL_Indexes_CanSee = Program.dbOperations.GetAllUL_See_OLsAsync
                           (Stack.Company_Index, Stack.UserLevel_Index).Select(d => d.OL_Index).ToList();

                        lstOrders = Program.dbOperations.GetAllOrders(Stack.Company_Index)
                            .Where(b => !lstStandardUserIndex.Contains(b.User_Index))
                            .Where(d => lstOL_Indexes_CanSee.Contains(d.CurrentLevel_Index)).ToList();
                    }
                    else   // فقط سفارشهایی که توسط کاربر وارد شونده ثبت شده اند، امکان مشاهده دارند
                        lstOrders = Program.dbOperations.GetAllOrders(Stack.Company_Index, Stack.UserIndex);
                }
                else if (Stack.UserLevel_Type == 1)
                    lstOrders = Program.dbOperations.GetAllOrders(Stack.Company_Index);
                else
                {
                    // شناسه مرحله سفارش که مربوط به حذف است
                    long removed_order_level_index = 0;
                    if (Program.dbOperations.GetAllOrder_LevelsAsync
                        (Stack.Company_Index).Any(d => d.RemovingLevel))
                    {
                        removed_order_level_index = Program.dbOperations.GetAllOrder_LevelsAsync
                            (Stack.Company_Index).First(d => d.RemovingLevel).Index;
                    }
                    // سفارشهای حذف شده ، نمایش داده نشود
                    lstOrders = Program.dbOperations.GetAllOrders(Stack.Company_Index)
                        .Where(b=>b.CurrentLevel_Index != removed_order_level_index)
                        .Where(d => !lstStandardUserIndex.Contains(d.User_Index)).ToList();
                }

                #region با سطح کاربری این کاربر چه مراحلی از سفارش را می توان تأیید نمود
                List<long> lstOL_Indexes_CanConfirm = Program.dbOperations.GetAllOL_ULsAsync
                    (Stack.Company_Index, 0, Stack.UserLevel_Index).Select(d => d.OL_Index).ToList();
                // علامت گذاری سفارشهایی که قابل تأیید توسط کاربر جاری می باشند
                foreach (Models.Order order in lstOrders)
                    order.C_B1 = (Stack.UserLevel_Type!=0) || lstOL_Indexes_CanConfirm.Contains(order.CurrentLevel_Index);
                #endregion
            }
            #endregion

            if (radOrders_Need_Confirmation.Checked)
                return lstOrders.Where(d => d.C_B1).OrderByDescending(d => d.DateTime_mi).ToList();
            else 
                return lstOrders.OrderByDescending(d=>d.DateTime_mi).ToList();
        }

        private void ShowData(bool ChangeHeaderTexts = true)
        {

            #region ترجمه سر ستونها و مخفی کردن بعضی ستونها
            if (ChangeHeaderTexts)
            {
                foreach (DataGridViewColumn col in dgvData.Columns)
                {
                    switch (col.Name)
                    {
                        //case "Index":
                        //    col.HeaderText = "index";
                        //    break;
                        case "Title":
                            col.HeaderText = "عنوان سفارش";
                            col.Width = 150;
                            break;
                        case "Customer_Name":
                            col.HeaderText = "نام خریدار";
                            col.Width = 150;
                            //col.DefaultCellStyle.BackColor = Color.LightGray;
                            break;
                        case "Date_sh":
                            col.HeaderText = "تاریخ ثبت";
                            //col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                            col.Width = 100;
                            break;
                        case "Level_Description":
                            col.HeaderText = "وضعیت سفارش";
                            //col.ReadOnly = true;
                            //col.DefaultCellStyle.BackColor = Color.LightGray;
                            col.Width = 200;
                            break;
                        default: col.Visible = false; break;
                    }
                }
            }
            #endregion
        }

        private void RadOrders_CheckedChanged(object sender, EventArgs e)
        {
            dgvData.DataSource = GetData();
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
            dgvData.CurrentCell = dgvData["Title", e.RowIndex];

            if (e.Button == MouseButtons.Right)
            {
                /////// Do something ///////
                string order_index = Convert.ToString(dgvData.CurrentRow.Cells["Index"].Value);
                Models.Order order = lstOrders.First(d=>d.Index.Equals(order_index));

                tsmiChangeOrderLevel.Visible = order.C_B1;

                // انتخاب سلولی که روی آن کلیک راست شده است
                dgvData.CurrentCell = dgvData[e.ColumnIndex, e.RowIndex];
                contextMenuStrip1.Show(dgvData, new Point(iX, iY));
            }
        }

        private void DgvData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            TsmiOrderDetails_Click(null, null);
        }

        private void TsmiOrderDetails_Click(object sender, EventArgs e)
        {
            string order_index = Convert.ToString(dgvData.CurrentRow.Cells["Index"].Value);
            Models.Order order = Program.dbOperations.GetOrderAsync(order_index);
            if (Program.dbOperations.GetOrder_LevelAsync(order.CurrentLevel_Index).OrderCanChange)
            {
                new L2100_OneOrder(order_index).ShowDialog();

                Models.Order order1 = Program.dbOperations.GetOrderAsync(order_index);
                // در صورت به وجود آمدن تغییری در سفارش ، جدول را بروز کن
                if (!order.Title.Equals(order1.Title)
                    || (order.Customer_Index != order1.Customer_Index)
                    || (order.DateTime_mi != order1.DateTime_mi))
                {
                    lstOrders = new List<Models.Order>();
                    dgvData.DataSource = GetData();
                }
            }
            else
                new L2120_OneOrder_Items(order_index, true).ShowDialog();
        }

        private void TxtST_Enter(object sender, EventArgs e)
        {
            AcceptButton = btnSearch;
        }

        private void TxtST_Leave(object sender, EventArgs e)
        {
            AcceptButton = null;
        }

        private void BtnShowAll_Click(object sender, EventArgs e)
        {
            dgvData.DataSource = GetData();
        }

        private void TsmiOrderHistory_Click(object sender, EventArgs e)
        {
            string index = Convert.ToString(dgvData.CurrentRow.Cells["Index"].Value);
            new L2140_OneOrder_History(index).ShowDialog();
        }

        private void TsmiChangeOrder_Click(object sender, EventArgs e)
        {
            string order_index = Convert.ToString(dgvData.CurrentRow.Cells["Index"].Value);
            new L2100_OneOrder(order_index).ShowDialog();

            if (Program.dbOperations.GetOrderAsync(order_index).PreviousLevel_Index
                < Stack.OrderLevel_SendToCompany)
                dgvData.DataSource = GetData();
        }

        private void TsmiSentToCompany_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("آیا مایل به ارسال سفارش به شرکت می باشید؟"
                + "\n" + "در صورت ارسال سفارش به شرکت ، امکان تغییر سفارش وجود نخواهد داشت"
                , "", MessageBoxButtons.YesNo) == DialogResult.No) return;

            string OrderIndex = Convert.ToString(dgvData.CurrentRow.Cells["Index"].Value);
            Models.Order order = Program.dbOperations.GetOrderAsync(OrderIndex);

            order.PreviousLevel_Index = Stack.OrderLevel_OrderCompleted;
            order.CurrentLevel_Index = Stack.OrderLevel_SendToCompany;
            order.NextLevel_Index = Stack.OrderLevel_SaleConfirmed;
            order.Level_Description = "در انتظار تأیید واحد فروش";
            Program.dbOperations.UpdateOrderAsync(order);
            // ثبت در تاریخچه
            new ThisProject().Create_OrderHistory(order);

            MessageBox.Show("سفارش با موفقیت به شرکت ارسال گردید.");
        }

        private void TsmiOrderSentOut_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("آیا مایل به خروج سفارش از شرکت می باشید؟"
                , "اخطار", MessageBoxButtons.YesNo) == DialogResult.No) return;

            string OrderIndex = Convert.ToString(dgvData.CurrentRow.Cells["Index"].Value);
            Models.Order order = Program.dbOperations.GetOrderAsync(OrderIndex);

            order.PreviousLevel_Index = order.CurrentLevel_Index;
            order.CurrentLevel_Index = Stack.OrderLevel_SentOut;
            order.NextLevel_Index = 0;
            order.Level_Description = "سفارش از کارخانه خارج شده است";
            Program.dbOperations.UpdateOrderAsync(order);
            // ثبت در تاریخچه
            new ThisProject().Create_OrderHistory(order);

            MessageBox.Show("سفارش از شرکت خارج گردید.");
        }

        private void BtnDeleteAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("آیا از حذف تمام سفارشها اطمینان دارید؟"
                , "اخطار", MessageBoxButtons.YesNo) == DialogResult.No) return;

            if (MessageBox.Show("با انجام این عمل تمام اطلاعات سفارشها از بین خواهد رفت"
                +"\n"+ "آیا مطمئن هستید؟"
                , "اخطار", MessageBoxButtons.YesNo) == DialogResult.No) return;

            Program.dbOperations.DeleteAllOrdersAsync();
            Program.dbOperations.DeleteAllOrder_ItemsAsync();
            Program.dbOperations.DeleteAllOrder_Item_PropertiesAsync();
            Program.dbOperations.DeleteAllOrder_HistorysAsync();

            lstOrders.RemoveAll(d=>d.Id>0);
            MessageBox.Show("اطلاعات با موفقیت حذف شد");
            dgvData.DataSource = GetData();
        }

        private void TsmiDelete_Click(object sender, EventArgs e)
        {
            string OrderIndex = Convert.ToString(dgvData.CurrentRow.Cells["Index"].Value);
            Models.Order order = Program.dbOperations.GetOrderAsync(OrderIndex);

            long remove_order_level_index = 0;
            if (Program.dbOperations.GetAllOrder_LevelsAsync(Stack.Company_Index).Any(d => d.RemovingLevel))
                remove_order_level_index = Program.dbOperations.GetAllOrder_LevelsAsync
                    (Stack.Company_Index).First(d => d.RemovingLevel).Index;

            if (order.CurrentLevel_Index == remove_order_level_index)
                MessageBox.Show("سفارش قبلا حذف شده است");
            else
            {
                if (MessageBox.Show("آیا مایل به حذف سفارش می باشید؟"
                    , order.Title, MessageBoxButtons.YesNo) == DialogResult.No) return;

                if (remove_order_level_index > 0)
                    order.CurrentLevel_Index = remove_order_level_index;
                order.Level_Description = "سفارش حذف شده است";
                Program.dbOperations.UpdateOrderAsync(order);

                lstOrders.Remove(lstOrders.First(d => d.Index == order.Index));
                lstOrders.Add(order);
                //dgvData.DataSource = GetData();
                dgvData.DataSource = GetData();// 0, Stack.UserLevel <= Stack.UserLevel_Supervisor3);

                new ThisProject().Create_OrderHistory(order);//, "حذف توسط " + Program.dbOperations.GetUserAsync(Stack.UserIndex).Name);
            }
        }

        private void TsmiCancel_Click(object sender, EventArgs e)
        {
            int irow = dgvData.CurrentRow.Cells["Index"].RowIndex;
            string OrderIndex = Convert.ToString(dgvData.CurrentRow.Cells["Index"].Value);
            Models.Order order = Program.dbOperations.GetOrderAsync(OrderIndex);

            long cancel_order_level_index = 0;
            if (Program.dbOperations.GetAllOrder_LevelsAsync(Stack.Company_Index).Any(d => d.RemovingLevel))
                cancel_order_level_index = Program.dbOperations.GetAllOrder_LevelsAsync
                    (Stack.Company_Index).First(d => d.CancelingLevel).Index;

            if (order.CurrentLevel_Index == cancel_order_level_index)
                MessageBox.Show("سفارش قبلا لغو شده است");
            else
            {
                if (MessageBox.Show("آیا مایل به لغو سفارش می باشید؟"
                    , order.Title, MessageBoxButtons.YesNo) == DialogResult.No) return;

                if (cancel_order_level_index > 0)
                    order.CurrentLevel_Index = cancel_order_level_index;
                order.Level_Description = "سفارش لغو شده است";
                Program.dbOperations.UpdateOrderAsync(order);

                lstOrders.Remove(lstOrders.First(d => d.Index == order.Index));
                lstOrders.Add(order);
                //dgvData.DataSource = GetData();
                dgvData.DataSource = GetData();// 0, Stack.UserLevel <= Stack.UserLevel_Supervisor3);
                dgvData.CurrentCell = dgvData["Title", irow];
                new ThisProject().Create_OrderHistory(order);//, "حذف توسط " + Program.dbOperations.GetUserAsync(Stack.UserIndex).Name);
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtST_OrderTitle.Text)
                && string.IsNullOrWhiteSpace(txtST_CustomerName.Text))
            {
                dgvData.DataSource = GetData();
                return;
            }

            panel1.Enabled = false;
            //dgvData.Visible = false;
            Application.DoEvents();

            //ShowData(false);
            List<Models.Order> lstOrders = (List<Models.Order>)dgvData.DataSource;
            //MessageBox.Show(lstItems.Count.ToString());

            //if (!string.IsNullOrWhiteSpace(txtST_Name.Text)
            //   || !string.IsNullOrWhiteSpace(txtST_SmallCode.Text))
            {
                foreach (Control c in groupBox1.Controls)
                {
                    //MessageBox.Show(c.Text);
                    if (c.Name.Length > 4)
                    {
                        if (c.Name.Substring(0, 5).Equals("txtST"))
                            if (!string.IsNullOrWhiteSpace(c.Text))
                            {
                                lstOrders = SearchThis(lstOrders, c.Name);
                                if ((lstOrders == null) || !lstOrders.Any()) break;
                            }
                    }
                }
            }

            dgvData.DataSource = lstOrders;

            //System.Threading.Thread.Sleep(500);
            Application.DoEvents();
            panel1.Enabled = true;
            //dgvData.Visible = true;

        }

        // جستجوی موردی
        private List<Models.Order> SearchThis(List<Models.Order> lstOrders, string TextBoxName)
        {
            switch (TextBoxName)
            {
                case "txtST_OrderTitle":
                    switch (cmbST_OrderTitle.SelectedIndex)
                    {
                        case 0:
                            return lstOrders.Where(d => d.Title.ToLower().Contains(txtST_OrderTitle.Text.ToLower())).ToList();
                        case 1:
                            return lstOrders.Where(d => d.Title.ToLower().StartsWith(txtST_OrderTitle.Text.ToLower())).ToList();
                        case 2:
                            return lstOrders.Where(d => d.Title.ToLower().Equals(txtST_OrderTitle.Text.ToLower())).ToList();
                        default: return lstOrders;
                    }
                //break;
                case "txtST_CustomerName":
                    switch (cmbST_CustomerName.SelectedIndex)
                    {
                        case 0:
                            return lstOrders.Where(d => d.Customer_Name.ToLower().Contains(txtST_CustomerName.Text.ToLower())).ToList();
                        case 1:
                            return lstOrders.Where(d => d.Customer_Name.ToLower().StartsWith(txtST_CustomerName.Text.ToLower())).ToList();
                        case 2:
                            return lstOrders.Where(d => d.Customer_Name.ToLower().Equals(txtST_CustomerName.Text.ToLower())).ToList();
                        default: return lstOrders;
                    }
            }

            return null;
        }

        // ---
        private void BtnImportOrdersFromExcel_Click(object sender, EventArgs e)
        {
            GetDataFromExcel_OneSheet(Application.StartupPath + @"\_Requirements\Orders.xlsm", "Link");
        }

        // ---
        private void GetDataFromExcel_OneSheet(string ExcelFilePath,string SheetName)
        {
            if (MessageBox.Show(
                "لطفا در فایل اکسل باز شده، و در شیت " + SheetName + " اطلاعات خود را وارد نموده و سپس آنرا ذخیره نمایید."
                + "\n" + "دقت نمایید فایل اکسل نباید ببندید"
                , "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No) return;

            long user_index = Program.dbOperations.GetUserAsync("Senior_Auto").Index;
            long user_level_index = Program.dbOperations.GetAllUser_ULsAsync(Stack.Company_Index, user_index).First().UL_Index;
            //MessageBox.Show("user_index = " + user_index
            //    + "\n" + "user_level_index = " + user_level_index);
            //return;

            panel1.Enabled = false;
            progressBar1.Style = ProgressBarStyle.Marquee;
            progressBar1.Visible = true;
            Application.DoEvents();
            //pictureBox1.Visible = true;

            EXCEL.Application excelApp = new EXCEL.Application();
            excelApp.DisplayAlerts = false;
            excelApp.Visible = true;
            excelApp.WindowState = EXCEL.XlWindowState.xlMaximized;
            EXCEL.Workbook wb = excelApp.Workbooks.Open(ExcelFilePath);
            try
            {
                //MessageBox.Show(openFileDialog1.FileName,"100");
                //wb = excelApp.Workbooks.Open(openFileDialog1.FileName);
                bool bIsBOM_sheetExists = wb.Worksheets.OfType<EXCEL.Worksheet>().Any(ws => ws.Name.ToLower().Equals(SheetName.ToLower()));
                //MessageBox.Show("", "200");
                if (bIsBOM_sheetExists)
                {
                    //progressBar1.Maximum = 303;
                    EXCEL.Worksheet ws = wb.Worksheets[SheetName];
                    ws.Activate();
                    if (MessageBox.Show("آیا اطلاعات وارد شوند؟", "", MessageBoxButtons.YesNo)
                        == DialogResult.Yes)
                    {
                        #region بررسی اینکه آیا کاربر فایل اکسل را بسته است یا خیر. در صورت بسته بودن، آنرا باز میکند
                        try
                        {
                            ws = wb.Worksheets[SheetName];
                        }
                        catch
                        {
                            excelApp = new EXCEL.Application();
                            excelApp.DisplayAlerts = false;
                            excelApp.WindowState = EXCEL.XlWindowState.xlMaximized;
                            wb = excelApp.Workbooks.Open(ExcelFilePath);
                            ws = wb.Worksheets[SheetName];
                        }
                        excelApp.Visible = false;
                        #endregion

                        #region Get OrdersData
                        // تعداد سطرها
                        int n = 1;
                        while ((ws.Cells[n, 3].Value != null) && ws.Cells[n, 4].Value != null) n++;
                        // ستونها
                        int m = 7;
                        while (ws.Cells[1, m].Value != null) m++;

                        progressBar1.Style = ProgressBarStyle.Blocks;
                        progressBar1.Maximum = m;// * n;
                        progressBar1.Value = 0;

                        #region خریدار
                        if (!Program.dbOperations.GetAllCustomersAsync(Stack.Company_Index).Any(d=>d.Name.Contains("جورکش")))
                        {
                            Program.dbOperations.AddCustomer(new Models.Customer
                            {
                                Company_Index = Stack.Company_Index,
                                Index = user_index + Stack_Methods.DateTimeNow_Shamsi("/",":",true),
                                User_Index = user_index,
                                Name = "آقای جورکش",
                            });
                        }
                        Models.Customer customer = Program.dbOperations.GetAllCustomersAsync(Stack.Company_Index).FirstOrDefault(d => d.Name.Contains("جورکش"));
                        #endregion

                        int j = 6;  // شمارنده ستوها
                        while (++j < m)
                        {
                            Models.Order order = null;
                            string title = Convert.ToString(ws.Cells[1, j].Value);
                            //MessageBox.Show(title + "\nj= " + j + "\ncustomer= " + customer.Name, "100");
                            // اگر سفارشی با این عنوان وجود نداشت، آنرا اضافه کن
                            if (!Program.dbOperations.GetAllOrders(user_index).Any(d => d.Title.Equals(title)))
                            {
                                //MessageBox.Show(title,"200");
                                #region اضافه کردن سفارش جدید
                                order = new Models.Order
                                {
                                    Company_Index = Stack.Company_Index,
                                    Index = user_index + Stack_Methods.DateTimeNow_Shamsi("/", ":", true),
                                    Title = title,
                                    User_Index = user_index,
                                    Customer_Index = customer.Index,
                                    Customer_Name = customer.Name,
                                    DateTime_mi = DateTime.Now,
                                    Date_sh = Stack_Methods.Miladi_to_Shamsi_YYYYMMDD(DateTime.Now),
                                    Time = Stack_Methods.NowTime_HHMMSSFFF().Substring(0, 8),
                                    PreviousLevel_Index = 12,
                                    CurrentLevel_Index = 13,
                                    Level_Description = "در حال تولید",
                                    NextLevel_Index = Stack.OrderLevel_Producting,
                                };
                                Program.dbOperations.AddOrder(order);
                                //Application.DoEvents();
                                new ThisProject().Create_OrderHistory(order,null,user_index,user_level_index);//, "در حال تولید");
                                // ثبت جدول مراحل گذرانده سفارش
                                Program.dbOperations.AddOrder_OLAsync(new Models.Order_OL
                                {
                                    Company_Index = Stack.Company_Index,
                                    Order_Index = order.Index,
                                    OrderLevel_Index = 13,
                                });

                                Application.DoEvents();

                                //continue;
                                //order = Program.dbOperations.GetAllOrdersAsync(user_index).First(d => d.Title.Equals(title));
                                #endregion

                                int i = 1;
                                while (++i < n)
                                {
                                    //i++;
                                    #region بررسی وضعیت سلولهای یک ردیف و شرط پایان حلقه
                                    bool b3 = ws.Cells[i, 3].Value != null;
                                    bool b4 = ws.Cells[i, 4].Value != null;

                                    if (b3) b3 = ws.Cells[i, 3].Value.ToString().Length > 0;
                                    if (b4) b4 = ws.Cells[i, 4].Value.ToString().Length > 0;

                                    bool bStop = !b3 || !b4;
                                    if (bStop) break;

                                    bool bi = ws.Cells[i, j].Value != null;
                                    if (bi) bi = ws.Cells[i, j].Value.ToString().Length > 0;
                                    if (!bi) continue;
                                    #endregion

                                    #region اضافه کردن کالا و تعداد آن به کالاهای سفارش
                                    double quantity = Convert.ToDouble(ws.Cells[i, j].Value);
                                    if (quantity <= 0) continue;

                                    string item_Small_code = null;
                                    item_Small_code = Convert.ToString(ws.Cells[i, 3].Value);
                                    Models.Item item = Program.dbOperations.GetItem(item_Small_code, true);
                                    if (item != null)
                                    {
                                        Models.Order_Item order_item = new Models.Order_Item
                                        {
                                            Company_Index = Stack.Company_Index,
                                            Order_Index = order.Index,
                                            Item_Index = item.Index,
                                            Item_Name_Samll = item.Name_Samll,
                                            Item_SmallCode = item.Code_Small,
                                            Item_Module = item.Module,
                                            Quantity = quantity,
                                            
                                        };

                                        Program.dbOperations.AddOrder_Item(order_item);
                                    }
                                    #endregion
                                }

                                if (progressBar1.Value < progressBar1.Maximum)
                                    progressBar1.Value++;
                                Application.DoEvents();

                            }
                        }
                        #endregion
                    }

                }
                else
                {
                    MessageBox.Show("شیت " + SheetName + " یافت نشد!", "خطا");
                }
            }
            catch
            {
                //if (wb == null) wb = excelApp.Workbooks.Add();
            }
            finally
            {
                try
                {
                    wb.Close(SaveChanges: false);
                    excelApp.Workbooks.Close();
                    excelApp.Quit();

                    while (Marshal.ReleaseComObject(wb) != 0) { }
                    while (Marshal.ReleaseComObject(excelApp.Workbooks) != 0) { }
                    while (Marshal.ReleaseComObject(excelApp) != 0) { }
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    Application.DoEvents();

                    dgvData.DataSource = GetData();// user_index);
                    //panel1.Enabled = true;
                }
                catch { }
            }

            panel1.Enabled = true;
            progressBar1.Visible = false;
            //pictureBox1.Visible = false;

        }

        private void BtnReturn_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void TsmiChangeOrderLevel_Click(object sender, EventArgs e)
        {
            string order_index = Convert.ToString(dgvData.CurrentRow.Cells["Index"].Value);
            Models.Order order = Program.dbOperations.GetOrderAsync(order_index);

            new L2150_OneOrder_Change_OrderLevel(order_index).ShowDialog();

            Models.Order order1 = Program.dbOperations.GetOrderAsync(order_index);
            // در صورت به وجود آمدن تغییری در سفارش ، جدول را بروز کن
            if ((order.CurrentLevel_Index != order1.CurrentLevel_Index)
                || (order.PreviousLevel_Index != order1.PreviousLevel_Index))
            {
                lstOrders = new List<Models.Order>();
                dgvData.DataSource = GetData();
            }
        }

        private void TsmiWarehouseChecklist_Click(object sender, EventArgs e)
        {
            string OrderIndex = Convert.ToString(dgvData.CurrentRow.Cells["Index"].Value);
            new M2200_Order_StockItems(OrderIndex).ShowDialog();
        }







    }
}
