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

            Stack.lx = -1;  // شناسه کالا
            Stack.bx = false;   // آیا تغییری اتفاق افتاده است؟
            type = _type;
            #region تنظیمات کنترلها با توجه به نوع استفاده از فرم
            if (type == 2)     // add
            {
                item = new Models.Item();
                btnSave.Text = "ثبت کالا و بستن";
            }
            else
            {
                // اگر از این کالا در حواله ای استفاده شده باشد، نباید امکان تغییر داشته باشد
                if (Program.dbOperations.GetWarehouse_Remittance_RowAsync
                    (_item.Code_Small, Stack.Company_Index) != null)
                        type = 0;

                item = _item;
                Text = item.Name_Samll;
            }

            if(type != 0)   // Add or Edit
            {
                //panel2.Enabled = true;
                foreach (Control c in panel2.Controls.Cast<Control>()
                    .Where(d => d.Name.Substring(0, 4).Equals("text")).ToList())
                {
                    TextBox txt = (TextBox)c;
                    txt.ReadOnly = false;
                }

                cmbCategories.Enabled = true;
                cmbWarehouses.Enabled = true;
                chkEnable.Enabled = true;
                chkSalable.Enabled = true;
                chkBookable.Enabled = true;
                btnSave.Visible = true;
            }
            #endregion
        }

        private void K1302_Item_Details_Shown(object sender, EventArgs e)
        {
            cmbWarehouses.Items.AddRange(Program.dbOperations.GetAllWarehousesAsync(Stack.Company_Index)
                 .Select(d => d.Name).ToArray());
            cmbCategories.Items.AddRange(Program.dbOperations.GetAllCategoriesAsync(Stack.Company_Index)
                 .Select(d => d.Name).ToArray());

            if (type == 2)  // add
            {
                cmbWarehouses.SelectedIndex = 0;
                cmbCategories.SelectedIndex = 0;
                //chkEnable.Checked = true;
                //chkSalable.Checked = true;
            }
            else
            {
                textBox1.Text = item.Name_Samll;
                textBox2.Text = item.Code_Small;
                textBox3.Text = item.Name_Full;
                textBox4.Text = item.Code_Full;
                textBox5.Text = item.Unit;
                textBox6.Text = item.Weight.ToString();
                textBox7.Text = item.FixedPrice.ToString();
                textBox8.Text = item.SalesPrice.ToString();
                textBox9.Text = item.Wh_OrderPoint.ToString();
                chkEnable.Checked = item.Enable;
                chkSalable.Checked = item.Salable;
                chkBookable.Checked = item.Bookable;

                cmbWarehouses.Text = Program.dbOperations.GetWarehouseAsync(item.Warehouse_Index).Name;
                cmbCategories.Text = Program.dbOperations.GetCategoryAsync(item.Category_Index).Name;
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
            bool bEverythingOK = true;

            #region خطایابی
            if(string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("کد کالا نباید خالی باشد", "خطا");
                bEverythingOK = false;
            }
            if(string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("نام کالا نباید خالی باشد", "خطا");
                bEverythingOK = false;
            }

            if(type==1)
            {
                if (Program.dbOperations.GetAllItemsAsync(Stack.Company_Index).Where(d1=> d1.Index != item.Index)
                    .Any(j => j.Code_Small.ToLower().Equals(textBox1.Text.ToLower())))
                {
                    MessageBox.Show("کد کالا قبلا استفاده شده است", "خطا");
                    bEverythingOK = false;
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
                    else bEverythingOK = false;
                }
                #endregion
            }

            if (string.IsNullOrWhiteSpace(textBox6.Text)) textBox6.Text = "0";
            else if (!double.TryParse(textBox6.Text,out double d6))
            {
                MessageBox.Show("وزن کالا باید به صورت عددی قابل قبول وارد شود", "خطا");
                bEverythingOK = false;
            }

            if (string.IsNullOrWhiteSpace(textBox9.Text)) textBox9.Text = "0";
            else if (!double.TryParse(textBox9.Text,out double d9))
            {
                MessageBox.Show("نقطه سفارش کالا باید به صورت عددی قابل قبول وارد شود", "خطا");
                bEverythingOK = false;
            }
            #endregion

            if(bEverythingOK)
                bEverythingOK = MessageBox.Show("آیا از ثبت تغییرات اطمینان دارید؟", ""
                , MessageBoxButtons.YesNo) == DialogResult.Yes;

            if (bEverythingOK)
            {
                item.Company_Index = Stack.Company_Index;
                item.Enable = true;
                item.Name_Samll = textBox1.Text;
                item.Code_Small = textBox2.Text;
                item.Name_Full = textBox3.Text;
                item.Code_Full = textBox4.Text;
                if (string.IsNullOrWhiteSpace(textBox5.Text)) textBox5.Text = "عدد";
                item.Unit = textBox5.Text;
                item.Weight = Convert.ToDouble(textBox6.Text);
                if (string.IsNullOrWhiteSpace(textBox7.Text)) textBox7.Text = "0";
                item.FixedPrice = Convert.ToInt64(textBox7.Text);
                if (string.IsNullOrWhiteSpace(textBox8.Text)) textBox8.Text = "0";
                item.SalesPrice = Convert.ToInt64(textBox8.Text);
                item.Warehouse_Index = Program.dbOperations.GetWarehouseAsync(Stack.Company_Index, cmbWarehouses.Text).Index;
                item.Wh_OrderPoint = Convert.ToDouble(textBox9.Text);
                item.Enable = chkEnable.Checked;
                item.Salable = chkSalable.Checked;
                item.Bookable = chkBookable.Checked;

                pictureBox1.Visible = true;
                Application.DoEvents();
                if (type == 1)  // edit
                {
                    Program.dbOperations.UpdateItem(item);
                }
                else if (type == 2) // add
                {
                    Stack.lx = Program.dbOperations.AddItem(item);
                }

                Stack.bx = true;
                timer1.Enabled = true;
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            timer1.Enabled = false;
            Close();
        }
    }
}
