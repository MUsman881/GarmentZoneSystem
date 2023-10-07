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
            LoadUsers();
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

        public void LoadUsers()
        {
            int i = 0;
            dataGridView1.Rows.Clear();
            con.Open();
            cmd = new SqlCommand("select name, username, role, isactive from tblUser where username like '" + txtSearch.Text + "%'", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView1.Rows.Add(i, dr["name"].ToString(), dr["username"].ToString(), dr["role"].ToString(), dr["isactive"].ToString());
            }
            dr.Close();
            con.Close();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadUsers();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dataGridView1.Columns[e.ColumnIndex].Name;

            if (colName == "Edit")
            {
                frmUpdateUser frm = new frmUpdateUser(this);
                frm.txtName.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                frm.txtUser.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                frm.cbRole.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                frm.isActive.Checked = bool.Parse(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString());
                frm.ShowDialog();
            }
        }
    }
}
