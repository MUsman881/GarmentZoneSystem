using GarmentZone.DBConnection;
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

namespace GarmentZone.Screens
{
    public partial class frmPurchase : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
        DbConnection db = new DbConnection();
        string stitle = "Garments Zone";

        public frmPurchase()
        {
            InitializeComponent();
            con = new SqlConnection(db.MyConnection());
            LoadVendor();
            cboProduct.Enabled = false;
            txtQty.Enabled = false;
            txtPrice.Enabled = false;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void LoadVendor()
        {
            cboVendor.Items.Clear();
            con.Open();
            cmd = new SqlCommand("select * from tblVendor", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cboVendor.Items.Add(dr["vendor"].ToString());
            }
            dr.Close();
            con.Close();
        }

        public void LoadProducts()
        {
            cboProduct.Items.Clear();
            con.Open();
            cmd = new SqlCommand("Select pname from tblProduct where vendorid = '" + lblVendorID.Text + "' order by pname", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cboProduct.Items.Add(dr["pname"].ToString());
            }
            dr.Close();
            con.Close();
        }

        private void generateNo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Random rnd = new Random();
            txtRefNo.Clear();
            txtRefNo.Text += rnd.Next();
        }

        private void cboVendor_TextChanged(object sender, EventArgs e)
        {
            con.Open();
            cmd = new SqlCommand("select * from tblVendor where vendor like '" + cboVendor.Text + "'", con);
            dr = cmd.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                lblVendorID.Text = dr["id"].ToString();
            }
            dr.Close();
            con.Close();
            LoadProducts();
            cboProduct.Enabled = true;
        }

        private void txtPrice_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int qty = int.Parse(txtQty.Text);
                double price = double.Parse(txtPrice.Text);
                double bill = price * qty;
                txtBill.Text = bill.ToString();
            }
            catch (Exception)
            {
                txtBill.Text = "00";
            }
        }

        private void cboProduct_TextChanged(object sender, EventArgs e)
        {
            txtQty.Enabled = true;
            txtPrice.Enabled = true;
        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 8)
            {
                //accept backspace character
            }
            else if ((e.KeyChar < 48) || (e.KeyChar > 57)) //ascii code 48-57 between 0-9
            {
                e.Handled = true;
            }
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 8)
            {
                //accept backspace character
            }
            else if ((e.KeyChar < 48) || (e.KeyChar > 57)) //ascii code 48-57 between 0-9
            {
                e.Handled = true;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if(txtRefNo.Text == "")
                {
                    MessageBox.Show("Please generate invoice no.", "Garments Zone", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Garments Zone", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkStockHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}