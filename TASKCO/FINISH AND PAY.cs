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
    public partial class FINISH_AND_PAY : Form
    {
        string totalPrice;
        
        public FINISH_AND_PAY(string total)
        {
            InitializeComponent();
            totalPrice = total;
        }

        private void FINISH_AND_PAY_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            var PAYMENT = new PAYMENT(totalPrice);
            PAYMENT.Closed += (s, args) => this.Close();
            PAYMENT.Show();
        }
    }
}
