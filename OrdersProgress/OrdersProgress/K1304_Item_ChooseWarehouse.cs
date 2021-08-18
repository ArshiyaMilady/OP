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
    public partial class K1304_Item_ChooseWarehouse : X210_ExampleForm_Normal
    {
        Models.Item item = null;

        public K1304_Item_ChooseWarehouse(Models.Item _item = null)
        {
            InitializeComponent();

            Stack.ix = -1;
            if (_item != null)
            {
                item = _item;
                label3.Text = label3.Text + " برای" + item.Name_Samll;
            }
        }

        private void K1304_Item_ChooseWarehouse_Shown(object sender, EventArgs e)
        {
            comboBox1.Items.AddRange(Program.dbOperations.GetAllWarehousesAsync(Stack.Company_Index)
                .Select(d => d.Name).ToArray());
            if (item == null)
            {
                if (comboBox1.Items.Count > 0)
                    comboBox1.SelectedIndex = 0;
            }
            else
            {
                if (comboBox1.Items.Count > 0)
                {
                    if (Program.dbOperations.GetWarehouseAsync(item.Warehouse_Index) != null)
                        comboBox1.Text = Program.dbOperations.GetWarehouseAsync(item.Warehouse_Index).Name;
                    else
                        comboBox1.SelectedIndex = 0;
                }
            }
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("آیا از انتخاب خود اطمینان دارید?", ""
                , MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            Stack.lx = Program.dbOperations.GetWarehouseAsync(Stack.Company_Index, comboBox1.Text).Index;
            Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Stack.ix = -1;
            Close();
        }
    }
}
