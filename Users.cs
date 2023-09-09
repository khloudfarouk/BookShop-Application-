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
    public partial class Users : Form
    {
        public Users()
        {
            InitializeComponent();
            populate();
        }

        SqlConnection con = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename=C:\Users\computer shopping\Desktop\BookShop\BookShop\Database1.mdf;Integrated Security = True");

        private void populate()
        {
            con.Open();
            string query = "select * from UserTb1";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            UserDGV.DataSource = ds.Tables[0];
            con.Close();

        }


        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (UnameTb.Text == "" || AddTb.Text == "" || PassTb.Text == "" || PhoneTb.Text == "" )
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();

                    string query = "insert into UserTb1 values('" + UnameTb.Text + "','" + PhoneTb.Text + "','" + AddTb.Text + "','" + PassTb.Text + "')";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Saved Successfully");
                    con.Close();
                    populate();
                    Reset();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }
        }
        private void Reset()
        {
            UnameTb.Text = "";
            PassTb.Text = "";
            PhoneTb.Text = "";
            AddTb.Text = "";
        }
        private void ResetBtn_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();

                    string query = "delete from UsernameTb1 where UId=" + key + ";";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Book Deleted Successfully");
                    con.Close();
                    populate();
                    Reset();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }
        }
        int key = 0;
        private void UserDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            UnameTb.Text = UserDGV.SelectedRows[0].Cells[1].Value.ToString();
            PhoneTb.Text = UserDGV.SelectedRows[0].Cells[2].Value.ToString();
            AddTb.Text = UserDGV.SelectedRows[0].Cells[3].Value.ToString();
            PassTb.Text = UserDGV.SelectedRows[0].Cells[4].Value.ToString();
            
            if (UnameTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(UserDGV.SelectedRows[0].Cells[0].Value.ToString());
            }

        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (UnameTb.Text == "" || PhoneTb.Text == "" || AddTb.Text == "" || PassTb.Text == "" )
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();

                    string query = "update UserTb1 set UName='" + UnameTb.Text + "',UPhone='" + PhoneTb.Text + "',UAdd='" + AddTb.Text + "',UPass='" + PassTb.Text + "'where UId=" + key + ";";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Updated Successfully");
                    con.Close();
                    populate();
                    Reset();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }
        }

        private void label10_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Books Obj = new Books();
            Obj.Show();
            this.Hide();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            Dash Obj = new Dash();
            Obj.Show();
            this.Hide();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
    

