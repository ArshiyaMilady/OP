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
                btnSave.Text = "ثبت کالا";
                btnSave.Visible = true;
            }
            else
            {
                item = _item;
                Text = item.Name_Samll;

                if (_type == 1)     // edit
                {
                    //panel2.Enabled = true;
                    foreach(Control c in panel2.Controls.Cast<Control>()
                        .Where(d=>d.Name.Substring(0,4).Equals("text")).ToList())
                    {
                        TextBox txt = (TextBox)c;
                        txt.ReadOnly = false;
                    }
                    comboBox1.Enabled = true;
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

                comboBox1.Items.AddRange(Program.dbOperations.GetAllWarehousesAsync(Stack.Company_Index)
                        .Select(d => d.Name).ToArray());
                comboBox1.Text = Program.dbOperations.GetWarehouseAsync(item.Warehouse_Index).Name;
            }
        }

        private void BtnReturn_Click(object sender, EventArgs e)
        {
            Close();
        }

        // قیمتها
        private void TextBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // وزن
        private void TextBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            #region خطایابی
            if(string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("کد کالا نباید خالی باشد", "خطا");
                return;
            }
            if(string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("نام کالا نباید خالی باشد", "خطا");
                return;
            }

            if(type==1)
            {
                if (Program.dbOperations.GetAllItemsAsync(Stack.Company_Index).Where(d => d.Index != item.Index)
                    .Any(j => j.Code_Small.ToLower().Equals(textBox1.Text.ToLower())))
                {
                    MessageBox.Show("کد کالا قبلا استفاده شده است", "خطا");
                    return;
                }
            }
            else if (type == 2)
            {
                #region درصورت اضافه کردن کالا، کد کالا تکراری باشد، آیا ورژن جدیدی کالا اضافه گردد؟
                if (Program.dbOperations.GetAllItemsAsync(Stack.Company_Index)
                    .Any(j => j.Code_Small.ToLower().Equals(textBox1.Text.ToLower())))
                {
                    bool bSaveChange = MessageBox.Show("کد قبلا استفاده شده است. آیا مایل به تعریف ورژن جدیدی از این کد می باشید؟"
                                        + "\n" + "دقت نمایید با تأیید این عمل، کدهای قبلی غیر فعال شده، و تمام ارتباطات از این پس از ورژن جدید استفاده می شوند"
                                        , "اخطار", MessageBoxButtons.YesNo) == DialogResult.Yes;
                    if (bSaveChange)
                    {
                        foreach (Models.Item it1 in Program.dbOperations.GetAllItemsAsync(Stack.Company_Index)
                            .Where(j => j.Code_Small.ToLower().Equals(textBox1.Text.ToLower())).ToList())
                        {
                            it1.Enable = false;
                            Program.dbOperations.UpdateItemAsync(it1);
                        }
                    }
                }
                #endregion
            }

            if (!double.TryParse(textBox6.Text,out double d))
            {
                MessageBox.Show("وزن کالا باید به صورت عددی قابل قبول وارد شود", "خطا");
                return;
            }
            #endregion

            if (type == 1)  // edit
            {
                item.Name_Samll = textBox1.Text;
                item.Code_Small = textBox2.Text;
                item.Name_Full = textBox3.Text;
                item.
            }
            else if (type == 2) // add
            {
                item = new Models.Item();

            }
        }
    }
}
