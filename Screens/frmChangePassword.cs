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
    public partial class frmChangePassword : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        DbConnection db = new DbConnection();
        SqlDataReader dr;
        frmPOS f;

        public frmChangePassword(frmPOS frm)
        {
            InitializeComponent();
            con = new SqlConnection(db.MyConnection());
            f = frm;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string _oldpass = db.GetPassword(f.lblUsername.Text);
                if (_oldpass != txtOldpass.Text)
                {
                    MessageBox.Show("Old password did not matched!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (txtNewpass.Text == String.Empty && txtConfirmpass.Text == String.Empty)
                {
                    MessageBox.Show("Please enter password!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (txtNewpass.Text != txtConfirmpass.Text)
                {
                    MessageBox.Show("Confirm new password did not matched!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    if (MessageBox.Show("Change Password?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        con.Open();
                        cmd = new SqlCommand("update tbluser set password = @password where username = @username", con);
                        cmd.Parameters.AddWithValue("@password", txtNewpass.Text);
                        cmd.Parameters.AddWithValue("@username", f.lblUsername.Text);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Password has been changed successfully.", "Save Changes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void frmChangePassword_Load(object sender, EventArgs e)
        {

        }
    }
}
