using GarmentZone.DBConnection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace GarmentZone.Screens
{
    public partial class frmUpdateUser : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
        DbConnection db = new DbConnection();
        frmUserAccount frm;

        public frmUpdateUser(frmUserAccount f)
        {
            InitializeComponent();
            con = new SqlConnection(db.MyConnection());
            frm = f;
        }

        private void frmUpdateUser_Load(object sender, EventArgs e)
        {

        }

        private void Clear()
        {
            txtUser.Clear();
            txtName.Clear();
            cbRole.Items.Clear();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                cmd = new SqlCommand("update tbluser set username = @username, role = @role, name = @name, isactive = @isactive where username like @username", con);
                cmd.Parameters.AddWithValue("@username", txtUser.Text);
                cmd.Parameters.AddWithValue("@role", cbRole.Text);
                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@isactive", isActive.Checked.ToString());
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("User has been updated!", "Garments Zone", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Clear();
                this.Dispose();
                frm.LoadUsers();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Garments Zone", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
