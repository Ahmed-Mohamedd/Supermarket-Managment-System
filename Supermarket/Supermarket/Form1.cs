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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void bunifuTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            
            sqlOP s = new sqlOP();
            s.conn.Open();
            SqlCommand cmd = new SqlCommand( "select * from supermarket.[login].[users_view] where  @username=userName and @password=password and @usertype = userType  " , s.conn);
            cmd.Parameters.AddWithValue("@username", username.Text);
            cmd.Parameters.AddWithValue("@password", password.Text);
            cmd.Parameters.AddWithValue("@usertype", comboBox1.Text);
            cmd.ExecuteNonQuery();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows )
            {
                dr.Read();
                if (dr.GetString(3) == "admin")
                {
                    MessageBox.Show("Welcome " + dr.GetString(1).ToUpper() + "\n" + "You have signed as a/an " + dr.GetString(3).ToUpper(), "Title", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    productForm productForm = new productForm();
                    productForm.ShowDialog();
                    this.Hide();
                }
                else 
                {
                    if (dr.GetString(3) == "seller")
                    {
                        MessageBox.Show("Welcome " + dr.GetString(1).ToUpper() + "\n" + "You have signed as a/an " + dr.GetString(3).ToUpper(), "Title", MessageBoxButtons.OK, MessageBoxIcon.Information);
                       
                    }
                }
            }
        }

        private void close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            username.Text = "";
            password.Text = "";
            comboBox1.Text = "Select Role";

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                password.PasswordChar = default;
            }
            else 
            {
                password.PasswordChar = '*';
            }
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            password.PasswordChar = '*';
        }
    }
}
