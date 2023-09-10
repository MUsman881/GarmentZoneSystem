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
    public partial class frmStockIn : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
        DbConnection db = new DbConnection();
        string stitle = "Garments Zone";

        public frmStockIn()
        {
            InitializeComponent();
            con = new SqlConnection(db.MyConnection());
            LoadVendor();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


        }

        public void LoadStockIn()
        {
            int i = 0;
            dataGridView2.Rows.Clear();
            con.Open();
            cmd = new SqlCommand("select * from vwStockin where refno like '" + txtRefNo.Text + "' and status like 'Pending'", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView2.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr["vendor"].ToString());
            }
            dr.Close();
            con.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dataGridView2.Columns[e.ColumnIndex].Name;
            if (colName == "colDelete")
            {
                if (MessageBox.Show("Remove this item?", stitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cmd = new SqlCommand("delete from tblstockin where id = '" + dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                    cmd.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("Item has been removed successfully.", stitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadStockIn();
                }
            }
        }

        private void frmStockIn_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if(cboVendor.Text == String.Empty)
            {
                MessageBox.Show("Please select vendor to browse products.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            frmSearchProductStockIn frm = new frmSearchProductStockIn(this);
            frm.LoadProducts();
            frm.ShowDialog();
        }

        public void Clear()
        {
            txtRefNo.Clear();
            txtStockby.Clear();
            date.Value = DateTime.Now;
            cboVendor.Text = "";
            txtAddress.Clear();
            txtPerson.Clear();
        }

        private void LoadStockHistory()
        {
            int i = 0;
            dataGridView1.Rows.Clear();
            con.Open();
            cmd = new SqlCommand("select * from vwStockin where cast(sdate as date) between '" + date1.Value.ToShortDateString() + "' and '" + date2.Value.ToShortDateString() + "' and status like 'Done'", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView1.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), DateTime.Parse(dr[5].ToString()).ToShortDateString(), dr[6].ToString(), dr["vendor"].ToString());
            }
            dr.Close();
            con.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView2.Rows.Count > 0)
                {
                    if (MessageBox.Show("Are you sure you want to save this records?", stitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        for (int i = 0; i < dataGridView2.Rows.Count; i++)
                        {
                            //update tblproduct qty
                            con.Open();
                            cmd = new SqlCommand("update tblproduct set qty = qty + " + int.Parse(dataGridView2.Rows[i].Cells[5].Value.ToString()) + " where pcode like '" + dataGridView2.Rows[i].Cells[3].Value.ToString() + "'", con);
                            cmd.ExecuteNonQuery();
                            con.Close();

                            //update tblstockin qty
                            con.Open();
                            cmd = new SqlCommand("update tblstockin set qty = qty + " + int.Parse(dataGridView2.Rows[i].Cells[5].Value.ToString()) + ", status ='Done' where id like '" + dataGridView2.Rows[i].Cells[1].Value.ToString() + "'", con);
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                        Clear();
                        LoadStockIn(); 
                    }
                }
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, stitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cboVendor_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        public void LoadVendor()
        {
            cboVendor.Items.Clear();
            con.Open();
            cmd = new SqlCommand("select * from tblVendor", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cboVendor.Items.Add(dr["vendor"].ToString());
            }
            dr.Close();
            con.Close();
        }

        private void cboVendor_TextChanged(object sender, EventArgs e)
        {
            con.Open();
            cmd = new SqlCommand("select * from tblVendor where vendor like '" + cboVendor.Text + "'", con);
            dr = cmd.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                lblVendorID.Text = dr["id"].ToString();
                txtPerson.Text = dr["contactperson"].ToString();
                txtAddress.Text = dr["address"].ToString();
            }
            dr.Close();
            con.Close();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Random rnd = new Random();
            txtRefNo.Clear();
            txtRefNo.Text += rnd.Next();
        }

        private void linkStockHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LoadStockHistory();
        }
    }
}
