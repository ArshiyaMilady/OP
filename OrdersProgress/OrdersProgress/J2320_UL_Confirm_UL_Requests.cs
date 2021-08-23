using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// برای تأیید درخواست ها به انبار چه سطح کاربری ، سرپرست سرپرست سطح کاربری دیگر به حساب می آید
namespace OrdersProgress
{
    public partial class J2320_UL_Confirm_UL_Requests : X210_ExampleForm_Normal
    {
        long ul_index;
        List<Models.User_Level> lstUL = new List<Models.User_Level>();

        public J2320_UL_Confirm_UL_Requests(long _ul_index)
        {
            InitializeComponent();
            ul_index = _ul_index;
            Text = "   " + Program.dbOperations.GetUser_LevelAsync(ul_index).Description;
        }

        private void J2320_UL_Confirm_UL_Requests_Shown(object sender, EventArgs e)
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

                    foreach (Models.UL_Confirm_UL_Request ucur in Program.dbOperations
                        .GetAllUL_Confirm_UL_RequestsAsync(Stack.Company_Index, Stack.UserLevel_Index))
                    {
                        Models.User_Level ul = Program.dbOperations.GetUser_LevelAsync(ucur.UL_Index);
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
                ul.C_B1 = Program.dbOperations.GetAllUL_Confirm_UL_RequestsAsync(Stack.Company_Index, ul_index)
                    .Any(d => d.Supervisor_UL_Index == ul.Index);
            }


            return lstUL.Where(d=>d.Index!=ul_index).OrderByDescending(d => d.C_B1).ToList();
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

            #region حذف شود : قبلا بوده است، اما در جدول انتخاب نشده است 
            if (Program.dbOperations.GetAllUL_Confirm_UL_RequestsAsync(Stack.Company_Index, ul_index).Any())
            {
                foreach (Models.UL_Confirm_UL_Request ucur
                    in Program.dbOperations.GetAllUL_Confirm_UL_RequestsAsync(Stack.Company_Index, ul_index))
                {
                    if (!lstUL.Where(d => d.C_B1).Any(d => d.Index == ucur.UL_Index))
                    {
                        Program.dbOperations.DeleteUL_Confirm_UL_RequestAsync(ucur);
                    }
                }
            }
            #endregion

            #region اضافه شود 
            foreach (Models.User_Level ul in lstUL.Where(d => d.C_B1).ToList())
            {
                if (!Program.dbOperations.GetAllUL_Confirm_UL_RequestsAsync(Stack.Company_Index, ul_index,ul.Index).Any())
                {
                    Program.dbOperations.AddUL_Confirm_UL_Request(new Models.UL_Confirm_UL_Request
                    {
                        Company_Index = Stack.Company_Index,
                        UL_Index = ul_index,
                        Supervisor_UL_Index = ul.Index,
                    });
                }
            }
            #endregion

            MessageBox.Show("تغییرات با موفقیت ثبت گردید.");
            Close();
        }

        private void BtnDeleteAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("آیا از حذف تمام روابط سطوح کاربری و تأیید کننده های درخواستها اطمینان دارید؟"
                , "", MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            Program.dbOperations.DeleteAllUL_Confirm_UL_RequestsAsync();
        }

        // فقط یکی از سطح ها انتخاب شود
        private void DgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            if (dgvData.Columns[e.ColumnIndex].Name.Equals("C_B1"))
            {
                bool C_B1 = Convert.ToBoolean(dgvData[e.ColumnIndex, e.RowIndex].Value);
                if (!C_B1)
                {
                    foreach (DataGridViewRow row in dgvData.Rows)
                        if (row.Index != e.RowIndex)
                            row.Cells["C_B1"].Value = false;

                    //dgvData["C_B1", e.RowIndex].Value = true;
                }

            }
        }





        //
    }
}
