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
    public partial class L1143_OL_on_Returning_Comment : X210_ExampleForm_Normal
    {
        Models.Order order = null;
        // شناسه مراحل سفارشی می توانند از این مرحله از سفارش بازگردند
        List<long> lstOLs_can_return = new List<long>();

        public L1143_OL_on_Returning_Comment(string _order_index)
        {
            InitializeComponent();

            Stack.sx = null;
            Stack.lx = -1;
            order = Program.dbOperations.GetOrderAsync(_order_index);
            label3.Text = "شماره سفارش : " + order.Id;
        }

        private void L1143_OL_on_Returning_Comment_Shown(object sender, EventArgs e)
        {
            // شناسه مراحل سفارشی می توانند از این مرحله از سفارش بازگردند
            lstOLs_can_return = Program.dbOperations.GetAllOrder_Level_on_ReturningsAsync
                (Stack.Company_Index, order.CurrentLevel_Index).Select(d=>d.OL_Retruned_Index).ToList();

            comboBox1.Items.AddRange(Program.dbOperations.GetAllOrder_LevelsAsync
                (Stack.Company_Index).Where(d => lstOLs_can_return.Contains(d.Index)).Select(d => d.Description2).ToArray());

            if (comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedIndex = 0;
                if (comboBox1.Items.Count == 1) comboBox1.Enabled = false;
            }
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("متنی وارد نشده است");
                return;
            }
            else
            {
                Stack.lx = lstOLs_can_return[comboBox1.SelectedIndex];
                Stack.sx = textBox1.Text;
                Close();
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
