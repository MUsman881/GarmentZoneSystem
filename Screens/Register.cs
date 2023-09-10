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
    public partial class Register : MetroFramework.Forms.MetroForm
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        DbConnection db = new DbConnection();
        SqlDataReader dr;
        public Register()
        {
            InitializeComponent();
            con = new SqlConnection(db.MyConnection());
        }

        private void Register_Load(object sender, EventArgs e)
        {
            txtUsername.Focus();
        }

        private void Clear()
        {
            txtConfrmPass.Clear();
            txtName.Clear();
            txtPass.Clear();
            cbRole.Text = "";
            txtUsername.Clear();
            txtUsername.Focus();
        }

        private void txtLogin_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login frm = new Login();
            frm.ShowDialog();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPass.Text != txtConfrmPass.Text)
                {
                    MessageBox.Show("Password did not match!", "Wrong Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtUsername.Text == "")
                {
                    MessageBox.Show("Please enter your username", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                con.Open();
                cmd = new SqlCommand("insert into tblUser (username, password, role, name)values(@username, @password, @role, @name)", con);
                cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                cmd.Parameters.AddWithValue("@password", txtPass.Text);
                cmd.Parameters.AddWithValue("@role", cbRole.Text);
                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.ExecuteNonQuery();

                con.Close();
                MessageBox.Show("Your account has been registerd successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Clear();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Error");
            }
        }
    }
}
