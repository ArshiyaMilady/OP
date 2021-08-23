using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// هر سطح کاربری درخواست کدام دسته از کالاها را می تواند از انبار داشته باشد
namespace OrdersProgress
{
    public partial class J2310_UL_Request_Categories : X210_ExampleForm_Normal
    {
        long ul_index;
        List<Models.Category> lstCats = new List<Models.Category>();

        public J2310_UL_Request_Categories(long _ul_index)
        {
            InitializeComponent();
            ul_index = _ul_index;
            Text = "    " + Program.dbOperations.GetUser_LevelAsync(ul_index).Description;
        }

        private void J2310_UL_Request_Categories_Shown(object sender, EventArgs e)
        {
            dgvData.DataSource = GetData();
            ShowData();
        }

        private List<Models.Category> GetData()
        {
            if (!lstCats.Any())
            {
                //if (Stack.UserLevel_Type == 1)
                lstCats = Program.dbOperations.GetAllCategoriesAsync(Stack.Company_Index).ToList();
            }

            foreach (Models.Category cat in lstCats)
            {
                Models.UL_Request_Category urc = null;
                if ((urc = Program.dbOperations.GetAllUL_Request_CategoriesAsync(Stack.Company_Index, ul_index)
                    .FirstOrDefault(d => d.Category_Index == cat.Index)) != null)
                {
                    cat.C_B1 = true;
                    cat.Need_Supervisor_Confirmation = urc.Need_Supervisor_Confirmation;
                    cat.Need_Manager_Confirmation = urc.Need_Manager_Confirmation;
                }
            }

            return lstCats.OrderByDescending(d => d.C_B1).ToList();
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
                    case "Name":
                        col.HeaderText = "نام دسته";
                        col.ReadOnly = true;
                        col.Width = 200;
                        break;
                    //case "Description":
                    //    col.HeaderText = "شرح";
                    //    col.ReadOnly = true;
                    //    col.Width = 200;
                    //    break;
                    case "Need_Supervisor_Confirmation":
                        col.HeaderText = "نیاز به تأیید سرپرست؟";
                        col.Width = 100;
                        break;
                    //case "Need_Manager_Confirmation":
                    //    col.HeaderText = "نیاز به تأیید مدیریت";
                    //    col.Width = 100;
                    //    break;

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

            panel1.Enabled = false;
            Application.DoEvents();

            List<Models.Category> lstCats1 = (List<Models.Category>)dgvData.DataSource;

            #region حذف شود : قبلا بوده است، اما در جدول انتخاب نشده است 
            if (Program.dbOperations.GetAllUL_Request_CategoriesAsync(Stack.Company_Index, ul_index).Any())
            {
                foreach (Models.UL_Request_Category ul_request_category
                    in Program.dbOperations.GetAllUL_Request_CategoriesAsync(Stack.Company_Index, ul_index))
                {
                    if (!lstCats1.Where(d => d.C_B1).Any(d => d.Index == ul_request_category.User_Level_Index))
                    {
                        Program.dbOperations.DeleteUL_Request_CategoryAsync(ul_request_category);
                    }
                }
            }
            #endregion

            #region موارد جدید اضافه شود 
            foreach (Models.Category cat in lstCats1.Where(d => d.C_B1).ToList())
            {
                if (!Program.dbOperations.GetAllUL_Request_CategoriesAsync(Stack.Company_Index, ul_index)
                    .Any(d => d.Category_Index == cat.Index))
                {
                    // تأیید مدیریت ، ابتدا نیاز به تأیید سرپرست دارد
                    if (cat.Need_Manager_Confirmation) cat.Need_Supervisor_Confirmation = true;
                    Program.dbOperations.AddUL_Request_Category(new Models.UL_Request_Category
                    {
                        Company_Index = Stack.Company_Index,
                        User_Level_Index = ul_index,
                        Category_Index = cat.Index,
                        Need_Supervisor_Confirmation = cat.Need_Supervisor_Confirmation,
                        Need_Manager_Confirmation = cat.Need_Manager_Confirmation,
                    }) ;
                }
            }
            #endregion

            MessageBox.Show("تغییرات با موفقیت ثبت گردید.");
            Close();
        }

        private void BtnDeleteAll_Click(object sender, EventArgs e)
        {

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







        //
    }
}
