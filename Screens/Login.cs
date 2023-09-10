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
    public partial class Login : MetroFramework.Forms.MetroForm
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        DbConnection db = new DbConnection();
        SqlDataReader dr;
        public string _password;
        public bool _isactive = false;

        public Login()
        {
            InitializeComponent();
            con = new SqlConnection(db.MyConnection());
        }

        private void Login_Load(object sender, EventArgs e)
        {
            txtUsername.Focus();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string _role = "", _name = "", _username="";
            try
            {
                bool found = false;

                con.Open();
                cmd = new SqlCommand("Select * from tblUser where username = @username and password = @password", con);
                cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                cmd.Parameters.AddWithValue("@password", txtPassword.Text);
                dr = cmd.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    found = true;
                    _role = dr["role"].ToString();
                    _username = dr["username"].ToString();
                    _name = dr["name"].ToString();
                    _password = dr["password"].ToString();
                    _isactive = bool.Parse(dr["isactive"].ToString());
                }
                dr.Close();
                con.Close();

                if (found == true)
                {
                    if(_isactive == false)
                    {
                        MessageBox.Show("Account is inactive. Unable to login", "Inactive Account", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                    if (_role == "Cashier")
                    {
                        MessageBox.Show("Welcome " + _name + "!", "Access Granted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPassword.Clear();
                        txtUsername.Clear();
                        this.Hide();
                        frmPOS frm = new frmPOS(this);
                        frm.lblUser.Text = _name;
                        frm.lblUsername.Text = _username;
                        frm.lblName.Text = _name + " | " + _role;
                        frm.ShowDialog();
                    } 
                    else
                    {
                        MessageBox.Show("Welcome " + _name + "!", "Access Granted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPassword.Clear();
                        txtUsername.Clear();
                        this.Hide();
                        Dashboard frm = new Dashboard();
                        frm.lblUser.Text = _username;
                        frm.lblName.Text = _name;
                        frm.lblRole.Text = _role;
                        frm._pass = _password;
                        frm.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show("Invalid username or password!", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("EXIT APPLICATION?","CONFIRM", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void txtUsername_Enter(object sender, EventArgs e)
        {
            //txtPassword.Focus();
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            //btnLogin.Focus();
        }
    }
}
