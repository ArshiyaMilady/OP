using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using EXCEL = Microsoft.Office.Interop.Excel;
using OrdersProgress.Models;
using File = System.IO.File;

namespace OrdersProgress
{
    public partial class Mm100_Priorities : X210_ExampleForm_Normal
    {
        //bool ExitPermission = true;
        EXCEL.Application excelApp = null;
        EXCEL.Workbook wb = null;
        EXCEL.Worksheet wsPr = null;

        public Mm100_Priorities()
        {
            InitializeComponent();
        }

        private void L100_Priorities_Shown(object sender, EventArgs e)
        {
            string ExcelFilePath = Application.StartupPath + @"\Link.xlsx";
            if (!File.Exists(ExcelFilePath))
            {
                MessageBox.Show("فایل " + "Link.xlsx" + " یافت نشد!"
                    , "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                return;
            }

            excelApp = new EXCEL.Application();
            //excelApp.WindowState = EXCEL.XlWindowState.xlMaximized;
            excelApp.DisplayAlerts = false;
            excelApp.Visible = false;

            wb = excelApp.Workbooks.Open(ExcelFilePath);
            if (wb.Worksheets.Cast<EXCEL.Worksheet>().Any(d => d.Name.Equals("Priorities")))
            {
                wsPr = wb.Worksheets["Priorities"];
                wsPr.PageSetup.RightHeader = "XXX";
                #region گرفتن اطلاعات اولویتها از فایل اکسل
                List<OrderPriority> lstOP = new List<OrderPriority>();
                int i = 1;
                while(wsPr.Cells[++i,3].Value != null)    // نام سفارش نباید خالی باشد
                {
                    if (wsPr.Cells[i, 3].Value.ToString().Length > 0)
                    {
                        if (wsPr.Cells[i, 4].Value != null) // اولویت نباید خالی باشد
                        {
                            if (!wsPr.Cells[i, 4].Value.ToString().Equals("تکمیل"))   // تکمیل نباشد
                            {
                                if (int.TryParse(wsPr.Cells[i, 4].Value.ToString(), out int priority))
                                {
                                    lstOP.Add(new OrderPriority
                                    {
                                        Id = Convert.ToInt32(wsPr.Cells[i, 1].Value),
                                        Order_Title = wsPr.Cells[i, 3].Value.ToString(),
                                        Priority = priority,
                                        IsCompleted = false
                                    });
                                }
                            }
                        }
                    }
                }
                #endregion

                if (lstOP.Count>0)
                {
                    dgvData.DataSource = lstOP.OrderBy(d=>d.Priority).ToList();
                    ArrangeColumns();
                }
            }
            else
            {
                MessageBox.Show("شیف " + "Priorities" + " یافت نشد!"
                    , "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                return;
            }

            panel1.Enabled = true;
        }

        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("آیا از ثبت تغییرات اطمینان دارید؟", ""
                , MessageBoxButtons.YesNo) == DialogResult.No) return;

            panel1.Enabled = false;

            List<OrderPriority> lstOP = (List<OrderPriority>)dgvData.DataSource;
            int i = 1;
            //while (ws.Cells[++i, 3].Value != null)    // نام سفارش نباید خالی باشد
            while ((++i) < lstOP.Count+3)    // نام سفارش نباید خالی باشد
            {
                int wsId = Convert.ToInt32(wsPr.Cells[i, 1].Value);
                if (lstOP.Any(d => d.Id == wsId))
                {
                    OrderPriority op = lstOP.First(d => d.Id == wsId);
                    if (op.IsCompleted)
                        wsPr.Cells[i, 4].Value = "تکمیل";
                    else
                        wsPr.Cells[i, 4].Value = op.Priority;
                }
            }
            wb.Save();
            Application.DoEvents();
            panel1.Enabled = true;
        }

        private void L100_Priorities_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((excelApp!=null)&&(wb != null))
            {
                wb.Close(SaveChanges: false);
                excelApp.Workbooks.Close();
                excelApp.Quit();

                while (Marshal.ReleaseComObject(wb) != 0) { }
                while (Marshal.ReleaseComObject(excelApp.Workbooks) != 0) { }
                while (Marshal.ReleaseComObject(excelApp) != 0) { }
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        private void BtnReturn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void DgvData_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            bStopMethod = false;
            // Don't throw an exception when we're done.
            e.ThrowException = false;
            if (dgvData.Columns[dgvData.CurrentCell.ColumnIndex].Name.Equals("Priority"))
            {
                MessageBox.Show("لطفا «اولویت» را به صورت عدد وارد نمایید"
                    , "خطای نوع داده", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("خطای نوع داده", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // If this is true, then the user is trapped in this cell.
            e.Cancel = false;
        }

        private void ArrangeColumns()
        {
            #region ترجمه سر ستونها و مخفی کردن بعضی ستونها
            foreach (DataGridViewColumn col in dgvData.Columns)
            {
                switch (col.Name)
                {
                    //case "Id":
                    //    col.HeaderText = "Id";
                    //    break;
                    case "OrderName":
                        col.HeaderText = "نام سفارش";
                        col.ReadOnly = true;
                        col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        col.DefaultCellStyle.BackColor = Color.WhiteSmoke;
                        break;
                    case "Priority":
                        col.HeaderText = "اولویت";
                        break;
                    case "IsCompleted":
                        col.HeaderText = "تکمیل؟";
                        break;
                    default: col.Visible = false; break;
                }
            }

            #endregion
        }

        int InitialPriority = -1;
        private void DgvData_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0) return;
            if (bStopMethod) return;
            InitialPriority = Convert.ToInt32(dgvData["Priority", e.RowIndex].Value);
        }

        bool bStopMethod = false;
        private void DgvData_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0) return;
            if (bStopMethod) return;

            if (!dgvData.Columns[e.ColumnIndex].Name.Equals("Priority")) return;
            bStopMethod = true;

            //MessageBox.Show(dgvData[e.ColumnIndex, e.RowIndex].Value.ToString());
            int newPriority = Convert.ToInt32(dgvData["Priority", e.RowIndex].Value);
            if (InitialPriority == newPriority) return;

            int id = Convert.ToInt32(dgvData["Id", e.RowIndex].Value);

            #region اعمال اولویت جدید
            List<OrderPriority> lstOP = (List<OrderPriority>)dgvData.DataSource;
            dgvData.DataSource = null;
            List<OrderPriority> lstOP1 = new List<OrderPriority>();
            //foreach(OrderPriority op in lstOP.Where(d=>d.Id!=id).Where(j=>j.Priority >= newPriority).ToList())
            for (int k = 0; k < lstOP.Count; k++)
            {
                OrderPriority op = lstOP[k];
                if (op.Id != id)
                {
                    if (newPriority < InitialPriority)
                    {
                        if (op.Priority >= newPriority)
                            op.Priority = lstOP[k].Priority + 1;
                    }
                    else
                    {
                        if (op.Priority > newPriority)
                            op.Priority = lstOP[k].Priority + 2;
                    }
                }
                else
                {
                    if (newPriority > InitialPriority)
                        op.Priority = newPriority + 1;
                }
                //lstOP[k].Priority = lstOP[k].Priority + 1;
                lstOP1.Add(op);
            }
            Application.DoEvents();
            #endregion

            dgvData.DataSource = SortPriority_BeginFromOne(lstOP1);//.OrderBy(d=>d.Priority).ToList();
            Application.DoEvents();
            dgvData.Refresh();
            ArrangeColumns();
            Application.DoEvents();
            bStopMethod = false;
        }


        private List<OrderPriority> SortPriority_BeginFromOne(List<OrderPriority> lstOP1)
        {
            #region مرتب کردن با شروع از عدد یک
            List<OrderPriority> lstOP = lstOP1.OrderBy(d => d.Priority).ToList();
            List<OrderPriority> lstOP2 = new List<OrderPriority>();
            int p = 0;
            for (p = 0; p < lstOP.Count; p++)
            {
                OrderPriority op = lstOP[p];
                op.Priority = p + 1;
                lstOP2.Add(op);
            }
            Application.DoEvents();
            return lstOP2;
            #endregion
        }


    }
}
