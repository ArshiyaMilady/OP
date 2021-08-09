﻿using System;
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
    public partial class J2202_UL_See_ULs : X210_ExampleForm_Normal
    {
        long main_ul_index;
        List<Models.User_Level> lstUL = new List<Models.User_Level>();

        public J2202_UL_See_ULs(long _main_ul_index)
        {
            InitializeComponent();

            main_ul_index = _main_ul_index;
            Text = "   " + Program.dbOperations.GetUser_LevelAsync(main_ul_index).Description;
        }

        private void J2202_UL_See_ULs_Shown(object sender, EventArgs e)
        {
            dgvData.DataSource = GetData();
            ShowData();
        }

        private List<Models.User_Level> GetData()
        {
            if (!lstUL.Any())
            {
                if (Stack.UserLevel_Type == 0)
                {
                    //MessageBox.Show(Stack.UserLevel_Type.ToString());

                    foreach (Models.UL_See_UL ul_see_ul in Program.dbOperations
                        .GetAllUL_See_ULsAsync(Stack.Company_Index, Stack.UserLevel_Index))
                    {
                        Models.User_Level ul = Program.dbOperations.GetUser_LevelAsync(ul_see_ul.UL_Index);
                        lstUL.Add(ul);
                    }
                }
                else
                {
                    if (Stack.UserLevel_Type == 1)
                        lstUL = Program.dbOperations.GetAllUser_LevelsAsync(Stack.Company_Index).ToList();
                    else
                        lstUL = Program.dbOperations.GetAllUser_LevelsAsync(Stack.Company_Index)
                            .Where(d => d.Type == 0).ToList();
                }
            }

            foreach (Models.User_Level ul in lstUL)
            {
                //if (Program.dbOperations.GetAllUL_See_ULsAsync(Stack.Company_Index, main_ul_index).Any())
                //    ul.C_B1 = true;

                ul.C_B1 = Program.dbOperations.GetAllUL_See_ULsAsync(Stack.Company_Index, main_ul_index)
                    .Any(d=>d.UL_Index == ul.Index);

            }


            return lstUL.OrderByDescending(d => d.C_B1).ToList();
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
                    case "Description":
                        col.HeaderText = "شرح";
                        col.ReadOnly = true;
                        col.Width = 200;
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

            List<Models.User_Level> lstUL = (List<Models.User_Level>)dgvData.DataSource;

            #region اضافه کردن سطح کاربری جدید و حذف سطوح انتخاب نشده
            if (Program.dbOperations.GetAllUL_See_ULsAsync(Stack.Company_Index, main_ul_index).Any())
            {
                #region اضافه شود : در جدول انتخاب شده ، اما قبلا نبوده است
                foreach (Models.User_Level ul in lstUL.Where(d => d.C_B1).ToList())
                {
                    if (!Program.dbOperations.GetAllUL_See_ULsAsync(Stack.Company_Index, main_ul_index)
                        .Any(d => d.UL_Index == ul.Index))
                    {
                        Program.dbOperations.AddUL_See_UL(new Models.UL_See_UL
                        {
                            Company_Index = Stack.Company_Index,
                            MainUL_Index = main_ul_index,
                            UL_Index = ul.Index,
                        });
                    }
                }
                #endregion

                #region حذف شود : قبلا بوده است، اما در جدول انتخاب نشده است 
                foreach (Models.UL_See_UL ul_see_ul
                    in Program.dbOperations.GetAllUL_See_ULsAsync(Stack.Company_Index, main_ul_index))
                {
                    if (!lstUL.Where(d => d.C_B1).Any(d => d.Index == ul_see_ul.UL_Index))
                    {
                        Program.dbOperations.DeleteUL_See_ULAsync(ul_see_ul);
                    }
                }
                #endregion
            }
            else
            {
                #region اضافه شود 
                foreach (Models.User_Level ul in lstUL.Where(d => d.C_B1).ToList())
                {
                    if (!Program.dbOperations.GetAllUL_See_ULsAsync(Stack.Company_Index, main_ul_index)
                        .Any(d => d.UL_Index == ul.Index))
                    {
                        Program.dbOperations.AddUL_See_UL(new Models.UL_See_UL
                        {
                            Company_Index = Stack.Company_Index,
                            MainUL_Index = main_ul_index,
                            UL_Index = ul.Index,
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
            if (MessageBox.Show("آیا از حذف تمام روابط سطوح کاربری اطمینان دارید؟"
                , "", MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            Program.dbOperations.DeleteAllUL_See_ULsAsync();
        }

    }
}
