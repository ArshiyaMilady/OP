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
    public partial class M1134_Warehouse_RequestItem_Rows : Form
    {
        long warehouse_request_index;
        bool 

        public M1134_Warehouse_RequestItem_Rows(long _warehouse_request_index)
        {
            InitializeComponent();

            warehouse_request_index = _warehouse_request_index;
            Text = "   کد درخواست : " + Program.dbOperations.GetWarehouse_RequestAsync(warehouse_request_index).Index_in_Company;

            // اگر کاربر جاری ، دارای سطحی باشد که بتواند مواردی از درخواست را تأیید نماید
            btnConfirm.Visible = (Stack.UserLevel_Type == 1) || (Stack.UserLevel_Type == 2)
                || Program.dbOperations.GetAllWarehouse_Request_RowsAsync(Stack.Company_Index
                , warehouse_request_index).Where(d => d.Need_Supervisor_Confirmation)
                .Any(j => j.Supervisor_Confirmer_LevelIndex == Stack.UserLevel_Index);
        }

        private void M1134_Warehouse_RequestItem_Rows_Shown(object sender, EventArgs e)
        {
            dgvData.DataSource = GetData();
            ShowData();

            Application.DoEvents();
            panel1.Enabled = true;
        }

        private List<Models.Warehouse_Request_Row> GetData()
        {
            
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


        private void BtnReturn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            CancelButton = null;
            if (MessageBox.Show("آیا از تأیید موارد درخواست اطمینان دارید؟"
                , "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {

            }

            CancelButton = btnReturn;
        }
    }
}
