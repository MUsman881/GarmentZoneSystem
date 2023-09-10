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
    public partial class frmBrand : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        DbConnection db = new DbConnection();
        frmBrandList frmlist;

        public frmBrand(frmBrandList flist)
        {
            InitializeComponent();
            con = new SqlConnection(db.MyConnection());
            frmlist = flist;
        }

        private void frmBrand_Load(object sender, EventArgs e)
        {

        }

        private void Clear()
        {
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            txtBrand.Clear();
            txtBrand.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if(MessageBox.Show("Are you sure you want to save this brand?","New Brand",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cmd = new SqlCommand("INSERT into tblBrand(Brand)Values(@brand)", con);
                    cmd.Parameters.AddWithValue("@brand", txtBrand.Text);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Brand has been saved successfully.");
                    Clear();
                    frmlist.LoadBrands();
                }
               

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to update this brand?", "Update Brand", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cmd = new SqlCommand("update tblBrand set brand = @brand where id like '" + lblID.Text + "'", con);
                    cmd.Parameters.AddWithValue("@brand", txtBrand.Text);
                    cmd.ExecuteNonQuery();
                    con.Close(); 
                    MessageBox.Show("Brand has been update successfully.");
                    Clear();
                    frmlist.LoadBrands();
                    this.Dispose();
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
