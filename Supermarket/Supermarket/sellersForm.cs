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
    public partial class sellersForm : Form
    {
        sqlOP s = new sqlOP();
        public sellersForm()
        {
            InitializeComponent();
        }

        private void close_Click(object sender, EventArgs e) // close the app
        {
            Application.Exit();
        }

        private void label7_Click(object sender, EventArgs e) // go back to login form
        {
            Form1 log = new Form1();
            this.Hide();
            log.Show();
        }

        private void bunifuThinButton25_Click(object sender, EventArgs e) // goto category form
        {
            categoryForm categoryForm = new categoryForm();
            this.Hide();
            categoryForm.Show();
        }

        private void bunifuThinButton26_Click(object sender, EventArgs e) // foto product form
        {
            productForm productForm = new productForm();
            this.Show();
            productForm.Show();
        }

        public void refreshProducts()  // A method used to provide DataGridView with latest updates in Database
        {
            string query = "select * from [production].[sellers_view]";
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
            bunifuTextBox3.Text="";
            bunifuTextBox5.Text="";
        }

        private void sellersForm_Load(object sender, EventArgs e)
        {
            refreshProducts();
        }

        private void bunifuDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex>=0)
            {
                DataGridViewRow row = bunifuDataGridView1.Rows[e.RowIndex];
                id.Text = row.Cells[0].Value.ToString();
                bunifuTextBox1.Text = row.Cells[1].Value.ToString();
                bunifuTextBox2.Text = row.Cells[2].Value.ToString();
                bunifuTextBox4.Text = row.Cells[3].Value.ToString();
                bunifuTextBox3.Text = row.Cells[4].Value.ToString();
                bunifuTextBox5.Text = row.Cells[5].Value.ToString();
            }
        }
        private void bunifuThinButton21_Click(object sender, EventArgs e) // insert a new seller
        {
            s.open();
            s.sellersOP("[production].sp_seller_ins", -1, bunifuTextBox1.Text, Convert.ToInt32(bunifuTextBox2.Text), bunifuTextBox4.Text, bunifuTextBox3.Text, bunifuTextBox5.Text);
            MessageBox.Show("New Seller Inserted Successfully", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            refreshProducts();
            clearInputs();
        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            s.open();
            s.sellersOP("[production].sp_seller_up", Convert.ToInt32(id.Text), bunifuTextBox1.Text, Convert.ToInt32(bunifuTextBox2.Text), bunifuTextBox4.Text, bunifuTextBox3.Text, bunifuTextBox5.Text);
            MessageBox.Show(" Seller Updated Successfully", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            refreshProducts();
            clearInputs();
        }

        private void bunifuThinButton23_Click(object sender, EventArgs e)
        {
            s.open();
            s.sellersOP("[production].sp_seller_del", Convert.ToInt32(id.Text), bunifuTextBox1.Text, Convert.ToInt32(bunifuTextBox2.Text), bunifuTextBox4.Text, bunifuTextBox3.Text, bunifuTextBox5.Text);
            MessageBox.Show("Seller Deleted Successfully", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            refreshProducts();
            clearInputs();
        }
    }
}
