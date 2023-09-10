using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarmentZone.DBConnection
{
    class DbConnection
    {
        SqlConnection con;
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;


        private double dailysales;
        private int productline;
        private int stockonhand;
        private int criticalitems;

        private string conn;

        public string MyConnection()
        {
            conn = @"Data source=localhost;initial catalog=GarmentZone;integrated security=true;";
            return conn;
        }

        public double DailySales()
        {
            string sdate = DateTime.Now.ToShortDateString();
            con = new SqlConnection();
            con.ConnectionString = conn;
            con.Open();
            cmd = new SqlCommand("select isnull(sum(total),0) as total from tblcart where sdate between '" +sdate+ "' and '" + sdate+ "' and status like 'Sold'", con);
            dailysales = double.Parse(cmd.ExecuteScalar().ToString());
            con.Close();
            return dailysales;
        }

        public double ProductLine()
        {
            con = new SqlConnection();
            con.ConnectionString = conn;
            con.Open();
            cmd = new SqlCommand("select count(*) from tblproduct", con);
            productline = int.Parse(cmd.ExecuteScalar().ToString());
            con.Close();
            return productline;
        }

        public double StockOnHand()
        {
            con = new SqlConnection();
            con.ConnectionString = conn;
            con.Open();
            cmd = new SqlCommand("select isnull(sum(qty),0) as qty from tblproduct", con);
            stockonhand = int.Parse(cmd.ExecuteScalar().ToString());
            con.Close();
            return stockonhand;
        }

        public double CriticalItems()
        {
            con = new SqlConnection();
            con.ConnectionString = conn;
            con.Open();
            cmd = new SqlCommand("select count(*) from vwCriticalItems", con);
            criticalitems = int.Parse(cmd.ExecuteScalar().ToString());
            con.Close();
            return criticalitems;
        }

        public string GetPassword(string username)
        {
            string password = "";
            con.ConnectionString = conn;
            con.Open();
            cmd = new SqlCommand("select * from tbluser where username = @username", con);
            cmd.Parameters.AddWithValue("@username", username);
            dr = cmd.ExecuteReader();
            dr.Read();
            if(dr.HasRows)
            {
                password = dr["password"].ToString();
            }
            dr.Close();
            con.Close();
            return password;
        }
    }
}
