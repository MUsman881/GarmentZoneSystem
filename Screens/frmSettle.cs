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
    public partial class frmSettle : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        DbConnection db = new DbConnection();
        SqlDataReader dr;
        string stitle = "Garments Zone";
        frmPOS frmpos;

        public frmSettle(frmPOS f)
        {
            InitializeComponent();
            con = new SqlConnection(db.MyConnection());
            txtCash.Focus();
            frmpos = f;
            this.KeyPreview = true;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void txtCash_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double sale = double.Parse(txtSale.Text);
                double cash = double.Parse(txtCash.Text);
                double change = cash - sale;

                txtChange.Text = change.ToString("#,##0.00");

            }
            catch(Exception)
            {
                txtChange.Text = "0.00";
            }
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn7.Text;
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn8.Text;
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn9.Text;
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn4.Text;
        }

        private void btnc_Click(object sender, EventArgs e)
        {
            txtCash.Clear();
            txtCash.Focus();
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn5.Text;
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn6.Text;
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn0.Text;
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn1.Text;
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn2.Text;
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn3.Text;
        }

        private void btn00_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn00.Text;
        }

        private void frmSettle_Load(object sender, EventArgs e)
        {

        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            try
            {
                if ((double.Parse(txtChange.Text) < 0) || (txtCash.Text == string.Empty))
                {
                    MessageBox.Show("Insufficient amount. Please enter the correct amount!", stitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCash.Focus();
                    return;
                }
                else
                {
                    if(frmpos.txtCustomerName.Text == string.Empty)
                    {
                        MessageBox.Show("ENTER CUSTOMER NAME!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    for (int i = 0; i < frmpos.dataGridView1.Rows.Count; i++)
                    {
                        con.Open();
                        cmd = new SqlCommand("update tblProduct set qty = qty - " + int.Parse(frmpos.dataGridView1.Rows[i].Cells[5].Value.ToString()) + "where pcode = '" + frmpos.dataGridView1.Rows[i].Cells[2].Value.ToString() + "'", con);
                        cmd.ExecuteNonQuery();
                        con.Close();

                        con.Open();
                        cmd = new SqlCommand("update tblcart set status ='Sold', customername = @customername where id = '" + frmpos.dataGridView1.Rows[i].Cells[1].Value.ToString() + "'", con);
                        cmd.Parameters.AddWithValue("@customername", frmpos.txtCustomerName.Text);
                        cmd.ExecuteNonQuery();
                        con.Close();

                    }

                    con.Open();
                    cmd = new SqlCommand("INSERT INTO tblCustomer (name, mobile) values (@name, @mobile)", con);
                    cmd.Parameters.AddWithValue("@name", frmpos.txtCustomerName.Text);
                    cmd.Parameters.AddWithValue("@mobile", frmpos.txtCustomerMobile.Text);
                    cmd.ExecuteNonQuery();
                    con.Close();

                    frmReciept frm = new frmReciept(frmpos);
                    frm.loadReport(txtCash.Text, txtChange.Text);
                    frm.ShowDialog();


                    MessageBox.Show("Payment successfully saved!", "Payment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmpos.GetTransNo();
                    frmpos.LoadCart();
                    frmpos.txtCustomerName.Clear();
                    frmpos.txtCustomerMobile.Clear();
                    frmpos.btnNew.Enabled = true;
                    frmpos.btnDiscount.Enabled = false;
                    frmpos.btnSettle.Enabled = false;
                    frmpos.btnCancel.Enabled = false;
                    this.Dispose();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Insufficient amount. Please enter the correct amount!", stitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void frmSettle_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Dispose();
            }
            else if(e.KeyCode == Keys.Enter)
            {
                btnEnter_Click(sender, e);
            }
        }
    }
}
