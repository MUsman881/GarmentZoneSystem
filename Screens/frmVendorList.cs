using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GarmentZone.DBConnection;
using System.Data.SqlClient;

namespace GarmentZone.Screens
{
    public partial class frmVendorList : Form
    {
        SqlConnection cn;
        SqlCommand cmd = new SqlCommand();
        DbConnection db = new DbConnection();
        SqlDataReader dr;

        public frmVendorList()
        {
            InitializeComponent();
            cn = new SqlConnection();
            cn.ConnectionString = db.MyConnection();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            frmVendor frm = new frmVendor(this);
            frm.btnSave.Enabled = true;
            frm.btnUpdate.Enabled = false;
            frm.ShowDialog();
        }

        public void LoadVendor()
        {
            dataGridView1.Rows.Clear();
            int i = 0;
            cn.Open();
            cmd = new SqlCommand("select * from tblVendor", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView1.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString());
            }
            dr.Close();
            cn.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dataGridView1.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                frmVendor frm = new frmVendor(this);
                frm.lblID.Text = dataGridView1[1, e.RowIndex].Value.ToString();
                frm.txtVendor.Text = dataGridView1[2, e.RowIndex].Value.ToString();
                frm.txtAddress.Text = dataGridView1[3, e.RowIndex].Value.ToString();
                frm.txtPerson.Text = dataGridView1[4, e.RowIndex].Value.ToString();
                frm.txtMobile.Text = dataGridView1[5, e.RowIndex].Value.ToString();
                frm.txtEmail.Text = dataGridView1[6, e.RowIndex].Value.ToString();
                frm.btnSave.Enabled = false;
                frm.btnUpdate.Enabled = true;
                frm.ShowDialog();
            }
            else if (colName == "Delete")
            {
                if (MessageBox.Show("DELETE THIS VENDOR? CLICK YES TO CONFIRM", "DELETE VENDOR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cmd = new SqlCommand("delete from tblVendor where id like '" + dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", cn);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("VENDOR HAS BEEN DELETED SUCCESSFULLY.", "DELETE VENDOR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadVendor();
                }
            }
        }
    }
}
