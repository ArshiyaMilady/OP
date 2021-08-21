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
            chkAutomaticWarehouseBooking.Checked = Program.dbOperations.GetCompanyAsync(Stack.Company_Index).Warehouse_Booking;
        }

        private void BtnReturn_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
