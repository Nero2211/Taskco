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
using MySql.Data.MySqlClient;
using System.IO;
using System.Text.RegularExpressions;

namespace TASKCO
{
    public partial class PAY_AND_SCAN_PAGE : Form
    {
        private MySqlConnection connection;
        private List<database_content> basket = new List<database_content>();
        private List<database_content> checkBasket = new List<database_content>();
        private List<database_content> basket_ageRest = new List<database_content>();
        ArrayList fruitorvegbasket = new ArrayList();
        int counter = 0;
        double total;
        string input = string.Empty;
        Boolean barcode;
        Boolean bar_quantity;
        Boolean bar_price;
        Boolean bar_code;
          
        public PAY_AND_SCAN_PAGE()
        {
            InitializeComponent();
            initialize();
            timer2.Start();
        }
        private void initialize()
        {
            String server = "localhost"; //local host (WAMP)
            String database = "TaskcoProducts"; //database name
            String uid = "root"; //database username
            String password = ""; //database password
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            connection = new MySqlConnection(connectionString);
        }

        private void PAY_AND_SCAN_PAGE_Load(object sender, EventArgs e)
        {
            
        }

   
        private void timer1_Tick(object sender, EventArgs e)
        {
            if(counter > 100)
            {
                timer1.Enabled = false;
                counter = 0;
                if(textBox5.Text != "")
                {
                    this.Selecter(textBox5); //passes bCode text
                    textBox5.Clear();
                }
            }
            else
            {
                counter = counter + 75;
            }
        }

        private void Selecter(TextBox bCode)
        {
            double VAT = 0;
            string oldString = bCode.Text;
            string newString = string.Join("", Regex.Split(oldString, @"(?:\r\n|\n|\r)"));
            string query = "SELECT * FROM items WHERE item_code ='" + newString + "'";
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                database_content X = new database_content();

                while (dataReader.Read())
                {                           
                    X.item_code = dataReader["item_code"].ToString();
                    X.item_name = dataReader["item_name"].ToString();
                    X.item_description = dataReader["item_description"].ToString();
                    X.item_price = (double)dataReader["item_price"];
                    X.item_agerestriction = int.Parse(dataReader["item_agerestriction"].ToString());
                    X.item_VAT = int.Parse(dataReader["item_VAT"].ToString());

                    if (X.item_VAT.Equals(1))
                    {
                        VAT = Math.Round(((X.item_price * 20) / 100), 2);
                    }
                    total = X.item_price + total;

                    if (X.item_agerestriction == 1)
                    {
                        label12.Visible = true;
                        button18.Visible = true;
                        basket_ageRest.Add(X);
                    }
                    else
                    {
                        //Adding X into the Basket
                        basket.Add(X);
                        showItemsOnScreen(X, VAT);
                    }               
                }
                dataReader.Close();
                this.CloseConnection();
            }
            else
            {
                MessageBox.Show("Error - no product found");
            }
        }

        private void showItemsOnScreen(database_content curItem, double VAT)
        {
             total = 0;
                    foreach (database_content x in basket)
                    {
                        total = total + curItem.item_price;
                        if (curItem.item_VAT.Equals(1))
                        {
                            VAT = Math.Round(((curItem.item_price * 20) / 100), 2);
                        }
                    }
                    total = total + VAT;
                    label10.Text = "£" + total.ToString();

                    string[] row = { curItem.item_code, curItem.item_name, curItem.item_description, curItem.item_price.ToString(), VAT.ToString() };
                    
                    var listViewItem = new ListViewItem(row);
                    listView1.Items.Add(listViewItem);
        }

        private void ageVer(database_content curItem)
        {

        }

        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server. Contact administrator");
                        break;
                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
      

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            counter = 0;
            if (timer1.Enabled == false)
            {
                timer1.Enabled = true;
            }
        }

        private void label10_Click(object sender, EventArgs e)
        {
            
        }

        private void btn_remove_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {   total = 0;
                foreach (ListViewItem eachItem in listView1.SelectedItems)
                {
                    listView1.MultiSelect = true;
                    basket.RemoveAt(listView1.SelectedItems[0].Index);
                    listView1.Items.Remove(eachItem);       
                }
                
                foreach (database_content item in basket)
                {
                    total = total + item.item_price;
                }
            }
            else { MessageBox.Show("Please select an item to remove"); }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (listView1.Items.Count == 0)
            {
                MessageBox.Show("The basket contains no items");
            }
            else
            {
                DialogResult result = MessageBox.Show("Do you wish to clear your basket?",
                "Clear basket",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    listView1.Clear();
                }
                else if (result == DialogResult.No)
                {
                    //close the messagebox
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i = 0;

            if (barcode == true)
            {
                textBox1.Text += i;
            }
            else if (bar_quantity == true)
            {
                textBox2.Text += i;
            }
            else if (bar_price == true)
            {
                textBox3.Text += i;
            }
            else if (bar_code == true)
            {
                textBox4.Text += i;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int i = 1;

            if (barcode == true)
            {
                textBox1.Text += i;
            }
            else if (bar_quantity == true)
            {
                textBox2.Text += i;
            }
            else if (bar_price == true)
            {
                textBox3.Text += i;
            }
            else if (bar_code == true)
            {
                textBox4.Text += i;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int i = 2;

            if (barcode == true)
            {
                textBox1.Text += i;
            }
            else if (bar_quantity == true)
            {
                textBox2.Text += i;
            }
            else if (bar_price == true)
            {
                textBox3.Text += i;
            }
            else if (bar_code == true)
            {
                textBox4.Text += i;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int i = 3;

            if (barcode == true)
            {
                textBox1.Text += i;
            }
            else if (bar_quantity == true)
            {
                textBox2.Text += i;
            }
            else if (bar_price == true)
            {
                textBox3.Text += i;
            }
            else if (bar_code == true)
            {
                textBox4.Text += i;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int i = 4;

            if (barcode == true)
            {
                textBox1.Text += i;
            }
            else if (bar_quantity == true)
            {
                textBox2.Text += i;
            }
            else if (bar_price == true)
            {
                textBox3.Text += i;
            }
            else if (bar_code == true)
            {
                textBox4.Text += i;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int i = 5;

            if (barcode == true)
            {
                textBox1.Text += i;
            }
            else if (bar_quantity == true)
            {
                textBox2.Text += i;
            }
            else if (bar_price == true)
            {
                textBox3.Text += i;
            }
            else if (bar_code == true)
            {
                textBox4.Text += i;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int i = 6;

            if (barcode == true)
            {
                textBox1.Text += i;
            }
            else if (bar_quantity == true)
            {
                textBox2.Text += i;
            }
            else if (bar_price == true)
            {
                textBox3.Text += i;
            }
            else if (bar_code == true)
            {
                textBox4.Text += i;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            int i = 7;

            if (barcode == true)
            {
                textBox1.Text += i;
            }
            else if (bar_quantity == true)
            {
                textBox2.Text += i;
            }
            else if (bar_price == true)
            {
                textBox3.Text += i;
            }
            else if (bar_code == true)
            {
                textBox4.Text += i;
            }
        }
        
        private void button9_Click(object sender, EventArgs e)
        {
            int i = 8;

            if (barcode == true)
            {
                textBox1.Text += i;
            }
            else if (bar_quantity == true)
            {
                textBox2.Text += i;
            }
            else if (bar_price == true)
            {
                textBox3.Text += i;
            }
            else if (bar_code == true)
            {
                textBox4.Text += i;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            int i = 9;

            if (barcode == true)
            {
                textBox1.Text += i;
            }
            else if (bar_quantity == true)
            {
                textBox2.Text += i;
            }
            else if (bar_price == true)
            {
                textBox3.Text += i;
            }
            else if (bar_code == true)
            {
                textBox4.Text += i;
            }
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
            if (barcode == true)
            {
                removeLastKey(textBox1);
            }
            else if (bar_quantity == true)
            {
                removeLastKey(textBox2);
            }
            else if (bar_price == true)
            {
                removeLastKey(textBox3);
            }
            else if (bar_code == true)
            {
                removeLastKey(textBox4);
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (barcode == true)
            {
                textBox1.Clear();
            }
            else if (bar_quantity == true)
            {
                textBox2.Clear();
            }
            else if (bar_price == true)
            {
                textBox3.Clear();
            }
            else if (bar_code == true)
            {
                textBox4.Clear();
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            this.Hide();
            var FRUITS_AND_VEGETABLES_PAGE = new FRUITS_AND_VEGETABLES_PAGE(ref basket);
            FRUITS_AND_VEGETABLES_PAGE.Show(this);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            #region manualBcode
            //if (barcode == true)
            //{
            //    double VAT = 0;
            //    string oldString = textBox1.Text;
            //    string newString = string.Join("", Regex.Split(oldString, @"(?:\r\n|\n|\r)"));
            //    string query = "SELECT * FROM items WHERE item_code ='" + newString + "'";
            //    if (this.OpenConnection() == true)
            //    {
            //        MySqlCommand cmd = new MySqlCommand(query, connection);
            //        MySqlDataReader dataReader = cmd.ExecuteReader();
            //        while (dataReader.Read())
            //        {
            //            database_content X = new database_content();
            //            X.item_code = dataReader["item_code"].ToString();
            //            X.item_name = dataReader["item_name"].ToString();
            //            X.item_description = dataReader["item_description"].ToString();
            //            X.item_price = (double)dataReader["item_price"];
            //            X.item_VAT = int.Parse(dataReader["item_VAT"].ToString());
            //            if (X.item_VAT.Equals(1))
            //            {
            //                VAT = Math.Round(((X.item_price * 20) / 100), 2);
            //            }
                        
            //            if (X.item_agerestriction.Equals(1))
            //            {
            //                label12.Visible = true;
            //                button18.Visible = true;

            //            }
            //            //loop to work out the calculation
            //            total = 0;
            //            foreach (database_content x in basket)
            //            {
            //                total = total + X.item_price;
            //                if (X.item_VAT.Equals(1))
            //                {
            //                    VAT = Math.Round(((X.item_price * 20) / 100), 2);
            //                }
            //            }
            //            total = total + VAT;
            //            label10.Text = "£" + total.ToString();
            //            //Adding X into the Basket
            //            basket.Add(X);
            //            string[] row = { X.item_code, X.item_name, X.item_description, X.item_price.ToString(), VAT.ToString() };

            //            var listViewItem = new ListViewItem(row);
            //            listView1.Items.Add(listViewItem);
            //        }
            //        dataReader.Close();
            //        this.CloseConnection();
            //    }
            //    else
            //    {
            //        MessageBox.Show("Error - no product found");
            //    }
            //    //set all boolean to false
            //    barcode = false;
            //    bar_quantity = false;
            //    bar_price = false;
            //    bar_code = false;
            //}
            //else if (bar_code == true && bar_code.Equals("12345"))
            //{
            //    double calculation;
            //    double manual_price = double.Parse(textBox2.Text);
            //    double manual_quantity = double.Parse(textBox3.Text);

            //    if (bar_price.Equals("00.00"))
            //    {
            //        if (bar_quantity.Equals("00"))
            //        {
            //            calculation = manual_price * manual_quantity;              
            //        }
            //        else
            //        {
            //            MessageBox.Show("Please enter a quantity");
            //        }
            //    }
            //    else
            //    {
            //        MessageBox.Show("Please enter a price");
            //    }
            //    //set all boolean to false
            //    barcode = false;
            //    bar_quantity = false;
            //    bar_price = false;
            //    bar_code = false;
            //}
            //else if (bar_code == true && (bar_code != "12345".ToString()))
            //{
            //    label13.Visible = true;
            //}
            #endregion
            Selecter(textBox1); //passes manual bCode text
        }

        private void button18_Click(object sender, EventArgs e)
        {
            this.Hide();
            var ADULTS_CONTENTS = new ADULTS_CONTENTS(ref basket_ageRest, ref basket);
            //ADULTS_CONTENTS.Closed += (s, args) => this.Close();
            ADULTS_CONTENTS.Show(this);
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            barcode = false;
            bar_quantity = true;
            bar_price = false;
            bar_code = false;
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            barcode = true;
            bar_quantity = false;
            bar_price = false;
            bar_code = false;
        }

        private void textBox3_Click(object sender, EventArgs e)
        {
            barcode = false;
            bar_quantity = false;
            bar_price = true;
            bar_code = false;
        }

        private void textBox4_Click(object sender, EventArgs e)
        {
            barcode = false;
            bar_quantity = false;
            bar_price = false;
            bar_code = true;
        }

        private void PAY_AND_SCAN_PAGE_Activated(object sender, EventArgs e)
        {
            total = 0;
            double VAT;
            listView1.Items.Clear();
            foreach (database_content item in basket)
            {
                string[] row = { item.item_code, item.item_name, item.item_description, item.item_price.ToString()};
                var listViewItem = new ListViewItem(row);
                listView1.Items.Add(listViewItem);

                total = total + item.item_price;
                if (item.item_VAT.Equals(1))
                {
                    VAT = Math.Round(((item.item_price * 20) / 100), 2);
                    total = total + VAT;
                }
            }
            
            label10.Text = total.ToString();

            textBox5.Focus();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (button18.BackColor == Color.Yellow)
            {
                button18.BackColor = Color.Red;
            }
            else
            {
                button18.BackColor = Color.Yellow;
            }
            if (label12.ForeColor == Color.Yellow)
            {
                label12.ForeColor = Color.Red;
            }
            else
            {
                label12.ForeColor = Color.Yellow;
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (listView1.Items.Count == 0)
            {
                MessageBox.Show("The basket contains no items");
            }
            else
            {
                this.Hide();
                FINISH_AND_PAY finishandpay = new FINISH_AND_PAY(label10.Text);
                finishandpay.Show();
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        //public static string label10;

        public void setBasket(ArrayList list)
        {

            fruitorvegbasket = list;

        }


    }
    public class database_content
    {
        public string item_code { get; set; }
        public string item_name { get; set; }
        public string item_description { get; set; }
        public double item_price { get; set; }
        public int item_agerestriction { get; set; }
        public int item_VAT { get; set; }
    }
}
