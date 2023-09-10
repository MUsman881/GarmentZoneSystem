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

namespace GarmentZone.Screens
{
    public partial class frmPOS : Form
    {
        string id;
        string price;
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        DbConnection db = new DbConnection();
        SqlDataReader dr;
        string stitle = "Garments Zone";
        int qty;
        Login login;

        public frmPOS(Login l)
        {
            InitializeComponent();
            con = new SqlConnection(db.MyConnection());
            lblDate.Text = DateTime.Now.ToLongDateString();
            this.KeyPreview = true;
            txtCustomerName.Focus();
            login = l;
            NotifyCriticalItems();
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

        public void GetTransNo()
        {
            try
            {
                string sdate = DateTime.Now.ToString("yyyyMMdd");
                string transno;
                int count = 0;
                con.Open();
                cmd = new SqlCommand("select top 1 transno from tblCart where transno like '" + sdate + "%' order by id desc", con);
                dr = cmd.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    transno = dr[0].ToString();
                    count = int.Parse(transno.Substring(8, 4));
                    lblTransno.Text = sdate + (count + 1);
                }
                else
                {
                    transno = sdate + "1001";
                    lblTransno.Text = transno;
                }
                dr.Close();
                con.Close();
            }
            catch(Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, stitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void LoadCart()
        {
            try
            {
                Boolean hasRecord = false;

                dataGridView1.Rows.Clear();
                int i = 0;
                double total = 0;
                double disc = 0;

                con.Open();
                cmd = new SqlCommand("select c.id, c.pcode, p.pname,c.price,c.qty, c.disc,c.total from tblCart as c inner join tblProduct as p on c.pcode = p.pcode where transno like '" + lblTransno.Text + "' and status like 'Pending'", con);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    total += double.Parse(dr["price"].ToString());
                    disc += double.Parse(dr["disc"].ToString());
                    dataGridView1.Rows.Add(i, dr["id"].ToString(), dr["pcode"].ToString() ,dr["pname"].ToString(), dr["price"].ToString(), dr["qty"].ToString(), dr["disc"].ToString(), double.Parse(dr["total"].ToString()).ToString("#,##0.00"));
                    hasRecord = true;
                }
                dr.Close();
                con.Close();

                lblSubTotal.Text = total.ToString("#,##0.00");
                lblDisc.Text = disc.ToString("#,##0.00");
                GetCartTotal();
                if (hasRecord = true)
                {
                    btnSettle.Enabled = true;
                    btnDiscount.Enabled = true;
                    btnCancel.Enabled = true;
                }
                else
                {
                    btnSettle.Enabled = false;
                    btnDiscount.Enabled = false;
                    btnCancel.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, stitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void GetCartTotal()
        {
            double subTotal = double.Parse(lblSubTotal.Text);
            double disc = double.Parse(lblDisc.Text);
            double saleTotal = subTotal - disc;
            lblsaleTotal.Text = saleTotal.ToString("#,##0.00");
            lblTotalAmount.Text = saleTotal.ToString("#,##0.00");
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            if(dataGridView1.Rows.Count > 0)
            {
                return;
            }
            GetTransNo();
            txtSearch.Enabled = true;
            txtSearch.Focus();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtSearch.Text == String.Empty)
                {
                    return;
                }
                else
                {
                    string _pcode;
                    double _price;
                    int _qty;
                    con.Open();
                    cmd = new SqlCommand("select * from tblProduct where barcode like '" + txtSearch.Text + "'", con);
                    dr = cmd.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        qty = int.Parse(dr["qty"].ToString());
                        _pcode = dr["pcode"].ToString();
                        _price = double.Parse(dr["price"].ToString());
                        _qty = int.Parse(txtQty.Text);

                        dr.Close();
                        con.Close();

                        AddToCart(_pcode, _price, _qty);

                    }
                    dr.Close();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, stitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void AddToCart(string _pcode, double _price, int _qty)
        {
            string id = "";
            bool found = false;
            int cart_qty = 0;

            con.Open();
            cmd = new SqlCommand("Select * from tblcart where transno = @transno and pcode = @pcode", con);
            cmd.Parameters.AddWithValue("@transno", lblTransno.Text);
            cmd.Parameters.AddWithValue("@pcode", _pcode);
            dr = cmd.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                found = true;
                id = dr["id"].ToString();
                cart_qty = int.Parse(dr["qty"].ToString());
            }
            else
            {
                found = false;
            }
            dr.Close();
            con.Close();
            if (found == true)
            {
                if (qty < (int.Parse(txtQty.Text) + cart_qty))
                {
                    MessageBox.Show("UNABLE TO PROCEED. REMAINING QTY ON HAND IS " + qty, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                con.Open();
                cmd = new SqlCommand("update tblCart set qty = (qty + " + _qty + ") where id = '" + id + "'", con);
                cmd.ExecuteNonQuery();
                con.Close();

                txtSearch.SelectionStart=0;
                txtSearch.SelectionLength = txtSearch.Text.Length;
                LoadCart();
            }
            else
            {
                if (qty < int.Parse(txtQty.Text))
                {
                    MessageBox.Show("Unable to proceed. Remaining qty on hand is " + qty, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                con.Open();
                cmd = new SqlCommand("insert into tblCart (transno, pcode, price, qty, sdate, cashier) values (@transno, @pcode, @price, @qty, @sdate, @cashier)", con);
                cmd.Parameters.AddWithValue("@transno", lblTransno.Text);
                cmd.Parameters.AddWithValue("@pcode", _pcode);
                cmd.Parameters.AddWithValue("@price", _price);
                cmd.Parameters.AddWithValue("@qty", _qty);
                cmd.Parameters.AddWithValue("@sdate", DateTime.Now);
                cmd.Parameters.AddWithValue("@cashier", lblUser.Text);
                cmd.ExecuteNonQuery();
                con.Close();

                txtSearch.SelectionStart = 0;
                txtSearch.SelectionLength = txtSearch.Text.Length;
                LoadCart();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dataGridView1.Columns[e.ColumnIndex].Name;
            if (colName == "Delete")
            {
                if(MessageBox.Show("Remove this item?", stitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cmd = new SqlCommand("delete from tblCart where id like '" + dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Item has successfully removed.", stitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadCart();
                }
            }
            else if(colName == "colPlus")
            {
                int i = 0;
                con.Open();
                cmd = new SqlCommand("select sum(qty) as qty from tblproduct where pcode like '" + dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString() + "' group by pcode", con);
                i = int.Parse(cmd.ExecuteScalar().ToString());
                con.Close();

                if (int.Parse(dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString()) < i)
                {
                    con.Open();
                    cmd = new SqlCommand("update tblcart set qty = qty + " + int.Parse(txtQty.Text) + " where transno like '" + lblTransno.Text + "' and pcode like '" + dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString() + "'",con);
                    cmd.ExecuteNonQuery();
                    con.Close();

                    LoadCart();
                }
                else
                {
                    MessageBox.Show("Unable to add qty, remaining qty on hand is " + i + "!", "Out of Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            else if (colName == "colSub")
            {
                int i = 0;
                con.Open();
                cmd = new SqlCommand("select sum(qty) as qty from tblcart where pcode like '" + dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString() + "' and transno like '" + lblTransno.Text + "' group by transno, pcode", con);
                i = int.Parse(cmd.ExecuteScalar().ToString());
                con.Close();

                if (i > 1)
                {
                    con.Open();
                    cmd = new SqlCommand("update tblcart set qty = qty - " + int.Parse(txtQty.Text) + " where transno like '" + lblTransno.Text + "' and pcode like '" + dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString() + "'", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    LoadCart();
                }
                else
                {
                    MessageBox.Show("Unable to add qty, remaining qty on cart is " + i + "!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if(lblTransno.Text == "00000000000000") { return; }
            frmLookUp frm = new frmLookUp(this);
            frm.LoadProducts();
            frm.ShowDialog();
        }

        private void btnDiscount_Click(object sender, EventArgs e)
        {
            frmDiscount frm = new frmDiscount(this);
            frm.txtDiscount.Focus();
            frm.lblID.Text = id;
            frm.txtPrice.Text = price;
            frm.ShowDialog();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            int i = dataGridView1.CurrentRow.Index;
            id = dataGridView1[1, i].Value.ToString();
            price = dataGridView1[7, i].Value.ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("hh:mm:ss tt");
            lblDate1.Text = DateTime.Now.ToLongDateString();
        }

        private void btnSettle_Click(object sender, EventArgs e)
        {
            frmSettle frm = new frmSettle(this);
            frm.txtCash.Focus();
            frm.txtSale.Text = lblTotalAmount.Text;
            frm.ShowDialog();
        }

        private void btnSale_Click(object sender, EventArgs e)
        {
            frmSoldItems frm = new frmSoldItems();
            frm.dt1.Enabled = false;
            frm.dt2.Enabled = false;
            frm.user = lblUser.Text;
            frm.cboCashier.Enabled = false;
            frm.cboCashier.Text = lblUser.Text;
            frm.ShowDialog();

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            if(dataGridView1.Rows.Count > 0)
            {
                MessageBox.Show("UNABLE TO LOGOUT, PLEASE CANCEL THE TRANSACTION.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if(MessageBox.Show("LOGOUT APPLICATION?","Logout",MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Hide();
                Login frm = new Login();
                frm.ShowDialog();
            }
        }

        private void frmPOS_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.F1)
            {
                btnNew_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F2)
            {
                btnSearch_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F3)
            {
                btnDiscount_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F4)
            {
                btnSettle_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F5)
            {
                btnCancel_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F6)
            {
                btnSale_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F7)
            {
                btnChangePass_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F8)
            {
                txtSearch.SelectionStart = 0;
                txtSearch.SelectionLength = txtSearch.Text.Length;
            }
            else if (e.KeyCode == Keys.F10)
            {
                button7_Click(sender, e);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Remove all items from cart?","Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                con.Open();
                cmd = new SqlCommand("delete from tblcart where transno like '" + lblTransno.Text + "'", con);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("All items has been successfully removed!", "Remove Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadCart();
            }
        }

        private void btnChangePass_Click(object sender, EventArgs e)
        {
            frmChangePassword frm = new frmChangePassword(this);
            frm.ShowDialog();
        }
    }
}
