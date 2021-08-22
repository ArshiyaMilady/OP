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
    public partial class L0900_Orders_and_Details : X210_ExampleForm_Normal
    {
        public L0900_Orders_and_Details()
        {
            InitializeComponent();
        }

        private void L0900_Orders_and_Details_Shown(object sender, EventArgs e)
        {
            cmbST_CustomerName.SelectedIndex = 0;
            cmbST_OrderTitle.SelectedIndex = 0;

            dgvData.DataSource = GetData();
            //ShowData();

            Application.DoEvents();
            panel1.Enabled = true;
        }

        private List<Models.Order> GetData(long user_index = 0, bool bShowWhichOrders = false)
        {
            return Program.dbOperations.GetAllOrdersAsync(Stack.Company_Index).ToList();
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
                            //col.Width = 50;
                            break;
                        case "Customer_Name":
                            col.HeaderText = "نام خریدار";
                            //col.DefaultCellStyle.BackColor = Color.LightGray;
                            break;
                        case "Date_sh":
                            col.HeaderText = "تاریخ ثبت";
                            //col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                            break;
                        case "Level_Description":
                            col.HeaderText = "وضعیت سفارش";
                            //col.ReadOnly = true;
                            //col.DefaultCellStyle.BackColor = Color.LightGray;
                            //col.Width = 50;
                            break;
                        default: col.Visible = false; break;
                    }
                }
            }
            #endregion
        }

        int iX = 0, iY = 0;
        private void dgvData_MouseDown(object sender, MouseEventArgs e)
        {
            iX = e.X;
            iY = e.Y;
        }

        private void dgvData_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            return;

            if (e.ColumnIndex < 0 || e.RowIndex < 0) return;
            dgvData.CurrentCell = dgvData["Title", e.RowIndex];

            if (e.Button == MouseButtons.Right)
            {
                /////// Do something ///////
                string order_index = Convert.ToString(dgvData.CurrentRow.Cells["Index"].Value);
                Models.Order order = Program.dbOperations.GetOrderAsync(order_index);

                tsmiChangeOrder.Visible = order.CurrentLevel_Index < Stack.OrderLevel_SendToCompany;
                tsmiSentToCompany.Visible = order.CurrentLevel_Index == Stack.OrderLevel_OrderCompleted;
                tsmiWarehouseChecklist.Visible = (Stack.UserLevel_Index <= Stack.UserLevel_Supervisor3)
                    && Program.dbOperations.GetAllOrder_StockItems(order_index).Any();

                // انتخاب سلولی که روی آن کلیک راست شده است
                dgvData.CurrentCell = dgvData[e.ColumnIndex, e.RowIndex];
                contextMenuStrip1.Show(dgvData, new Point(iX, iY));
            }
        }

        private void DgvData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            //TsmiOrderDetails_Click(null, null);
        }

        private void TsmiOrderDetails_Click(object sender, EventArgs e)
        {
            //string order_index = Convert.ToString(dgvData.CurrentRow.Cells["Index"].Value);
            //new L2120_OneOrder_Items(order_index, true).ShowDialog();
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
                + "\n" + "آیا مطمئن هستید؟"
                , "اخطار", MessageBoxButtons.YesNo) == DialogResult.No) return;

            Program.dbOperations.DeleteAllOrdersAsync();
            Program.dbOperations.DeleteAllOrder_ItemsAsync();
            Program.dbOperations.DeleteAllOrder_Item_PropertiesAsync();
            Program.dbOperations.DeleteAllOrder_HistorysAsync();

            MessageBox.Show("اطلاعات با موفقیت حذف شد");
            dgvData.DataSource = GetData();
        }

        private void TsmiDelete_Click(object sender, EventArgs e)
        {

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

        private void BtnReturn_Click(object sender, EventArgs e)
        {
            Close();
        }


    }
}
