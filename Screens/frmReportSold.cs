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
using Microsoft.Reporting.WinForms;
using System.Security.Cryptography.X509Certificates;

namespace GarmentZone.Screens
{
    public partial class frmReportSold : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
        DbConnection db = new DbConnection();
        frmSoldItems frm;
        string storename = "Garments Zone";
        string address = "SHOP#2, Kohenoor City";

        public frmReportSold(frmSoldItems f)
        {
            InitializeComponent();
            con = new SqlConnection(db.MyConnection());
            frm = f;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void frmReportSold_Load(object sender, EventArgs e)
        {

        }


        public void LoadReport()
        {
            try
            {
                ReportDataSource rptDS;

                this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"\Reports\rptSoldItem.rdlc";
                this.reportViewer1.LocalReport.DataSources.Clear();

                DataSet1 ds = new DataSet1();
                SqlDataAdapter da = new SqlDataAdapter();

                con.Open();
                if (frm.cboCashier.Text == "All Cashier")
                {
                    da.SelectCommand = new SqlCommand("select c.id, c.transno, c.pcode, p.pname, c.price, c.qty, c.disc as discount, c.total from tblcart as c inner join tblproduct as p on c.pcode = p.pcode where status like 'Sold' and sdate between '" + frm.dt1.Value + "' and '" + frm.dt2.Value + "'", con);
                }
                else
                {
                    da.SelectCommand = new SqlCommand("select c.id, c.transno, c.pcode, p.pname, c.price, c.qty, c.disc as discount, c.total from tblcart as c inner join tblproduct as p on c.pcode = p.pcode where status like 'Sold' and sdate between '" + frm.dt1.Value + "' and '" + frm.dt2.Value + "' and cashier like '" + frm.cboCashier.Text + "'", con);
                }
                da.Fill(ds.Tables["dtSoldItemReport"]);
                con.Close();

                ReportParameter pDate = new ReportParameter("pDate", "Date From: " + frm.dt1.Value.ToShortDateString() + " To: " + frm.dt2.Value.ToShortDateString());
                ReportParameter pCashier = new ReportParameter("pCashier", "Cashier: " + frm.cboCashier.Text);
                ReportParameter pHeader = new ReportParameter("pHeader", "SALES REPORT");
                ReportParameter pStoreName = new ReportParameter("pStoreName", storename);
                ReportParameter pAddress = new ReportParameter("pAddress", address);

                reportViewer1.LocalReport.SetParameters(pDate);
                reportViewer1.LocalReport.SetParameters(pCashier);
                reportViewer1.LocalReport.SetParameters(pHeader);
                reportViewer1.LocalReport.SetParameters(pStoreName);
                reportViewer1.LocalReport.SetParameters(pAddress);

                rptDS = new ReportDataSource("DataSet_SoldItemReport", ds.Tables["dtSoldItemReport"]);
                reportViewer1.LocalReport.DataSources.Add(rptDS); 
                reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
                reportViewer1.ZoomMode = ZoomMode.Percent;
                reportViewer1.ZoomPercent = 100;

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
} 
