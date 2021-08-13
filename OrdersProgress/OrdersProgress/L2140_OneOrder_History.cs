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
    public partial class L2140_OneOrder_History : X210_ExampleForm_Normal
    {
        string OrderIndex = null;

        public L2140_OneOrder_History(string _OrderIndex)
        {
            InitializeComponent();

            OrderIndex = _OrderIndex;
            Text = Text + Program.dbOperations.GetOrderAsync(OrderIndex).Title;
        }

        private void L2140_OneOrder_History_Shown(object sender, EventArgs e)
        {
            dgvData.DataSource = GetData();
            ShowData();
        }

        // NeedToCorrect_C_B1 : آیا نیاز به بروز رسانی پارامتر «سی-بی 1» می باشد؟
        private List<Models.Order_History> GetData()//bool NeedToCorrect_C_B1 = true)
        {
            return Program.dbOperations.GetAllOrder_HistorysAsync(Stack.Company_Index, OrderIndex);
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
                        case "OrderLevel_Description":
                            col.HeaderText = "شرح مرحله انجام شده";
                            col.Width = 500;
                            break;
                        case "User_Name":
                            col.HeaderText = "نام کاربر";
                            col.Width = 200;
                            break;
                        case "DateTime_sh":
                            col.HeaderText = "تاریخ";
                            col.Width = 200;
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




    }
}
