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
    public partial class frmUserAccount : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        DbConnection db = new DbConnection();
        SqlDataReader dr;
        string title = "Garments Zone";
        Dashboard d;
        public frmUserAccount(Dashboard d)
        {
            InitializeComponent();
            con = new SqlConnection(db.MyConnection());
            this.d = d;
            LoadUser();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void frmUserAccount_Resize(object sender, EventArgs e)
        {
            metroTabControl1.Left = (this.Width - metroTabControl1.Width) / 2;
            metroTabControl1.Top = (this.Height - metroTabControl1.Height) / 2;
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void frmUserAccount_Load(object sender, EventArgs e)
        {
            txtUser.Focus();
        }
        private void Clear()
        {
            txtName.Clear();
            txtPass.Clear();
            txtRetype.Clear();
            txtUser.Clear();
            cbRole.Text = "";
            txtUser.Focus();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPass.Text != txtRetype.Text)
                {
                    MessageBox.Show("Password did not match!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                con.Open();
                cmd = new SqlCommand("insert into tbluser (username, password, role, name) values (@username, @password, @role, @name)", con);
                cmd.Parameters.AddWithValue("@username", txtUser.Text);
                cmd.Parameters.AddWithValue("@password", txtPass.Text);
                cmd.Parameters.AddWithValue("@role", cbRole.Text);
                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("New Account has been created!", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                Clear();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtOldPass.Text != d._pass)
                {
                    MessageBox.Show("Old password did not matched!", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (txtNewPass.Text != txtConfirmPass.Text)
                {
                    MessageBox.Show("Confirm new password did not matched!", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                con.Open();
                cmd = new SqlCommand("update tblUser set password =@password where username =@username", con);
                cmd.Parameters.AddWithValue("@password", txtConfirmPass.Text);
                cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Password has been changed successfully!", "Change Password", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtOldPass.Clear();
                txtNewPass.Clear();
                txtConfirmPass.Clear();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtNewPass_TextChanged(object sender, EventArgs e)
        {

        }

        public void LoadUser()
        {
            cboUser.Items.Clear();
            con.Open();
            cmd = new SqlCommand("select * from tblUser", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cboUser.Items.Add(dr["username"].ToString());
            }
            dr.Close();
            con.Close();
        }


        private void cboUser_TextChanged(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                cmd = new SqlCommand("select * from tbluser where username = @username", con);
                cmd.Parameters.AddWithValue("@username", cboUser.Text);
                dr = cmd.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    checkBox1.Checked = bool.Parse(dr["isactive"].ToString());
                }
                else
                {
                    checkBox1.Checked = false;
                }
                dr.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                cmd = new SqlCommand("update tbluser set isactive =@isactive where username =@username", con);
                cmd.Parameters.AddWithValue("@isactive", checkBox1.Checked.ToString());
                cmd.Parameters.AddWithValue("@username", cboUser.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Account status has been changed.", "Update Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                checkBox1.Checked = false;
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
