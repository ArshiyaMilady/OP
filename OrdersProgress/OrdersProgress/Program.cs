using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using OrdersProgress.Models;

namespace OrdersProgress
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new J1000_MainForm());
        }

        static dbOperations dbOP;
        public static dbOperations dbOperations
        {
            get
            {
                if (dbOP == null)
                {
                    dbOP = new dbOperations(Path.Combine(Application.StartupPath, "System.SQLite.DB.db3"));
                    //dbOP = new dbOperations(Path.Combine(Application.StartupPath, "OPdb.db3"));
                }
                return dbOP;
            }
        }

    }
}
