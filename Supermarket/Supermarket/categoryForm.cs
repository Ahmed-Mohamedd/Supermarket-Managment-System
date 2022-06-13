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
    public partial class categoryForm : Form
    {
        sqlOP s = new sqlOP();
        public categoryForm()
        {
            InitializeComponent();
        }

        private void categoryForm_Load(object sender, EventArgs e)
        {
            refreshProducts();
        }

        private void close_Click(object sender, EventArgs e) // close cuurent form
        {
            Application.Exit();
        }

        private void label7_Click(object sender, EventArgs e) // logOut and go back to login form
        {
            Form1 log = new Form1();
            this.Hide();
            log.Show();
        }

        public void refreshProducts()  // A method used to provide DataGridView with latest updates in Database
        {
            string query = "select * from [production].[cat_view]";
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
        }

        private void bunifuDataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex>=0)
            {
                DataGridViewRow row = bunifuDataGridView1.Rows[e.RowIndex];
                id.Text = row.Cells[0].Value.ToString();
                bunifuTextBox1.Text = row.Cells[1].Value.ToString();
                bunifuTextBox2.Text = row.Cells[2].Value.ToString();
            }
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e) // inserting a new category
        {
            s.open();
            s.categOperation("[production].sp_ins_categ",-1, bunifuTextBox1.Text, bunifuTextBox2.Text);
            MessageBox.Show("Category Inserted Successfully", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            refreshProducts();
            clearInputs();
        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)  //update
        {
            s.open();
            s.categOperation("[production].sp_up_categ",Convert.ToInt32(id.Text), bunifuTextBox1.Text, bunifuTextBox2.Text);
            MessageBox.Show("Category Updated Successfully", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            refreshProducts();
            clearInputs();
        }

        private void bunifuThinButton23_Click(object sender, EventArgs e) // delete
        {
            s.open();
            s.categOperation("[production].sp_del_categ", Convert.ToInt32(id.Text), bunifuTextBox1.Text, bunifuTextBox2.Text);
            MessageBox.Show("Category Deleted Successfully", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            refreshProducts();
            clearInputs();
        }

        private void bunifuThinButton26_Click(object sender, EventArgs e)
        {
            productForm productForm = new productForm();
            this.Hide();
            productForm.Show();
        }

        private void bunifuThinButton25_Click(object sender, EventArgs e)
        {
            sellersForm sellersForm = new sellersForm();
            this.Hide();
            sellersForm.Show();
        }
    }
}
