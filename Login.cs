using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BookShop
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        SqlConnection con = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename=C:\Users\computer shopping\Desktop\BookShop\BookShop\Database1.mdf;Integrated Security = True");
        public static string UserName = "";
        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select count (*) from UserTb1 where UName= '" + UnameTb.Text + "'and UPass='" + UPassTb.Text + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if(dt.Rows[0][0].ToString()=="1")
            {
                UserName = UnameTb.Text;
               Books obj = new Books();
                obj.Show();
                this.Hide();
                con.Close();
            }
            else
            {
                MessageBox.Show("Wrong Username or Password");
            }

            con.Close();

        }

        private void label4_Click(object sender, EventArgs e)
        {
            AdminLogin Obj = new AdminLogin();
            Obj.Show();
            this.Hide();
        }
    }
}
