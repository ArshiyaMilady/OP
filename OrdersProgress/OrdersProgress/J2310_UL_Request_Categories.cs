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
        List<Models.User_Level> lstULs = new List<Models.User_Level>();
        List<Models.UL_Request_Category> lstUL_CRs = new List<Models.UL_Request_Category>();

        public J2310_UL_Request_Categories(long _ul_index)
        {
            InitializeComponent();
            ul_index = _ul_index;
            Text = "    " + Program.dbOperations.GetUser_LevelAsync(ul_index).Description;
        }

        private void J2310_UL_Request_Categories_Shown(object sender, EventArgs e)
        {
            //dgvCats.DataSource = GetData();
            GetData();
            ShowData();
        }

        private void GetData()
        {
            dgvCats.DataSource = Program.dbOperations.GetAllCategoriesAsync(Stack.Company_Index);
            if(dgvCats.Rows.Count > 0) dgvCats.CurrentCell = dgvCats["Name", 0];

            dgvULs.DataSource = Program.dbOperations.GetAllUser_LevelsAsync(Stack.Company_Index)
                .Where(d=>d.Index!=ul_index).ToList();
            if (dgvULs.Rows.Count > 0) dgvULs.CurrentCell = dgvULs["Description", 0];

            foreach (Models.UL_Request_Category ulrc in Program.dbOperations
                .GetAllUL_Request_CategoriesAsync(Stack.Company_Index, ul_index))
            {
                PutData_in_dgvUL_RC(ulrc);
            }
        }

        private void PutData_in_dgvUL_RC(Models.UL_Request_Category ul_rc)
        {
            int iRow = dgvUL_RC.Rows.Add();
            DataGridViewRow row = dgvUL_RC.Rows[iRow];
            row.Tag = ul_rc;
            row.Cells["colCategory_Name"].Value = Program.dbOperations.GetCategoryAsync(ul_rc.Category_Index).Name;
            if(ul_rc.Supervisor_UL_Index>0)
                row.Cells["colSupervisor_UL_Description"].Value = Program.dbOperations.GetUser_LevelAsync(ul_rc.Supervisor_UL_Index).Description;
        }

        private void ShowData()
        {
            #region جدول دسته ها
            foreach (DataGridViewColumn col in dgvCats.Columns)
            {
                switch (col.Name)
                {
                    //case "C_B1":
                    //    col.HeaderText = "انتخاب";
                    //    col.Width = 50;
                    //    break;
                    case "Name":
                        col.HeaderText = "نام دسته";
                        col.ReadOnly = true;
                        col.Width = 200;
                        break;

                    default: col.Visible = false; break;
                }

                if (col.ReadOnly) col.DefaultCellStyle.BackColor = Color.WhiteSmoke;
                else col.DefaultCellStyle.BackColor = Color.White;
            }
            #endregion

            #region جدول سطوح کاربری

            foreach (DataGridViewColumn col in dgvULs.Columns)
            {
                switch (col.Name)
                {
                    //case "C_B1":
                    //    col.HeaderText = "انتخاب";
                    //    col.Width = 50;
                    //    break;
                    case "Description":
                        col.HeaderText = "سطح کاربری سرپرست";
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

            panel1.Enabled = false;
            Application.DoEvents();

            List<Models.Category> lstCats1 = (List<Models.Category>)dgvCats.DataSource;

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
                        //Need_Supervisor_Confirmation = cat.Need_Supervisor_Confirmation,
                        //Need_Manager_Confirmation = cat.Need_Manager_Confirmation,
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

            foreach (DataGridViewRow row in dgvCats.Rows.Cast<DataGridViewRow>())
                row.Cells["C_B1"].Value = bChooseAll;
            if (bChooseAll) btnChooseAll.Text = "عدم انتخاب همه";
            else btnChooseAll.Text = "انتخاب همه";
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            //dgvULs.Enabled = !checkBox1.Checked;
        }

        private void BtnAddNew_Click(object sender, EventArgs e)
        {
            //try
            {
                long cat_index = Convert.ToInt64(dgvCats.CurrentRow.Cells["Index"].Value);
                long supervisor_ul_index = checkBox1.Checked ? 0 : Convert.ToInt64(dgvULs.CurrentRow.Cells["Index"].Value);
                long ul_cr_index = Program.dbOperations.AddUL_Request_Category(
                    new Models.UL_Request_Category {
                        Company_Index = Stack.Company_Index,
                        User_Level_Index = ul_index,
                        Category_Index = cat_index,
                        Supervisor_UL_Index = supervisor_ul_index,
                    });

                PutData_in_dgvUL_RC(Program.dbOperations.GetUL_Request_CategoryAsync(ul_cr_index));
            }
            //catch { }
        }

        private void DgvUL_RC_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            if(dgvUL_RC.Columns[e.ColumnIndex].Name.Equals("colRemove"))
            {
                if (MessageBox.Show("آیا از حذف مورد انتخاب شده ، اطمینان دارید؟"
                    , "اخطار", MessageBoxButtons.YesNo) != DialogResult.Yes) return;

                DataGridViewRow row = dgvUL_RC.Rows[e.RowIndex];
                Models.UL_Request_Category ulrc = (Models.UL_Request_Category)row.Tag;
                Program.dbOperations.DeleteUL_Request_CategoryAsync(ulrc);
                dgvUL_RC.CurrentCell = null;
                dgvUL_RC.Rows.Remove(row);
            }
        }






        //
    }
}
