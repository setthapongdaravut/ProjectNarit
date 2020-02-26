using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App_Compensation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent(); 
        }

        //action when click menu Data
        private void Datamenu_Click(object sender, EventArgs e)
        {
            slider.Top = ((Bunifu.Framework.UI.BunifuFlatButton)sender).Top;
            Panel pnl1 = contain;
            pnl1.Visible = true;

        }

        //action when click menu data analize
        private void Graphmenu_Click(object sender, EventArgs e)
        {
            slider.Top = ((Bunifu.Framework.UI.BunifuFlatButton)sender).Top;
            panel2.Show();
            contain.Hide(); 
        }
    }
}
