using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using GarmentZone.DBConnection;
using GarmentZone.Screens;

namespace GarmentZone
{
    public partial class frmQty : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        DbConnection db = new DbConnection();
        SqlDataReader dr;
        string stitle = "Garments Zone";

        private string pcode;
        private double price;
        private int qty;
        private String transno;
        frmPOS fpos;

        public frmQty(frmPOS frmpos)
        {
            InitializeComponent();
            con = new SqlConnection(db.MyConnection());
            fpos = frmpos;
        }

        public void ProductDetails(String pcode, double price, String transno, int qty)
        {
            this.pcode = pcode;
            this.price = price;
            this.transno = transno;
            this.qty = qty;
        }

        private void frmQty_Load(object sender, EventArgs e)
        { 
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            
            if ((e.KeyCode == Keys.Enter) && (txtQty.Text != String.Empty))
            {
                string id = "";
                bool found = false;
                int cart_qty = 0;

                con.Open();
                cmd = new SqlCommand("Select * from tblcart where transno = @transno and pcode = @pcode", con);
                cmd.Parameters.AddWithValue("@transno", transno);
                cmd.Parameters.AddWithValue("@pcode", pcode);
                dr = cmd.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    found = true;
                    id = dr["id"].ToString();
                    cart_qty = int.Parse(dr["qty"].ToString());
                }
                else
                {
                    found = false;
                }
                dr.Close();
                con.Close();

                if (found == true)
                {
                    if (qty < (int.Parse(txtQty.Text) + cart_qty))
                    {
                        MessageBox.Show("Unable to proceed. Remaining qty on hand is " + qty, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    con.Open();
                    cmd = new SqlCommand("update tblCart set qty = (qty + " + int.Parse(txtQty.Text) + ") where id = '" + id + "'", con);
                    cmd.ExecuteNonQuery();
                    con.Close();

                    fpos.txtSearch.Clear();
                    fpos.txtSearch.Focus();
                    fpos.LoadCart();
                    this.Dispose();
                }
                else
                {
                    if (qty < int.Parse(txtQty.Text))
                    {
                        MessageBox.Show("Unable to proceed. Remaining qty on hand is " + qty, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    con.Open();
                    cmd = new SqlCommand("insert into tblCart (transno, pcode, price, qty, sdate, cashier) values (@transno, @pcode, @price, @qty, @sdate, @cashier)", con);
                    cmd.Parameters.AddWithValue("@transno", transno);
                    cmd.Parameters.AddWithValue("@pcode", pcode);
                    cmd.Parameters.AddWithValue("@price", price);
                    cmd.Parameters.AddWithValue("@qty", int.Parse(txtQty.Text));
                    cmd.Parameters.AddWithValue("@sdate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@cashier", fpos.lblUser.Text);
                    cmd.ExecuteNonQuery();
                    con.Close();

                    fpos.txtSearch.Clear();
                    fpos.txtSearch.Focus();
                    fpos.LoadCart();
                    this.Dispose();
                }
            }
        }

    }
}
