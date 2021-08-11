using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EXCEL = Microsoft.Office.Interop.Excel;

namespace OrdersProgress
{
    public partial class M2100_OrdersPriority : X210_ExampleForm_Normal
    {
        public M2100_OrdersPriority()
        {
            InitializeComponent();
        }

        private void M2100_OrdersPriority_Shown(object sender, EventArgs e)
        {
            cmbWarehouses.Items.Add("تمام انبارها");
            cmbWarehouses.Items.AddRange(Program.dbOperations.GetAllWarehousesAsync(Stack.Company_Index, true).Select(d => d.Name).ToArray());
            cmbWarehouses.SelectedIndex = 0;

            cmbST_OrderTitle.SelectedIndex = 0;

            dgvData.DataSource = GetData(true);
            ShowData();

            Application.DoEvents();
            progressBar1.Visible = false;
            panel1.Enabled = true;
        }

        // ReadOnlyBased = true : تغییر رنگ ستونها با توجه به فقط خواندنی بودن آنها تغییر میکند
        private void ShowData(bool ReadOnlyBased = true)
        {
            #region ترجمه سر ستونها و مخفی کردن بعضی ستونها
            foreach (DataGridViewColumn col in dgvData.Columns)
            {
                switch (col.Name)
                {
                    case "Order_Title":
                        col.HeaderText = "عنوان سفارش";
                        col.ReadOnly = true;
                        //col.DefaultCellStyle.BackColor = Color.LightGray;
                        col.Width = 200;
                        break;
                    case "Priority":
                        col.HeaderText = "اولویت";
                        col.Width = 75;
                        break;
                    case "IsCompleted":
                        col.HeaderText = "تکمیل؟";
                        col.ReadOnly = true;
                        //col.DefaultCellStyle.BackColor = Color.LightGray;
                        col.Width = 50;
                        break;
                    case "TotalQuantity":
                        col.HeaderText = "تعداد کل کالاها در سفارش";
                        col.ReadOnly = true;
                        //col.DefaultCellStyle.BackColor = Color.LightGray;
                        col.Width = 100;
                        break;
                    case "CanTakeQuantity":
                        col.HeaderText = "تعداد قابل ارسال از انبار";
                        col.Width = 100;
                        col.ReadOnly = true;
                        break;
                    case "ProgressPercent":
                        col.HeaderText = "درصد تکمیل";
                        col.ReadOnly = true;
                        //col.DefaultCellStyle.BackColor = Color.LightGray;
                        col.Width = 100;
                        break;
                    case "RemainedQuantity":
                        col.HeaderText = "کسری";
                        col.ReadOnly = true;
                        //col.DefaultCellStyle.BackColor = Color.LightGray;
                        col.Width = 100;
                        break;
                    default: col.Visible = false; break;
                }

                if (ReadOnlyBased)
                {
                    if (col.ReadOnly)
                        col.DefaultCellStyle.BackColor = Color.WhiteSmoke;
                    else
                        col.DefaultCellStyle.BackColor = Color.White;
                }
                else col.DefaultCellStyle.BackColor = Color.WhiteSmoke;

            }
            #endregion
        }

        private List<Models.OrderPriority> GetData(bool bInitialSetting = false, bool JustUnzeroPriority = false)
        {
            if (bInitialSetting)
            {
                if (Program.dbOperations.GetAllOrderPrioritysAsync(Stack.Company_Index).Any())
                {
                    // حذف سفارشهای ارسال شده یا برگشت شده یا کنسل شده
                    foreach (Models.OrderPriority op in Program.dbOperations.GetAllOrderPrioritysAsync(Stack.Company_Index))
                    {
                        bool bRemove = (Program.dbOperations.GetOrderAsync(op.Order_Index)
                            .CurrentLevel_Index >= Stack.OrderLevel_SentOut)
                                || (Program.dbOperations.GetOrderAsync(op.Order_Index)
                                .CurrentLevel_Index < Stack.OrderLevel_SendToCompany);
                        // حذف سفارشهایی که از کارخانه خارج شده اند
                        if (bRemove) Program.dbOperations.DeleteOrderPriorityAsync(op);

                        Application.DoEvents();
                    }

                    // اضافه کردن سفارشهای جدید
                    foreach (Models.Order order in Program.dbOperations.GetAllOrdersAsync(Stack.Company_Index)
                        .Where(d => (d.CurrentLevel_Index >= Stack.OrderLevel_SendToCompany)
                            && (d.CurrentLevel_Index < Stack.OrderLevel_SentOut)).ToList())
                    {
                        AddOrderPriority(order);
                        Application.DoEvents();
                    }
                }
                else
                {
                    // اضافه کردن سفارشها
                    foreach (Models.Order order in Program.dbOperations.GetAllOrdersAsync(Stack.Company_Index)
                        .Where(d => (d.CurrentLevel_Index >= Stack.OrderLevel_SendToCompany)
                            && (d.CurrentLevel_Index < Stack.OrderLevel_SentOut)).ToList())
                    {
                        Program.dbOperations.AddOrderPriorityAsync(new Models.OrderPriority
                        {
                            Company_Index = Stack.Company_Index,
                            Order_Index = order.Index,
                            Order_Title = order.Title,
                            TotalQuantity = 0,// Program.dbOperations.GetAllOrder_ItemsAsync(order.Index).Sum(d => d.Quantity),
                        });
                        Application.DoEvents();
                    }
                    SetDefaultPriorities();
                }

            }

            if(JustUnzeroPriority)
                return Program.dbOperations.GetAllOrderPrioritysAsync(Stack.Company_Index).Where(d=>d.Priority>0).OrderBy(d => d.Priority).ThenBy(j => j.Index).ToList();
            else
                return Program.dbOperations.GetAllOrderPrioritysAsync(Stack.Company_Index).OrderBy(d => d.Priority).ThenBy(j => j.Index).ToList();
        }

        private void AddOrderPriority(Models.Order order)
        {
            if (Program.dbOperations.GetOrderPriorityAsync(order.Index) == null)
            {
                double dTotalQuantity = 0;
                double dCanTakeQuantity = 0;
                double dRemainedQuantity = 0;
                if (Program.dbOperations.GetAllOrder_StockItemsAsync(Stack.Company_Index, order.Index).Any())
                {
                    dTotalQuantity = Program.dbOperations.GetAllOrder_StockItemsAsync(Stack.Company_Index, order.Index).Sum(d => d.Quantity);
                    dCanTakeQuantity = Program.dbOperations.GetAllOrder_StockItemsAsync(Stack.Company_Index, order.Index).Sum(d => d.Quantity_CanTake);
                    dRemainedQuantity = Program.dbOperations.GetAllOrder_StockItemsAsync(Stack.Company_Index, order.Index).Sum(d => d.Quantity_Remained);
                }

                int iProgressPercent = 0;
                if (dTotalQuantity > 0)
                    iProgressPercent = (int)(100 * dCanTakeQuantity / dTotalQuantity);
                Program.dbOperations.AddOrderPriorityAsync(new Models.OrderPriority
                {
                    Company_Index = Stack.Company_Index,
                    Order_Index = order.Index,
                    Order_Title = order.Title,
                    TotalQuantity = dTotalQuantity,
                    CanTakeQuantity = dCanTakeQuantity,
                    RemainedQuantity = dRemainedQuantity,
                    ProgressPercent = iProgressPercent,
                });

            }

        }

        // بروز رسانی اطلاعات مربوط به تعداد ها و پیشرفت
        private void UpdateOrderPriority(string order_index)
        {
            if (Program.dbOperations.GetOrderPriorityAsync(order_index) != null)
            {
                if (Program.dbOperations.GetAllOrder_StockItemsAsync(Stack.Company_Index, order_index).Any())
                {
                    double dTotalQuantity = 0;
                    double dCanTakeQuantity = 0;
                    double dRemainedQuantity = 0;
                    dTotalQuantity = Program.dbOperations.GetAllOrder_StockItemsAsync(Stack.Company_Index, order_index).Sum(d => d.Quantity);
                    dCanTakeQuantity = Program.dbOperations.GetAllOrder_StockItemsAsync(Stack.Company_Index, order_index).Sum(d => d.Quantity_CanTake);
                    dRemainedQuantity = Program.dbOperations.GetAllOrder_StockItemsAsync(Stack.Company_Index, order_index).Sum(d => d.Quantity_Remained);

                    int iProgressPercent = 0;
                    if (dTotalQuantity > 0)
                        iProgressPercent = (int)(100 * dCanTakeQuantity / dTotalQuantity);

                    Models.OrderPriority order_priority = Program.dbOperations.GetOrderPriorityAsync(order_index);
                    order_priority.TotalQuantity = dTotalQuantity;
                    order_priority.CanTakeQuantity = dCanTakeQuantity;
                    order_priority.RemainedQuantity = dRemainedQuantity;
                    order_priority.ProgressPercent = iProgressPercent;
                    order_priority.IsCompleted = order_priority.CanTakeQuantity == order_priority.TotalQuantity;

                    Program.dbOperations.UpdateOrderPriorityAsync(order_priority);
                }
            }
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
            dgvData.CurrentCell = dgvData["Order_Title", e.RowIndex];

            if (e.Button == MouseButtons.Right)
            {
                /////// Do something ///////
                string OrderIndex = Convert.ToString(dgvData.CurrentRow.Cells["Order_Index"].Value);
                tsmiShow_StockItems.Visible = Program.dbOperations.GetAllOrder_StockItemsAsync(Stack.Company_Index, OrderIndex).Any();

                // انتخاب سلولی که روی آن کلیک راست شده است
                dgvData.CurrentCell = dgvData[e.ColumnIndex, e.RowIndex];
                contextMenuStrip1.Show(dgvData, new Point(iX, iY));
            }
        }

        private void TsmiShow_StockItems_Click(object sender, EventArgs e)
        {
            string OrderIndex = Convert.ToString(dgvData.CurrentRow.Cells["Order_Index"].Value);
            new M2200_Order_StockItems(OrderIndex).ShowDialog();
        }

        private void BtnDeleteAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("آیا از حذف تمام موارد اطمینان دارید؟"
               , "اخطار", MessageBoxButtons.YesNo) == DialogResult.No) return;

            Program.dbOperations.DeleteAllOrderPrioritysAsync();
            Program.dbOperations.DeleteAllOrders_StockItemsAsync();
            dgvData.DataSource = GetData();
        }

        private void BtnSetDefaultPriorities_Click(object sender, EventArgs e)
        {
            SetDefaultPriorities();
            dgvData.DataSource = GetData();
        }

        private void SetDefaultPriorities()
        {
            if(!Program.dbOperations.GetAllOrderPrioritysAsync(Stack.Company_Index).Any())
            {
                MessageBox.Show("سفارشی جهت تعیین اولویت وجود ندارد");
                return;
            }

            #region تنظیم اولویتها
            //int min_priority = Program.dbOperations.GetAllOrderPrioritysAsync(Stack.Company_Index).Max(d=>d.Priority);
            //if (min_priority == 0) min_priority = 10;
            int priority = 0;

            // اول آنهایی که قبلا اولویت داشته اند
            foreach (Models.OrderPriority op in Program.dbOperations.GetAllOrderPrioritysAsync(Stack.Company_Index)
                .Where(d=>d.Priority > 0).OrderBy(d => d.Priority).ThenBy(j => j.Index).ToList())
            {
                priority += 10;
                op.Priority = priority;
                Program.dbOperations.UpdateOrderPriorityAsync(op);

                Application.DoEvents();
            }
            // سپس آنهایی که دارای اولویت صفر هستند
            foreach (Models.OrderPriority op in Program.dbOperations.GetAllOrderPrioritysAsync(Stack.Company_Index)
                .Where(d=>d.Priority == 0).OrderBy(d => d.Priority).ThenBy(j => j.Index).ToList())
            {
                priority += 10;
                op.Priority = priority;
                Program.dbOperations.UpdateOrderPriorityAsync(op);

                Application.DoEvents();
            }
            #endregion
        }

        private void BtnShowAll_Click(object sender, EventArgs e)
        {
            dgvData.DataSource = GetData();
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtST_OrderTitle.Text))
            {
                //ShowData(false);
                return;
            }

            panel1.Enabled = false;
            //dgvData.Visible = false;
            Application.DoEvents();

            //ShowData(false);
            List<Models.OrderPriority> lstOP = (List<Models.OrderPriority>)dgvData.DataSource;
            //MessageBox.Show(lstItems.Count.ToString());

            if (!string.IsNullOrWhiteSpace(txtST_OrderTitle.Text))
            {
                foreach (Control c in groupBox1.Controls)
                {
                    //MessageBox.Show(c.Text);
                    if (c.Name.Length > 4)
                    {
                        if (c.Name.Substring(0, 5).Equals("txtST"))
                            if (!string.IsNullOrWhiteSpace(c.Text))
                            {
                                lstOP = SearchThis(lstOP, c.Name);
                                if ((lstOP == null) || !lstOP.Any()) break;
                            }
                    }
                }
            }

            dgvData.DataSource = lstOP;

            //System.Threading.Thread.Sleep(500);
            Application.DoEvents();
            panel1.Enabled = true;
            //dgvData.Visible = true;

        }

        // جستجوی موردی
        private List<Models.OrderPriority> SearchThis(List<Models.OrderPriority> lstOP1, string TextBoxName)
        {
            switch (TextBoxName)
            {
                case "txtST_OrderTitle":
                    switch (cmbST_OrderTitle.SelectedIndex)
                    {
                        case 0:
                            return lstOP1.Where(d => d.Order_Title.ToLower().Contains(txtST_OrderTitle.Text.ToLower())).ToList();
                        case 1:
                            return lstOP1.Where(d => d.Order_Title.ToLower().StartsWith(txtST_OrderTitle.Text.ToLower())).ToList();
                        case 2:
                            return lstOP1.Where(d => d.Order_Title.ToLower().Equals(txtST_OrderTitle.Text.ToLower())).ToList();
                        default: return lstOP1;
                    }
            }

            return null;
        }

        private void ChkCanEdit_CheckedChanged(object sender, EventArgs e)
        {
            dgvData.ReadOnly = !chkCanEdit.Checked;
            ShowData(chkCanEdit.Checked);   // آیا رنگ ستونها با توجه به تغییر وضعیت فقط خواندنی بودن آنها تغییر کند؟

            chkShowUpdateMessage.Enabled = chkCanEdit.Checked;
        }

        private void tsmiDoChangesForOrder_Click(object sender, EventArgs e)
        {

        }

        private void DgvData_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // Don't throw an exception when we're done.
            e.ThrowException = false;
            if (dgvData.Columns[dgvData.CurrentCell.ColumnIndex].Name.Equals("Priority"))
            {
                MessageBox.Show("لطفا «اولویت» را به صورت عدد وارد نمایید.", "خطای نوع داده", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (dgvData.Columns[dgvData.CurrentCell.ColumnIndex].Name.Equals("CanTakeQuantity"))
            {
                MessageBox.Show("لطفا «تعداد قابل برداشت» را به صورت عدد وارد نمایید.", "خطای نوع داده", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("خطای نوع داده", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // If this is true, then the user is trapped in this cell.
            e.Cancel = false;
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

            Models.OrderPriority order_priority = Program.dbOperations.GetOrderPriorityAsync(index);
            switch (dgvData.Columns[e.ColumnIndex].Name)
            {
                case "Priority":
                    order_priority.Priority = Convert.ToInt32(dgvData["Priority", e.RowIndex].Value);
                    break;
                case "CanTakeQuantity":
                    order_priority.CanTakeQuantity = Convert.ToDouble(dgvData["CanTakeQuantity", e.RowIndex].Value);
                    break;
            }

            if (chkCanEdit.Checked)
            {
                if (chkShowUpdateMessage.Checked)
                {
                    bSaveChange = MessageBox.Show("آیا از ثبت تغییرات اطمینان دارید؟"
                        , "", MessageBoxButtons.YesNo) == DialogResult.Yes;
                }
            }

            if (bSaveChange)
            {
                panel1.Enabled = false;
                Application.DoEvents();
                Program.dbOperations.UpdateOrderPriorityAsync(order_priority);
                SetDefaultPriorities();
                dgvData.DataSource = GetData();
                Application.DoEvents();
                panel1.Enabled = true;
            }
            else dgvData[e.ColumnIndex, e.RowIndex].Value = InitailValue;
        }

        // اعمال اولویتهای پیشنهادی سیستم در برداشتن کالاها از انبار
        private void BtnSystemPrioritiesRecommended_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("بسته به تعداد سفارشات و تعداد کالاها ، این عمل ممکن چندین دقیقه به طول انجامد."
                + "آیا مایل به شروع عستید؟","",MessageBoxButtons.OKCancel) != DialogResult.OK) return;

            panel1.Enabled = false;
            progressBar1.Maximum = 3 * GetData().Count();
            progressBar1.Value = 0;
            progressBar1.Style = ProgressBarStyle.Blocks;
            progressBar1.Visible = true;
            Application.DoEvents();

            warehouse_index = (cmbWarehouses.SelectedIndex > 0)
                ? Program.dbOperations.GetWarehouseAsync(cmbWarehouses.Text).Index : 0;

            #region 10 : حذف اطلاعات قبلی
            if (chkCreateOrder_StockItems.Checked)
            {
                foreach (Models.OrderPriority op in GetData())
                {
                    if (Program.dbOperations.GetAllOrder_StockItemsAsync(Stack.Company_Index, op.Order_Index).Any())
                        Program.dbOperations.DeleteOrder_StockItems(Stack.Company_Index, op.Order_Index);
                        //foreach (Models.Order_StockItem osi in Program.dbOperations.GetAllOrder_StockItemsAsync(op.Order_Index))
                        //    Program.dbOperations.DeleteOrder_StockItem(osi);

                    op.TotalQuantity = 0;
                    op.CanTakeQuantity = 0;
                    op.RemainedQuantity = 0;
                    op.ProgressPercent = 0;
                    Program.dbOperations.UpdateOrderPriorityAsync(op);

                    if (progressBar1.Value < progressBar1.Maximum)
                        progressBar1.Value++;
                    Application.DoEvents();
                }
            }
            else progressBar1.Value = progressBar1.Maximum / 3;
            #endregion

            #region 20 : تهیه لیست کالاهای قابل برداشت از انبار

            foreach (Models.OrderPriority op in GetData())
            {
                if (chkCreateOrder_StockItems.Checked || !Program.dbOperations.GetAllOrder_StockItemsAsync(Stack.Company_Index, op.Order_Index).Any())
                    CreateOrders_StockItems(op);

                if (progressBar1.Value < progressBar1.Maximum)
                    progressBar1.Value++;
                Application.DoEvents();
            }

            //CreateOrders_StockItems(GetData().First());
            #endregion

            #region 30 : محاسبه مقدار پیشرفت برای هر سفارش، بر اساس اولویتهای آن
            // تمام کالاهای انبار که دارای مقداری بیش از صفر می باشند
            List<Models.Warehouse_Inventory> lstWI = Program.dbOperations
                .GetAllWarehouse_InventorysAsync(Stack.Company_Index,warehouse_index)
                .Where(d => d.Quantity_Real > 0).ToList();

            // ارزش دهی مقدار کاذب از هر کالا درانبار
            foreach (Models.Warehouse_Inventory wi in lstWI)
                wi.Quantity_x = wi.Quantity_Real;
            Program.dbOperations.UpdateSomeWarehouse_Inventory(lstWI);
            //new M1100_StockInventory().ShowDialog();

            List<Models.OrderPriority> lstOP = GetData();
            // صفر کردن تمام درصد پیشرفتها
            if (lstOP.Any(d => d.Priority > 0 || d.ProgressPercent > 0))
            {
                foreach (Models.OrderPriority op1 in lstOP)
                {
                    op1.Priority = 0;
                    op1.ProgressPercent = 0;
                    Program.dbOperations.UpdateOrderPriority(op1);
                }
                //Program.dbOperations.UpdateOrdersPriorities(lstOP);
            }


            lstWI = Program.dbOperations.GetAllWarehouse_Inventories(warehouse_index);
              //.Where(d => d.Quantity_x > 0).ToList();

            int priority = 10;
            for (int i = 0; i < GetData().Count; i++)
            {
                //lstOP = GetData().Where(d => d.ProgressPercent == 0).ToList();

                #region تعیین تعداد قابل ارسال و کسری برای هر سفارش 
                foreach (Models.OrderPriority op in lstOP)
                {
                    foreach (Models.Order_StockItem osi in Program.dbOperations.GetAllOrder_StockItems(op.Order_Index))
                    {
                        //osi.Quantity_CanTake = 0;
                        //osi.Quantity_Remained = 0;

                        if (lstWI.Any(d => d.Item_Code.Equals(osi.Item_SmallCode)))
                            Program.dbOperations.UpdateOrder_StockItem(
                                GetOrder_StockItem_from_Warehouse(lstWI.First(d
                                => d.Item_Code.Equals(osi.Item_SmallCode)), osi,false));
                    }
                }
                #endregion

                #region پیدا کردن و ثبت بیشترین درصد پیشرفت
                Models.OrderPriority op_by_max_progress = null;
                foreach (Models.OrderPriority op in lstOP)
                {
                    if (op_by_max_progress == null)
                    {
                        op_by_max_progress = op;
                        op_by_max_progress.TotalQuantity = Program.dbOperations.GetAllOrder_StockItems(op.Order_Index).Sum(d => d.Quantity);
                        op_by_max_progress.CanTakeQuantity = Program.dbOperations.GetAllOrder_StockItems(op.Order_Index).Sum(d => d.Quantity_CanTake);
                        op_by_max_progress.RemainedQuantity = Program.dbOperations.GetAllOrder_StockItems(op.Order_Index).Sum(d => d.Quantity_Remained);
                        if (op_by_max_progress.TotalQuantity > 0)
                            op_by_max_progress.ProgressPercent = (int)(100 * op_by_max_progress.CanTakeQuantity / op_by_max_progress.TotalQuantity);
                    }
                    else
                    {
                        op.TotalQuantity = Program.dbOperations.GetAllOrder_StockItems(op.Order_Index).Sum(d => d.Quantity);
                        op.CanTakeQuantity = Program.dbOperations.GetAllOrder_StockItems(op.Order_Index).Sum(d => d.Quantity_CanTake);
                        op.RemainedQuantity = Program.dbOperations.GetAllOrder_StockItems(op.Order_Index).Sum(d => d.Quantity_Remained);

                        int iProgressPercent = 0;
                        if (op.TotalQuantity > 0)
                            iProgressPercent = (int)(100 * op.CanTakeQuantity / op.TotalQuantity);

                        if (iProgressPercent > op_by_max_progress.ProgressPercent)
                        {
                            op_by_max_progress = op;
                            op_by_max_progress.ProgressPercent = iProgressPercent;
                        }
                    }
                }

                if (op_by_max_progress != null)
                {
                    if (op_by_max_progress.ProgressPercent == 0) break;

                    op_by_max_progress.Priority = priority;
                    priority += 10;
                    Program.dbOperations.UpdateOrderPriority(op_by_max_progress);
                    //MessageBox.Show(op_by_max_progress.Order_Title, op_by_max_progress.Priority.ToString());
                    // تغییرات در موجودی غیر اصلی انبار
                    foreach (Models.Order_StockItem osi in Program.dbOperations
                        .GetAllOrder_StockItems(op_by_max_progress.Order_Index))
                    {
                        if (lstWI.Any(d => d.Item_Code.Equals(osi.Item_SmallCode)))
                            Program.dbOperations.UpdateOrder_StockItem(
                                GetOrder_StockItem_from_Warehouse(lstWI.First(d
                                => d.Item_Code.Equals(osi.Item_SmallCode)), osi));
                    }

                    lstWI = Program.dbOperations.GetAllWarehouse_Inventories(Stack.Company_Index); //warehouse_index)
                        //.Where(d => d.Quantity_x > 0).ToList();

                    //new M1100_StockInventory().ShowDialog();

                    lstOP.Remove(op_by_max_progress);
                }
                #endregion

                if (progressBar1.Value < progressBar1.Maximum)
                    progressBar1.Value++;
                Application.DoEvents();
            }
            #endregion


            if (GetData().Any(d => d.Priority == 0))
                SetDefaultPriorities();
            dgvData.DataSource = GetData();

            Application.DoEvents();
            progressBar1.Visible = false;
            panel1.Enabled = true;
        }

        int warehouse_index = 0;
        // اعمال اولویتهای کاربر در برداشتن کالاها از انبار
        private void BtnDoUserPriorities_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("بر اساس تعداد سفارشات و تعداد کالاها ، این عمل ممکن است چندین دقیقه به طول انجامد."
                + "آیا مایل به شروع هستید؟", "", MessageBoxButtons.OKCancel) != DialogResult.OK) return;

            if (dgvData.Rows.Count != GetData().Count)
                dgvData.DataSource = GetData();

            panel1.Enabled = false;
            progressBar1.Maximum = 3 * GetData().Count();
            progressBar1.Value = 0;
            progressBar1.Style = ProgressBarStyle.Blocks;
            progressBar1.Visible = true;
            Application.DoEvents();

            warehouse_index = (cmbWarehouses.SelectedIndex > 0)
                ? Program.dbOperations.GetWarehouseAsync(cmbWarehouses.Text).Index : 0;

            #region 10 : حذف اطلاعات قبلی
            if (chkCreateOrder_StockItems.Checked)
            {
                foreach (Models.OrderPriority op in GetData())
                {
                    if (Program.dbOperations.GetAllOrder_StockItemsAsync(Stack.Company_Index, op.Order_Index).Any())
                        Program.dbOperations.DeleteOrder_StockItems(Stack.Company_Index, op.Order_Index);

                    if (progressBar1.Value < progressBar1.Maximum)
                        progressBar1.Value++;
                    Application.DoEvents();
                }
            }
            else progressBar1.Value = progressBar1.Maximum / 3;
            #endregion

            #region 20 : تهیه لیست کالاهای قابل برداشت از انبار

            foreach (Models.OrderPriority op in GetData())
            {
                if (chkCreateOrder_StockItems.Checked || !Program.dbOperations.GetAllOrder_StockItemsAsync(Stack.Company_Index, op.Order_Index).Any())
                    CreateOrders_StockItems(op);

                if (progressBar1.Value < progressBar1.Maximum)
                    progressBar1.Value++;
                Application.DoEvents();
            }

            //CreateOrders_StockItems(GetData().First());
            #endregion

            #region 30 : محاسبه مقدار پیشرفت برای هر سفارش، بر اساس اولویتهای آن
            // تمام کالاهای انبار که دارای مقداری بیش از صفر می باشند
            List<Models.Warehouse_Inventory> lstWI = Program.dbOperations
                .GetAllWarehouse_InventorysAsync(warehouse_index);
                //.Where(d => d.Quantity_Real > 0).ToList();
            // ارزش دهی مقدار کاذب از هر کالا
            foreach (Models.Warehouse_Inventory wi in lstWI)
            {
                wi.Quantity_x = wi.Quantity_Real;
                Program.dbOperations.UpdateWarehouse_InventoryAsync(wi);
            }
            //Program.dbOperations.UpdateAllWarehouse_InventoryAsync(lstWI);

            foreach (Models.OrderPriority op in GetData())
            {
                lstWI = Program.dbOperations
                    .GetAllWarehouse_InventorysAsync(warehouse_index);
                    //.Where(d => d.Quantity_x > 0).ToList();
                foreach (Models.Order_StockItem osi in Program.dbOperations.GetAllOrder_StockItemsAsync(Stack.Company_Index, op.Order_Index))
                {
                    if (lstWI.Any(d => d.Item_Code.Equals(osi.Item_SmallCode)))
                        Program.dbOperations.UpdateOrder_StockItem(
                            GetOrder_StockItem_from_Warehouse(lstWI.First
                            (d => d.Item_Code.Equals(osi.Item_SmallCode)), osi));
                }

                if (progressBar1.Value < progressBar1.Maximum)
                    progressBar1.Value++;
                Application.DoEvents();
            }
            #endregion

            #region 40 : بروز رسانی جدول اولویتهای سفارشات با توجه به کالاهای تولیدی
            foreach (Models.OrderPriority op in GetData())
            {
                UpdateOrderPriority(op.Order_Index);
                //List<Models.Order_StockItem> lstOSI = Program.dbOperations.GetAllOrder_StockItemsAsync(op.Order_Index);
                //op.TotalQuantity = lstOSI.Sum(d => d.Quantity);
                //op.CanTakeQuantity = lstOSI.Sum(d => d.Quantity_CanTake);
                //op.RemainedQuantity = lstOSI.Sum(d => d.Quantity_Remained);
                //Program.dbOperations.UpdateOrderPriorityAsync(op);
            }

            #endregion

            dgvData.DataSource = GetData();

            Application.DoEvents();
            progressBar1.Visible = false;
            panel1.Enabled = true;

        }

        // تعداد لازم را از انبار (درصورت وجود) بر می دارد و تغییرات را ذخیره می کند
        // DoChange_in_Warehouse = true : تغییرات در انبار (در متغیر غیر اصلی) ذخیره گردد
        private Models.Order_StockItem GetOrder_StockItem_from_Warehouse
            (Models.Warehouse_Inventory wi,  Models.Order_StockItem osi, bool DoChange_in_Warehouse=true)
        {
            double wi_quantity_x = wi.Quantity_x;
            if (wi_quantity_x > 0)
            {
                // اگر تعداد کالا در انبار بیشتر یا مساوی تعداد کالای درخواستی باشد
                if (wi_quantity_x >= osi.Quantity)
                {
                    wi_quantity_x = wi_quantity_x - osi.Quantity;
                    osi.Quantity_CanTake = osi.Quantity;
                    osi.Quantity_Remained = 0;
                }
                else
                {
                    osi.Quantity_CanTake = wi_quantity_x;
                    osi.Quantity_Remained = osi.Quantity - wi_quantity_x;
                    wi_quantity_x = 0;
                }

                if (DoChange_in_Warehouse)
                {
                    //Models.Warehouse_Inventory wi1 = Program.dbOperations.GetWarehouse_InventoryAsync(wi.Index);
                    wi.Quantity_x = wi_quantity_x;
                    Program.dbOperations.UpdateWarehouse_Inventory(wi);
                }
            }
            else
            {
                osi.Quantity_CanTake = 0;
                osi.Quantity_Remained = osi.Quantity;
            }

            //Program.dbOperations.UpdateOrder_StockItem(osi);
            return osi;
        }

        // چه کالاهایی از سفارش در انبار است؟ ممکن است بعضی ماژول ها به صورت مستقیم در انبار
        // نباشند. پس لازم است زیرساخت آنها مورد بررسی قرار بگیرند
        private void CreateOrders_StockItems(Models.OrderPriority op)
        {
            if (!Program.dbOperations.GetAllOrder_StockItemsAsync(Stack.Company_Index, op.Order_Index).Any())
            {
                foreach (Models.Order_Item oi in Program.dbOperations.GetAllOrder_ItemsAsync(Stack.Company_Index, op.Order_Index))
                    AddOrdersItem_to_OrderStockItems(oi.Item_SmallCode, oi.Order_Index, oi.Quantity);
            }
        }

        private void BtnReturn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AddOrdersItem_to_OrderStockItems(string item_code, string order_index, double quantity,string top_code=null) // Models.Order_Item oi1)
        {
            bool bIsItemDefined_in_Stock = false;   // آیا کالا در انبار موجود است
            bIsItemDefined_in_Stock = Program.dbOperations.GetAllWarehouse_InventorysAsync(Stack.Company_Index)
                .Any(d => d.Item_Code.Equals(item_code));

            int warehouse_index = (cmbWarehouses.SelectedIndex > 0)
                ? Program.dbOperations.GetWarehouseAsync(cmbWarehouses.Text).Index : 0;

            if (bIsItemDefined_in_Stock)
            {
                Models.Item item = Program.dbOperations.GetItem(item_code, true);

                #region ماژول بالایی این کالا
                string top_name = null;
                try
                {
                    if (string.IsNullOrEmpty(top_code))
                    {
                        top_code = item.Code_Small;
                        top_name = item.Name_Samll;
                    }
                    else
                        top_name = Program.dbOperations.GetItem(top_code, true).Name_Samll;
                }
                catch { }
                #endregion

                if (item != null)
                {
                    Program.dbOperations.AddOrder_StockItem(new Models.Order_StockItem
                    {
                        Order_Index = order_index,
                        Item_Index = item.Index,
                        Item_SmallCode = item.Code_Small,
                        Item_Name_Samll = item.Name_Samll,
                        WarehouseIndex = warehouse_index,
                        Quantity = quantity,
                        Quantity_Remained = quantity,
                        Top_Code=top_code,
                        Top_Name=top_name,
                    });
                }
            }
            else
            {
                List<Relation_by_Level> lstRL = new ThisProject().AllSubRelations(item_code);
                if (lstRL.Any(d => d.Level == 1))
                {
                    foreach (Relation_by_Level rl in lstRL.Where(d => d.Level == 1).ToList())
                        AddOrdersItem_to_OrderStockItems(rl.Code, order_index, rl.Quantity * quantity,item_code);
                }
            }
        }



        private void BtnPdf_from_All_Click(object sender, EventArgs e)
        {
            panel1.Enabled = false;
            Application.DoEvents();

            List<Models.OrderPriority> lstOP = (List<Models.OrderPriority>)dgvData.DataSource;

            #region اصلا نیازی به انجام کاری می باشد؟
            int nOrders_with_StockItems = 0;
            foreach (Models.OrderPriority op in lstOP)
            {
                if (Program.dbOperations.GetAllOrder_StockItems(op.Order_Index).Any())
                    nOrders_with_StockItems++;
            }

            if (nOrders_with_StockItems == 0)
            {
                MessageBox.Show("لطفا ابتدا اولویتها را بر روی سفارشها اعمال نمایید", "خطا");
                panel1.Enabled = true;
                return;
            }
            else if (nOrders_with_StockItems != lstOP.Count)
            {
                MessageBox.Show("بعضی سفارشها دارای لیست اقلام جهت ارسال نمی باشند.لطفا ابتدا اولویتها را بر روی سفارشها اعمال نمایید", "خطا");
                panel1.Enabled = true;
                return;
            }
            #endregion

            progressBar1.Value = 0;
            progressBar1.Maximum = dgvData.Rows.Count;
            progressBar1.Style = ProgressBarStyle.Blocks;
            progressBar1.Visible = true;
            Application.DoEvents();

            // فولدر برای خروجی پی دی اف ها
            var dir_info = Directory.CreateDirectory(Application.StartupPath + @"\PDF\" + Stack_Methods.DateTimeNow_Shamsi("", "").Substring(0, 15));

            EXCEL.Application excelApp = new EXCEL.Application();
            excelApp.Visible = false;
            EXCEL._Workbook wb = null;

            foreach (Models.OrderPriority op in lstOP)
            {
                //MessageBox.Show(op.Order_Title);

                if (Program.dbOperations.GetAllOrder_StockItems(op.Order_Index).Any())
                {
                    //DataGridView dgv2 = new DataGridView();
                    dgv2.DataSource = Program.dbOperations.GetAllOrder_StockItems(op.Order_Index);
                    for (int i = 1; i <= dgv2.Rows.Count; i++) dgv2["C_I1", i - 1].Value = i;
                    ShowData2();
                    //MessageBox.Show(Program.dbOperations.GetAllOrder_StockItems(op.Order_Index).Count.ToString());

                    // تهیه نام برای فایل خروجی پی دی اف
                    string PdfFileName = dir_info.FullName + @"\" + op.Order_Title + ".pdf";
                    //MessageBox.Show(PdfFileName);

                    wb = excelApp.Workbooks.Open(Application.StartupPath + @"\_Requirements\Empty_Forms\Shop_CheckList_1.xltm");

                    DGV_to_PDF_for_ThisProject(dgv2, PdfFileName, wb.Worksheets["Sheet1"], 6, op.Order_Title);//,6,op.Order_Title);

                    Application.DoEvents();
                    wb.Close(SaveChanges: false);
                }

                if(progressBar1.Value<progressBar1.Maximum)
                    progressBar1.Value++;
                Application.DoEvents();
            }

            excelApp.Quit();
            if(wb!=null) while (Marshal.ReleaseComObject(wb) != 0) { }
            while (Marshal.ReleaseComObject(excelApp.Workbooks) != 0) { }
            while (Marshal.ReleaseComObject(excelApp) != 0) { }
            GC.Collect();
            GC.WaitForPendingFinalizers();

            Application.DoEvents();
            progressBar1.Visible = false;
            panel1.Enabled = true;
        }

        private void ShowData2(bool ReadOnlyBased = true)
        {
            #region ترجمه سر ستونها و مخفی کردن بعضی ستونها
            foreach (DataGridViewColumn col in dgv2.Columns)
            {
                switch (col.Name)
                {
                    case "C_I1":
                        col.HeaderText = "ردیف";
                        col.Width = 40;
                        break;
                    //case "C_B1":
                    //    col.HeaderText = "انتخاب";
                    //    col.Width = 60;
                    //    break;
                    case "Top_Code":
                        col.HeaderText = "کد ماژول";
                        col.ReadOnly = true; //  Stack.UserLevel > Stack.UserLevel_Supervisor1;
                        col.Width = 60;
                        break;
                    case "Top_Name":
                        col.HeaderText = "نام ماژول";
                        col.ReadOnly = true; //  Stack.UserLevel > Stack.UserLevel_Supervisor1;
                        col.Width = 120;
                        break;
                    case "Item_SmallCode":
                        col.HeaderText = "ریز کد";// "کد کالا";
                        col.ReadOnly = true; //  Stack.UserLevel > Stack.UserLevel_Supervisor1;
                        col.Width = 100;
                        break;
                    case "Item_Name_Samll":
                        col.HeaderText = "نام قطعه";
                        col.ReadOnly = true; // Stack.UserLevel > Stack.UserLevel_Supervisor3;
                        col.Width = 200;
                        break;
                    case "Quantity":
                        col.HeaderText = "تعداد قطعه";
                        col.ReadOnly = true; // Stack.UserLevel > Stack.UserLevel_Supervisor1;
                        col.Width = 100;
                        break;
                    case "Quantity_CanTake":
                        col.HeaderText = "تعداد قطعات قابل ارسال";
                        //col.ReadOnly = true;// Stack.UserLevel > Stack.UserLevel_Supervisor3;
                        col.Width = 120;
                        break;
                    case "C_S1":
                        col.HeaderText = "تعداد قطعات ارسالی";
                        col.ReadOnly = true; // Stack.UserLevel > Stack.UserLevel_Supervisor1;
                        col.Width = 80;
                        break;

                    default: col.Visible = false; break;
                }

                if (ReadOnlyBased)
                {
                    if (col.ReadOnly)
                        col.DefaultCellStyle.BackColor = Color.WhiteSmoke;
                    else
                        col.DefaultCellStyle.BackColor = Color.White;
                }
                else col.DefaultCellStyle.BackColor = Color.WhiteSmoke;


                //if (col.ReadOnly) col.DefaultCellStyle.BackColor = Color.WhiteSmoke;
                //else col.DefaultCellStyle.BackColor = Color.White;
            }
            #endregion
        }

        private void BtnSaveForAllOrders_Click(object sender, EventArgs e)
        {

        }

        public bool DGV_to_PDF_for_ThisProject(DataGridView dgv, string sPdfFileName
            , EXCEL.Worksheet ws, int base_empty_column = -1, string sRightHeader = null)
        {
            if (dgv.Rows.Count < 1) return false;
            if (dgv.Columns.Count < 1) return false;

            int ciFirstExcelRow = 1;
            //try
            {
                // ثبت عنوان سر ستونها
                int iExcelColumn = 1;
                int iExcelRow = ciFirstExcelRow;

                //bool bIsItFirstColumn = true;

                for (int j = 0; j < dgv.Columns.Count; j++)
                {
                    if (!dgv.Columns[j].Visible) continue;

                    iExcelRow = ciFirstExcelRow;
                    ws.Cells[1, iExcelColumn].Value = dgv.Columns[j].HeaderText;
                    for (int i = 0; i < dgv.Rows.Count; i++)
                    {
                        //try 
                        {
                            if (dgv.Rows[i].Visible)
                            {
                                iExcelRow++;
                                if (dgv[j, i].Value != null)
                                {
                                    try
                                    { ws.Cells[iExcelRow, iExcelColumn].Value = dgv[j, i].Value; }
                                    catch { }
                                }
                                //Color back_color = dgv[j, i].InheritedStyle.BackColor;
                                //if (back_color.Name.ToLower().Equals("window")) back_color = Color.White;
                                //ws.Cells[iExcelRow, iExcelColumn].Interior.Color = back_color;

                                //if (MessageBox.Show(dgv[j, i].InheritedStyle.BackColor.Name,"",MessageBoxButtons.OKCancel)
                                //    == DialogResult.Cancel) return false;

                            }
                        }
                        //catch { continue; }
                    }
                    iExcelColumn++;
                }

                ws.Columns.AutoFit();
                ws.Rows[RowIndex: 1].Font.Bold = true;
                ws.Rows[RowIndex: ciFirstExcelRow].Font.Bold = true;
                if (iExcelColumn > 1)
                {
                    ws.Range[ws.Cells[ciFirstExcelRow, 1], ws
                        .Cells[iExcelRow, iExcelColumn - 1]].Borders.LineStyle
                        = EXCEL.XlLineStyle.xlContinuous;

                    ws.Range[ws.Cells[ciFirstExcelRow, 1], ws
                       .Cells[iExcelRow, iExcelColumn - 1]].ShrinkToFit = false;
                }

                new ThisProject().Merge_in_ThisProject(ws, 2, dgv.Rows.Count);
                new ThisProject().Merge_in_ThisProject(ws, 3, dgv.Rows.Count);

                // حذف سطرهای خالی
                if (base_empty_column > 0)
                {
                    while (ws.Cells[iExcelRow + 1, base_empty_column].Value == null)
                        ws.Rows[iExcelRow + 1].delete();
                }

                if (!string.IsNullOrEmpty(sRightHeader))
                    ws.PageSetup.RightHeader ="        "+ sRightHeader;

                //ws.PageSetup.PaperSize = EXCEL.XlPaperSize.xlPaperA4;
                //ws.PageSetup.Zoom = 80;//Page setup when printing, zoom ratio
                //ws.PageSetup.FitToPagesWide = 1;
                ////ws.PageSetup.TopMargin = 0.2; //The top margin is 0
                ////ws.PageSetup.BottomMargin = 0.2; //The bottom margin is 0
                //ws.PageSetup.LeftMargin = 0.4; //The left margin is 0
                //ws.PageSetup.RightMargin = 0.4; //The right margin is 0
                //ws.PageSetup.CenterHorizontally = true;//Horizontal center

                ws.ExportAsFixedFormat(EXCEL.XlFixedFormatType.xlTypePDF
                    , sPdfFileName);//.Substring(0,sPdfFileName.Length-5) + ".pdf");

                return true;
            }

        }

    }
}
