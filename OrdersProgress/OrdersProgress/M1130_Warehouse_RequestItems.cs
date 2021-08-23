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
        List<Models.Warehouse_Request> lstRequests_passed = new List<Models.Warehouse_Request>();
        bool just_show_myRequests = false;

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
            just_show_myRequests = !panel2.Visible;
            //MessageBox.Show(just_show_myRequests.ToString());

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
            GetData();
        }

        private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (just_show_myRequests)
                 radMyRequests.Checked = true;
            else radRequests_Need_Confirmation.Checked = true;
            

                ShowData();

            progressBar1.Visible = false;
            panel1.Enabled = true;
        }

        private void GetData()
        {
            if(!lstRequests_need_confirmation.Any() && !lstRequests_mine.Any() 
                && !lstRequests_passed.Any())
            {
                // درخواستهای من
                lstRequests_mine.AddRange(Program.dbOperations.GetAllWarehouse_RequestsAsync(Stack.Company_Index)
                    .Where(d => d.User_Index == Stack.UserIndex).ToList());

                if (!just_show_myRequests)
                {
                    // شناسه سطوح کاربرانی که درخواستهای آنها نیاز به تأیید سطح کاربر جاری دارد
                    List<long> lstUsers_Need_Confirmation = Program.dbOperations.GetAllUL_Confirm_UL_RequestsAsync
                        (Stack.Company_Index, 0, Stack.UserLevel_Index).Select(d => d.UL_Index).ToList();

                    // درخواستهای در انتظار تأیید
                    lstRequests_need_confirmation.AddRange(Program.dbOperations.GetAllWarehouse_RequestsAsync(Stack.Company_Index)
                        .Where(d => d.Need_Supervisor_Confirmation).Where(j => !j.Sent_to_Warehouse)
                        .Where(n=>lstUsers_Need_Confirmation.Contains(n.UserLevel_Index)).ToList());

                    // شناسه درخواستهایی که توسط کاربر جاری در تاریخچه تأیید شده اند
                    List<long> lstConfirmed_in_History = Program.dbOperations.GetAllWarehouse_Request_HistorysAsync
                        (Stack.Company_Index).Where(d => d.User_Index == Stack.UserIndex).Select(d => d.Warehouse_Request_Index).ToList();

                    // درخواستهایی که توسط کاربر جاری تأیید یا عدم تأیید شده اند
                    lstRequests_passed.AddRange(Program.dbOperations.GetAllWarehouse_RequestsAsync
                        (Stack.Company_Index).Where(d => lstConfirmed_in_History.Contains(d.User_Index)).ToList());
                }
            }
        }

        private void ShowData()
        {
            #region ترجمه سر ستونها و مخفی کردن بعضی ستونها
            foreach (DataGridViewColumn col in dgvData.Columns)
            {
                switch (col.Name)
                {
                    case "Index_in_Company":
                        col.HeaderText = "شماره درخواست";
                        col.Width = 125;
                        break;
                    case "Unit_Name":
                        col.HeaderText = "نام واحد";
                        col.Width = 150;
                        break;
                    case "User_Name":
                        col.HeaderText = "درخواست کننده";
                        col.Width = 150;
                        //col.DefaultCellStyle.BackColor = Color.LightGray;
                        break;
                    case "Date_sh":
                        col.HeaderText = "تاریخ ثبت";
                        //col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        col.Width = 100;
                        break;
                    case "Status_Description":
                        col.HeaderText = "وضعیت درخواست";
                        //col.ReadOnly = true;
                        //col.DefaultCellStyle.BackColor = Color.LightGray;
                        col.Width = 300;
                        break;
                    default: col.Visible = false; break;
                }
            }
            #endregion
        }

        private void BtnAddNew_Click(object sender, EventArgs e)
        {
            new M1120_WarehouseRequest_AddNew().Show();

            // آیا کاربر درخواست(های) جدیدی را ثبت کرده است
            if (Stack.bx)
            {
                // درخواستهای من
                lstRequests_mine.AddRange(Program.dbOperations.GetAllWarehouse_RequestsAsync(Stack.Company_Index)
                    .Where(d => d.User_Index == Stack.UserIndex).ToList());
                if (just_show_myRequests) // (radMyRequests.Checked)
                    dgvData.DataSource = lstRequests_mine;
            }
        }

        private void BtnCostCenters_Click(object sender, EventArgs e)
        {
            new M1140_CostCenters().ShowDialog();
        }

        private void RadRequests_CheckedChanged(object sender, EventArgs e)
        {
            if (radRequests_Need_Confirmation.Checked)
                dgvData.DataSource = lstRequests_need_confirmation;
            else if (radMyRequests.Checked)
                dgvData.DataSource = lstRequests_mine;
            else if (radConfirmedRequests.Checked)
                dgvData.DataSource = lstRequests_passed;
        }











        //
    }
}
