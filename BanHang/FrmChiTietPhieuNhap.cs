using System.Data;
using System.Data.SQLite;

namespace BanHang
{
    public partial class FrmChiTietPhieuNhap : Form
    {
        private int _phieuNhapId;

        public FrmChiTietPhieuNhap(int phieuNhapId)
        {
            InitializeComponent();
            _phieuNhapId = phieuNhapId;
            this.Load += FrmChiTietPhieuNhap_Load;
        }

        private void FrmChiTietPhieuNhap_Load(object sender, EventArgs e)
        {
            LoadThongTinPhieuNhap();
            LoadChiTietPhieuNhap();
        }

        private void LoadThongTinPhieuNhap()
        {
            using (var conn = DatabaseHelper.GetConnection())
            using (var cmd = new SQLiteCommand(
                "SELECT MaPhieu, NgayNhap, NhaCungCap, NhanVienNhap, TongTien, GhiChu " +
                "FROM PhieuNhapKho WHERE Id=@Id", conn))
            {
                cmd.Parameters.AddWithValue("@Id", _phieuNhapId);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        txtMaPhieu.Text = reader["MaPhieu"].ToString();
                        dtpNgayNhap.Value = Convert.ToDateTime(reader["NgayNhap"]);
                        txtNhaCungCap.Text = reader["NhaCungCap"].ToString();
                        txtNhanVienNhap.Text = reader["NhanVienNhap"].ToString();
                        txtTongTien.Text = Convert.ToDecimal(reader["TongTien"]).ToString("N0") + " VNĐ";
                        txtGhiChu.Text = reader["GhiChu"].ToString();
                    }
                }
            }
        }

        private void LoadChiTietPhieuNhap()
        {
            using (var conn = DatabaseHelper.GetConnection())
            using (var adapter = new SQLiteDataAdapter(
                "SELECT c.Id, s.MaSanPham, s.TenSanPham, c.SoLuong, c.DonGiaNhap, c.ThanhTien " +
                "FROM ChiTietPhieuNhap c " +
                "JOIN SanPham s ON c.SanPhamId = s.Id " +
                "WHERE c.PhieuNhapKhoId=@Id", conn))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@Id", _phieuNhapId);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dgvChiTiet.DataSource = dt;

                if (dgvChiTiet.Columns["Id"] != null)
                    dgvChiTiet.Columns["Id"].Visible = false;

                if (dgvChiTiet.Columns.Contains("DonGiaNhap"))
                {
                    dgvChiTiet.Columns["DonGiaNhap"].DefaultCellStyle.Format = "N0"; // 1,000,000
                }

                if (dgvChiTiet.Columns.Contains("ThanhTien"))
                {
                    dgvChiTiet.Columns["ThanhTien"].DefaultCellStyle.Format = "N0"; // 1,000,000
                }

                var columnHeaders = new Dictionary<string, string>
                {
                    { "MaSanPham", "Mã Sản Phẩm" },
                    { "TenSanPham", "Tên Sản Phẩm" },
                    { "SoLuong", "Số Lượng" },
                    { "DonGiaNhap", "Đơn Giá Nhập" },
                    { "ThanhTien", "Thành Tiền" }
                };

                SetColumnHeaders(dgvChiTiet, columnHeaders);
            }
        }

        private void SetColumnHeaders(DataGridView dgv, Dictionary<string, string> headers)
        {
            foreach (var kvp in headers)
            {
                if (dgv.Columns.Contains(kvp.Key))
                    dgv.Columns[kvp.Key].HeaderText = kvp.Value;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form currentForm = this.FindForm();
            if (currentForm != null)
            {
                // Ẩn form hiện tại
                currentForm.Hide();

                // Tạo form mới và mở dưới dạng hộp thoại modal
                FrmDanhSachPhieuNhap frmDanhSachPhieuNhap = new FrmDanhSachPhieuNhap();
                frmDanhSachPhieuNhap.ShowDialog();

                // Đảm bảo form hiện tại sẽ bị đóng sau khi form mới đóng
                if (!currentForm.IsDisposed)
                {
                    currentForm.Close();
                }
            }
        }
    }
}
