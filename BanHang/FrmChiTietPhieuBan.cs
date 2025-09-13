using System.Data;
using System.Data.SQLite;

namespace BanHang
{
    public partial class FrmChiTietPhieuBan : Form
    {
        private int _phieuNhapId;

        public FrmChiTietPhieuBan(int phieuNhapId)
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
                @"SELECT h.Id, h.MaHoaDon, h.NgayBan,
                               k.TenKhachHang,
                               h.NhanVienBan, h.TongTien
                        FROM HoaDonBan h
                        LEFT JOIN KhachHang k ON h.KhachHangId = k.Id
                        WHERE h.Id=@Id
                        ORDER BY h.NgayBan DESC", conn))
            {
                cmd.Parameters.AddWithValue("@Id", _phieuNhapId);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        txtMaPhieu.Text = reader["MaHoaDon"].ToString();
                        dtpNgayNhap.Value = Convert.ToDateTime(reader["NgayBan"]);
                        txtNhaCungCap.Text = reader["TenKhachHang"].ToString();
                        txtNhanVienNhap.Text = reader["NhanVienBan"].ToString();
                        txtTongTien.Text = Convert.ToDecimal(reader["TongTien"]).ToString("N0") + " VNĐ";
                    }
                }
            }
        }

        private void LoadChiTietPhieuNhap()
        {
            using (var conn = DatabaseHelper.GetConnection())
            using (var adapter = new SQLiteDataAdapter(
                "SELECT c.Id, s.MaSanPham, s.TenSanPham, c.SoLuong, c.DonGia, c.ThanhTien " +
                "FROM ChiTietHoaDonBan c " +
                "JOIN SanPham s ON c.SanPhamId = s.Id " +
                "WHERE c.HoaDonBanId=@Id", conn))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@Id", _phieuNhapId);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dgvChiTiet.DataSource = dt;

                if (dgvChiTiet.Columns["Id"] != null)
                    dgvChiTiet.Columns["Id"].Visible = false;

                if (dgvChiTiet.Columns.Contains("DonGia"))
                {
                    dgvChiTiet.Columns["DonGia"].DefaultCellStyle.Format = "N0"; // 1,000,000
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
                    { "DonGia", "Đơn Giá" },
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
                FrmDanhSachPhieuBan frmDanhSachPhieuBan = new FrmDanhSachPhieuBan();
                frmDanhSachPhieuBan.ShowDialog();

                // Đảm bảo form hiện tại sẽ bị đóng sau khi form mới đóng
                if (!currentForm.IsDisposed)
                {
                    currentForm.Close();
                }
            }
        }
    }
}
