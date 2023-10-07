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

namespace GarmentZone.Screens
{
    public partial class frmProduct : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
        DbConnection db = new DbConnection(); 
        frmProductList frmProductList = new frmProductList();
        Dashboard d;

        public frmProduct(frmProductList list)
        {
            InitializeComponent(); 
            con = new SqlConnection(db.MyConnection());
            frmProductList = list;
            Dashboard dashboard = new Dashboard();
            d = dashboard;
        }

        public void LoadCategory()
        {
            cboCategory.Items.Clear();
            con.Open();
            cmd = new SqlCommand("select category from tblCategory", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cboCategory.Items.Add(dr[0].ToString());
            }
            dr.Close();
            con.Close();
        }

        public void LoadBrand()
        {
            cboBrand.Items.Clear();
            con.Open();
            cmd = new SqlCommand("select brand from tblBrand", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cboBrand.Items.Add(dr[0].ToString());
            }
            dr.Close();
            con.Close();
        }

        public void LoadVendor()
        {
            cboVendor.Items.Clear();
            con.Open();
            cmd = new SqlCommand("select vendor from tblVendor", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cboVendor.Items.Add(dr["vendor"].ToString());
            }
            dr.Close();
            con.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void frmProduct_Load(object sender, EventArgs e)
        {
            LoadBrand();
            LoadCategory();
            LoadVendor();
        }

        private void Clear()
        {
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            pcode.Clear();
            pname.Clear();
            txtBarcode.Clear();
            pdesc.Clear();
            price.Clear();
            cboCategory.Text = "";
            cboBrand.Text = "";
            cboVendor.Text = "";
            txtReorder.Clear();
            pcode.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to save this Product?", "Save Product", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string bid = "", cid = "", vendorid="";

                    con.Open();
                    cmd = new SqlCommand("Select id from tblBrand where brand like '" + cboBrand.Text + "'", con);
                    dr = cmd.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        bid = dr[0].ToString();
                    }
                    dr.Close();
                    con.Close();

                    con.Open();
                    cmd = new SqlCommand("Select id from tblCategory where category like '" + cboCategory.Text + "'", con);
                    dr = cmd.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        cid = dr[0].ToString();
                    }
                    dr.Close();
                    con.Close();

                    con.Open();
                    cmd = new SqlCommand("Select id from tblVendor where vendor like '" + cboVendor.Text + "'", con);
                    dr = cmd.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        vendorid = dr[0].ToString();
                    }
                    dr.Close();
                    con.Close();

                    con.Open();
                    cmd = new SqlCommand("INSERT into tblProduct(pcode, pname,barcode, pdesc, bid, cid,vendorid, price,reorder)Values(@pcode, @pname,@barcode, @pdesc, @bid, @cid,@vendorid, @price,@reorder)", con);
                    cmd.Parameters.AddWithValue("@pcode", pcode.Text);
                    cmd.Parameters.AddWithValue("@pname", pname.Text);
                    cmd.Parameters.AddWithValue("@barcode", txtBarcode.Text);
                    cmd.Parameters.AddWithValue("@pdesc", pdesc.Text);
                    cmd.Parameters.AddWithValue("@bid", bid);
                    cmd.Parameters.AddWithValue("@cid", cid);
                    cmd.Parameters.AddWithValue("@vendorid", vendorid);
                    cmd.Parameters.AddWithValue("@price", double.Parse(price.Text));
                    cmd.Parameters.AddWithValue("@reorder", int.Parse(txtReorder.Text));
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Product has been saved successfully.", "Save Product", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear();
                    frmProductList.LoadProducts();
                }


            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to update this Product?", "Update Product", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string bid = "", cid = "", vendorid="";

                    con.Open();
                    cmd = new SqlCommand("Select id from tblBrand where brand like '" + cboBrand.Text + "'", con);
                    dr = cmd.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        bid = dr[0].ToString();
                    }
                    dr.Close();
                    con.Close();

                    con.Open();
                    cmd = new SqlCommand("Select id from tblCategory where category like '" + cboCategory.Text + "'", con);
                    dr = cmd.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        cid = dr[0].ToString();
                    }
                    dr.Close();
                    con.Close();

                    con.Open();
                    cmd = new SqlCommand("Select id from tblVendor where vendor like '" + cboVendor.Text + "'", con);
                    dr = cmd.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        vendorid = dr[0].ToString();
                    }
                    dr.Close();
                    con.Close();

                    con.Open();
                    cmd = new SqlCommand("update tblProduct set  pname =@pname, barcode=@barcode, pdesc=@pdesc, bid=@bid, cid=@cid, vendorid=@vendorid, price=@price, reorder=@reorder where pcode like @pcode", con);
                    cmd.Parameters.AddWithValue("@pcode", pcode.Text);
                    cmd.Parameters.AddWithValue("@pname", pname.Text); 
                    cmd.Parameters.AddWithValue("@barcode", txtBarcode.Text);
                    cmd.Parameters.AddWithValue("@pdesc", pdesc.Text);
                    cmd.Parameters.AddWithValue("@bid", bid);
                    cmd.Parameters.AddWithValue("@cid", cid);
                    cmd.Parameters.AddWithValue("@vendorid", vendorid);
                    cmd.Parameters.AddWithValue("@price", double.Parse(price.Text));
                    cmd.Parameters.AddWithValue("@reorder", int.Parse(txtReorder.Text));
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Product has been update successfully.", "Update Product", MessageBoxButtons.OK ,MessageBoxIcon.Information);
                    Clear();
                    frmProductList.LoadProducts();
                    this.Dispose();
                    d.LoadDashboard();
                }


            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void price_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 46)
            {
                //accept .character
            }
            else if (e.KeyChar == 8) {
                //accept backspace character
            }
            else if((e.KeyChar < 48) || (e.KeyChar > 57)) //ascii code 48-57 between 0-9
            {
                e.Handled = true;
            }
        }

        private void price_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
