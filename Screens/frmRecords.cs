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
using System.Xml.Linq;
using System.Windows.Forms.DataVisualization.Charting;

namespace GarmentZone.Screens
{
    public partial class frmRecords : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
        DbConnection db = new DbConnection();
        public frmRecords()
        {
            InitializeComponent();
            con = new SqlConnection(db.MyConnection());
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void LoadRecord()
        {
            dataGridView1.Rows.Clear();
            int i = 0;
            con.Open();
            if(cboTopSelect.Text == "SORT BY QTY")
            {
                cmd = new SqlCommand("select top 10 pcode, pname, isnull(sum(qty),0) as qty, isnull(sum(total),0) as total from vwSoldItems where sdate between '" + metroDateTime1.Value.ToString() + "' and '" + metroDateTime2.Value.ToString() + "' and status like 'Sold' group by pcode, pname order by qty desc", con);
            }
            else if(cboTopSelect.Text == "SORT BY TOTAL AMOUNT")
            {
                cmd = new SqlCommand("select top 10 pcode, pname, isnull(sum(qty),0) as qty, isnull(sum(total),0) as total from vwSoldItems where sdate between '" + metroDateTime1.Value.ToString() + "' and '" + metroDateTime2.Value.ToString() + "' and status like 'Sold' group by pcode, pname order by total desc", con);
            }
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView1.Rows.Add(i, dr["pcode"].ToString(), dr["pname"].ToString(), dr["qty"].ToString(), dr["total"].ToString());
            }
            dr.Close();
            con.Close();
        }

        public void LoadInventory()
        {
            dataGridView4.Rows.Clear();
            int i = 0;
            con.Open();
            cmd = new SqlCommand("select p.pcode, p.barcode, p.pname, b.brand, c.category, p.price, p.qty, p.reorder from tblproduct as p inner join tblbrand as b on p.bid = b.id inner join tblcategory as c on p.cid = c.id", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView4.Rows.Add(i, dr["pcode"].ToString(), dr["barcode"].ToString(), dr["pname"].ToString(), dr["brand"].ToString(), dr["category"].ToString(), dr["price"].ToString(), dr["reorder"].ToString(), dr["qty"].ToString());
            }
            dr.Close();
            con.Close();
        }

        public void LoadCriticalItems()
        {
            try
            {
                dataGridView3.Rows.Clear();
                int i = 0;
                con.Open();
                cmd = new SqlCommand("select * from vwCriticalItems", con);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    dataGridView3.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString());
                }
                dr.Close();
                con.Close();
            }
            catch(Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
           
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {
        }

        private void linkPrint_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmInventoryReport frm = new frmInventoryReport();
            frm.LoadInventoryReport();
            frm.ShowDialog();
        }

        public void LoadCancelledItems()
        {
            dataGridView6.Rows.Clear();
            int i = 0;
            con.Open();
            cmd = new SqlCommand("select * from vwcancelleditems where sdate between '" + metroDateTime5.Value.ToString() + "' and '" + metroDateTime6.Value.ToString() + "'", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView6.Rows.Add(i, dr["transno"].ToString(), dr["pcode"].ToString(), dr["pname"].ToString(), dr["price"].ToString(), dr["qty"].ToString(), dr["total"].ToString(), dr["sdate"].ToString(), dr["voidby"].ToString(), dr["cancelledby"].ToString(), dr["reason"].ToString(), dr["action"].ToString());
            }
            dr.Close();
            con.Close();
        }

        private void btnLoadCancelledItems_Click(object sender, EventArgs e)
        {
           
        }

        public void LoadStockInHistory()
        {
            int i = 0;
            dataGridView7.Rows.Clear();
            con.Open();
            cmd = new SqlCommand("select * from vwStockin where cast(sdate as date) between '" + metroDateTime7.Value.ToShortDateString() + "' and '" + metroDateTime8.Value.ToShortDateString() + "' and status like 'Done'", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView7.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString());
            }
            dr.Close();
            con.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        public void LoadChartTopSelling()
        {
            SqlDataAdapter da = new SqlDataAdapter();
            con.Open();
            if (cboTopSelect.Text == "SORT BY QTY")
            {
                da = new SqlDataAdapter("select top 10  pcode, isnull(sum(qty),0) as qty from vwSoldItems where sdate between '" + metroDateTime1.Value.ToString() + "' and '" + metroDateTime2.Value.ToString() + "' and status like 'Sold' group by pcode order by qty desc", con);
            }
            else if (cboTopSelect.Text == "SORT BY TOTAL AMOUNT")
            {
                da = new SqlDataAdapter("select top 10 pcode, isnull(sum(total),0) as total from vwSoldItems where sdate between '" + metroDateTime1.Value.ToString() + "' and '" + metroDateTime2.Value.ToString() + "' and status like 'Sold' group by pcode order by total desc", con);
            }
           
            DataSet ds = new DataSet();
            da.Fill(ds, "TOPSELLING");
            chart1.DataSource = ds.Tables["TOPSELLING"];
            Series series = chart1.Series[0];
            series.ChartType = SeriesChartType.Doughnut;

            series.Name = "TOP SELLING";
            var chart = chart1;
            chart.Series[0].XValueMember = "pcode";
            if (cboTopSelect.Text == "SORT BY QTY") 
            {
                chart.Series[0].YValueMembers = "qty";
            }
            if (cboTopSelect.Text == "SORT BY TOTAL AMOUNT")
            {
                chart.Series[0].YValueMembers = "total";
            }
            chart.Series[0].IsValueShownAsLabel = true;
            if (cboTopSelect.Text == "SORT BY QTY")
            {
                chart.Series[0].LabelFormat = "{#,##0}";
            }
            if (cboTopSelect.Text == "SORT BY TOTAL AMOUNT")
            {
                chart.Series[0].LabelFormat = "{#,##0.00}";
            }
            con.Close();
        }

        private void cboTopSelect_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblSoldItems_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                dataGridView1.Rows.Clear();
                int i = 0;
                con.Open();
                cmd = new SqlCommand("select c.pcode, p.pname, c.price, sum(c.qty) as total_qty, sum(c.disc) as total_disc, sum(c.total) as total from tblcart as c inner join tblproduct as p on c.pcode = p.pcode where status like 'Sold' and sdate between '" + metroDateTime3.Value.ToString() + "' and '" + metroDateTime4.Value.ToString() + "' group by c.pcode, p.pname, c.price", con);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    dataGridView2.Rows.Add(i, dr["pcode"].ToString(), dr["pname"].ToString(), double.Parse(dr["price"].ToString()).ToString("#,##0.00"), dr["total_qty"].ToString(), dr["total_disc"].ToString(), double.Parse(dr["total"].ToString()).ToString("#,##0.00"));
                }
                dr.Close();
                con.Close();

                con.Open();
                cmd = new SqlCommand("select isnull(sum(total),0) as total from tblcart where status like 'Sold' and sdate between '" + metroDateTime3.Value.ToString() + "' and '" + metroDateTime4.Value.ToString() + "'", con);
                label4.Text = double.Parse(cmd.ExecuteScalar().ToString()).ToString("#,##0.00");
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void linkPrintSoldItems_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (dataGridView2.Rows.Count > 0) 
            {
                frmInventoryReport frm = new frmInventoryReport();
                frm.LoadSoldItems("select c.pcode, p.pname, c.price, sum(c.qty) as total_qty, sum(c.disc) as total_disc, sum(c.total) as total from tblcart as c inner join tblproduct as p on c.pcode = p.pcode where status like 'Sold' and sdate between '" + metroDateTime3.Value.ToString() + "' and '" + metroDateTime4.Value.ToString() + "' group by c.pcode, p.pname, c.price", "From : " + metroDateTime3.Value.ToString() + " To : " + metroDateTime4.Value.ToString());
                frm.ShowDialog();
            }
        }

        private void linkPrintTopSelling_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmInventoryReport frm = new frmInventoryReport();
            if (cboTopSelect.Text == "SORT BY QTY")
            {
                frm.LoadTopSelling("select top 10 pcode, pname, isnull(sum(qty),0) as qty, isnull(sum(total),0) as total from vwSoldItems where sdate between '" + metroDateTime1.Value.ToString() + "' and '" + metroDateTime2.Value.ToString() + "' and status like 'Sold' group by pcode, pname order by qty desc", "From : " + metroDateTime1.Value.ToString() + " To : " + metroDateTime2.Value.ToString(), "TOP SELLING ITEMS SORT BY QTY");
            }
            else if (cboTopSelect.Text == "SORT BY TOTAL AMOUNT")
            {
                frm.LoadTopSelling("select top 10 pcode, pname, isnull(sum(qty),0) as qty, isnull(sum(total),0) as total from vwSoldItems where sdate between '" + metroDateTime1.Value.ToString() + "' and '" + metroDateTime2.Value.ToString() + "' and status like 'Sold' group by pcode, pname order by total desc", "From : " + metroDateTime1.Value.ToString() + " To : " + metroDateTime2.Value.ToString(), "TOP SELLING ITEMS SORT BY TOTAL AMOUNT");
            }
            frm.ShowDialog();
        }

        private void linkLoadTopSelling_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (cboTopSelect.Text == String.Empty)
            {
                MessageBox.Show("Please select from the dropdown list.", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            LoadRecord();
            LoadChartTopSelling();
        }

        private void linkLoadChart_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmChart f = new frmChart();
            f.lblTitle.Text = "SOLD ITEMS [ " + metroDateTime3.Value.ToShortDateString() + " - " + metroDateTime4.Value.ToShortDateString() + " ]";
            f.LoadChartSold("select p.pname, sum(c.total) as total from tblcart as c inner join tblproduct as p on c.pcode = p.pcode where status like 'Sold' and sdate between '" + metroDateTime3.Value.ToString() + "' and '" + metroDateTime4.Value.ToString() + "' group by p.pname order by total desc");
            f.ShowDialog();
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void panel4_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void linkCancelledItems_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LoadCancelledItems();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void linkStockHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LoadStockInHistory();
        }

        private void linkPrintStockHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmInventoryReport frm = new frmInventoryReport();
            string param = "Date Covered: " + metroDateTime7.Value.ToString() + " - " + metroDateTime8.Value.ToString();
            frm.LoadStockInReport("select * from vwStockin where cast(sdate as date) between '" + metroDateTime7.Value.ToShortDateString() + "' and '" + metroDateTime8.Value.ToShortDateString() + "' and status like 'Done'", param);
            frm.ShowDialog();
        }

        private void linkCancelledReport_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmInventoryReport frm = new frmInventoryReport();
            string param = "Date Covered: "+ metroDateTime5.Value.ToString() + " - " + metroDateTime6.Value.ToString();
            frm.LoadCancelledItemsReport("select * from vwcancelleditems where sdate between '" + metroDateTime5.Value.ToString() + "' and '" + metroDateTime6.Value.ToString() + "'", param);
            frm.ShowDialog(); 
        }
    }
}
