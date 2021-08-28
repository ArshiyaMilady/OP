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
    public partial class M1134_Warehouse_RequestItem_Rows : X210_ExampleForm_Normal
    {
        long warehouse_request_index;
        bool bConfirm_is_Possible = false;  // آیا امکان تغییر در فرم وجود داشته باشد
        bool bAnything_to_Confirm = false;  // آیا چیزی برای تأیید وجود دارد؟
        // تمام دسته کالاهایی که کاربر جاری می تواند تأیید نماید
        List<long> lstCategories_UserLevel_Can_Confirm = new List<long>();
        List<Models.Warehouse_Request_Row> lstRows = new List<Models.Warehouse_Request_Row>();

        public M1134_Warehouse_RequestItem_Rows(long _warehouse_request_index,bool _bConfirm_is_Possible = true)
        {
            InitializeComponent();

            warehouse_request_index = _warehouse_request_index;
            bConfirm_is_Possible = _bConfirm_is_Possible;
            Stack.bx = false;   // آیا تغییری (تأیید یا عدم تأییدی) اتفاق افتاده است؟

            if(!backgroundWorker1.IsBusy)
                backgroundWorker1.RunWorkerAsync();
        }

        private void M1134_Warehouse_RequestItem_Rows_Shown(object sender, EventArgs e)
        {
            Models.Warehouse_Request wr = Program.dbOperations.GetWarehouse_RequestAsync(warehouse_request_index);
            Text = "   کد درخواست : " + wr.Index_in_Company;
            textBox1.Text = wr.Unit_Name;
            textBox2.Text = wr.User_Name;
            textBox3.Text = wr.DateTime_sh;

            Application.DoEvents();
            panel1.Enabled = true;
        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            lstRows = Program.dbOperations.GetAllWarehouse_Request_RowsAsync(Stack.Company_Index
                , warehouse_request_index);

            // برای کاربران غیر از ادمین
            if ((Stack.UserLevel_Type != 1) && (Stack.UserLevel_Type != 2))
            {
                // اگر کاربر جاری با کاربر ثبت کننده درخواست ، یکی نباشد
                if (Stack.UserIndex != Program.dbOperations.GetWarehouse_RequestAsync(warehouse_request_index).User_Index)
                {
                    // قبل از تغییر لیست سطرها
                    bAnything_to_Confirm = lstRows.Any(d => d.Need_Supervisor_Confirmation);

                    // اگر موردی باشد که نیاز به تأیید سرپرست داشته باشد
                    if (bAnything_to_Confirm)
                    {
                        lstCategories_UserLevel_Can_Confirm = Program.dbOperations
                          .GetAllUL_Request_CategoriesAsync(Stack.Company_Index)
                          .Where(d => d.Supervisor_UL_Index == Stack.UserLevel_Index)
                          .Select(d => d.Category_Index).Distinct().ToList();
                        lstRows = lstRows.Where(d => lstCategories_UserLevel_Can_Confirm.Contains(d.Item_Category_Index)).ToList();
                    }
                    // بعد از تغییر لیست سطرها
                    bAnything_to_Confirm = lstRows.Any(d => d.Need_Supervisor_Confirmation);

                    bAnything_to_Confirm = bAnything_to_Confirm && bConfirm_is_Possible;
                }
            }
        }

        private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // اگر کاربر جاری ، دارای سطحی باشد که بتواند مواردی از درخواست را تأیید نماید
            btnConfirm.Visible = bAnything_to_Confirm;
            label4.Visible = bAnything_to_Confirm;

            dgvData.DataSource = lstRows;
            ShowData();
        }

        private void ShowData()
        {
            #region ترجمه سر ستونها و مخفی کردن بعضی ستونها
            foreach (DataGridViewColumn col in dgvData.Columns)
            {
                switch (col.Name)
                {
                    case "C_B1":
                        if (bAnything_to_Confirm)
                        {
                            col.HeaderText = "انتخاب";
                            col.Width = 50;
                        }
                        else col.Visible = false;
                        break;
                    case "CostCenter_Index":
                        col.HeaderText = "مرکز هزینه";
                        col.ReadOnly = true;
                        col.Width = 150;
                        break;
                    case "Item_SmallCode":
                        col.HeaderText = "کد کالا";
                        col.ReadOnly = true;
                        col.Width = 100;
                        break;
                    case "Item_Name":
                        col.HeaderText = "نام کالا";
                        col.Width = 150;
                        col.ReadOnly = true;
                        break;
                    case "Quantity":
                        col.HeaderText = "تعداد";
                        col.ReadOnly = true;
                        col.Width = 50;
                        break;
                    case "Item_Unit":
                        col.HeaderText = "واحد";
                        col.ReadOnly = true;
                        col.Width = 100;
                        break;
                    case "Reason_of_Cancelling":
                        col.HeaderText = "علت عدم تأیید";
                        col.Width = 200;
                        col.ReadOnly = !bAnything_to_Confirm;
                        break;
                    case "Status_Description":
                        col.HeaderText = "وضعیت";
                        col.Width = 200;
                        col.ReadOnly = true;
                        break;
                    default: col.Visible = false; break;
                }
            }
            #endregion
        }

        private void BtnReturn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            CancelButton = null;
            #region بررسی خالی نبودن ستون توضیحات برای موارد تأیید نشده
            for (int i = 0; i < dgvData.Rows.Count; i++)
            {
                DataGridViewRow row = dgvData.Rows[i];
                row.DefaultCellStyle.ForeColor = Color.Black;
                bool bC_B1 = Convert.ToBoolean(row.Cells["C_B1"].Value);
                string des = null;
                if (!bC_B1)
                {
                    if (row.Cells["Reason_of_Cancelling"].Value != null) des 
                            = row.Cells["Reason_of_Cancelling"].Value.ToString();
                    if (string.IsNullOrEmpty(des))
                    {
                        row.DefaultCellStyle.ForeColor = Color.Red;
                        MessageBox.Show("لطفا برای موارد انتخاب نشده، علت عدم تأیید خود را در ستون مربوطه ثبت نمایید");
                        return;
                    }
                }
            }
            #endregion

            // آیا مورد تأیید شده ای وجود دارد
            bool Any_Confirmed = dgvData.Rows.Cast<DataGridViewRow>()  //.ToList()
                .Any(d => Convert.ToBoolean(d.Cells["C_B1"].Value));
            // آیا مورد تأیید نشده ای وجود دارد
            bool Any_Canceled = dgvData.Rows.Cast<DataGridViewRow>()
                .Any(d => !Convert.ToBoolean(d.Cells["C_B1"].Value));

            #region پیام اطمینان
            string msg = null;
            if (Any_Confirmed && !Any_Canceled)
                msg = "آیا از تأیید موارد درخواست اطمینان دارید؟";
            else if (Any_Confirmed && Any_Canceled)
                msg = "آیا از تأیید یا عدم تأیید موارد درخواست اطمینان دارید؟";
            else if (!Any_Confirmed && Any_Canceled)
                msg = "آیا از لغو موارد درخواست اطمینان دارید؟";
            #endregion

            if (MessageBox.Show(msg, "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                #region تعیین وضعیت ردیف های درخواست

                for (int i = 0; i < dgvData.Rows.Count; i++)
                {
                    DataGridViewRow row = dgvData.Rows[i];
                    long wr_row_index = Convert.ToInt64(row.Cells["Index"].Value);
                    bool bC_B1 = Convert.ToBoolean(row.Cells["C_B1"].Value);
                    string cancel_description = bC_B1 ? null : row.Cells["Reason_of_Cancelling"].Value.ToString();

                    Models.Warehouse_Request_Row wr_row = Program.dbOperations.GetWarehouse_Request_RowAsync(wr_row_index);

                    if (bC_B1)
                    {
                        Models.UL_Request_Category urc = Program.dbOperations
                            .GetAllUL_Request_CategoriesAsync(Stack.Company_Index, Stack.UserLevel_Index)
                            .FirstOrDefault(d => d.Category_Index == wr_row.Item_Category_Index);
                        if (urc != null)
                        {
                            wr_row.Need_Supervisor_Confirmation = urc.Supervisor_UL_Index > 0;
                            wr_row.Supervisor_Confirmer_LevelIndex = urc.Supervisor_UL_Index;
                            if (urc.Supervisor_UL_Index > 0)
                            {
                                wr_row.Status_Description = "در انتظار تأیید "
                                    + Program.dbOperations.GetUser_LevelAsync(urc.Supervisor_UL_Index).Description;
                            }
                            else
                                wr_row.Status_Description = "تأیید شده است";
                        }
                        else
                        {
                            wr_row.Need_Supervisor_Confirmation = false;
                            wr_row.Supervisor_Confirmer_LevelIndex = 0;
                            wr_row.Status_Description = "تأیید شده است";
                        }
                        //Any_Confirmed = true;
                    }
                    else
                    {
                        wr_row.Need_Supervisor_Confirmation = false;
                        wr_row.Canceled = true;
                        wr_row.Reason_of_Cancelling = cancel_description;
                        wr_row.Status_Description = "توسط "
                            + Program.dbOperations.GetUser_LevelAsync(Stack.UserLevel_Index).Description
                            + "لغو گردید";

                        //Any_Canceled = true;
                    }

                    Program.dbOperations.UpdateWarehouse_Request_RowAsync(wr_row);
                }
                #endregion

                #region تعیین وضعیت درخواست
                string wr_History_Description = null;
                if (Any_Confirmed && !Any_Canceled)
                    wr_History_Description = "تمام موارد مربوطه توسط " + Stack.UserName + " تأیید شدند";
                else if (Any_Confirmed && Any_Canceled)
                    wr_History_Description = "بعضی از موارد توسط " + Stack.UserName + " لغو شدند";
                else if (!Any_Confirmed && Any_Canceled)
                    wr_History_Description = "تمام موارد مربوطه توسط " + Stack.UserName + " لغو شد";

                // اگر وضعیت تمام ردیف های درخواست مشخص شده باشد
                Models.Warehouse_Request wr = Program.dbOperations.GetWarehouse_RequestAsync(warehouse_request_index);

                // ثبت درخواست در تاریخچه
                new ThisProject().Create_RequestHistory(wr, wr_History_Description);

                wr_History_Description = null;

                if (!Program.dbOperations.GetAllWarehouse_Request_RowsAsync
                    (Stack.Company_Index, warehouse_request_index).Any(d => d.Need_Supervisor_Confirmation))
                {
                    // اگر تمام ردیف ها کنسل شده باشند = ردیف کنسل نشده ای نباشد
                    if (!Program.dbOperations.GetAllWarehouse_Request_RowsAsync
                        (Stack.Company_Index, warehouse_request_index).Any(d => !d.Canceled))
                    {
                        wr.Request_Canceled = true;
                        wr.Status_Description = "درخواست لغو گردید.برای اطلاعات بیشتر به تاریخچه مراجعه نمایید";
                        wr_History_Description = "درخواست کاملا لغو گردید";
                    }
                    // اگر بعضی از موارد کنسل شده باشند
                    else if (Program.dbOperations.GetAllWarehouse_Request_RowsAsync
                        (Stack.Company_Index, warehouse_request_index).Any(d => d.Canceled))
                    {
                        wr.Sent_to_Warehouse = true;
                        wr.Status_Description = "بعضی از موارد درخواست لغو شدند.به انبار ارسال شد.اطلاعات بیشتر در تاریخچه";
                        wr_History_Description = "در خواست به انبار ارسال شد";
                    }
                    // اگر تمام موارد تأیید شده باشند
                    else
                    {
                        wr.Sent_to_Warehouse = true;
                        wr.Status_Description = "تمام موارد درخواست تأیید شدند.به انبار مراجعه نمایید";
                        wr_History_Description = "در خواست به انبار ارسال شد";
                    }

                    // ثبت درخواست در تاریخچه
                    new ThisProject().Create_RequestHistory(wr, wr_History_Description);
                }
                else
                {
                    wr.Status_Description = "در حال انجام مراحل تأیید";
                }

                Program.dbOperations.UpdateWarehouse_RequestAsync(wr);
                #endregion

                MessageBox.Show("ثبت اطلاعات با موفقیت انجام شد");
                Stack.bx = true;
                Close();
                return;
            }

            CancelButton = btnReturn;
        }

        // رزرو کالا در انبار - تعداد رزروها را بر می گرداند
        private void AutoBooking(Models.Warehouse_Request_Row wr_row)
        {
            if(Program.dbOperations.GetCompanyAsync(Stack.Company_Index).Warehouse_AutomaticBooking)
            { }
        }






        //
    }
}
