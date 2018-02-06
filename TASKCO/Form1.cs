using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TASKCO
{
    public partial class MAIN : Form
    {
        //ArrayList test = new ArrayList();
        public MAIN()
        {
            InitializeComponent();
            button1.TabStop = false;
            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //button1.BackColor = Color.Transparent;      
            this.Hide();
            var PAY_AND_SCAN_PAGE = new PAY_AND_SCAN_PAGE();
            PAY_AND_SCAN_PAGE.Show();
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            button1.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.button1click));
            button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
        }
        private void button1_MouseLeave_1(object sender, EventArgs e)
        {
            button1.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.button1normal1));
        }      
    }
}
