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
    public partial class PAYMENT : Form
    {
        string input = string.Empty;
        double totalPayment;

        public PAYMENT(string payment)
        {
            InitializeComponent();
            totalPayment = double.Parse(payment.ToString());
            totalCal();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            var FINAL_OR_PRINTING_RECEIPT = new FINAL_OR_PRINTING_RECEIPT(textBox2.Text);
            FINAL_OR_PRINTING_RECEIPT.Closed += (s, args) => this.Close();
            FINAL_OR_PRINTING_RECEIPT.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
           DialogResult result = MessageBox.Show("Return to the Main Menu?",
           "Main Menu",
           MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                this.Close();
                var MAIN = new MAIN();
                MAIN.Closed += (s, args) => this.Close();
                MAIN.Show();
            }
            else if (result == DialogResult.No)
            {
                //close the messagebox
            } 
        }

        private void PAYMENT_Load(object sender, EventArgs e)
        {

        }

        private void button15_Click(object sender, EventArgs e)
        {
            this.textBox6.Text = "";
            input += "0";
            this.textBox6.Text += input;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            this.textBox6.Text = "";
            input += "1";
            this.textBox6.Text += input;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.textBox6.Text = "";
            input += "2";
            this.textBox6.Text += input;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.textBox6.Text = "";
            input += "3";
            this.textBox6.Text += input;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.textBox6.Text = "";
            input += "4";
            this.textBox6.Text += input;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.textBox6.Text = "";
            input += "5";
            this.textBox6.Text += input;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.textBox6.Text = "";
            input += "6";
            this.textBox6.Text += input;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.textBox6.Text = "";
            input += "7";
            this.textBox6.Text += input;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.textBox6.Text = "";
            input += "8";
            this.textBox6.Text += input;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.textBox6.Text = "";
            input += "9";
            this.textBox6.Text += input;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (textBox6.Text == "12345")
            {
                label10.Visible = true;
                textBox2.Text = "£4.99";
                totalPayment = totalPayment - 4.99;
                totalCal();
            }
            else if (textBox6.Text == "10101")
            {
                label10.Visible = true;
                textBox2.Text = "£6.99";
            }
            else
            {
                label9.Visible = true;
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            textBox6.Clear();
        }

        private void removeLastKey(TextBox tb)
        {
            if (tb.Text.Length > 0)
            {
                tb.Text = tb.Text.Remove(tb.Text.Length - 1, 1);
            }
        }
        private void button11_Click(object sender, EventArgs e)
        {
            removeLastKey(textBox6);
        }
        public void totalCal()
        {
            textBox1.Text = totalPayment.ToString(); 
            textBox3.Text = totalPayment.ToString();
            textBox4.Text = totalPayment.ToString();
            textBox5.Text = "00.00";
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
