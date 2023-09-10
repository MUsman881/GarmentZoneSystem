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
using Tulpep.NotificationWindow;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO.Ports;

namespace GarmentZone.Screens
{
    public partial class Dashboard : MetroFramework.Forms.MetroForm
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
        DbConnection db = new DbConnection();
        public string _pass;
        public Dashboard()
        {
            InitializeComponent();
            con = new SqlConnection(db.MyConnection());
            NotifyCriticalItems();
            lblDailySales.Text = db.DailySales().ToString("#,##0.00");
            lblProductline.Text = db.ProductLine().ToString();
            lblStockOnHand.Text = db.StockOnHand().ToString();
            lblCriticalItems.Text = db.CriticalItems().ToString();
            LoadChart();
        }

        public void LoadChart()
        {
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter("select Year(sdate) as year, isnull(sum(total),0.0) as total from tblcart where status like 'Sold' group by Year(sdate)", con);
            DataSet ds = new DataSet();

            da.Fill(ds, "Sales");
            chart1.DataSource = ds.Tables["Sales"];
            Series series1 = chart1.Series["Series1"];
            series1.ChartType = SeriesChartType.Doughnut;

            series1.Name = "SALES";

            var chart = chart1;
            chart.Series[series1.Name].XValueMember = "year";
            chart.Series[series1.Name].YValueMembers = "total";
            chart.Series[0].IsValueShownAsLabel = true;
            con.Close();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {

        }
        
        public void NotifyCriticalItems()
        {
            string critical = "";
            con.Open();
            cmd = new SqlCommand("select count(*) from vwCriticalItems", con);
            string count = cmd.ExecuteScalar().ToString();
            con.Close();

            int i = 0;
            con.Open();
            cmd = new SqlCommand("select * from vwCriticalItems", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                critical += i + ". " + dr["pname"].ToString() + Environment.NewLine;
            }
            dr.Close();
            con.Close();

            PopupNotifier popup = new PopupNotifier();
            popup.Image = Properties.Resources.icons8_error_24;
            popup.TitleText = count + " CRITICAL ITEM(S)";
            popup.ContentText = critical;
            popup.Popup();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("LOGOUT APPLICATION?", "CONFIRM", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Hide();
                Login frm = new Login();
                frm.ShowDialog();
            }
        }

        private void btnBrand_Click(object sender, EventArgs e)
        {
            frmBrandList frm = new frmBrandList();
            frm.TopLevel = false;
            panel3.Controls.Add(frm);
            frm.BringToFront();
            frm.Show();
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            frmCategoryList frm = new frmCategoryList();
            frm.TopLevel = false;
            panel3.Controls.Add(frm);
            frm.BringToFront();
            frm.LoadCategories();
            frm.Show();
        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            frmProductList frm = new frmProductList();
            frm.TopLevel = false;
            panel3.Controls.Add(frm);
            frm.BringToFront();
            frm.LoadProducts();
            frm.Show();
        }

        private void btnStockIn_Click(object sender, EventArgs e)
        {
            frmStockIn frm = new frmStockIn();
            frm.TopLevel = false;
            panel3.Controls.Add(frm);
            frm.BringToFront();
            frm.Show();
        }

        private void btnUser_Click(object sender, EventArgs e)
        {
            frmUserAccount frm = new frmUserAccount(this);
            frm.TopLevel = false;
            frm.txtUsername.Text = lblUser.Text;
            panel3.Controls.Add(frm);
            frm.BringToFront();
            frm.Show();
        }

        private void btnSalesHistory_Click(object sender, EventArgs e)
        {
            frmSoldItems frm = new frmSoldItems();
            frm.ShowDialog();
        }

        private void btnRecords_Click(object sender, EventArgs e)
        {
            frmRecords frm = new frmRecords();
            frm.TopLevel = false;
            frm.LoadCriticalItems();
            frm.LoadInventory();
            frm.LoadCancelledItems();
            panel3.Controls.Add(frm);
            frm.BringToFront();
            frm.Show();
        }

        private void btnSystemSettings_Click(object sender, EventArgs e)
        {
            frmStore frm = new frmStore();
            frm.TopLevel = false;
            frm.LoadRecords();
            panel3.Controls.Add(frm);
            frm.BringToFront();
            frm.Show();
        }

        private void Dashboard_Resize(object sender, EventArgs e)
        {
            panelblock.Left = (this.Width - panelblock.Width) / 10;
        }

        private void btnVendor_Click(object sender, EventArgs e)
        {
            frmVendorList frm = new frmVendorList();
            frm.TopLevel = false;
            frm.LoadVendor();
            panel3.Controls.Add(frm);
            frm.BringToFront();
            frm.Show();
        }

        private void btnAdjustment_Click(object sender, EventArgs e)
        {
            frmAdjustment f = new frmAdjustment(this);
            f.TopLevel = false;
            panel3.Controls.Add(f);
            f.LoadProducts();
            f.txtUser.Text = lblUser.Text;
            f.ReferenceNo();
            f.BringToFront();
            f.Show();
        }

        private void panelblock_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
