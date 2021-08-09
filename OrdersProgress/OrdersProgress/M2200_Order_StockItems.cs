using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrdersProgress
{
    public partial class M2200_Order_StockItems : X210_ExampleForm_Normal
    {
        readonly string OrderIndex;

        public M2200_Order_StockItems(string _OrderIndex)
        {
            InitializeComponent();

            OrderIndex = _OrderIndex;
            Text = Text + Program.dbOperations.GetOrderAsync(OrderIndex).Title;

            btnSave.Visible = Stack.UserLevel < Stack.UserLevel_Supervisor2;
        }

        private void M2200_Order_StockItems_Shown(object sender, EventArgs e)
        {
            cmbSort.SelectedIndex = 0;

            dgvData.DataSource = GetData();
            ShowData();
        }

        private List<Models.Order_StockItem> GetData()
        {
            return Program.dbOperations.GetAllOrder_StockItemsAsync(Stack.Company_Index, OrderIndex);
        }

        private void ShowData(bool ReadOnlyBased = true)
        {
            #region ترجمه سر ستونها و مخفی کردن بعضی ستونها
            foreach (DataGridViewColumn col in dgvData.Columns)
            {
                switch (col.Name)
                {
                    case "C_B1":
                        col.HeaderText = "انتخاب";
                        col.Width = 60;
                        break;
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
                        col.HeaderText = "تعداد قطعات ارسالی";
                        //col.ReadOnly = true;// Stack.UserLevel > Stack.UserLevel_Supervisor3;
                        col.Width = 100;
                        break;
                    //case "Quantity_Remained":
                    //    col.HeaderText = "کسری";
                    //    col.ReadOnly = true; // Stack.UserLevel > Stack.UserLevel_Supervisor1;
                    //    col.Width = 100;
                    //    break;

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

        //List<Models.Order_StockItem> lstOSI = new List<Models.Order_StockItem>();
        // تمام قطعاتی از سفارش که کد آنها در انبار تعریف شده باشد را بر میگرداند
        private void GetOrder_StockItems(string order_item_code, double quantity)
        {
            // اگر کالا در انبار تعریف شده باشد
            if (Program.dbOperations.GetWarehouse_InventoryAsync(order_item_code) != null)
            {
                Models.Item it = Program.dbOperations.GetItemAsync(order_item_code, true);
                Models.Order_StockItem osi = new Models.Order_StockItem
                {
                    Order_Index = OrderIndex,
                    Item_Index = it.Index,
                    Item_Name_Samll = it.Name_Samll,
                    Item_SmallCode = it.Code_Small,
                    Quantity = quantity,
                    Quantity_Remained = quantity,
                };
                Program.dbOperations.AddOrder_StockItem(osi);
            }
            else
            {
                // پیدا کردن کالا در جدول کالاها
                Models.Item item = Program.dbOperations.GetItemAsync(order_item_code, true);
                if (item.Module)
                {
                    foreach (Models.Module item1 in Program.dbOperations.GetAllModulesAsync(Stack.Company_Index,1, order_item_code))
                    {
                        GetOrder_StockItems(item1.Item_Code_Small, quantity * item1.Quantity);
                    }
                }
            }
        }

        private void BtnDeleteAll_Click(object sender, EventArgs e)
        {
            foreach (Models.Order_StockItem osi in Program.dbOperations.GetAllOrder_StockItemsAsync(Stack.Company_Index, OrderIndex))
                Program.dbOperations.DeleteOrder_StockItemAsync(osi);
        }

        private void ChkCanEdit_CheckedChanged(object sender, EventArgs e)
        {
            //dgvData.ReadOnly = !chkCanEdit.Checked;
            //ShowData(chkCanEdit.Checked);   // آیا رنگ ستونها با توجه به تغییر وضعیت فقط خواندنی بودن آنها تغییر کند؟

            //chkShowUpdateMessage.Enabled = chkCanEdit.Checked;
        }

        //object InitailValue = null;
        private void DgvData_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            //if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            //InitailValue = dgvData[e.ColumnIndex, e.RowIndex].Value;//.ToString();
        }

        private void DgvData_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            //if (dgvData[e.ColumnIndex, e.RowIndex].Value == InitailValue) return;
            //MessageBox.Show("1");
            switch (dgvData.Columns[e.ColumnIndex].Name)
            {
                //case "C_B1":
                //    bool bC_B1 = Convert.ToBoolean(dgvData["C_B1", e.RowIndex].Value);
                //    dgvData.CurrentRow.Cells["Quantity_CanTake"].ReadOnly = !bC_B1;
                //    if (!bC_B1) dgvData.CurrentRow.Cells["Quantity_CanTake"].Style.BackColor = Color.WhiteSmoke;
                //    else dgvData.CurrentRow.Cells["Quantity_CanTake"].Style.BackColor = Color.White;
                //    break;
                case "Quantity_CanTake":
                    double dQ = Convert.ToDouble(dgvData["Quantity", e.RowIndex].Value);
                    double dQuantity_CanTake = Convert.ToDouble(dgvData["Quantity_CanTake", e.RowIndex].Value);
                    if (dQuantity_CanTake > dQ) dgvData["Quantity_CanTake", e.RowIndex].Value = dQ;

                    //dgvData.CurrentRow.Cells["Quantity_CanTake"].ReadOnly = !bC_B1;
                    //if (!bC_B1) dgvData.CurrentRow.Cells["Quantity_CanTake"].Style.BackColor = Color.WhiteSmoke;
                    //else dgvData.CurrentRow.Cells["Quantity_CanTake"].Style.BackColor = Color.White;
                    break;
            }


        }

        private void DgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            //if (dgvData[e.ColumnIndex, e.RowIndex].Value == InitailValue) return;
            //MessageBox.Show("1");
            switch (dgvData.Columns[e.ColumnIndex].Name)
            {
                case "Quantity_CanTake":
                    bool bC_B1 = Convert.ToBoolean(dgvData["C_B1", e.RowIndex].Value);
                    dgvData.CurrentRow.Cells["Quantity_CanTake"].ReadOnly = !bC_B1;
                    break;
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("با تأیید این عمل ، مقدار کالاهای ارسالی از موجودی انبار کسر خواهد شد."
                + "\n" + "آیا مایل به ثبت اطلاعات می باشید؟", "اخطار", MessageBoxButtons.YesNo)
                != DialogResult.Yes) return;

            // ...
        }

        private void BtnExportToExcel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("از چک لیست نمایش داده شده، خروجی گرفته شود؟"
                , "", MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            panel1.Enabled = false;

            dgvData.Columns["C_B1"].Visible = false;

            for (int i = 1; i <= dgvData.Rows.Count; i++)
                dgvData["C_I1", i - 1].Value = i;
            dgvData.Columns["C_I1"].HeaderText = "ردیف";
            dgvData.Columns["C_I1"].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            dgvData.Columns["C_I1"].Width = 50;
            dgvData.Columns["C_I1"].Visible = true;

            progressBar1.Value = 0;
            progressBar1.Maximum = dgvData.Columns.Cast<DataGridViewColumn>().Where(d => d.Visible).Count();
            progressBar1.Visible = true;
            Application.DoEvents();

            Models.Order order = Program.dbOperations.GetOrderAsync(OrderIndex);
            #region تهیه نام برای فایل خروجی اکسل
            string ExcelFileName = Application.StartupPath + @"\_Requirements\Shop_CheckList\Shop_CheckList_" + order.Title +".xlsx";
            if(File.Exists(ExcelFileName))
                ExcelFileName = Application.StartupPath + @"\_Requirements\Shop_CheckList\Shop_CheckList_"
                    + order.Title +"_"+ Stack_Methods.DateTimeNow_Shamsi("","")+ ".xlsx";

            //MessageBox.Show(ExcelFileName);
            #endregion

            if (ShowType == 1)
            {
                File.Copy(Application.StartupPath + @"\_Requirements\Empty_Forms\Shop_CheckList_1.xlsx", ExcelFileName);
                Application.DoEvents();
                new ThisProject().DGV_to_Excel_for_ThisProject(dgvData, ExcelFileName, null, progressBar1, true,6,order.Title);
            }
            else if (ShowType == 2)
            {
                File.Copy(Application.StartupPath + @"\_Requirements\Empty_Forms\Shop_CheckList_2.xlsx", ExcelFileName);
                Application.DoEvents();
                new ThisProject().DGV_to_Excel_for_ThisProject(dgvData, ExcelFileName, null, progressBar1, false,4, order.Title);
            }


            dgvData.Columns["C_B1"].Visible = true;
            dgvData.Columns["C_I1"].HeaderText = null;
            dgvData.Columns["C_I1"].Visible = false;

            progressBar1.Visible = false;
            panel1.Enabled = true;
            Application.DoEvents();
        }

        private void BtnReturn_Click(object sender, EventArgs e)
        {
            Close();
        }


        byte ShowType = 1;  // نمایش به صورت تفکیکی است
        private void BtnAccumulativeCodes_Click(object sender, EventArgs e)
        {
            panel1.Enabled = false;
            Application.DoEvents();

            if (ShowType == 1)  // باید به صورت تجمیعی شود
            {
                cmbSort.Enabled = false;
                // عدم امکان ثبت اطلاعات زیرا معلوم نیست از هر ماژول چند تا زیرقطعه ارسال می شود
                btnSave.Enabled = false;    

                progressBar1.Value = 0;
                progressBar1.Maximum = Program.dbOperations.GetAllOrder_StockItems(OrderIndex)
                    .Select(d => d.Item_SmallCode).ToList().Count;
                progressBar1.Visible = true;

                List<Models.Order_StockItem> lstOSI = new List<Models.Order_StockItem>();
                foreach (string osi_code in Program.dbOperations.GetAllOrder_StockItems(OrderIndex)
                    .Select(d => d.Item_SmallCode).ToList())
                {
                    lstOSI.Add(new Models.Order_StockItem
                    {
                        Item_SmallCode = osi_code,
                        Item_Name_Samll = Program.dbOperations.GetItem(osi_code, true).Name_Samll,
                        Quantity = Program.dbOperations.GetAllOrder_StockItems(OrderIndex)
                            .Where(d => d.Item_SmallCode.Equals(osi_code)).Sum(j => j.Quantity),
                        Quantity_CanTake = Program.dbOperations.GetAllOrder_StockItems(OrderIndex)
                            .Where(d => d.Item_SmallCode.Equals(osi_code)).Sum(j => j.Quantity_CanTake),
                    });
                    progressBar1.Value++;
                    Application.DoEvents();
                }

                progressBar1.Visible = false;
                //dgvData.Columns["C_B1"].Visible = false;
                dgvData.Columns["Top_Code"].Visible = false;
                dgvData.Columns["Top_Name"].Visible = false;
                dgvData.DataSource = lstOSI;

                ShowType = 2;
                btnAccumulativeCodes.Text = "نمایش تفکیکی";
                toolTip1.SetToolTip(btnAccumulativeCodes, "تعداد هر قطعه بر اساس تعداد ماژول سفارش شده، نمایش داده می شود");
            }
            else    // باید به صورت تفکیکی شود
            {
                cmbSort.Enabled = true;
                btnSave.Enabled = true;

                dgvData.DataSource = Program.dbOperations.GetAllOrder_StockItems(OrderIndex);
                //dgvData.Columns["C_B1"].Visible = false;
                dgvData.Columns["Top_Code"].Visible = true;
                dgvData.Columns["Top_Name"].Visible = true;

                ShowType = 1;
                btnAccumulativeCodes.Text = "نمایش تجمیعی";
                toolTip1.SetToolTip(btnAccumulativeCodes, "حالتی که کد هر قطعه فقط یکبار در لیست می آید");
            }

            Application.DoEvents();
            panel1.Enabled = true;
        }

        private void CmbSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                List<Models.Order_StockItem> lstOSI = (List<Models.Order_StockItem>)dgvData.DataSource;

                switch (cmbSort.SelectedIndex)
                {
                    case 0:
                        dgvData.DataSource = lstOSI.OrderBy(d => d.Index).ToList();
                        break;
                    case 1:
                        if (ShowType == 1)
                            dgvData.DataSource = lstOSI.OrderBy(d => d.Top_Code).ToList();
                        break;
                    case 2:
                        dgvData.DataSource = lstOSI.OrderBy(d => d.Item_SmallCode).ToList();
                        break;
                }
            }
            catch { }
        }

    }
}
