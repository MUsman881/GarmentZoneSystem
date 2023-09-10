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
using System.Windows.Forms.DataVisualization.Charting;

namespace GarmentZone.Screens
{
    public partial class frmChart : Form
    {
        SqlConnection con = new SqlConnection();
        DbConnection db = new DbConnection();

        public frmChart()
        {
            InitializeComponent();
            con = new SqlConnection(db.MyConnection());
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void LoadChartSold(string sql)
        {
            SqlDataAdapter da;
            con.Open();

            da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds, "SOLD");
            chart1.DataSource = ds.Tables["SOLD"];
            Series series = chart1.Series[0];
            series.ChartType = SeriesChartType.Doughnut;

            series.Name = "SOLD ITEMS";
            chart1.Series[0].XValueMember = "pname";
            //chart1.Series[0]["PieLabelStyle"] = "Outside";
            chart1.Series[0].YValueMembers = "total";
            chart1.Series[0].LabelFormat = "{#,##0,00}";
            chart1.Series[0].IsValueShownAsLabel = true;

            con.Close();
        }
    }
}
