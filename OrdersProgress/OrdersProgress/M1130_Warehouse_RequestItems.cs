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
    public partial class M1130_Warehouse_RequestItems : X210_ExampleForm_Normal
    {
        List<Models.Warehouse_Request> lstRequests_need_confirmation = new List<Models.Warehouse_Request>();
        List<Models.Warehouse_Request> lstRequests_mine = new List<Models.Warehouse_Request>();
        List<Models.Warehouse_Request> lstRequests_confirmed = new List<Models.Warehouse_Request>();

        public M1130_Warehouse_RequestItems()
        {
            InitializeComponent();
        }

        private void M1130_Warehouse_RequestItems_Load(object sender, EventArgs e)
        {
            if (!backgroundWorker1.IsBusy)
                backgroundWorker1.RunWorkerAsync();

            // اگر کاربر، ادمین ، کاربر ارشد یا سرپرست واحد باشد
            panel2.Visible = (Stack.UserLevel_Type != 0)
                || Program.dbOperations.GetAllUL_Confirm_UL_RequestsAsync(Stack.Company_Index, 0, Stack.UserIndex).Any();

            // دکمه «درخواست جدید» در صورتیکه قابل استفاده است که برای سطح دسترسی کاربر، دسته محصولی جهت درخواست تعریف شده باشد
            btnAddNew.Visible = (Stack.UserLevel_Type !=0) ||
                Program.dbOperations.GetAllUL_Request_CategoriesAsync(Stack.Company_Index, Stack.UserLevel_Index).Any();

            btnCostCenters.Visible = (Stack.UserLevel_Type != 0);
        }

        private void M1130_Warehouse_RequestItems_Shown(object sender, EventArgs e)
        {

        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (panel2.Visible)
                radRequests_Need_Confirmation.Checked = true;
            else radMyRequests.Checked = true;

            if (radRequests_Need_Confirmation.Checked)
            {

            }

            progressBar1.Visible = false;
            panel1.Enabled = true;
        }

        private void GetData()
        {
            if(!lstRequests.Any())
            {
                if (panel2.Visible)
                {
                    // شناسه سطوح کاربرانی که درخواستهای آنها نیاز به تأیید سطح کاربر جاری دارد
                    List<long> lstUsers_Need_Confirmation = Program.dbOperations.GetAllUL_Confirm_UL_RequestsAsync
                        (Stack.Company_Index, 0, Stack.UserLevel_Index).Select(d => d.UL_Index).ToList();

                    lstRequests.AddRange(Program.dbOperations.GetAllWarehouse_RequestsAsync(Stack.Company_Index)
                        .Where(d => d.Need_Supervisor_Confirmation)//.Where(j => j.Supervisor_Confirmer_Index <= 0)
                        .Where(n=>lstUsers_Need_Confirmation.Contains(n.UserLevel_Index)).ToList());

                    // درخواستهای سطوح کاربری دیگر (که سرپرست هستند) و نیاز به تأیید سطح کاربر جاری دارند
                    if(Program.dbOperations.GetAllUL_Confirm_UL_RequestsAsync(Stack.Company_Index,Stack.UserLevel_Index).Any())
                    {

                    }
                }
            }



        }

        private void ShowData(int ActionType = 1)
        {

            #region ترجمه سر ستونها و مخفی کردن بعضی ستونها
            if (ActionType == 1)
            {
                foreach (DataGridViewColumn col in dgvData.Columns)
                {
                    switch (col.Name)
                    {
                        case "Id":
                            col.HeaderText = "شماره سفارش";
                            col.Width = 125;
                            break;
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
                            col.Width = 300;
                            break;
                        default: col.Visible = false; break;
                    }
                }
            }
            #endregion
        }

        private void BtnAddNew_Click(object sender, EventArgs e)
        {
            new M1120_WarehouseRequest_AddNew().Show();
        }

        private void BtnCostCenters_Click(object sender, EventArgs e)
        {
            new M1140_CostCenters().ShowDialog();
        }











        //
    }
}
