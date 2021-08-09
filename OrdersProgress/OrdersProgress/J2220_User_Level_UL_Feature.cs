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
    public partial class J2220_User_Level_UL_Feature : X210_ExampleForm_Normal
    {
        long user_level_index;
        //List<Models.UL_Feature> lstULF_Result = new List<Models.UL_Feature>();

        public J2220_User_Level_UL_Feature(long _user_level_index)
        {
            InitializeComponent();

            user_level_index = _user_level_index;

            Text = "   " + Program.dbOperations.GetUser_LevelAsync(user_level_index).Description;
        }

        private void J2220_User_Level_UL_Feature_Shown(object sender, EventArgs e)
        {
            GetData();
            ShowData();
        }

        private void GetData()//List<Models.UL_Feature> lstULF)
        {
            List<Models.UL_Feature> lstUL_Feature = Program.dbOperations.GetAllUL_FeaturesAsync(Stack.Company_Index);
            //MessageBox.Show(Stack.UserLevel_Type.ToString());
            if(Stack.UserLevel_Type != 1)
                lstUL_Feature = lstUL_Feature.Where(d=>!d.Unique_Phrase.Substring(0,1).Equals("d")).ToList();

            if (Program.dbOperations.GetAllUser_Level_UL_FeaturesAsync(Stack.Company_Index,user_level_index).Any())
            {
                foreach (Models.User_Level_UL_Feature user_Level_ul_feature in 
                    Program.dbOperations.GetAllUser_Level_UL_FeaturesAsync(Stack.Company_Index,user_level_index))
                {
                    if (lstUL_Feature.Any(d => d.Index == user_Level_ul_feature.UL_Feature_Index))
                    {
                        Models.UL_Feature ulf = lstUL_Feature.First(d => d.Index == user_Level_ul_feature.UL_Feature_Index);
                        ulf.C_B1 = true;
                    }
                }
            }
            //if (!Stack.lstUser_ULF_UniquePhrase.Contains("ulf-k1120"))
            //    lstULF = lstULF.Where(d => !d.Unique_Phrase.Equals("ulf-k1120")).ToList();
            dgvData.DataSource = lstUL_Feature.OrderByDescending(d => d.C_B1).ThenBy(j => j.Unique_Phrase).ToList();
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
                    case "Unique_Phrase":
                        if (Stack.lstUser_ULF_UniquePhrase.Contains("dk1130"))
                        {
                            col.HeaderText = "عبارت شاخص";
                            col.ReadOnly = true;
                            col.Width = 120;
                        }
                        else col.Visible = false;
                        break;
                    case "Description":
                        col.HeaderText = "شرح";
                        col.ReadOnly = true;
                        col.Width = 500;
                        break;

                    default: col.Visible = false; break;
                }

                if (col.ReadOnly) col.DefaultCellStyle.BackColor = Color.WhiteSmoke;
                else col.DefaultCellStyle.BackColor = Color.White;
            }
            #endregion
        }


        private void BtnReturn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("آیا از ثبت تغییرات اطمینان دارید؟"
                , "", MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            List<Models.UL_Feature> lstUL_Feature = (List<Models.UL_Feature>) dgvData.DataSource;

            #region اضافه کردن امکانات جدید و حذف امکانات انتخاب نشده
            if (Program.dbOperations.GetAllUser_Level_UL_FeaturesAsync(Stack.Company_Index,user_level_index).Any())
            {
                #region اضافه شود : در جدول انتخاب شده ، اما قبلا نبوده است
                foreach (Models.UL_Feature ulf in lstUL_Feature.Where(d => d.C_B1).ToList())
                {
                    if (!Program.dbOperations.GetAllUser_Level_UL_FeaturesAsync(Stack.Company_Index,user_level_index)
                        .Any(d=>d.UL_Feature_Index == ulf.Index))
                    {
                        Program.dbOperations.AddUser_Level_UL_FeatureAsync(new Models.User_Level_UL_Feature
                        {
                            Company_Index = Stack.Company_Index,
                            UL_Feature_Unique_Phrase = ulf.Unique_Phrase,
                            User_Level_Index = user_level_index,
                            UL_Feature_Index = ulf.Index,
                        });
                    }
                }
                #endregion

                #region حذف شود : قبلا بوده است، اما در جدول انتخاب نشده است 
                foreach (Models.User_Level_UL_Feature ul_ulf 
                    in Program.dbOperations.GetAllUser_Level_UL_FeaturesAsync(Stack.Company_Index,user_level_index))
                {
                    if (!lstUL_Feature.Where(d => d.C_B1).Any(d=>d.Index == ul_ulf.UL_Feature_Index))
                    {
                        Program.dbOperations.DeleteUser_Level_UL_Feature(ul_ulf);
                    }
                }
                #endregion
            }
            else
            {
                #region اضافه شود 
                foreach (Models.UL_Feature ulf in lstUL_Feature.Where(d => d.C_B1).ToList())
                {
                    if (!Program.dbOperations.GetAllUser_Level_UL_FeaturesAsync(Stack.Company_Index,user_level_index)
                        .Any(d => d.UL_Feature_Index == ulf.Index))
                    {
                        Program.dbOperations.AddUser_Level_UL_FeatureAsync(new Models.User_Level_UL_Feature
                        {
                            Company_Index = Stack.Company_Index,
                            UL_Feature_Unique_Phrase = ulf.Unique_Phrase,
                            User_Level_Index = user_level_index,
                            UL_Feature_Index = ulf.Index,
                        });
                    }
                }
                #endregion
            }
            #endregion

            MessageBox.Show("تغییرات با موفقیت ثبت گردید.");
            Close();
        }

        private void BtnDeleteAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("آیا از حذف تمام روابط سطوح کاربری و امکانات اطمینان دارید؟"
                , "", MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            Program.dbOperations.DeleteAllUser_Level_UL_FeaturesAsync();
        }

        bool bChooseAll = false;
        private void BtnChooseAll_Click(object sender, EventArgs e)
        {
            bChooseAll = !bChooseAll;

            foreach (DataGridViewRow row in dgvData.Rows.Cast<DataGridViewRow>())
                row.Cells["C_B1"].Value = bChooseAll;
            if (bChooseAll) btnChooseAll.Text = "عدم انتخاب همه";
            else btnChooseAll.Text = "انتخاب همه";
        }
    }
}
