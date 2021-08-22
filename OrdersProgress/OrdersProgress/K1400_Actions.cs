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
    public partial class K1400_Actions : X210_ExampleForm_Normal
    {
        Models.dbOperations dbOP = new Models.dbOperations();

        public K1400_Actions()
        {
            InitializeComponent();

            if (Stack.UserLevel_Index <= Stack.UserLevel_Supervisor1)
            {
                btnDeleteAll.Visible = true;
            }
        }

        private void BtnReturn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void K1400_Actions_Shown(object sender, EventArgs e)
        {
            dgvData.DataSource = dbOP.GetAllActionsAsync(Stack.Company_Index);
            ArrangeColumns();
        }

        private void ArrangeColumns()
        {
            #region ترجمه سر ستونها و مخفی کردن بعضی ستونها
            foreach (DataGridViewColumn col in dgvData.Columns)
            {
                switch (col.Name)
                {
                    //case "Index":
                    //    col.HeaderText = "شناسه";
                    //    break;
                    case "Name":
                        col.HeaderText = "نام فعالیت";
                        //col.ReadOnly = true;
                        //col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        //col.DefaultCellStyle.BackColor = Color.WhiteSmoke;
                        col.Width = 300;
                        break;
                    case "Time":
                        col.HeaderText = "زمان اجرا برای یک نفر نیرو";
                        col.Width = 200;
                        break;
                    case "Workers":
                        col.HeaderText = "نفر نیروی لازم";
                        col.Width = 120;
                        break;
                    case "Enable":
                        col.HeaderText = "فعال؟";
                        col.Width = 50;
                        break;
                    default: col.Visible = false; break;
                }

                //col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                //col.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            }

            #endregion
        }

        private void BtnAddNew_Click(object sender, EventArgs e)
        {
            long index = dbOP.GetNewIndex_Action();

            if (dbOP.AddActionAsync(new Models.Action
            {
                Company_Index = Stack.Company_Index,
                Index = index,
                Enable = true,
                Name = "نام " + (index % 100000),
            }) > 0)
                dgvData.DataSource = dbOP.GetAllActionsAsync(Stack.Company_Index);
        }

        private void DeleteAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("آیا از حذف همه کالاها اطمینان دارید؟"
                , "اخطار", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                == DialogResult.No) return;

            dbOP.DeleteAllActionsAsync();
            dgvData.DataSource = dbOP.GetAllActionsAsync(Stack.Company_Index);
        }

        private void TsmiDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("آیا از حذف این مورد اطمینان دارید؟", "", MessageBoxButtons.YesNo)
                == DialogResult.No) return;

            try
            {
                long index = Convert.ToInt64(dgvData.CurrentRow.Cells["Index"].Value);
                Models.Action action = dbOP.GetActionAsync(index);
                dbOP.DeleteActionAsync(action);
                dgvData.DataSource = dbOP.GetAllActionsAsync(Stack.Company_Index);
            }
            catch { MessageBox.Show("خطا در اجرای عملیات"); }
        }

        int iX = 0, iY = 0;
        private void dgvData_MouseDown(object sender, MouseEventArgs e)
        {
            iX = e.X;
            iY = e.Y;
        }

        private void dgvData_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0) return;
            if (e.Button == MouseButtons.Right)
            {
                /////// Do something ///////
                // انتخاب سلولی که روی آن کلیک راست شده است
                dgvData.CurrentCell = dgvData[e.ColumnIndex, e.RowIndex];
                contextMenuStrip1.Show(dgvData, new Point(iX, iY));
            }
        }

        private void K1400_Actions_FormClosing(object sender, FormClosingEventArgs e)
        {
            panel1.Focus();
        }

        private void DgvData_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            long index = Convert.ToInt64(dgvData["Index", e.RowIndex].Value);

            Models.Action action = dbOP.GetActionAsync(index);
            switch (dgvData.Columns[e.ColumnIndex].Name)
            {
                case "Name":
                    action.Name = Convert.ToString(dgvData["Name", e.RowIndex].Value);
                    if (string.IsNullOrWhiteSpace(action.Name))
                        return;
                    else if (dbOP.GetActionAsync(action.Name) != null)
                    {
                        MessageBox.Show("نام قبلا استفاده شده است", "خطا در ثبت تغییرات");
                        return;
                    }
                    break;
                case "Time":
                    action.Time = Convert.ToDouble(dgvData["Time", e.RowIndex].Value);
                    break;
                case "Workers":
                    action.Workers = Convert.ToDouble(dgvData["Workers", e.RowIndex].Value);
                    break;
                case "Enable":
                    action.Enable = Convert.ToBoolean(dgvData["Enable", e.RowIndex].Value);
                    break;
            }

            dbOP.UpdateActionAsync(action);


        }


        private void DgvData_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            // Don't throw an exception when we're done.
            e.ThrowException = false;

            MessageBox.Show("لطفا اطلاعات را به صورت عدد وارد نمایید"
                , dgvData.Columns[e.ColumnIndex].HeaderText, MessageBoxButtons.OK, MessageBoxIcon.Error);

            // If this is true, then the user is trapped in this cell.
            e.Cancel = false;
        }



    }
}
