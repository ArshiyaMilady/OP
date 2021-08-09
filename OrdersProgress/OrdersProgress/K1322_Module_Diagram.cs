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
    public partial class K1322_Module_Diagram : X210_ExampleForm_Normal
    {
        private readonly string code = null;

        public K1322_Module_Diagram(string _code)
        {
            InitializeComponent();

            code = _code;
        }

        private void K1322_Module_Diagram_Shown(object sender, EventArgs e)
        {
            List<Relation_by_Level> lstRL = new ThisProject().AllSubRelations(code);

            Relation_by_Level rl = lstRL.First(d => d.Level == 0);
            TreeNode tn0 = treeView1.Nodes.Add(rl.Index.ToString(), rl.Name);

            for (int i = 1; i <= lstRL.Max(d => d.Level); i++)
            {
                foreach(Relation_by_Level rl1 in lstRL.Where(d=>d.Level == i).ToList())
                {
                    TreeNode tn = treeView1.Nodes.Find(rl1.Top_Index.ToString(), true).First();
                    tn.Nodes.Add(rl1.Index.ToString(), rl1.Name + " - تعداد : " + rl1.Quantity );
                }
            }

            for(long i= lstRL.Min(d => d.Index); i<=lstRL.Max(d=>d.Index);i++)
            {
                treeView1.Nodes.Find(i.ToString(), true).First().Expand();
            }

        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (Opacity >= 1) timer1.Enabled = false;
            else Opacity += 0.25;

        }

        private void BtnReturn_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
