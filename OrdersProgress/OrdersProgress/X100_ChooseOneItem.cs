using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OrdersProgress
{
    public partial class X100_ChooseOneItem : Form
    {
        public X100_ChooseOneItem(string sTitle , List<string> Items)
        {
            InitializeComponent();

            Text = sTitle;
            for (int i = 0; i < Items.Count; i++)
            {
                dgvData.Rows.Add();
                dgvData["colItem", i].Value = Items[i];
            }

        }

        private void X001_ChooseOneItem_Load(object sender, EventArgs e)
        {
            dgvData.AllowUserToAddRows = false;
            dgvData.AllowUserToDeleteRows = false;
            Stack.sx = null;
            dgvData.CurrentCell = null;
        }

        private void BtnReturn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void DgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.RowIndex < 0) || (e.ColumnIndex < 0)) return;

            bool bC = Convert.ToBoolean(dgvData["colCheck", e.RowIndex].Value);

            if (!bC)
            {
                foreach (DataGridViewRow row in dgvData.Rows)
                    row.Cells["colCheck"].Value = false;
            }

            dgvData["colCheck", e.RowIndex].Value = !bC;
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridViewRow row = dgvData.Rows.Cast<DataGridViewRow>()
                    .FirstOrDefault(d => Convert.ToBoolean(d.Cells["colCheck"].Value));
                if (row != null)
                {
                    Stack.sx = row.Cells["colItem"].Value.ToString();
                    Close();
                    return;
                }
            }
            catch {}
            MessageBox.Show("لطفا یک مورد را انتخاب نمایید");
        }
    }
}
