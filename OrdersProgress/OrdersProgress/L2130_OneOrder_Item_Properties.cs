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
    public partial class L2130_OneOrder_Item_Properties : X210_ExampleForm_Normal
    {
        Models.Order_Item order_Item;
        bool bOrderReadOnly = false;

        public L2130_OneOrder_Item_Properties(Models.Order_Item _order_Item, bool _bOrderReadOnly = false)
        {
            InitializeComponent();

            order_Item = _order_Item;
            bOrderReadOnly = _bOrderReadOnly;

            if(bOrderReadOnly)
            {
                btnSave.Visible = false;
            }

            Text = order_Item.Item_Name_Samll;
        }

        private void L2130_OneOrder_Item_Properties_Shown(object sender, EventArgs e)
        {
            //dgvData.DataSource = Program.dbOperations.GetAllOrder_Item_PropertiesAsync(order_Item.Order_Index, order_Item.Item_Index);

            dgvData.Columns.Add("row", "ردیف");
            dgvData.Columns["row"].ReadOnly = true;
            dgvData.Columns["row"].Width = 40;
            dgvData.Columns["row"].DefaultCellStyle.BackColor = Color.WhiteSmoke;

            MakeDGV_Rows();
            MakeDGV_Columns();
            PutData_in_DGV();
        }

        private void MakeDGV_Columns()
        {
            foreach(Models.Order_Item_Property oip in Program.dbOperations.GetAllOrder_Item_PropertiesAsync
                (Stack.Company_Index, order_Item.Order_Index,order_Item.Item_Index).Where(d=>d.ItemBatch_Counter==1).ToList())
            {
                dgvData.Columns.Add(oip.Property_Index.ToString(), oip.Property_Name);
                dgvData.Columns[oip.Property_Index.ToString()].ReadOnly = !oip.Property_ChangingValue || bOrderReadOnly;
                if (oip.Property_ChangingValue && !bOrderReadOnly)
                    dgvData.Columns[oip.Property_Index.ToString()].DefaultCellStyle.BackColor = Color.White;
                else
                    dgvData.Columns[oip.Property_Index.ToString()].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            }
        }

        private void MakeDGV_Rows()
        {
            for(int i=0;i<order_Item.Quantity;i++)
            {
                dgvData.Rows.Add();
                dgvData["row", i].Value = i+1;
            }
        }

        private void PutData_in_DGV()
        {
            //foreach (DataGridViewColumn col in dgvData.Columns)
            for (int j = 1; j < dgvData.Columns.Count; j++)
            {
                //if (!col.ReadOnly)
                {
                    long PropertyIndex = Convert.ToInt64(dgvData.Columns[j].Name);
                    for (int i = 0; i < dgvData.Rows.Count; i++)
                    {
                        Models.Order_Item_Property oip = Program.dbOperations.GetOrder_Item_PropertyAsync
                            (order_Item.Order_Index, order_Item.Item_Index, PropertyIndex, i + 1);
                        dgvData[j, i].Value = oip.Property_Value;
                        dgvData[j, i].Tag = oip.Index;
                    }
                }
            }


        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("آیا از ذخیره تغییرات اطمینان دارید؟"
                , "", MessageBoxButtons.YesNo) == DialogResult.No) return;

            panel1.Enabled = false;
            progressBar1.Visible = true;
            Application.DoEvents();

            //foreach (DataGridViewColumn col in dgvData.Columns)
            for (int j = 1; j < dgvData.Columns.Count; j++)
            {
                DataGridViewColumn col = dgvData.Columns[j];
                if (!col.ReadOnly)
                {
                    //MessageBox.Show(col.HeaderText);
                    for (int i=0;i<dgvData.Rows.Count;i++)
                    {
                        Models.Order_Item_Property oip = Program.dbOperations.GetOrder_Item_PropertyAsync
                            (Convert.ToInt64(dgvData[j, i].Tag));
                            //(order_Item.Order_Index, order_Item.Item_Index, PropertyIndex, i + 1);
                        string NewValue = Convert.ToString(dgvData[col.Name, i].Value);
                        //MessageBox.Show(NewValue, col.HeaderText);

                        bool b1 = string.IsNullOrEmpty(oip.Property_Value);
                        bool b2 = string.IsNullOrEmpty(NewValue);

                        //MessageBox.Show( "b1 = " + b1.ToString() + "\n" + "b2 = " + b2.ToString()
                        //    , "0 " + col.HeaderText);
                        if (b1 && b2) continue; // اگر هر دو خالی باشند

                        bool bUpdate = (!b1 && b2) || (b1 && !b2);  // اگر یکی خالی باشد

                        //MessageBox.Show(bUpdate.ToString(),"1 " + col.HeaderText);
                        if (!bUpdate) bUpdate = !oip.Property_Value.Equals(NewValue);
                        //MessageBox.Show(bUpdate.ToString(), "2 " + col.HeaderText);

                        // اگر مقدار جدید باشد، تغییرات اعمال می شود
                        if (bUpdate)
                        {
                            //MessageBox.Show("oip.Property_Value = " + oip.Property_Value
                            //    + "\n" + "NewValue = " + NewValue);
                            oip.Property_Value = NewValue;
                            Program.dbOperations.UpdateOrder_Item_PropertyAsync(oip);
                        }
                        Application.DoEvents();
                    }
                }
            }

            if (MessageBox.Show("ثبت تغییرات با موفقیت انجام شد. آیا از صفحه خارج می شوید؟"
                , "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Close();
            }
            else
            {
                Application.DoEvents();
                panel1.Enabled = true;
                progressBar1.Visible = false;
            }

        }
    }
}
