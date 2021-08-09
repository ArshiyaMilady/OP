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
    public partial class L2150_OneOrder_Change_OrderLevel : X210_ExampleForm_Normal
    {
        string order_index;
        List<Models.Order_Level> lstOLs = new List<Models.Order_Level>();

        public L2150_OneOrder_Change_OrderLevel(string _order_index)
        {
            InitializeComponent();

            order_index = _order_index;
        }

        private void L2150_OneOrder_Change_OrderLevel_Shown(object sender, EventArgs e)
        {

            dgvData.DataSource = GetData();
            
            ShowData();
        }

        private List<Models.Order_Level> GetData()
        {
            if (!lstOLs.Any())
            {
                List<long> lstOL_indexes = new List<long>();
                if (Stack.UserLevel_Type != 0)
                    lstOL_indexes.AddRange(Program.dbOperations.GetAllOrder_LevelsAsync
                        (Stack.Company_Index).Select(d => d.Index).ToList());
                else
                    // مراحل سفارش که کاربر (با سطح خودش) می تواند تأیید کند
                    lstOL_indexes = Program.dbOperations.GetAllOL_ULsAsync(Stack.Company_Index
                        , 0, Stack.UserLevel_Index).Select(d => d.OL_Index).ToList();
        
                // شناسه (های) مراحل بعدی سفارش  
                List<long> lstNext_OLs = new ThisProject().Next_OrderLevel_Indexes(order_index);

                foreach (long ol_index in lstOL_indexes)
                {
                    Models.Order_Level ol = Program.dbOperations.GetOrder_LevelAsync(ol_index);
                    if (ol.ReturningLevel || lstNext_OLs.Contains(ol_index)) lstOLs.Add(ol);
                }
            }

            #region اگر برای کاربر جاری ، امکان تغییر مرحله سفارش وجود نداشت
            if (lstOLs.Count == 1)
                if(Program.dbOperations.GetOrder_LevelAsync(lstOLs.First().Index).ReturningLevel)
                {
                    MessageBox.Show("امکان تغییر مرحله سفارش وجود ندارد");
                    Close();
                    return null;
                }
            #endregion

            return lstOLs.OrderBy(d=>d.Sequence).ToList();
        }

        private void ShowData()
        {
            #region ترجمه سر ستونها و مخفی کردن بعضی ستونها
            foreach (DataGridViewColumn col in dgvData.Columns)
            {
                switch (col.Name)
                {
                    case "C_B1":
                        col.HeaderText = "انتخاب";
                        col.Width = 50;
                        break;
                    case "Description":
                        col.HeaderText = "شرح";
                        col.ReadOnly = true;
                        col.Width = 300;
                        break;

                    default: col.Visible = false; break;
                }

                if (col.ReadOnly) col.DefaultCellStyle.BackColor = Color.WhiteSmoke;
                else col.DefaultCellStyle.BackColor = Color.White;
            }
            #endregion
        }

        // فقط یکی از سطح ها انتخاب شود
        private void DgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            if (dgvData.Columns[e.ColumnIndex].Name.Equals("C_B1"))
            {
                bool C_B1 = Convert.ToBoolean(dgvData[e.ColumnIndex, e.RowIndex].Value);
                if (!C_B1)
                {
                    foreach (DataGridViewRow row in dgvData.Rows)
                        if (row.Index != e.RowIndex)
                            row.Cells["C_B1"].Value = false;

                    //dgvData["C_B1", e.RowIndex].Value = true;
                }

            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if(!dgvData.Rows.Cast<DataGridViewRow>().Any(d=>Convert.ToBoolean(d.Cells["C_B1"].Value)))
            {
                MessageBox.Show("لطفا یک مرحله را انتخاب نمایید", "خطا");
            }
            else
            {
                DataGridViewRow row = dgvData.Rows.Cast<DataGridViewRow>().First(d => Convert.ToBoolean(d.Cells["C_B1"].Value));
                long ol_index = Convert.ToInt64(row.Cells["Index"].Value);
                Models.Order_Level order_level = Program.dbOperations.GetOrder_LevelAsync(ol_index);
                if(order_level.ReturningLevel)
                {
                    Stack.sx = null;
                    new X220_InputBox("علت برگشت").ShowDialog();
                    if(!string.IsNullOrEmpty(Stack.sx))
                    {

                    }
                }

            }
        }
    }
}
