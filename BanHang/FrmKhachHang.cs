using System.Data;
using System.Data.SQLite;

namespace BanHang
{
    public partial class FrmKhachHang : Form
    {
        private string connectionString = "Data Source=database.db;Version=3;";

        public FrmKhachHang()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM KhachHang";
                SQLiteDataAdapter da = new SQLiteDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvKhachHang.DataSource = dt;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string sql = "INSERT INTO KhachHang(MaCode, TenKhachHang, DiaChi, DienThoai, Email, GioiTinh) " +
                             "VALUES(@MaCode, @TenKhachHang, @DiaChi, @DienThoai, @Email, @GioiTinh)";
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaCode", txtMaCode.Text);
                cmd.Parameters.AddWithValue("@TenKhachHang", txtTenKH.Text);
                cmd.Parameters.AddWithValue("@DiaChi", txtDiaChi.Text);
                cmd.Parameters.AddWithValue("@DienThoai", txtDienThoai.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@GioiTinh", cbGioiTinh.SelectedIndex);
                cmd.ExecuteNonQuery();
                LoadData();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvKhachHang.CurrentRow != null)
            {
                int id = Convert.ToInt32(dgvKhachHang.CurrentRow.Cells["Id"].Value);
                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    string sql = "UPDATE KhachHang SET MaCode=@MaCode, TenKhachHang=@TenKhachHang, DiaChi=@DiaChi, DienThoai=@DienThoai, Email=@Email, GioiTinh=@GioiTinh, NgayCapNhat=CURRENT_TIMESTAMP WHERE Id=@Id";
                    SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@MaCode", txtMaCode.Text);
                    cmd.Parameters.AddWithValue("@TenKhachHang", txtTenKH.Text);
                    cmd.Parameters.AddWithValue("@DiaChi", txtDiaChi.Text);
                    cmd.Parameters.AddWithValue("@DienThoai", txtDienThoai.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@GioiTinh", cbGioiTinh.SelectedIndex);
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                    LoadData();
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvKhachHang.CurrentRow != null)
            {
                int id = Convert.ToInt32(dgvKhachHang.CurrentRow.Cells["Id"].Value);
                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    string sql = "DELETE FROM KhachHang WHERE Id=@Id";
                    SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                    LoadData();
                }
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaCode.Clear();
            txtTenKH.Clear();
            txtDiaChi.Clear();
            txtDienThoai.Clear();
            txtEmail.Clear();
            cbGioiTinh.SelectedIndex = -1;
        }

        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtMaCode.Text = dgvKhachHang.CurrentRow.Cells["MaCode"].Value.ToString();
                txtTenKH.Text = dgvKhachHang.CurrentRow.Cells["TenKhachHang"].Value.ToString();
                txtDiaChi.Text = dgvKhachHang.CurrentRow.Cells["DiaChi"].Value.ToString();
                txtDienThoai.Text = dgvKhachHang.CurrentRow.Cells["DienThoai"].Value.ToString();
                txtEmail.Text = dgvKhachHang.CurrentRow.Cells["Email"].Value.ToString();
                cbGioiTinh.SelectedIndex = Convert.ToInt32(dgvKhachHang.CurrentRow.Cells["GioiTinh"].Value);
            }
        }
    }
}
