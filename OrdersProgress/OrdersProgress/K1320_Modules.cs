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
    public partial class K1320_Modules : X210_ExampleForm_Normal
    {
        Models.Item module;

        public K1320_Modules(Models.Item _item)
        {
            InitializeComponent();

            module = _item;
            Text = "تعیین ساختار قطعه :    "+ module.Code_Small + " / " + module.Name_Samll;
        }

        private void K1320_Modules_Shown(object sender, EventArgs e)
        {
            cmbST_Name.SelectedIndex = 0;
            cmbST_SmallCode.SelectedIndex = 0;

            Program.dbOperations.Items_Reset_Values(Stack.Company_Index);
            ShowData();

            Application.DoEvents();
            progressBar1.Visible = false;
            panel1.Enabled = true;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            #region خطا : اگر تعداد ، صفر باشد
            bool bError_ZeroQuantity = false;
            foreach (Models.Item item in Program.dbOperations.GetAllItemsAsync(Stack.Company_Index, 1, 100)
                .Where(d => d.C_B1).ToList())
            {
                if (item.Quantity <= 0)
                {
                    bError_ZeroQuantity = true;
                    break;
                }
            }
            if (bError_ZeroQuantity)
            {
                MessageBox.Show("تمام مقادیر انتخاب شده باید دارای تعدادی بیشتر از صفر باشند", "خطا");
                return;
            }
            #endregion

            if (MessageBox.Show("آیا از ثبت تغییرات اطمینان دارید؟", "", MessageBoxButtons.YesNo)
                == DialogResult.No) return;

            panel1.Enabled = false;
            Application.DoEvents();

            module.Module = true;
            Program.dbOperations.UpdateItemAsync(module);

            List<Models.Module> lstModels = Program.dbOperations.GetAllModulesAsync(Stack.Company_Index,1, module.Code_Small);
            // غیرفعال کردن تمام رابطه های قبلی
            foreach (Models.Module md in lstModels)
            {
                md.Enable = false;
                Program.dbOperations.UpdateModuleAsync(md);
            }

            foreach(Models.Item item in Program.dbOperations.GetAllItemsAsync(Stack.Company_Index, 1,100).Where(d=>d.C_B1).ToList())
            {
                Program.dbOperations.AddModuleAsync(new Models.Module
                {
                    Company_Index = Stack.Company_Index,
                    Item_Index = item.Index,
                    Module_Code_Small = module.Code_Small,
                    Item_Code_Small = item.Code_Small,
                    Quantity = item.Quantity,
                    Enable = true,
                });
            }

            //dgvData.Sort(dgvData.Columns["C_B1"], ListSortDirection.Descending);
            ShowData(false);
            Application.DoEvents();
            panel1.Enabled = true;
            MessageBox.Show("ثبت ساختار با موفقیت انجام شد");



            #region ---
            /*
            List<Models.Module> lstModels = Program.dbOperations.GetAllModulesAsync(module.Code_Small);
            //foreach (Models.Module md in lstModels)
            //    Program.dbOperations.DeleteModule(md);

            List<Models.Item> lstItems = (List<Models.Item>)dgvData.DataSource;
            #region حذف کردن موارد تیک نخورده
            if (lstItems.Any(d => !d.C_B1))
            {
                foreach (Models.Item it in lstItems.Where(d => !d.C_B1).ToList())
                {
                    // اگر کالا در زیرساخت ماژول بود، آنرا حذف کن
                    if (lstModels.Any(d => d.Item_Code_Small.ToLower().Equals(it.Code_Small.ToLower())))
                        Program.dbOperations.DeleteModuleAsync(lstModels
                            .First(d => d.Item_Code_Small.ToLower().Equals(it.Code_Small.ToLower())));
                }
            }
            #endregion

            #region اضافه کردن موارد تیک خورده
            if (lstItems.Any(d => d.C_B1))
            {
                foreach (Models.Item it in lstItems.Where(d => d.C_B1).ToList())
                {
                    // اگر کالا در زیرساخت ماژول نبود، آنرا اضافه کن
                    if (!lstModels.Any(d => d.Item_Code_Small.ToLower().Equals(it.Code_Small.ToLower())))
                    {
                        Program.dbOperations.AddModuleAsync(new Models.Module
                        {
                            Module_Code_Small = module.Code_Small,
                            Item_Code_Small = it.Code_Small,
                        });
                    }
                }
                if (!module.Module)
                {
                    module.Module = true;
                    Program.dbOperations.UpdateItemAsync(module);
                }
            }
            #endregion



            lstModels = Program.dbOperations.GetAllModulesAsync(module.Code_Small);
            if (lstModels.Any() && !module.Module)
            {
                module.Module = true;
                Program.dbOperations.UpdateItemAsync(module);
            }
            else if(!lstModels.Any() && module.Module)
            {
                if (module.Module)
                {
                    module.Module = false;
                    Program.dbOperations.UpdateItemAsync(module);
                }
            }

            Stack.bx = true;

            ShowData((List<Models.Item>)dgvData.DataSource, false);

            Application.DoEvents();
            panel1.Enabled = true;
            MessageBox.Show("ثبت ساختار با موفقیت انجام شد");
            */
            #endregion
        }

        // NeedToCorrect_C_B1 : آیا نیاز به بروز رسانی پارامتر «سی-بی 1» می باشد؟
        private List<Models.Item> GetData(List<Models.Item> lstItems = null, bool NeedToCorrect_C_B1 = true)
        {
            if (lstItems == null) lstItems = Program.dbOperations.GetAllItemsAsync(Stack.Company_Index, 1, 100);
                      //.Where(d => d.Index != module.Index).ToList();

            #region بروز رسانی پارامتر «سی-بی 1»
            if (NeedToCorrect_C_B1)
            {
                // کالاهایی که زیرساخت کالای وارد شده به فرم می باشند
                List<Models.Module> lstModels = Program.dbOperations.GetAllModulesAsync(Stack.Company_Index,1, module.Code_Small);

                if (lstModels.Any())
                {
                    foreach (Models.Item item1 in lstItems)
                    {
                        //if (lstModels.Any(d => d.Item_Code_Small.ToLower().Equals(item1.Code_Small.ToLower())))
                        if (lstModels.Any(d => d.Item_Code_Small.ToLower().Equals(item1.Code_Small.ToLower())))
                        {
                            item1.C_B1 = true;
                            item1.Quantity = lstModels.First(d => d.Item_Code_Small.ToLower().Equals(item1.Code_Small.ToLower())).Quantity;
                            Program.dbOperations.UpdateItemAsync(item1);
                        }
                        Application.DoEvents();
                    }
                }
            }
            #endregion

            if (chkJustShowSubstructure.Checked)
            {
                return lstItems.Where(d => d.C_B1).OrderByDescending(d => d.C_B1).ToList();
            }
            else return lstItems.OrderByDescending(d => d.C_B1).ToList();
        }

        private void ShowData(bool ChangeHeaderTexts = true)
        {
            List<Models.Item> lstItems = GetData().Where(d => d.C_B1).ToList();
            // اگر این کالا دارای زیرساختی نباشد
            if(chkJustShowSubstructure.Checked && !lstItems.Any())
            {
                MessageBox.Show("در حال حاضر برای این کالا، زیرساختی تعریف نشده است");
                chkJustShowSubstructure.Checked = false;
                lstItems = GetData(null, false);
            }

            dgvData.DataSource = lstItems;

            #region ترجمه سر ستونها و مخفی کردن بعضی ستونها
            if (ChangeHeaderTexts)
            {
                foreach (DataGridViewColumn col in dgvData.Columns)
                {
                    switch (col.Name)
                    {

                        case "Code_Small":
                            col.HeaderText = "کد";
                            col.ReadOnly = true;
                            col.DefaultCellStyle.BackColor = Color.LightGray;
                            break;
                        case "Name_Samll":
                            col.HeaderText = "نام کالا";
                            col.ReadOnly = true;
                            col.DefaultCellStyle.BackColor = Color.LightGray;
                            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                            break;
                        case "C_B1":
                            col.HeaderText = "انتخاب";
                            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                            break;
                        case "Quantity":
                            col.HeaderText = "تعداد";
                            break;
                        //case "Unit":
                        //    col.HeaderText = "واحد";
                        //    col.ReadOnly = true;
                        //    col.DefaultCellStyle.BackColor = Color.LightGray;
                        //    col.Width = 50;
                        //    break;                       //case "Weight":
                        //    col.HeaderText = "وزن(کیلوگرم)";
                        //    break;
                        default: col.Visible = false; break;
                    }
                }
            }
            #endregion
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            List<Models.Item> lstItems = GetData(null,false);
            //List<Models.Item> lstItems = Program.dbOperations.GetAllItemsAsync(Stack.Company_Index,1).Where(d => d.Index != module.Index).ToList();

            if (string.IsNullOrWhiteSpace(txtST_Name.Text)
                && string.IsNullOrWhiteSpace(txtST_SmallCode.Text))
            {
                //ShowData(false);
                return;
            }


            panel1.Enabled = false;
            dgvData.Visible = false;
            Application.DoEvents();

            foreach (Control c in groupBox1.Controls)
            {
                //MessageBox.Show(c.Text);
                if (c.Name.Length > 4)
                {
                    if (c.Name.Substring(0, 5).Equals("txtST"))
                        if (!string.IsNullOrWhiteSpace(c.Text))
                        {
                            lstItems = SearchThis(lstItems, c.Name);
                            //MessageBox.Show(c.Text, c.Name);
                            //MessageBox.Show(lstItems.Count.ToString(), c.Name);
                            if ((lstItems == null) || !lstItems.Any()) break;
                        }
                }
            }

            //if (lstItems != null)
            //{
            //    //MessageBox.Show(lstItems.Count.ToString());
            //    if (radModule.Checked) lstItems = lstItems.Where(d => d.Module).ToList();
            //    else if (radNotModule.Checked) lstItems = lstItems.Where(d => !d.Module).ToList();
            //}

            dgvData.DataSource = GetData(lstItems,false);
            //ShowData(lstItems);//, false);

            Application.DoEvents();
            panel1.Enabled = true;
            //dgvData.Visible = true;
            dgvData.Show();
            //dgvData.Size = new Size(dgvData.Width+10, dgvData.Height+10);
            panel1.Update();
        }

        // جستجوی موردی
        private List<Models.Item> SearchThis(List<Models.Item> lstItems1, string TextBoxName)
        {
            switch (TextBoxName)
            {
                case "txtST_SmallCode":
                    switch (cmbST_SmallCode.SelectedIndex)
                    {
                        case 0:
                            return lstItems1.Where(d => d.Code_Small.ToLower().Contains(txtST_SmallCode.Text.ToLower())).ToList();
                        case 1:
                            return lstItems1.Where(d => d.Code_Small.ToLower().StartsWith(txtST_SmallCode.Text.ToLower())).ToList();
                        case 2:
                            return lstItems1.Where(d => d.Code_Small.ToLower().Equals(txtST_SmallCode.Text.ToLower())).ToList();
                        default: return lstItems1;
                    }
                //break;
                case "txtST_Name":
                    switch (cmbST_Name.SelectedIndex)
                    {
                        case 0:
                            return lstItems1.Where(d => d.Name_Samll.ToLower().Contains(txtST_Name.Text.ToLower())).ToList();
                        case 1:
                            return lstItems1.Where(d => d.Name_Samll.ToLower().StartsWith(txtST_Name.Text.ToLower())).ToList();
                        case 2:
                            return lstItems1.Where(d => d.Name_Samll.ToLower().Equals(txtST_Name.Text.ToLower())).ToList();
                        default: return lstItems1;
                    }
            }

            return null;
        }

        private void BtnReturn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void DgvData_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // Don't throw an exception when we're done.
            e.ThrowException = false;
            if (dgvData.Columns[dgvData.CurrentCell.ColumnIndex].Name.Equals("Quantity"))
            {
                MessageBox.Show("لطفا «تعداد» را به صورت عدد وارد نمایید.", "خطای نوع داده", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("خطای نوع داده", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // If this is true, then the user is trapped in this cell.
            e.Cancel = false;
        }

        private void DgvData_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            long index = Convert.ToInt64(dgvData["Index", e.RowIndex].Value);

            Models.Item item = Program.dbOperations.GetItemAsync(index);
            switch (dgvData.Columns[e.ColumnIndex].Name)
            {
                case "Quantity":
                    item.Quantity = Convert.ToDouble(dgvData["Quantity", e.RowIndex].Value);
                    break;
                case "C_B1":
                    item.C_B1 = Convert.ToBoolean(dgvData["C_B1", e.RowIndex].Value);
                    break;
            }
            Program.dbOperations.UpdateItemAsync(item);
        }

        private void RadModule_CheckedChanged(object sender, EventArgs e)
        {
            if (radModule.Checked) dgvData.DataSource = GetData(null,false).Where(d => d.Module).ToList();
            else if (radNotModule.Checked) dgvData.DataSource = GetData(null, false).Where(d => !d.Module).ToList();
            else dgvData.DataSource = GetData(null, false);
        }

        private void BtnShowAll_Click(object sender, EventArgs e)
        {
            RadModule_CheckedChanged(null, null);
        }

        private void TxtST_Enter(object sender, EventArgs e)
        {
            AcceptButton = btnSearch;
        }

        private void TxtST_Leave(object sender, EventArgs e)
        {
            AcceptButton = null;
        }

        private void ChkJustShowSubstructure_CheckedChanged(object sender, EventArgs e)
        {
            dgvData.DataSource = GetData(null, false);
        }

        private void BtnFlowchart_Click(object sender, EventArgs e)
        {
            if (Program.dbOperations.GetAllModulesAsync(Stack.Company_Index,1, module.Code_Small).Any())
                new K1322_Module_Diagram(module.Code_Small).ShowDialog();
            else
                MessageBox.Show("برای این کالا ساختاری تعریف نشده است");
        }
    }
}
