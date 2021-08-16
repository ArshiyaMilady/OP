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
    public partial class K1302_Item_Details : X210_ExampleForm_Normal
    {
        Models.Item item;
        int type = -1;

        // type = 0  :  view
        // type = 1  :  edit
        // type = 2  :  add
        public K1302_Item_Details(int _type = 0,Models.Item _item=null)
        {
            InitializeComponent();

            type = _type;
            #region تنظیمات کنترلها با توجه به نوع استفاده از فرم
            if (_type == 2)     // add
            {
                item = new Models.Item();
                btnSave.Text = "ثبت کالا";
                btnSave.Visible = true;
            }
            else
            {
                item = _item;
                Text = item.Name_Samll;

                if (_type == 1)     // edit
                {
                    panel2.Enabled = true;
                    btnSave.Visible = true;
                }
            }
            #endregion
        }

        private void K1302_Item_Details_Shown(object sender, EventArgs e)
        {
            if(type != 2)
            {
                textBox1.Text = item.Name_Samll;
                textBox2.Text = item.Code_Small;
                textBox3.Text = item.Name_Full;
                textBox4.Text = item.Code_Full;
                textBox5.Text = item.Unit;
                textBox6.Text = item.Weight.ToString();
                textBox7.Text = item.FixedPrice.ToString();
                textBox8.Text = item.FixedPrice.ToString();
                comboBox1.Text = Program.dbOperations.GetWarehouseAsync(item.Warehouse_Index).Name;
                if (type == 1)
                    comboBox1.Items.AddRange(Program.dbOperations.GetAllWarehousesAsync(Stack.Company_Index)
                        .Select(d => d.Name).ToArray());
                
            }
        }
    }
}
