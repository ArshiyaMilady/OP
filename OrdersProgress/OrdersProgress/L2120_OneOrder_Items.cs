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
    public partial class L2120_OneOrder_Items : X210_ExampleForm_Normal
    {
        string OrderIndex = null;
        bool bOrderReadOnly = false;

        public L2120_OneOrder_Items(string _OrderIndex, bool _bOrderReadOnly = false)
        {
            InitializeComponent();

            OrderIndex = _OrderIndex;
            bOrderReadOnly = _bOrderReadOnly;

            Text = Program.dbOperations.GetOrderAsync(OrderIndex).Title;
        }

        private void L2120_OneOrder_Items_Shown(object sender, EventArgs e)
        {
            cmbST_Name.SelectedIndex = 0;
            cmbST_SmallCode.SelectedIndex = 0;

            Models.Order order = Program.dbOperations.GetOrderAsync(OrderIndex);
            Models.Order_Level ol = Program.dbOperations.GetOrder_LevelAsync(order.CurrentLevel_Index);
            btnSave.Text = ol.MessageText;
            btnSave.Visible = ol.OrderCanChange;

            dgvData.DataSource = GetData();
            ShowData();
        }

        // NeedToCorrect_C_B1 : آیا نیاز به بروز رسانی پارامتر «سی-بی 1» می باشد؟
        private List<Models.Order_Item> GetData()//bool NeedToCorrect_C_B1 = true)
        {
            return Program.dbOperations.GetAllOrder_ItemsAsync(Stack.Company_Index,OrderIndex);
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
                        case "Item_SmallCode":
                            col.HeaderText = "کد";
                            break;
                        case "Item_Name_Samll":
                            col.HeaderText = "نام کالا";
                            break;
                        case "Quantity":
                            col.HeaderText = "تعداد";
                            break;
                        case "SalesPrice":
                            col.HeaderText = "قیمت واحد (ریال)";
                            break;
                        default: col.Visible = false; break;
                    }
                }
            }
            #endregion
        }

        private void TxtST_Name_Enter(object sender, EventArgs e)
        {
            AcceptButton = btnSearch;
        }

        private void TxtST_Name_Leave(object sender, EventArgs e)
        {
            AcceptButton = null;
        }

        private void BtnShowAll_Click(object sender, EventArgs e)
        {
            dgvData.DataSource = GetData();
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtST_Name.Text)
               && string.IsNullOrWhiteSpace(txtST_SmallCode.Text))
                return;


            panel1.Enabled = false;
            Application.DoEvents();

            //ShowData(false);
            List<Models.Order_Item> lstOI = GetData();//bOrderReadOnly, false);// (List<Models.Item>)dgvData.DataSource;
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
                                lstOI = SearchThis(lstOI, c.Name);
                                if ((lstOI == null) || !lstOI.Any()) break;
                            }
                    }
                }
            }

            dgvData.DataSource = lstOI;

            //System.Threading.Thread.Sleep(500);
            Application.DoEvents();
            panel1.Enabled = true;
            //dgvData.Visible = true;

        }

        // جستجوی موردی
        private List<Models.Order_Item> SearchThis(List<Models.Order_Item> lstOI1, string TextBoxName)
        {
            switch (TextBoxName)
            {
                case "txtST_SmallCode":
                    switch (cmbST_SmallCode.SelectedIndex)
                    {
                        case 0:
                            return lstOI1.Where(d => d.Item_SmallCode.ToLower().Contains(txtST_SmallCode.Text.ToLower())).ToList();
                        case 1:
                            return lstOI1.Where(d => d.Item_SmallCode.ToLower().StartsWith(txtST_SmallCode.Text.ToLower())).ToList();
                        case 2:
                            return lstOI1.Where(d => d.Item_SmallCode.ToLower().Equals(txtST_SmallCode.Text.ToLower())).ToList();
                        default: return lstOI1;
                    }
                //break;
                case "txtST_Name":
                    switch (cmbST_Name.SelectedIndex)
                    {
                        case 0:
                            return lstOI1.Where(d => d.Item_Name_Samll.ToLower().Contains(txtST_Name.Text.ToLower())).ToList();
                        case 1:
                            return lstOI1.Where(d => d.Item_Name_Samll.ToLower().StartsWith(txtST_Name.Text.ToLower())).ToList();
                        case 2:
                            return lstOI1.Where(d => d.Item_Name_Samll.ToLower().Equals(txtST_Name.Text.ToLower())).ToList();
                        default: return lstOI1;
                    }
            }

            return null;
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
                try
                {
                    string item_code = Convert.ToString(dgvData.CurrentRow.Cells["Item_SmallCode"].Value);
                    tsmiItemProperties.Visible = Program.dbOperations.GetAllItem_PropertiesAsync(Stack.Company_Index, item_code).Any();
                }
                catch { }
                
                // انتخاب سلولی که روی آن کلیک راست شده است
                dgvData.CurrentCell = dgvData[e.ColumnIndex, e.RowIndex];
                contextMenuStrip1.Show(dgvData, new Point(iX, iY));
            }
        }

        private void DgvData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            TsmiItemProperties_Click(null, null);
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            Models.Order order = Program.dbOperations.GetOrderAsync(OrderIndex);
            ThisProject this_project = new ThisProject();
            Models.Order_Level order_level = Program.dbOperations.GetOrder_LevelAsync(this_project.Next_OrderLevel_Indexes(order.Index).First());
            if (order.CurrentLevel_Index != order_level.Index)
            {
                if (MessageBox.Show("آیا از  " + order_level.MessageText + " اطمینان دارید؟"
                    , "", MessageBoxButtons.YesNo) != DialogResult.Yes) return;

                // در انتظار ارسال سفارش به واحد
                order.PreviousLevel_Index = order.CurrentLevel_Index;
                order.CurrentLevel_Index = this_project.Next_OrderLevel_Indexes(order.Index).First();
                //order.NextLevel_Index = Stack.OrderLevel_SendToCompany;
                order.Level_Description = Program.dbOperations.GetOrder_LevelAsync(order.CurrentLevel_Index).Description2;
                Program.dbOperations.UpdateOrderAsync(order);
                // ثبت در تاریخچه و مراحل گذرانده
                this_project.Create_OrderHistory(order);
                this_project.AddOrder_OrderLevel(order);

                if (!btnSave.Text.Equals(Program.dbOperations.GetOrder_LevelAsync(order.CurrentLevel_Index).MessageText))
                    btnSave.Text = Program.dbOperations.GetOrder_LevelAsync(order.CurrentLevel_Index).MessageText;
            }

            //if (MessageBox.Show("سفارش با موفقیت ثبت گردید."
            //    + "\n" + "آیا مایل به ارسال سفارش به شرکت می باشید؟"
            //    + "\n" + "در صورت ارسال سفارش به شرکت ، امکان تغییر سفارش وجود نخواهد داشت"
            //    ,"",MessageBoxButtons.YesNo)
            //    == DialogResult.No) return;

            //order.PreviousLevel_Index = order.CurrentLevel_Index;
            //order.CurrentLevel_Index = this_project.Next_OrderLevel_Indexes(order.Index).First();
            //order.NextLevel_Index = Stack.OrderLevel_SaleConfirmed;
            ////order.Level_Description = "در انتظار تأیید واحد فروش";
            //Program.dbOperations.UpdateOrderAsync(order);
            //// ثبت در تاریخچه
            //new ThisProject().Create_OrderHistory(order);
            //this_project.AddOrder_OrderLevel(order);

            MessageBox.Show(order_level.MessageText + " با موفقیت انجام گردید");
        }

        private void TxtST_Enter(object sender, EventArgs e)
        {
            AcceptButton = btnSearch;
        }

        private void TxtST_Leave(object sender, EventArgs e)
        {
            AcceptButton = null;
        }

        private void TsmiItemProperties_Click(object sender, EventArgs e)
        {
            long ItemIndex = Convert.ToInt64(dgvData.CurrentRow.Cells["Item_Index"].Value);
            if (Program.dbOperations.GetAllOrder_Item_PropertiesAsync(OrderIndex, ItemIndex).Any())
            {
                Models.Order_Item oi = Program.dbOperations.GetOrder_ItemAsync(OrderIndex, ItemIndex);
                //MessageBox.Show(oi.Item_SmallCode);
                new L2130_OneOrder_Item_Properties(oi, bOrderReadOnly).ShowDialog();
            }
            else
                MessageBox.Show("برای این کالا مشخصه ای تعریف نشده است");
        }
    }
}
