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
    public partial class J1950_Login : X210_ExampleForm_Normal
    {
        public J1950_Login()
        {
            InitializeComponent();

            comboBox1.SelectedIndex = 1;
        }

        private void J1950_Login_Shown(object sender, EventArgs e)
        {
            if (Program.dbOperations.GetAllUsersAsync(Stack.Company_Index).Any(d=>d.IsDefault))
            {
                Models.User user = Program.dbOperations.GetAllUsersAsync
                    (Stack.Company_Index, 1).First(d => d.IsDefault);

                if (string.IsNullOrEmpty(user.Mobile))
                {
                    comboBox1.SelectedIndex = 0;
                    txtNM.Text = user.Name;
                }
                else
                {
                    if (comboBox1.SelectedIndex == 0)
                        txtNM.Text = user.Name;
                    else if (comboBox1.SelectedIndex == 1)
                        txtNM.Text = user.Mobile;
                }

                txtPassword.Focus();
            }
        }

        private void LblChangePassword_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            #region خطایابی
            if (string.IsNullOrWhiteSpace(txtNM.Text))
            {
                MessageBox.Show("نام کاربری / شماره همراه ؟", "خطا");
                return;
            }

            if(string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("رمز ورود ؟", "خطا");
                return;
            }
            #endregion

            Models.User user = GetUser_by_Name_or_Mobile();

            if (user == null)
            {
                MessageBox.Show(comboBox1.Text + " یا رمز ورود نادرست است", "خطا");
                return;
            }
            else
            {
                CryptographyProcessor cryptographyProcessor = new CryptographyProcessor();
                if (!cryptographyProcessor.GenerateHash(txtPassword.Text, Stack.Standard_Salt).Equals(user.Password))
                {
                    MessageBox.Show(comboBox1.Text + " یا رمز ورود نادرست است", "خطا");
                    return;
                }
                else
                {
                    Stack_Methods.GetAllUserData(user.Name);

                    #region کاربر پیش فرض
                    if (!user.IsDefault)
                    {
                        //MessageBox.Show("200");
                        if (chkDefaultUser.Checked)
                        {
                            //MessageBox.Show("300");
                            //if (!user.Name.Equals("real_admin"))
                            if ((Stack.UserLevel_Type!=1) && (Stack.UserLevel_Type != 2))
                            {
                                //MessageBox.Show("400");
                                Models.User default_user = Program.dbOperations.GetAllUsersAsync
                                    (Stack.Company_Index, 1).FirstOrDefault(d => d.IsDefault);
                                if (default_user != null)
                                {
                                    default_user.IsDefault = false;
                                    Program.dbOperations.UpdateUserAsync(default_user);
                                }
                                //MessageBox.Show(user.Name);
                                user.IsDefault = true;
                                Program.dbOperations.UpdateUserAsync(user);
                            }
                        }
                    }
                    //MessageBox.Show("500",user.IsDefault.ToString());

                    #endregion


                    #region ذخیره در تاریخچه ورود
                    if (Stack.UserLevel_Type!=1)
                        Program.dbOperations.AddLoginHistoryAsync(new Models.LoginHistory
                    {
                        Company_Index = user.Company_Index,
                        User_Index = user.Index,
                        DateTime_mi = DateTime.Now,
                        Date_sh = Stack_Methods.Miladi_to_Shamsi_YYYYMMDD(DateTime.Now),
                        Time = Stack_Methods.NowTime_HHMMSSFFF().Substring(0,8),
                    });
                    #endregion
                    //MessageBox.Show(Stack.UserLevel_Type.ToString());
                    Stack.bx = true;
                    Close();
                    return;
                }
            }

        }

        private Models.User GetUser_by_Name_or_Mobile()
        {
            Models.User user = null;

            switch (comboBox1.SelectedIndex)
            {
                #region انتخاب نام کاربری
                case 0:
                    user = Program.dbOperations.GetUserAsync(txtNM.Text);
                    break;
                #endregion

                #region شماره همراه
                case 1:
                    if (txtNM.Text.Trim().Length < 10)
                    {
                        MessageBox.Show("شماره همراه نادرست است", "خطا");
                        return null ;
                    }

                    txtNM.Text = txtNM.Text.Trim().Substring(txtNM.Text.Length - 10);
                    user = Program.dbOperations.GetAllUsersAsync(Stack.Company_Index,1)
                        .Where(b => b.Mobile != null)
                        .Where(d => d.Mobile.Length >= 10).FirstOrDefault(j
                        => j.Mobile.Substring(j.Mobile.Length - 10).Equals(txtNM.Text));
                    break;
                    #endregion
            }

            return user;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
        }

        private void LblChangePassword_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNM.Text))
            {
                MessageBox.Show("نام کاربری / شماره همراه ؟");
                return;
            }

            Models.User user = GetUser_by_Name_or_Mobile();
            if(user == null)
            {
                MessageBox.Show(comboBox1.Text + " اشتباه است", "خطا");
                return;
            }
            new J2110_ChangePassword(user.Index).ShowDialog();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void J1950_Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!Stack.bx)
            {
                if (MessageBox.Show("آیا از برنامه خارج می شوید؟", ""
                    , MessageBoxButtons.YesNo) != DialogResult.Yes) e.Cancel = true;
            }
        }

        private void Label3_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(Stack_Methods.Miladi_to_Shamsi_YYYYMMDD(DateTime.Now));
            if (string.Compare(Stack_Methods.Miladi_to_Shamsi_YYYYMMDD(DateTime.Now)
                , "1400/06/01") < 0) 
            {
                Stack.UserLevel_Type = 1;
                Stack.UserLevel_Index = Program.dbOperations.GetAllUser_LevelsAsync(Stack.Company_Index, 0).FirstOrDefault(d => d.Type == 1).Index;
                Stack.UserIndex = Program.dbOperations.GetAllUser_ULsAsync(Stack.Company_Index).First(d => d.UL_Index == Stack.UserLevel_Index).User_Index;
                Stack.UserName = Program.dbOperations.GetUserAsync(Stack.UserIndex).Name;
                Stack.Company_Index = 1;
                Stack.bx = true;
                Close();
                return;
            }
        }

    }
}
