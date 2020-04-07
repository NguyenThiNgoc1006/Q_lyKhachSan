using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace Q_lyKhanhSan
{
    public partial class FrmKhachSan : Form
    {
        SqlConnection con = new SqlConnection();
        public FrmKhachSan()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string connectionString = "Data Source = localhost\\SQLEXPRESS; Initial Catalog = Quan_Ly_Khanh_San; Integrated Security = True";
            con.ConnectionString = connectionString;
            con.Open();

            loadDataToGridview();

        }

        private void loadDataToGridview()
        {
            string sql = "Select * From Phong";
            SqlDataAdapter adp = new SqlDataAdapter(sql, con);
            DataTable tablePhong = new DataTable();
            adp.Fill(tablePhong);

            dataGridView1.DataSource = tablePhong;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaPhong.Text = dataGridView1.CurrentRow.Cells["MaPhong"].Value.ToString();
            txtTenPhong.Text = dataGridView1.CurrentRow.Cells["TenPhong"].Value.ToString();
            txtDonGia.Text = dataGridView1.CurrentRow.Cells["DonGia"].Value.ToString();
            txtMaPhong.Enabled = false;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMaPhong.Enabled = true;
            txtTenPhong.Text = "";
            txtDonGia.Text = "";
            txtMaPhong.Text = "";

        }

 

        private void txtDonGia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= '0') && (e.KeyChar <= '9')) ||
               (Convert.ToInt32(e.KeyChar) == 8) || (Convert.ToInt32(e.KeyChar) == 13))
            {
                e.Handled = false;
            }
            else
                e.Handled = true;
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtMaPhong.Text == "")
            {
                MessageBox.Show("Bạn cần nhập mã phòng");
                txtMaPhong.Focus();
                return;
            }
            if (txtTenPhong.Text == "")
            {
                MessageBox.Show("Bạn cần nhập tên phòng");
                txtTenPhong.Focus();
              
            }
            if (txtDonGia.Text == "")
            {
                MessageBox.Show("Bạn cần nhập đơn giá");
                txtDonGia.Focus();
            }

            string sql = "select MaPhong from Phong where MaPhong= '" + txtMaPhong.Text + "'";
            if (CheckKey(sql))
            {
                MessageBox.Show("Mã phòng này đã có, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaPhong.Focus();
                txtMaPhong.Text = "";
                return;
            }
            else
            {
                sql = "insert into Phong values('" + txtMaPhong.Text + "', '" + txtTenPhong.Text + "'";
                if (txtDonGia.Text != "")
                    sql = sql + ", " + txtDonGia.Text.Trim();
                sql = sql + ") ";
            }
            MessageBox.Show(sql);
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();

            loadDataToGridview();

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            txtMaPhong.ReadOnly = true;
            string sql = "update Phong set TenPhong ='" + txtTenPhong.Text.ToString() + "', "
                + "DonGia = '" + txtDonGia.Text.ToString() + "'where MaPhong = '" + txtMaPhong.Text + "'";
            MessageBox.Show(sql);
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();

            loadDataToGridview();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            con.Close();
            this.Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            txtMaPhong.Text = "";
            txtTenPhong.Text = "";
            txtDonGia.Text = "";
            btnHuy.Enabled = false;
            btnThem.Enabled = true;
            loadDataToGridview();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            
                string sql = "Delete From Phong Where MaPhong = '" + txtMaPhong.Text + "'";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                loadDataToGridview();
            
        }
        private  bool CheckKey(string sql)
        {
            SqlDataAdapter adp = new SqlDataAdapter(sql, con);
            DataTable tablePhong = new DataTable();
            adp.Fill(tablePhong);
            if (tablePhong.Rows.Count > 0)
                return true;
            else return false;
        }

    }
}
