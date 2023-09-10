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
    public partial class frmDiscount : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
        DbConnection db = new DbConnection();
        string title = "Garments Zone";
        frmPOS fpos;
         
        public frmDiscount(frmPOS frm)
        {
            InitializeComponent();
            con = new SqlConnection(db.MyConnection());
            fpos = frm;
            txtDiscount.Focus();
            txtAmount.Text = "0.00";
            this.KeyPreview = true;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void frmDiscount_Load(object sender, EventArgs e)
        {
            
        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double discount = double.Parse(txtPrice.Text) - double.Parse(txtDiscount.Text);
                txtAmount.Text = discount.ToString("#,##0.00");
            }
            catch(Exception)
            {
                txtAmount.Text = "0.00";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if(MessageBox.Show("Add Discount? Click yes to confirm.", title, MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cmd = new SqlCommand("update tblCart set disc = @disc where id = @id", con);
                    cmd.Parameters.AddWithValue("@disc", Double.Parse(txtDiscount.Text));
                    cmd.Parameters.AddWithValue("@id", int.Parse(lblID.Text));
                    cmd.ExecuteNonQuery();
                    con.Close();
                    txtAmount.Clear();
                    txtPrice.Clear();
                    fpos.LoadCart();
                    this.Dispose();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmDiscount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Dispose();
            }
        }
    }
}
