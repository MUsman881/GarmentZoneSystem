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
    public partial class frmSoldItems : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
        DbConnection db = new DbConnection();

        public string user;

        public frmSoldItems()
        {
            InitializeComponent();
            con = new SqlConnection(db.MyConnection());
            dt1.Value = DateTime.Now;
            dt2.Value = DateTime.Now;
            LoadRecord();
            LoadCashier();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dataGridView1.Columns[e.ColumnIndex].Name;
            if(colName == "colCancel")
            {
                frmCancelDetails frm = new frmCancelDetails(this);
                frm.txtID.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                frm.txtTransNo.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                frm.txtPCode.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                frm.txtPName.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                frm.txtPrice.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                frm.txtQty.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                frm.txtDisc.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                frm.txtTotal.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                frm.txtCancelby.Text = user;

                frm.ShowDialog();
            }
        }

        public void LoadRecord()
        {
            int i = 0;
            double _total = 0;
            dataGridView1.Rows.Clear();

            con.Open();
            if (cboCashier.Text == "All Cashier")
            {
                cmd = new SqlCommand("select c.id, c.transno, c.pcode, p.pname, c.price, c.qty, c.disc, c.total, c.customername from tblcart as c inner join tblproduct as p on c.pcode = p.pcode where status like 'Sold' and sdate between '" + dt1.Value + "' and '" + dt2.Value + "'", con);
            }
            else
            {
                cmd = new SqlCommand("select c.id, c.transno, c.pcode, p.pname, c.price, c.qty, c.disc, c.total, c.customername from tblcart as c inner join tblproduct as p on c.pcode = p.pcode where status like 'Sold' and sdate between '" + dt1.Value + "' and '" + dt2.Value + "' and cashier like '" + cboCashier.Text + "'", con);
            }
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                _total += double.Parse(dr["total"].ToString());
                dataGridView1.Rows.Add(i, dr["id"].ToString(), dr["transno"].ToString(), dr["pcode"].ToString(), dr["pname"].ToString(), dr["price"].ToString(), dr["qty"].ToString(), dr["disc"].ToString(), dr["total"].ToString(), dr["customername"].ToString());
            }
            dr.Close();
            con.Close();
            lblTotal.Text = _total.ToString("#,##0.00");
        }

        private void dt1_ValueChanged(object sender, EventArgs e)
        {
            LoadRecord();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            frmReportSold frm = new frmReportSold(this);
            frm.LoadReport();
            frm.ShowDialog();
        }

        private void cboCashier_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        public void LoadCashier()
        {
            cboCashier.Items.Clear();
            cboCashier.Items.Add("All Cashier");
            con.Open();
            cmd = new SqlCommand("select * from tbluser where role like 'Cashier'", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cboCashier.Items.Add(dr["name"].ToString()); 
            }
            con.Close();
        }

        private void dt2_ValueChanged(object sender, EventArgs e)
        {
            LoadRecord();
        }

        private void cboCashier_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadRecord();
        }

        private void frmSoldItems_Load(object sender, EventArgs e)
        {

        }

        private void lblTotal_Click(object sender, EventArgs e)
        {

        }
    }
}
