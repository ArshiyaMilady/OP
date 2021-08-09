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
    public partial class X220_InputBox : X210_ExampleForm_Normal
    {
        public X220_InputBox(string _Label_msg)
        {
            InitializeComponent();

            if (string.IsNullOrEmpty(_Label_msg)) label1.Text = "";
            else label1.Text = _Label_msg;
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("متنی وارد نشده است");
                return;
            }
            else
                Stack.sx = textBox1.Text;
        }
    }
}
