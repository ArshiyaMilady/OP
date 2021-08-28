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

            tsmiDelete.Visible = Stack.UserLevel_Type == 1;

            // اگر کاربر، ادمین ، کاربر ارشد یا سرپرست واحد باشد
            panel2.Visible = (Stack.UserLevel_Type != 0)
                || Program.dbOperations.GetAllUL_Request_CategoriesAsync(Stack.Company_Index)
                    .Any(d=>d.Supervisor_UL_Index == Stack.UserLevel_Index);
            //panel2.Visible = (Stack.UserLevel_Type != 0)
            //    || Program.dbOperations.GetAllUL_Confirm_UL_RequestsAsync(Stack.Company_Index, 0, Stack.UserLevel_Index).Any();
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

        private void GetData(bool ForceReset=false)
        {
            if (ForceReset)
            {
                lstRequests_need_confirmation = new List<Models.Warehouse_Request>();
            }

            if((!lstRequests_need_confirmation.Any() && !lstRequests_mine.Any() 
                && !lstRequests_passed.Any()) || ForceReset)
            {
                // درخواستهای من
                lstRequests_mine = Program.dbOperations.GetAllWarehouse_RequestsAsync(Stack.Company_Index)
                    .Where(d => d.User_Index == Stack.UserIndex).ToList();

                if (!just_show_myRequests)
                {
                    #region اگر کاربر ادمین باشد
                    if((Stack.UserLevel_Type==1) || (Stack.UserLevel_Type==2))
                    {
                        lstRequests_need_confirmation = Program.dbOperations.GetAllWarehouse_RequestsAsync
                            (Stack.Company_Index).Where(d => !d.Sent_to_Warehouse && !d.Request_Canceled).ToList();

                        lstRequests_passed = Program.dbOperations.GetAllWarehouse_RequestsAsync
                            (Stack.Company_Index).Where(d => d.Sent_to_Warehouse || d.Request_Canceled).ToList();
                        return;
                    }
                    #endregion

                    #region درخواستهایی که نیاز به تأیید دارند
                    // تمام روابط سطح کاربری-سرپرست تأییدکننده 
                    List<Models.UL_Request_Category> lstULRC = Program.dbOperations
                        .GetAllUL_Request_CategoriesAsync(Stack.Company_Index)
                        .Where(d => d.Supervisor_UL_Index == Stack.UserLevel_Index).ToList();
                    //MessageBox.Show(Program.dbOperations
                    //    .GetAllUL_Request_CategoriesAsync(Stack.Company_Index)
                    //    .Where(d => d.Supervisor_UL_Index == Stack.UserLevel_Index)
                    //    .Count().ToString());

                    // تمام روابط مربوط به سطح کاربری سرپرست 
                    lstULRC.AddRange(Program.dbOperations
                        .GetAllUL_Request_CategoriesAsync(Stack.Company_Index)
                        .Where(d => (d.User_Level_Index == Stack.UserLevel_Index)
                            && (d.Supervisor_UL_Index == 0)).ToList());
                    //MessageBox.Show(Program.dbOperations
                    //    .GetAllUL_Request_CategoriesAsync(Stack.Company_Index)
                    //    .Where(d => (d.User_Level_Index == Stack.UserLevel_Index)
                    //        && (d.Supervisor_UL_Index == 0)).Count().ToString());

                    // تمام دسته کالاهایی که کاربر جاری می تواند تأیید نماید
                    List<long> lstCategories_UserLevel_Can_Confirm = lstULRC
                        .Select(d=>d.Category_Index).Distinct().ToList();

                    // تعیین درخواستهای در انتظار تأیید
                    foreach (Models.Warehouse_Request_Row wr_row in Program.dbOperations
                        .GetAllWarehouse_Request_RowsAsync(Stack.Company_Index)
                        .Where(d=>d.Need_Supervisor_Confirmation)
                        .Where(j=>lstCategories_UserLevel_Can_Confirm.Contains(j.Item_Category_Index)).ToList())
                    {
                        // اگر درخواست وارد لیست نشده باشد، آنرا وارد لیست می کند
                        if (!lstRequests_need_confirmation.Any(d => d.Index == wr_row.Warehouse_Request_Index))
                        {
                            if (lstULRC.Any(d => (d.Category_Index == wr_row.Item_Category_Index)
                                 && (d.Supervisor_UL_Index == wr_row.Supervisor_Confirmer_LevelIndex)))
                            {
                                Models.Warehouse_Request wr = Program.dbOperations.GetWarehouse_RequestAsync(wr_row.Warehouse_Request_Index);
                                lstRequests_need_confirmation.Add(wr);
                            }
                        }
                    }
                    #endregion

                    #region درخواستهایی که توسط کاربر جاری تأیید شده اند
                    // شناسه درخواستهایی که توسط کاربر جاری در تاریخچه تأیید شده اند
                    List<long> lstConfirmed_in_History = Program.dbOperations.GetAllWarehouse_Request_HistorysAsync
                        (Stack.Company_Index).Where(d => d.User_Index == Stack.UserIndex)
                        .Select(d => d.Warehouse_Request_Index).ToList();

                    // درخواستهایی که توسط کاربر جاری تأیید یا عدم تأیید شده اند
                    lstRequests_passed = Program.dbOperations.GetAllWarehouse_RequestsAsync
                        (Stack.Company_Index).Where(d=>d.User_Index!=Stack.UserIndex)
                        .Where(d => lstConfirmed_in_History.Contains(d.Index)).ToList();
                    #endregion
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
            new M1120_WarehouseRequest_AddNew().ShowDialog();
            //MessageBox.Show("100");

            // آیا کاربر درخواست(های) جدیدی را ثبت کرده است
            if (Stack.bx)
            {
                //MessageBox.Show("200");
                // درخواستهای من
                lstRequests_mine=Program.dbOperations.GetAllWarehouse_RequestsAsync(Stack.Company_Index)
                    .Where(d => d.User_Index == Stack.UserIndex).ToList();
                if (just_show_myRequests) // (radMyRequests.Checked)
                {
                    //MessageBox.Show("300");
                    dgvData.DataSource = lstRequests_mine;
                }
            }
        }

        private void BtnCostCenters_Click(object sender, EventArgs e)
        {
            new M1140_CostCenters().ShowDialog();
        }

        private void RadRequests_CheckedChanged(object sender, EventArgs e)
        {
            //MessageBox.Show("100");
            if (radRequests_Need_Confirmation.Checked)
                dgvData.DataSource = lstRequests_need_confirmation;
            else if (radMyRequests.Checked)
                dgvData.DataSource = lstRequests_mine;
            else if (radConfirmedRequests.Checked)
                dgvData.DataSource = lstRequests_passed;
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
            dgvData.CurrentCell = dgvData[e.ColumnIndex, e.RowIndex];

            if (e.Button == MouseButtons.Right)
            {
                /////// Do something ///////
                // انتخاب سلولی که روی آن کلیک راست شده است
                dgvData.CurrentCell = dgvData[e.ColumnIndex, e.RowIndex];
                contextMenuStrip1.Show(dgvData, new Point(iX, iY));
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = null;
            //List<Models.Warehouse_Request> lstWRs = (List<Models.Warehouse_Request>)dgvData.DataSource;
            if ((row = dgvData.Rows.Cast<DataGridViewRow>().FirstOrDefault
                (d => d.Cells["Index_in_Company"].Value.ToString().Equals(txtST_RequestIndex.Text))) != null)
                dgvData.CurrentCell = row.Cells["Index_in_Company"];
        }

        private void BtnShowAll_Click(object sender, EventArgs e)
        {
            RadRequests_CheckedChanged(null, null);
        }

        private void TsmiRequestDetails_Click(object sender, EventArgs e)
        {
            long warehouse_request_index = Convert.ToInt64(dgvData.CurrentRow.Cells["Index"].Value);
            new M1134_Warehouse_RequestItem_Rows(warehouse_request_index).ShowDialog();
        }

        private void TsmiRequestHistory_Click(object sender, EventArgs e)
        {
            long warehouse_request_index = Convert.ToInt64(dgvData.CurrentRow.Cells["Index"].Value);
            new M1132_Warehouse_Request_History(warehouse_request_index).ShowDialog();
        }

        private void DgvData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            TsmiRequestDetails_Click(null, null);
        }

        private void TsmiDelete_Click(object sender, EventArgs e)
        {
            long request_index = Convert.ToInt64(dgvData.CurrentRow.Cells["Index"].Value);
            Models.Warehouse_Request request = Program.dbOperations.GetWarehouse_RequestAsync(request_index);
            if (MessageBox.Show("آیا حذف درخواست شماره " + request.Index_in_Company + " اطمینان دارید؟"
                , "اخطار", MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            Program.dbOperations.DeleteWarehouse_RequestAsync(request);
            foreach (Models.Warehouse_Request_Row request_row in Program.dbOperations.GetAllWarehouse_Request_RowsAsync
                (Stack.Company_Index, request_index))
                Program.dbOperations.DeleteWarehouse_Request_RowAsync(request_row);

            GetData(true);
            RadRequests_CheckedChanged(null, null);
        }











        //
    }
}
