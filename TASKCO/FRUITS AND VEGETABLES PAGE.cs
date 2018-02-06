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
    public partial class FRUITS_AND_VEGETABLES_PAGE : Form
    {
        string input = string.Empty;
        string selectedFruitorVeg;
        double vat;        
        public ArrayList fruitorvegbasket = new ArrayList();
        database_content X = new database_content();
        private List<database_content> basket2;



        public FRUITS_AND_VEGETABLES_PAGE(ref List<database_content> test)
        {
            InitializeComponent();
            basket2 = test;

        }

        private void FRUITS_AND_VEGETABLES_PAGE_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {
            label5.Text = "";
            
        }

        private void button15_Click(object sender, EventArgs e)
        {
            int i = 0;
            textBox1.Text += i;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            int i = 9;
            textBox1.Text += i;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            int i = 8;
            textBox1.Text += i;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            int i = 7;
            textBox1.Text += i;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int i = 6;
            textBox1.Text += i;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int i = 5;
            textBox1.Text += i;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int i = 4;
            textBox1.Text += i;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int i = 3;
            textBox1.Text += i;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int i = 2;
            textBox1.Text += i;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            int i = 1;
            textBox1.Text += i;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1, 1);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        public void additemListView()
        {
            //database_content X = new database_content();
            X.item_name = selectedFruitorVeg;
            X.item_price = double.Parse(textBox2.Text);
            vat = double.Parse(textBox2.Text) * 20 / 100;
            basket2.Add(X);
            string[] row = { X.item_name, X.item_price.ToString(), vat.ToString("0.00") };
            var listViewItem = new ListViewItem(row);
            listView1.Items.Add(listViewItem);
        }
        private void button16_Click(object sender, EventArgs e)
        {
            #region
            //Fruits Selection from the ComboBox
            if (comboBox1.SelectedIndex == 0)
            {
                selectedFruitorVeg = "Apple";
                additemListView();
            }
            if (comboBox1.SelectedIndex == 1)
            {
                selectedFruitorVeg = "Banana";
                additemListView();
            }
            if (comboBox1.SelectedIndex == 2)
            {
                selectedFruitorVeg = "Pineapple";
                additemListView();
            }
            if (comboBox1.SelectedIndex == 3)
            {
                selectedFruitorVeg = "Cheery";
                additemListView();
            }
            #endregion
            #region Vegetables Selection
            //Vegetables selection code from the ComboBox
            if (comboBox2.SelectedIndex == 0)
            {
                selectedFruitorVeg = "Carrots";
                additemListView();
            }
            if (comboBox2.SelectedIndex == 1)
            {
                selectedFruitorVeg = "Cauliflower";
                additemListView();
            }
            if (comboBox2.SelectedIndex == 2)
            {
                selectedFruitorVeg = "Cabbage";
                additemListView();
            }
            if (comboBox2.SelectedIndex == 3)
            {
                selectedFruitorVeg = "Broccoli";
                additemListView();
            }
#endregion
        }

        public void calculation(double price)
        {
            double weight = double.Parse(textBox1.Text);
            double cost = weight * price;
            textBox2.Text = cost.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            #region ComboBox for Fruits
            if (comboBox1.SelectedIndex == 0)
            {
                calculation(0.79);//apple price per gram is 0.79 pence
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                calculation(0.69);
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                calculation(1.29);
            }
            else if (comboBox1.SelectedIndex == 3)
            {
                calculation(0.35);
            }
            #endregion
            #region ComboBox for Vegetables
            //ComboBox 2 Vegetables
            else if (comboBox2.SelectedIndex == 0)
            {
                calculation(0.79);
            }
            else if (comboBox2.SelectedIndex == 1)
            {
                calculation(1.00);
            }
            else if (comboBox2.SelectedIndex == 2)
            {
                calculation(0.79);
            }
            else if (comboBox2.SelectedIndex == 3)
            {
                calculation(0.59);
            }
            else if (comboBox1.SelectedIndex == -1 && comboBox2.SelectedIndex == -1)
            {
                MessageBox.Show("Please choose a fruit or a vegetable!");
            }
            #endregion
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void button18_Click(object sender, EventArgs e)
        {
            //PAY_AND_SCAN_PAGE 
            Owner.Show();
            this.Close();
            
        }
    }
}
