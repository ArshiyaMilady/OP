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
    public partial class J2110_ChangePassword : X210_ExampleForm_Normal
    {
        long user_index;

        public J2110_ChangePassword(long _user_index)
        {
            InitializeComponent();

            user_index = _user_index;
        }

      
        private void J2110_ChangePassword_Shown(object sender, EventArgs e)
        {

        }

        #region مشاهده رمز ها
        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            textBox1.UseSystemPasswordChar = false;
        }

        private void PictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            textBox2.UseSystemPasswordChar = false;
        }

        private void PictureBox3_MouseDown(object sender, MouseEventArgs e)
        {
            textBox3.UseSystemPasswordChar = false;
        }

     
        private void PictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            textBox1.UseSystemPasswordChar = true;
        }

        private void PictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            textBox2.UseSystemPasswordChar = true;
        }

        private void PictureBox3_MouseUp(object sender, MouseEventArgs e)
        {
            textBox3.UseSystemPasswordChar = true;
        }
        #endregion

        private void BtnReturn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            CryptographyProcessor cryptographyProcessor = new CryptographyProcessor();
            Models.User user = Program.dbOperations.GetUserAsync(user_index);

            #region خطایابی
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("رمز فعلی؟", "خطا");
                return;
            }

            if (!user.Password.Equals(cryptographyProcessor.GenerateHash(textBox1.Text,Stack.Standard_Salt)))
            {
                MessageBox.Show("رمز فعلی نادرست است", "خطا");
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("رمز جدید؟", "خطا");
                return;
            }
            else if (textBox2.Text.Length < 4)
            {
                MessageBox.Show("رمز جدید باید حداقل دارای 4 کاراکتر باشد","خطا");
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("تکرار رمز جدید؟", "خطا");
                return;
            }

            if(!textBox2.Text.Equals(textBox3.Text))
            {
                MessageBox.Show("رمز جدید با تکرار آن یکسان نمی باشد", "خطا");
                return;
            }
            #endregion

            user.Password = cryptographyProcessor.GenerateHash(textBox2.Text, Stack.Standard_Salt);
            Program.dbOperations.UpdateUserAsync(user);

            MessageBox.Show("رمز ورود با موفقیت تغییر کرد");
            Close();
        }
    }
}
