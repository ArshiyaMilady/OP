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
    public partial class M1132_Warehouse_Request_History : X210_ExampleForm_Normal
    {
        long warehouse_request_index;

        public M1132_Warehouse_Request_History(long _warehouse_request_index)
        {
            InitializeComponent();

            warehouse_request_index = _warehouse_request_index;
            Text = Text + Program.dbOperations.GetWarehouse_RequestAsync(warehouse_request_index).Index_in_Company;
        }

        private void M1132_Warehouse_Request_History_Shown(object sender, EventArgs e)
        {
            dgvData.DataSource = Program.dbOperations.GetAllWarehouse_Request_HistorysAsync
                (Stack.Company_Index, warehouse_request_index).OrderByDescending(d=>d.DateTime_mi)
                .ThenByDescending(d=>d.Id).ToList();

            ShowData();
        }

        private void ShowData()
        {
            #region ترجمه سر ستونها و مخفی کردن بعضی ستونها
            foreach (DataGridViewColumn col in dgvData.Columns)
            {
                switch (col.Name)
                {
                    case "User_Name":
                        col.HeaderText = "نام کاربر";
                        col.ReadOnly = true;
                        col.Width = 150;
                        break;
                    case "Date_sh":
                        col.HeaderText = "تاریخ";
                        col.ReadOnly = true;
                        col.Width = 100;
                        break;
                    case "Time":
                        col.HeaderText = "زمان";
                        col.Width = 100;
                        col.ReadOnly = true;
                        break;
                    case "Description":
                        col.HeaderText = "توضیحات";
                        //col.ReadOnly = true;
                        col.Width = 300;
                        break;
                    default: col.Visible = false; break;
                }
            }
            #endregion
        }

    }
}
