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
    public partial class frmLookUp : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
        DbConnection db = new DbConnection();
        frmPOS fpos;

        public frmLookUp(frmPOS frm)
        {
            InitializeComponent();
            con = new SqlConnection(db.MyConnection());
            fpos = frm;
            this.KeyPreview = true;
        }

        public void LoadProducts()
        {
            int i = 0;

            dataGridView1.Rows.Clear();
            con.Open();
            cmd = new SqlCommand("select p.pcode, p.pname, p.barcode, p.pdesc, b.brand, c.category, p.price, p.qty from tblProduct as p inner join tblBrand as b on b.id = p.bid inner join  tblCategory as c on c.id = p.cid where p.pname like '" + txtSearch.Text + "%'", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i += 1;
                dataGridView1.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString());
            }
            dr.Close();
            con.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadProducts();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dataGridView1.Columns[e.ColumnIndex].Name;
            if(colName == "Select")
            {
                frmQty frm = new frmQty(fpos);
                frm.ProductDetails(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString(), Double.Parse(dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString()), fpos.lblTransno.Text, int.Parse(dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString()));
                frm.ShowDialog();
            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void frmLookUp_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)
            {
                this.Dispose();
            }
        }
    }
}
