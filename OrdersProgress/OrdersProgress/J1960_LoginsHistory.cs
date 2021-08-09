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
    public partial class J1960_LoginsHistory : X210_ExampleForm_Normal
    {
        long user_index = 0;

        public J1960_LoginsHistory(long _user_index=0)
        {
            InitializeComponent();

            if (_user_index > 0)
            {
                user_index = _user_index;
                Text = Program.dbOperations.GetUserAsync(user_index).Real_Name;
            }
        }

        private void J1960_LoginsHistory_Shown(object sender, EventArgs e)
        {
            dgvData.DataSource = GetData();
            ShowData();
        }

        private List<Models.LoginHistory> GetData()//bool NeedToCorrect_C_B1 = true)
        {
            return Program.dbOperations.GetAllLoginHistorysAsync(Stack.Company_Index, user_index)
                .OrderByDescending(d=>d.DateTime_mi).ToList();
        }

        private void ShowData()//bool ChangeHeaderTexts = true)
        {
            #region ترجمه سر ستونها و مخفی کردن بعضی ستونها
            foreach (DataGridViewColumn col in dgvData.Columns)
            {
                switch (col.Name)
                {
                    case "User_Index":
                        col.HeaderText = "شناسه کاربر";
                        col.Width = 150;
                        break;
                    case "Date_sh":
                        col.HeaderText = "تاریخ";
                        col.Width = 150;
                        break;
                    case "Time":
                        col.HeaderText = "زمان";
                        col.Width = 150;
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
    }
}
