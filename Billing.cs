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
    public partial class Billing : Form
    {
        public Billing()
        {
            InitializeComponent();
            populate();
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
        private void UpdateBook()
        {
            int newQty = stock - Convert.ToInt32(QtyTb.Text);
            try
            {
                con.Open();

                string query = "update BookTb1 set BQty=" + newQty +  "where BId=" + key + ";";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                //MessageBox.Show("Book Updated Successfully");
                con.Close();
                populate();
               // Reset();

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }

        }
        int n = 0, Grdtotal = 0;
        private void SaveBtn_Click(object sender, EventArgs e)
        {
           
            if(QtyTb.Text==""||Convert.ToInt32(QtyTb.Text)>stock)
            {
                MessageBox.Show("No Enough Stock");
            }
            else
            {
                int total = Convert.ToInt32(QtyTb.Text) * Convert.ToInt32(PriceTb.Text);
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(BillDGV);
                newRow.Cells[0].Value = n + 1;
                newRow.Cells[1].Value = BTitleTb.Text;
                newRow.Cells[2].Value = QtyTb.Text;
                newRow.Cells[3].Value = PriceTb.Text;
                newRow.Cells[4].Value = total;
                BillDGV.Rows.Add(newRow);
                n++;
                UpdateBook();
                Grdtotal = Grdtotal + total;
                TotalLb1.Text = "Rs" + Grdtotal;

            }
        }
        int key=0,stock = 0;
        private void BookDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            BTitleTb.Text = BookDGV.SelectedRows[0].Cells[1].Value.ToString();
           
           // QtyTb.Text = BookDGV.SelectedRows[0].Cells[4].Value.ToString();
            PriceTb.Text = BookDGV.SelectedRows[0].Cells[5].Value.ToString();
            if (BTitleTb.Text == "")
            {
                key = 0;
                stock = 0;
            }
            else
            {
                key = Convert.ToInt32(BookDGV.SelectedRows[0].Cells[0].Value.ToString());
                stock= Convert.ToInt32(BookDGV.SelectedRows[0].Cells[4].Value.ToString());
            }
        }

        private void Billing_Load(object sender, EventArgs e)
        {
            UserNameLb1.Text = Login.UserName;
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {
            Login Obj =new Login();
            Obj.Show();
            this.Hide();

        }

        private void label8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Reset()
        {
            BTitleTb.Text = "";
            QtyTb.Text = "";
            PriceTb.Text = "";
            ClientNameTb.Text = "";
        }
        private void ResetBtn_Click(object sender, EventArgs e)
        {
            Reset();
        }
    }
}
