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
    public partial class frmProductList : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
        DbConnection db = new DbConnection();

        public frmProductList()
        {
            InitializeComponent();
            con = new SqlConnection(db.MyConnection());
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            frmProduct frm = new frmProduct(this);
            frm.btnSave.Enabled = true;
            frm.btnUpdate.Enabled = false;
            frm.LoadCategory();
            frm.LoadBrand();
            frm.ShowDialog();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void LoadProducts()
        {
            int i = 0;

            dataGridView1.Rows.Clear();
            con.Open();
            cmd = new SqlCommand("select p.pcode, p.pname, p.barcode, p.pdesc, b.brand, c.category, v.vendor, p.price, p.reorder from tblProduct as p inner join tblBrand as b on b.id = p.bid inner join tblCategory as c on c.id = p.cid inner join tblVendor as v on v.id = p.vendorid  where p.pname like '" + txtSearch.Text + "%'", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i += 1;
                dataGridView1.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString(), dr[8].ToString());
            }
            dr.Close();
            con.Close();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadProducts();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dataGridView1.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                frmProduct frm = new frmProduct(this);
                frm.btnSave.Enabled = false;
                frm.btnUpdate.Enabled = true;
                frm.pcode.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                frm.pname.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                frm.txtBarcode.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                frm.pdesc.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                frm.cboBrand.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                frm.cboCategory.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                frm.cboVendor.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                frm.price.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                frm.txtReorder.Text = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();

                frm.ShowDialog();
            }
            else if (colName == "Delete")
            {
                if (MessageBox.Show("Are you sure you want to delete this product?", "Delete Product", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cmd = new SqlCommand("delete from tblProduct where pcode like '" + dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Category has been delete successfully.", "Garments Zone", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadProducts();
                }
            }
        }
    }
}
