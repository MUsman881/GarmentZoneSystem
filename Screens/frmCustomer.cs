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
    public partial class frmCustomer : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        DbConnection db = new DbConnection();
        SqlDataReader dr;
        public frmCustomer()
        {
            InitializeComponent();
            con = new SqlConnection(db.MyConnection());
            LoadCustomers();
        }

        public void LoadCustomers()
        {
            int i = 0;
            con.Open();
            cmd = new SqlCommand("select * from tblcustomer order by name", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView1.Rows.Add(i, dr["name"].ToString(), dr["mobile"].ToString());
            }
            dr.Close();
            con.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
    }
}
