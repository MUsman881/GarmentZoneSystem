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
    public partial class frmVoid : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
        DbConnection db = new DbConnection();
        frmCancelDetails fc;
        public frmVoid(frmCancelDetails f)
        {
            InitializeComponent();
            con = new SqlConnection(db.MyConnection());
            fc = f;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnVoid_Click(object sender, EventArgs e)
        {
            try
            {
                if(txtPassword.Text != string.Empty)
                {
                    string user;
                    con.Open();
                    cmd = new SqlCommand("select * from tbluser where username = @username and password = @password", con);
                    cmd.Parameters.AddWithValue("@username", txtUser.Text);
                    cmd.Parameters.AddWithValue("@password", txtPassword.Text);
                    dr = cmd.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        user = dr["username"].ToString();
                        dr.Close();
                        con.Close();

                        SaveCancelOrder(user);
                        if(fc.cboAction.Text == "Yes")
                        {
                            UpdateData("update tblproduct set qty  = qty + " + int.Parse(fc.txtCancelQty.Text) + " where pcode = '" + fc.txtPCode.Text + "'");
                        }
                        UpdateData("update tblcart set qty = qty - " + int.Parse(fc.txtCancelQty.Text) + " where id like '" + fc.txtID.Text + "'");
                            
                        MessageBox.Show("Order Transaction successfully cancelled!", "Cancel Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Dispose();
                        fc.RefreshList();
                        fc.Dispose();
                    }
                    dr.Close();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void SaveCancelOrder(string user)
        {
            con.Open();
            cmd = new SqlCommand("insert into tblcancel (transno, pcode, price, qty, sdate, voidby, cancelledby, reason, action) values (@transno, @pcode, @price, @qty, @sdate, @voidby, @cancelledby, @reason, @action)", con);
            cmd.Parameters.AddWithValue("@transno", fc.txtTransNo.Text);
            cmd.Parameters.AddWithValue("@pcode", fc.txtPCode.Text);
            cmd.Parameters.AddWithValue("@price", double.Parse(fc.txtPrice.Text));
            cmd.Parameters.AddWithValue("@qty", int.Parse(fc.txtCancelQty.Text));
            cmd.Parameters.AddWithValue("@sdate", DateTime.Now);
            cmd.Parameters.AddWithValue("@voidby", user);
            cmd.Parameters.AddWithValue("@cancelledby", fc.txtCancelby.Text);
            cmd.Parameters.AddWithValue("@reason", fc.txtReason.Text);
            cmd.Parameters.AddWithValue("@action", fc.cboAction.Text);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void UpdateData(string sql)
        {
            con.Open();
            cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void frmVoid_Load(object sender, EventArgs e)
        {

        }

        private void txtPassword_Click(object sender, EventArgs e)
        {

        }
    }
}
