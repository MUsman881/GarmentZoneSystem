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
    public partial class frmStore : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        DbConnection db = new DbConnection();
        SqlDataReader dr;
        public frmStore()
        {
            InitializeComponent();
            con = new SqlConnection(db.MyConnection());
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void frmStore_Load(object sender, EventArgs e)
        {

        }

        public void LoadRecords()
        {
            con.Open();
            cmd = new SqlCommand("select * from tblstore", con);
            dr = cmd.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                txtAddress.Text = dr["address"].ToString();
                txtStore.Text = dr["store"].ToString();
            }
            else
            {
                txtStore.Clear();
                txtAddress.Clear();
            }
            con.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if(MessageBox.Show("SAVE STORE DETAILS?","CONFIRM", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int count;
                    con.Open();
                    cmd = new SqlCommand("select count(*) from tblstore", con);
                    count = int.Parse(cmd.ExecuteScalar().ToString());
                    con.Close();
                    if(count > 0)
                    {
                        con.Open();
                        cmd = new SqlCommand("update tblstore set store =@store, address=@address", con);
                        cmd.Parameters.AddWithValue("@store", txtStore.Text);
                        cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    else
                    {
                        con.Open();
                        cmd = new SqlCommand("insert into tblstore (store, address) values (@store, @address)", con);
                        cmd.Parameters.AddWithValue("@store", txtStore.Text);
                        cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    MessageBox.Show("STORE DETAILS HAS BEEN SUCCESSFULLY SAVED!", "SAVE RECORD", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex) 
            {
                con.Close();
                MessageBox.Show(ex.Message, "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }
    }
}
