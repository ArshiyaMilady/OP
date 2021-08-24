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
                (Stack.Company_Index, warehouse_request_index).OrderByDescending(d=>d.DateTime_mi).ToList();

            ShowData
        }

        private void ShowData()
        {
            #region ترجمه سر ستونها و مخفی کردن بعضی ستونها
            foreach (DataGridViewColumn col in dgvData.Columns)
            {
                switch (col.Name)
                {
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
                    case "Description":
                        col.HeaderText = "توضیحات";
                        //col.ReadOnly = true;
                        col.Width = 200;
                        break;
                    default: col.Visible = false; break;
                }
            }
            #endregion
        }

    }
}
