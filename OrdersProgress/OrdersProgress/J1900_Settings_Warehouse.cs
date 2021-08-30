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
    public partial class J1900_Settings_Warehouse : X210_ExampleForm_Normal
    {
        public J1900_Settings_Warehouse()
        {
            InitializeComponent();
        }

        private void J1900_Settings_Warehouse_Load(object sender, EventArgs e)
        {
            Models.Company company = Program.dbOperations.GetCompanyAsync(Stack.Company_Index);
            chkAutomaticWarehouseBooking.Checked = company.Warehouse_AutomaticBooking;
            numericUpDown1.Value = company.Warehouse_Booking_MaxHours;
            if (company.Warehouse_AutomaticBooking)
                panel2.Enabled = true;
        }

        private void BtnReturn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("آیا از ثبت تغییرات اطمینان دارید؟", ""
                , MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            if(Stack.bWarehouse_Booking_MaxHours != chkAutomaticWarehouseBooking.Checked)
            {
                Models.Company company = Program.dbOperations.GetCompanyAsync(Stack.Company_Index);
                company.Warehouse_AutomaticBooking = chkAutomaticWarehouseBooking.Checked;
                company.Warehouse_Booking_MaxHours = (long)numericUpDown1.Value;
                Program.dbOperations.UpdateCompanyAsync(company);
                Stack.bWarehouse_Booking_MaxHours = chkAutomaticWarehouseBooking.Checked;
            }
        }

        private void ChkAutomaticWarehouseBooking_CheckedChanged(object sender, EventArgs e)
        {
            panel2.Enabled = chkAutomaticWarehouseBooking.Checked;
        }
    }
}
