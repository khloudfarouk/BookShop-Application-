using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace BookShop
{
    public partial class Books : Form
    {
        public Books()
        {
            InitializeComponent();
            populate();
        }
        
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        SqlConnection con = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename=C:\Users\computer shopping\Desktop\BookShop\BookShop\Database1.mdf;Integrated Security = True");

        private void populate()
        {
            con.Open();
            string query = "select * from BookTb1";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            BookDGV.DataSource = ds.Tables[0];
            con.Close();

        }

        private void Filter()
        {
            con.Open();
            string query = "select * from BookTb1 where Bcat='"+CatCbSearchTb.SelectedItem.ToString()+"'";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            BookDGV.DataSource = ds.Tables[0];
            con.Close();

        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if(BTitleTb.Text=="" || BautTb.Text==""||QtyTb.Text==""||PriceTb.Text==""||BcatTb.SelectedIndex==-1)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();
                    
                    string query = "insert into BookTb1 values('" + BTitleTb.Text + "','" + BautTb.Text + "','" + BcatTb.SelectedItem.ToString() + "'," + QtyTb.Text + "," + PriceTb.Text + ")";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Book Saved Successfully");
                    con.Close();
                    populate();
                    Reset();

                }catch(Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
                
            }
        }
        
        private void BoutTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void CatCbSearchTb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Filter();
        }

        private void RefreshBtn_Click(object sender, EventArgs e)
        {
            populate();
            CatCbSearchTb.SelectedIndex = -1;

        }

        private void Reset()
        {
            BTitleTb.Text = "";
            BautTb.Text = "";
            BcatTb.SelectedIndex = -1;
            PriceTb.Text = "";
            QtyTb.Text = "";

        }
        private void ResetBtn_Click(object sender, EventArgs e)
        {
            Reset();

        }

        int key = 0;
        private void BookDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            BTitleTb.Text = BookDGV.SelectedRows[0].Cells[1].Value.ToString();
            BautTb.Text = BookDGV.SelectedRows[0].Cells[2].Value.ToString();
            CatCbSearchTb.SelectedItem = BookDGV.SelectedRows[0].Cells[3].Value.ToString();
            QtyTb.Text = BookDGV.SelectedRows[0].Cells[4].Value.ToString();
            PriceTb.Text = BookDGV.SelectedRows[0].Cells[5].Value.ToString();
            if(BTitleTb.Text=="")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(BookDGV.SelectedRows[0].Cells[0].Value.ToString());
            }

        }

        private void BcatTb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (key==0)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();

                    string query = "delete from BookTb1 where Bid=" + key + ";";

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

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (BTitleTb.Text == "" || BautTb.Text == "" || QtyTb.Text == "" || PriceTb.Text == "" || BcatTb.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();

                    string query = "update BookTb1 set BTitle='" + BTitleTb.Text + "',BAuther='" + BautTb.Text + "',BCat='" + BcatTb.SelectedItem.ToString() + "',BQty=" + QtyTb.Text + ",BPrice="+PriceTb.Text+"where BId="+key+";";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Book Updated Successfully");
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

        private void label7_Click(object sender, EventArgs e)
        {
            Users Obj = new Users();
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
