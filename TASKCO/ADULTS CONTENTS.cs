using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace TASKCO
{
    public partial class ADULTS_CONTENTS : Form
    {   
        //private MySqlConnection connection;
        string input = string.Empty;
        //double total;
        List<database_content> basket_main;
        List<database_content> basket_age;

        public ADULTS_CONTENTS(ref List<database_content> basket_ageRest, ref List<database_content> basket)
        {
            InitializeComponent();
            basket_age = basket_ageRest;
            basket_main = basket;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = "";
            input += "0";
            this.textBox1.Text += input;      
        }

        private void button14_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = "";
            input += "1";
            this.textBox1.Text += input;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = "";
            input += "2";
            this.textBox1.Text += input;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = "";
            input += "3";
            this.textBox1.Text += input;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = "";
            input += "4";
            this.textBox1.Text += input;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = "";
            input += "5";
            this.textBox1.Text += input;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = "";
            input += "6";
            this.textBox1.Text += input;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = "";
            input += "7";
            this.textBox1.Text += input;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = "";
            input += "8";
            this.textBox1.Text += input;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = "";
            input += "9";
            this.textBox1.Text += input;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
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
            removeLastKey(textBox1);
        }

        private void ADULTS_CONTENTS_Load(object sender, EventArgs e)
        {
            double total = 0;
            double VAT = 0;
            foreach (database_content item in basket_age)
            {
                if (item.item_VAT.Equals(1))
                {
                    VAT = Math.Round(((item.item_price * 20) / 100), 2);
                }
                string[] row = { item.item_name, item.item_description, item.item_price.ToString(), item.item_VAT.ToString(VAT.ToString()) };
                var ListViewItem = new ListViewItem(row);
                listView1.Items.Add(ListViewItem);

                total = total + item.item_price;
                label10.Text = total.ToString();
                
                total = total + VAT;
                label10.Text = total.ToString();
                }
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }
        

        private void button13_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "12345")
            {

                foreach (database_content item in basket_age)
                {
                    basket_main.Add(item);
                }

                basket_age.Clear();

               //label3.Visible = true;
               //database_content X = new database_content();
               //X.item_code = basket_Passed.ToString();
               //X.item_name = basket_Passed.ToString();
               //X.item_description = basket_Passed.ToString();
               //X.item_price = double.Parse(basket_Passed.ToString());
               //X.item_VAT = int.Parse(basket_Passed.ToString());
               Owner.Show();
               this.Close();

            }
            else
            {
                label5.Visible = true;
            }
        }
     
        //private bool CloseConnection()
        //{
        //    try
        //    {
        //        connection.Close();
        //        return true;
        //    }
        //    catch (MySqlException ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //        return false;
        //    }
        //}
        //private bool OpenConnection()
        //{
        //    try
        //    {
        //        connection.Open();
        //        return true;
        //    }
        //    catch (MySqlException ex)
        //    {
        //        switch (ex.Number)
        //        {
        //            case 0:
        //                MessageBox.Show("Cannot connect to server. Contact administrator");
        //                break;
        //            case 1045:
        //                MessageBox.Show("Invalid username/password, please try again");
        //                break;
        //        }
        //        return false;
        //    }
        //}

        private void button18_Click(object sender, EventArgs e)
        {
            Owner.Show();
            this.Close();
        }
    }
}
