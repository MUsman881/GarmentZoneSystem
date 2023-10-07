using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.Data.SqlClient;
using GarmentZone.DBConnection;

namespace GarmentZone.Screens
{
    public partial class frmReciept : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        DbConnection db = new DbConnection();
        SqlDataReader dr;
        string store = "Garments Zone";
        string address = "SHOP#2, Kohenoor City";
        frmPOS frm;

        public frmReciept(frmPOS f)
        {
            InitializeComponent();
            con = new SqlConnection(db.MyConnection());
            frm = f;
            this.KeyPreview = true;
        }

        private void frmReciept_Load(object sender, EventArgs e)
        {
            this.reportViewer1.RefreshReport();
        }

        public void loadReport(string pcash, string pchange)
        {
            ReportDataSource rptDatasource;
            try
            {
                this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"\Reports\rptReciept.rdlc";
                this.reportViewer1.LocalReport.DataSources.Clear();

                DataSet1 ds = new DataSet1();
                SqlDataAdapter da = new SqlDataAdapter();

                con.Open();
                da.SelectCommand = new SqlCommand("select c.id, c.transno, c.pcode, c.price, c.qty, c.disc, c.total, c.sdate, c.status, p.pname from tblCart as c inner join tblProduct as p on p.pcode = c.pcode where transno like '" + frm.lblTransno.Text + "'", con);
                da.Fill(ds.Tables["dtReciept"]);
                con.Close();

                ReportParameter pSubtotal = new ReportParameter("pSubtotal", frm.lblSubTotal.Text);
                ReportParameter pDiscount = new ReportParameter("pDiscount", frm.lblDisc.Text);
                ReportParameter pTotal = new ReportParameter("pTotal", frm.lblsaleTotal.Text);
                ReportParameter pCash = new ReportParameter("pCash", pcash);
                ReportParameter pChange = new ReportParameter("pChange", pchange);
                ReportParameter pStore = new ReportParameter("pStore", store);
                ReportParameter pAddress = new ReportParameter("pAddress", address);
                ReportParameter pTransaction = new ReportParameter("pTransaction", "Invoice #:" + frm.lblTransno.Text);
                ReportParameter pCashier = new ReportParameter("pCashier", frm.lblUser.Text);
                ReportParameter pCustomer = new ReportParameter("pCustomer", frm.txtCustomerName.Text);

                reportViewer1.LocalReport.SetParameters(pSubtotal);
                reportViewer1.LocalReport.SetParameters(pDiscount);
                reportViewer1.LocalReport.SetParameters(pTotal);
                reportViewer1.LocalReport.SetParameters(pCash);
                reportViewer1.LocalReport.SetParameters(pChange);
                reportViewer1.LocalReport.SetParameters(pStore);
                reportViewer1.LocalReport.SetParameters(pAddress);
                reportViewer1.LocalReport.SetParameters(pTransaction);
                reportViewer1.LocalReport.SetParameters(pCashier);
                reportViewer1.LocalReport.SetParameters(pCustomer);

                rptDatasource = new ReportDataSource("DataSet_Reciept", ds.Tables["dtReciept"]);
                reportViewer1.LocalReport.DataSources.Add(rptDatasource);
                reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
                reportViewer1.ZoomMode = ZoomMode.Percent;
                reportViewer1.ZoomPercent = 50;
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void frmReciept_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Dispose();
            }
        }
    }
}
