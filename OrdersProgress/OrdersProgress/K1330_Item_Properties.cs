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
    public partial class K1330_Item_Properties : X210_ExampleForm_Normal
    {
        Models.Item item;
        List<Models.Property> lstPrs = new List<Models.Property>();

        public K1330_Item_Properties(Models.Item _item)
        {
            InitializeComponent();

            item = _item;
            Text = "  تعیین مشخصات قطعه :    " + item.Code_Small + " - " + item.Name_Samll;
        }

        private void K1330_Item_Properties_Shown(object sender, EventArgs e)
        {
            Application.DoEvents();

            cmbST_Name.SelectedIndex = 0;
            cmbST_Description.SelectedIndex = 0;

            //Program.dbOperations.Properties_Reset_Values();
            dgvData.DataSource = GetData();
            ShowData();
            Application.DoEvents();

            if (!lstPrs.Where(d=>d.C_B1).Any())
            {
                progressBar1.Visible = false;
                MessageBox.Show("در حال حاضر برای این کالا، مشخصه ای تعریف نشده است");
                chkJustShowItemProperties.Checked = false;
            }

            Application.DoEvents();
            progressBar1.Visible = false;
            panel1.Enabled = true;
        }

        // NeedToCorrect_C_B1 : آیا نیاز به بروز رسانی پارامتر «سی-بی 1» می باشد؟
        private List<Models.Property> GetData() //List<Models.Property> lstPr = null, bool NeedToCorrect_C_B1 = true)
        {
            if (!lstPrs.Any())
            {
                lstPrs = Program.dbOperations.GetAllProperties(Stack.Company_Index, 1);

                #region
                {
                    // کالاهایی که زیرساخت کالای وارد شده به فرم می باشند
                    List<Models.Item_Property> lstIP = Program.dbOperations
                        .GetAllItem_PropertiesAsync(Stack.Company_Index, item.Code_Small);

                    if (lstIP.Any())
                    {
                        foreach (Models.Property pr1 in lstPrs)
                        {
                            if (lstIP.Any(d => d.Property_Index == pr1.Index))
                            {
                                Models.Item_Property ip = lstIP.First(d => d.Property_Index == pr1.Index);

                                pr1.C_B1 = true;
                                pr1.DefaultValue = ip.DefaultValue;
                                pr1.ChangingValue = ip.ChangingValue;
                                //Program.dbOperations.UpdatePropertyAsync(pr1);
                            }
                            Application.DoEvents();
                        }
                    }
                }
                #endregion
            }

            if (chkJustShowItemProperties.Checked)
                return lstPrs.Where(d => d.C_B1).ToList();
            else return lstPrs.OrderByDescending(d => d.C_B1).ToList();

        }

        //// NeedToCorrect_C_B1 : آیا نیاز به بروز رسانی پارامتر «سی-بی 1» می باشد؟
        //private List<Models.Property> GetData(List<Models.Property> lstPr = null, bool NeedToCorrect_C_B1 = true)
        //{
        //    if (lstPr == null) lstPr = Program.dbOperations.GetAllPropertiesAsync(Stack.Company_Index, 1);

        //    #region بروز رسانی پارامتر «سی-بی 1»
        //    if (NeedToCorrect_C_B1)
        //    {
        //        // کالاهایی که زیرساخت کالای وارد شده به فرم می باشند
        //        List<Models.Item_Property> lstIP = Program.dbOperations
        //            .GetAllItem_PropertiesAsync(Stack.Company_Index,item.Code_Small);

        //        if (lstIP.Any())
        //        {
        //            foreach (Models.Property pr1 in lstPr)
        //            {
        //                if (lstIP.Any(d => d.Property_Index == pr1.Index))
        //                {
        //                    pr1.C_B1 = true;
        //                    pr1.DefaultValue = lstIP.First(d => d.Property_Index == pr1.Index).DefaultValue;
        //                    Program.dbOperations.UpdatePropertyAsync(pr1);
        //                }
        //                Application.DoEvents();
        //            }
        //        }
        //    }
        //    #endregion

        //    if (chkJustShowItemProperties.Checked)
        //        return Program.dbOperations.GetAllPropertiesAsync(Stack.Company_Index,1).Where(d => d.C_B1).ToList();
        //    else return Program.dbOperations.GetAllPropertiesAsync(Stack.Company_Index,1)
        //            .OrderByDescending(d => d.C_B1).ToList();

        //}

        private void ShowData() //bool ChangeHeaderTexts = true)
        {
            //List<Models.Property> lstPr = GetData().Where(d => d.C_B1).ToList();
            //// اگر این کالا دارای مشخصه ای نباشد
            //if (chkJustShowItemProperties.Checked && !lstPr.Any())
            //{
            //    progressBar1.Visible = false;
            //    MessageBox.Show("در حال حاضر برای این کالا، مشخصه ای تعریف نشده است");
            //    chkJustShowItemProperties.Checked = false;
            //    //lstPr = GetData(null, false);
            //}

            //dgvData.DataSource = lstPr;

            #region ترجمه سر ستونها و مخفی کردن بعضی ستونها
            //if (ChangeHeaderTexts)
            {
                foreach (DataGridViewColumn col in dgvData.Columns)
                {
                    switch (col.Name)
                    {
                        case "Name":
                            col.HeaderText = "نام";
                            col.ReadOnly = true;
                            col.DefaultCellStyle.BackColor = Color.LightGray;
                            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                            break;
                        case "Description":
                            col.HeaderText = "شرح";
                            col.ReadOnly = true;
                            col.DefaultCellStyle.BackColor = Color.LightGray;
                            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                            break;
                        case "C_B1":
                            col.HeaderText = "انتخاب";
                            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                            break;
                        case "DefaultValue":
                            col.HeaderText = "مقدار پیش فرض";
                            break;
                        case "ChangingValue":
                            col.HeaderText = "آیا مشخصه در هنگام سفارش قابل تغییر باشد؟";
                            col.Width = 150;
                            break;
                        default: col.Visible = false; break;
                    }
                }
            }
            #endregion
        }


        private void BtnSearch_Click(object sender, EventArgs e)
        {
            //List<Models.Property> lstPr = Program.dbOperations.GetAllPropertiesAsync(1);

            if (string.IsNullOrWhiteSpace(txtST_Name.Text)
                && string.IsNullOrWhiteSpace(txtST_Description.Text))
            {
                dgvData.DataSource = GetData();// null, false);
                //ShowData(false);
                return;
            }


            panel1.Enabled = false;
            dgvData.Visible = false;
            Application.DoEvents();
            //ShowData(false);

            List<Models.Property> lstPr = GetData();// null, false); 
            //MessageBox.Show(lstItems.Count.ToString());

            foreach (Control c in groupBox1.Controls)
            {
                //MessageBox.Show(c.Text);
                if (c.Name.Length > 4)
                {
                    if (c.Name.Substring(0, 5).Equals("txtST"))
                        if (!string.IsNullOrWhiteSpace(c.Text))
                        {
                            lstPr = SearchThis(lstPr, c.Name);
                            //MessageBox.Show(c.Text, c.Name);
                            //MessageBox.Show(lstItems.Count.ToString(), c.Name);
                            if ((lstPr == null) || !lstPr.Any()) break;
                        }
                }
            }

            //ShowData(false);
            dgvData.DataSource = lstPr;

            Application.DoEvents();
            panel1.Enabled = true;
            dgvData.Visible = true;

        }

        // جستجوی موردی
        private List<Models.Property> SearchThis(List<Models.Property> lstPr1, string TextBoxName)
        {
            switch (TextBoxName)
            {
                case "txtST_Description":
                    switch (cmbST_Description.SelectedIndex)
                    {
                        case 0:
                            return lstPr1.Where(d => d.Description.ToLower().Contains(txtST_Description.Text.ToLower())).ToList();
                        case 1:
                            return lstPr1.Where(d => d.Description.ToLower().StartsWith(txtST_Description.Text.ToLower())).ToList();
                        case 2:
                            return lstPr1.Where(d => d.Description.ToLower().Equals(txtST_Description.Text.ToLower())).ToList();
                        default: return lstPr1;
                    }
                //break;
                case "txtST_Name":
                    switch (cmbST_Name.SelectedIndex)
                    {
                        case 0:
                            return lstPr1.Where(d => d.Name.ToLower().Contains(txtST_Name.Text.ToLower())).ToList();
                        case 1:
                            return lstPr1.Where(d => d.Name.ToLower().StartsWith(txtST_Name.Text.ToLower())).ToList();
                        case 2:
                            return lstPr1.Where(d => d.Name.ToLower().Equals(txtST_Name.Text.ToLower())).ToList();
                        default: return lstPr1;
                    }
            }

            return null;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("آیا از ثبت تغییرات اطمینان دارید؟", "", MessageBoxButtons.YesNo)
                == DialogResult.No) return;

            panel1.Enabled = false;
            Application.DoEvents();

            List<Models.Property> lstPr = (List<Models.Property>)dgvData.DataSource;
            //MessageBox.Show(lstPr.Where(d => d.ChangingValue).Count().ToString());

            #region حذف شود : قبلا بوده است، اما در جدول انتخاب نشده است 
            if (Program.dbOperations.GetAllItem_PropertiesAsync(Stack.Company_Index, item.Code_Small).Any())
            {
                foreach (Models.Item_Property ip
                    in Program.dbOperations.GetAllItem_PropertiesAsync(Stack.Company_Index, item.Code_Small))
                {
                    if (!lstPr.Where(d => d.C_B1).Any(d => d.Index == ip.Property_Index))
                        Program.dbOperations.DeleteItem_Property(ip);
                }
            }
            #endregion

            #region موارد جدید اضافه شود 
            foreach (Models.Property pr in lstPr.Where(d => d.C_B1).ToList())
            {
                //MessageBox.Show("C_B1 = " + pr.C_B1 + "\n" + "ChangingValue = " + pr.ChangingValue);

                if (!Program.dbOperations.GetAllItem_PropertiesAsync(Stack.Company_Index, item.Code_Small)
                    .Any(d => d.Property_Index == pr.Index))
                {
                    Program.dbOperations.AddItem_PropertyAsync(new Models.Item_Property
                    {
                        Company_Index = Stack.Company_Index,
                        Property_Index = pr.Index,
                        Item_Code_Small = item.Code_Small,
                        Item_Index = item.Index,
                        ChangingValue = pr.ChangingValue,
                        DefaultValue = pr.DefaultValue,
                    });
                }
            }
            #endregion
  
            Application.DoEvents();
            panel1.Enabled = true;
        }

        private void BtnReturn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void DgvData_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            return;

            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            //if (dgvData[e.ColumnIndex, e.RowIndex].Value == InitailValue) return;

            long index = Convert.ToInt64(dgvData["Index", e.RowIndex].Value);

            Models.Property property = Program.dbOperations.GetPropertyAsync(index);
            switch (dgvData.Columns[e.ColumnIndex].Name)
            {
                case "C_B1":
                    property.C_B1 = Convert.ToBoolean(dgvData["C_B1", e.RowIndex].Value);
                    break;
                case "DefaultValue":
                    property.DefaultValue = Convert.ToString(dgvData["DefaultValue", e.RowIndex].Value);
                    break;
            }
            Program.dbOperations.UpdatePropertyAsync(property);
        }

        private void ChkJustShowItemProperties_CheckedChanged(object sender, EventArgs e)
        {
            dgvData.DataSource = GetData();// null, false); 
        }
    }
}
