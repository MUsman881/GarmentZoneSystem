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
    public partial class frmSearchProductStockIn : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
        DbConnection db = new DbConnection();
        string stitle = "Garments Zone";
        frmStockIn f;

        public frmSearchProductStockIn(frmStockIn flist)
        {
            InitializeComponent();
            f = flist;
            con = new SqlConnection(db.MyConnection());
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
            cmd = new SqlCommand("Select pcode, pname, pdesc, qty from tblProduct where pname like '%" + txtSearch.Text + "%' and vendorid = '" + f.lblVendorID.Text + "' order by pname", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView1.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString());
            }
            dr.Close();
            con.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dataGridView1.Columns[e.ColumnIndex].Name;
            if (colName == "Select")
            {
                if (f.txtRefNo.Text == String.Empty)
                {
                    MessageBox.Show("Please Enter Reference No", stitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    f.txtRefNo.Focus();
                    return;
                }
                if (f.txtStockby.Text == String.Empty)
                {
                    MessageBox.Show("Please Enter Stock In By", stitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    f.txtRefNo.Focus();
                    return;
                }
                if (MessageBox.Show("Add this item?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cmd = new SqlCommand("insert into tblstockin (refno, pcode, sdate, stockinby, vendorid)values(@refno, @pcode, @sdate, @stockinby, @vendorid)", con);
                    cmd.Parameters.AddWithValue("@refno", f.txtRefNo.Text);
                    cmd.Parameters.AddWithValue("@pcode", dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
                    cmd.Parameters.AddWithValue("@sdate", f.date.Value);
                    cmd.Parameters.AddWithValue("@stockinby", f.txtStockby.Text);
                    cmd.Parameters.AddWithValue("@vendorid", f.lblVendorID.Text);
                    cmd.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("Successfully added", stitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    f.LoadStockIn();
                }
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadProducts();
        }
    }
}
