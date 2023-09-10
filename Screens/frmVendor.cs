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
using GarmentZone.DBConnection;

namespace GarmentZone.Screens
{
    public partial class frmVendor : Form
    {
        frmVendorList f;
        SqlConnection cn;
        SqlCommand cmd = new SqlCommand();
        DbConnection db = new DbConnection();

        public frmVendor(frmVendorList frm)
        {
            InitializeComponent();
            cn = new SqlConnection();
            cn.ConnectionString = db.MyConnection();
            f = frm;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if(MessageBox.Show("SAVE THIS RECORD? CLICK YES TO CONFIRM.","CONFIRM", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cmd = new SqlCommand("INSERT INTO tblVendor(vendor, address, contactperson, telephone, email)values(@vendor, @address, @contactperson, @telephone, @email)", cn);
                    cmd.Parameters.AddWithValue("@vendor", txtVendor.Text);
                    cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                    cmd.Parameters.AddWithValue("@contactperson", txtPerson.Text);
                    cmd.Parameters.AddWithValue("@telephone", txtMobile.Text);
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("VENDOR HAS BEEN SUCCESSFULLY SAVED", "SAVE VENDOR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear();
                    f.LoadVendor();
                }
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public void Clear()
        {
            txtVendor.Clear();
            txtPerson.Clear();
            txtMobile.Clear();
            txtEmail.Clear();
            txtAddress.Clear();
            txtVendor.Focus();
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("UPDATE THIS RECORD? CLICK YES TO CONFIRM.", "CONFIRM", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cmd = new SqlCommand("Update tblVendor set vendor =@vendor, address =@address, contactperson = @contactperson, telephone =@telephone, email =@email where id = @id", cn);
                    cmd.Parameters.AddWithValue("@vendor", txtVendor.Text);
                    cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                    cmd.Parameters.AddWithValue("@contactperson", txtPerson.Text);
                    cmd.Parameters.AddWithValue("@telephone", txtMobile.Text);
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@id", lblID.Text);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("VENDOR HAS BEEN SUCCESSFULLY UPDATED", "UPDATE VENDOR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear();
                    f.LoadVendor();
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void frmVendor_Load(object sender, EventArgs e)
        {

        }
    }
}
