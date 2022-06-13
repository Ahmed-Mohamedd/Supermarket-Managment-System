using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Supermarket
{
    public partial class productForm : Form
    {
         sqlOP s = new sqlOP();
        public productForm()
        {
            InitializeComponent();
        }

        public void refreshProducts()  // A method used to provide DataGridView with latest updates in Database
        {
            string query = "select * from [production].[products_view]";
            SqlDataAdapter adapter = new SqlDataAdapter(query, s.conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            var ds = new DataSet();
            adapter.Fill(ds);
            bunifuDataGridView1.DataSource = ds.Tables[0];
            s.close();
        }

        public void clearInputs()  // A method to clear all textBoxs 
        {
            id.Text="";
            bunifuTextBox1.Text="";
            bunifuTextBox2.Text="";
            bunifuTextBox4.Text="";
            comboBox1.Text ="Select Category";
            comboBox2.Text ="Select Selller";

        }

        public void getCategoriesName()
        {
            s.open();
            SqlCommand cmd = new SqlCommand("select catName from [production].[cat_view]", s.conn);
            cmd.ExecuteNonQuery();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while(dr.Read())
                {
                    comboBox1.Items.Add(dr.GetString(0));
                }
            }
            s.close();
        }
        public void getSellersName()
        {
            s.open();
            SqlCommand cmd = new SqlCommand("select sellerName from [production].[sellers_view]", s.conn);
            cmd.ExecuteNonQuery();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    comboBox2.Items.Add(dr.GetString(0));
                }
            }
            s.close();
        }

        private void productForm_Load(object sender, EventArgs e)
        {
            refreshProducts();
            getCategoriesName();
            getSellersName();
        }

        private void bunifuDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                DataGridViewRow row = bunifuDataGridView1.Rows[e.RowIndex];
                id.Text  = row.Cells[0].Value.ToString();
                bunifuTextBox1.Text = row.Cells[1].Value.ToString();
                bunifuTextBox2.Text = row.Cells[2].Value.ToString();
                bunifuTextBox4.Text = row.Cells[3].Value.ToString();
                comboBox1.SelectedIndex = (Convert.ToInt32(row.Cells[4].Value))-1;
                comboBox2.SelectedIndex = (Convert.ToInt32(row.Cells[5].Value))-1;
            }
        }

        private void close_Click(object sender, EventArgs e) // close the form
        {
            Application.Exit();
        }

        private  void label7_Click(object sender, EventArgs e)  // log out button
        {
            Form1 log = new Form1();
            log.Show();
            this.Hide(); 
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)  // adding new product
        {
            s.open();
            s.insert(bunifuTextBox1.Text, Convert.ToInt32(bunifuTextBox2.Text), Convert.ToInt32(bunifuTextBox4.Text), comboBox1.SelectedIndex+1 , comboBox2.SelectedIndex+1);
            MessageBox.Show("product Inserted successfully" , "INFO" , MessageBoxButtons.OK , MessageBoxIcon.Information);
            refreshProducts();  
        }


        private void bunifuThinButton22_Click(object sender, EventArgs e)  // editing product
        {
            s.open();
            s.update(Convert.ToInt32(id.Text), bunifuTextBox1.Text, Convert.ToInt32(bunifuTextBox2.Text), Convert.ToInt32(bunifuTextBox4.Text), comboBox1.SelectedIndex+1, comboBox2.SelectedIndex+1);
            MessageBox.Show("product Updated successfully", "Title", MessageBoxButtons.OK, MessageBoxIcon.Information);
            refreshProducts();
        }

        private void bunifuThinButton24_Click(object sender, EventArgs e) 
        {
            refreshProducts();
        }

        private void bunifuThinButton23_Click(object sender, EventArgs e) // Delete product
        {
            s.open();
            DialogResult result = MessageBox.Show("Do you want to delete this product", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                s.delete(Convert.ToInt32(id.Text));
                refreshProducts();
                MessageBox.Show("Product Deleted Successfully" ,"Info" , 0 ,MessageBoxIcon.Information );
            }
        }

        private void bunifuThinButton26_Click(object sender, EventArgs e)
        {
            categoryForm cat = new categoryForm();
            this.Hide();
            cat.Show();
        }

        private void bunifuThinButton25_Click(object sender, EventArgs e)
        {
            sellersForm sellers = new sellersForm();
            this.Hide();
            sellers.Show();
        }

        private void searchh_KeyUp(object sender, KeyEventArgs e) // 
        {
            string name = searchh.Text;
            Search(name);
        }


        public void Search(string name) // method to search by product name
        {
            s.open();
            string query = "select * from [production].[products_view] where productName like '%"+name+"%' ";
            SqlDataAdapter adapter = new SqlDataAdapter(query, s.conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            var ds = new DataSet();
            adapter.Fill(ds);
            bunifuDataGridView1.DataSource = ds.Tables[0];
            s.close();
        }

        //private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    int x = comboBox1.SelectedIndex+1;
        //    label9.Text = x.ToString();
        //}
    }
}
