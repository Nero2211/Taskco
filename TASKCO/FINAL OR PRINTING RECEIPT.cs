using System;
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
    public partial class FINAL_OR_PRINTING_RECEIPT : Form
    {
        
        public FINAL_OR_PRINTING_RECEIPT(String voucher)
        {
            InitializeComponent();
            label2.Text = voucher;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Close();
            var MAIN = new MAIN();
            MAIN.Closed += (s, args) => this.Close();
            MAIN.Show();
        }

        private void FINAL_OR_PRINTING_RECEIPT_Load(object sender, EventArgs e)
        {
            timer1.Interval = 10000;
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Start();
        }
    }
}
