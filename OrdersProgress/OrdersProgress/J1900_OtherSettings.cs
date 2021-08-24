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
    public partial class J1900_OtherSettings : X210_ExampleForm_Normal
    {
        public J1900_OtherSettings()
        {
            InitializeComponent();
        }

        private void J1900_OtherSettings_Load(object sender, EventArgs e)
        {
            chkAutomaticWarehouseBooking.Checked = Program.dbOperations.GetCompanyAsync(Stack.Company_Index).Warehouse_AutomaticBooking;
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
                Program.dbOperations.UpdateCompanyAsync(company);
                Stack.bWarehouse_Booking_MaxHours = chkAutomaticWarehouseBooking.Checked;
            }
        }
    }
}
