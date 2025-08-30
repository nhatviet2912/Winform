using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace BanHang
{
    public partial class FrmBanHang : Form
    {
        private DataTable dt;
        private CommonMenuStrip commonMenu;
        private TableLayoutPanel tableLayout;
        private int currentHoaDonId = -1;

        public FrmBanHang()
        {
            InitializeComponent();
            InitializeCommonMenu();
            SetupTableLayout();
        }

        private void SetupTableLayout()
        {
            tableLayout = new TableLayoutPanel();
            tableLayout.Dock = DockStyle.Fill;
            tableLayout.RowCount = 2;
            tableLayout.ColumnCount = 1;

            // Hàng cho menu (chiều cao tự động)
            tableLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            // Hàng cho content (chiếm 100% không gian còn lại)
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            // Thêm menu vào hàng đầu tiên
            tableLayout.Controls.Add(commonMenu, 0, 0);
            commonMenu.Dock = DockStyle.Fill;

            // Tạo panel cho nội dung
            Panel contentPanel = new Panel();
            contentPanel.Dock = DockStyle.Fill;
            tableLayout.Controls.Add(contentPanel, 0, 1);

            // Di chuyển các controls vào contentPanel
            var controlsToMove = this.Controls.OfType<Control>()
                                      .Where(c => c != commonMenu)
                                      .ToList();

            foreach (Control control in controlsToMove)
            {
                this.Controls.Remove(control);
                contentPanel.Controls.Add(control);
            }

            this.Controls.Add(tableLayout);
        }


        private void InitializeCommonMenu()
        {
            commonMenu = new CommonMenuStrip();
            commonMenu.Dock = DockStyle.Top;

            this.Controls.Add(commonMenu);
            this.MainMenuStrip = commonMenu.MainMenu;
        }

        private void FrmBanHang_Load(object sender, EventArgs e)
        {
            dgvChiTiet.Columns.Add("SanPhamId", "Mã SP");
            dgvChiTiet.Columns.Add("TenSP", "Tên sản phẩm");
            dgvChiTiet.Columns.Add("SoLuong", "Số lượng");
            dgvChiTiet.Columns.Add("DonGia", "Đơn giá");
            dgvChiTiet.Columns.Add("ThanhTien", "Thành tiền");

            dgvChiTiet.AllowUserToAddRows = false;
        }

        private void btnThemSanPham_Click(object sender, EventArgs e)
        {
            var frm = new FrmThemSanPham();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                string maSP = frm.MaSP;
                string tenSP = frm.TenSP;
                int soLuong = frm.SoLuong;
                decimal donGia = frm.DonGia;
                decimal thanhTien = soLuong * donGia;

                dgvChiTiet.Rows.Add(maSP, tenSP, soLuong, donGia, thanhTien);

                TinhTongTien();
            }
        }

        private void TinhTongTien()
        {
            decimal tong = 0;
            foreach (DataGridViewRow row in dgvChiTiet.Rows)
            {
                tong += Convert.ToDecimal(row.Cells["ThanhTien"].Value);
            }
            txtTongTien.Text = tong.ToString("N0");
        }

        private void btnLuuHoaDon_Click(object sender, EventArgs e)
        {
            try
            {
                using (var conn = DatabaseHelper.GetConnection())  // Sử dụng DatabaseHelper để lấy kết nối
                {
                    using (SQLiteTransaction trans = conn.BeginTransaction())
                    {
                        try
                        {
                            // Kiểm tra nếu là hóa đơn mới hay cập nhật
                            if (currentHoaDonId == -1)
                            {
                                InsertHoaDon(conn);
                            }

                            // Thêm chi tiết hóa đơn bán
                            foreach (DataGridViewRow row in dgvChiTiet.Rows)
                            {
                                if (row.IsNewRow) continue;
                                InsertChiTietHoaDon(conn, row);
                            }

                            // Commit transaction nếu không có lỗi
                            trans.Commit();
                            MessageBox.Show("Lưu hóa đơn bán thành công!");
                        }
                        catch (Exception ex)
                        {
                            // Rollback nếu có lỗi trong transaction
                            trans.Rollback();
                            MessageBox.Show("Lỗi khi lưu hóa đơn bán: " + ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi kết nối cơ sở dữ liệu
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message);
            }
        }

        private void InsertHoaDon(SQLiteConnection conn)
        {
            string sql = "INSERT INTO HoaDonBan (MaHoaDon, NgayBan, KhachHangId, NhanVienBan, TongTien) " +
                         "VALUES (@MaHoaDon, @NgayBan, @KhachHangId, @NhanVienBan, @TongTien); SELECT last_insert_rowid();";

            using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@MaHoaDon", txtMaHoaDon.Text);
                cmd.Parameters.AddWithValue("@NgayBan", dtpNgayBan.Value);
                cmd.Parameters.AddWithValue("@KhachHangId", Convert.ToInt32(txtKhachHangId.Text));
                cmd.Parameters.AddWithValue("@NhanVienBan", txtNhanVienBan.Text);
                cmd.Parameters.AddWithValue("@TongTien", Convert.ToDecimal(txtTongTien.Text));
                currentHoaDonId = Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        private void InsertChiTietHoaDon(SQLiteConnection conn, DataGridViewRow row)
        {
            string sql = "INSERT INTO ChiTietHoaDonBan (HoaDonBanId, SanPhamId, SoLuong, DonGia) " +
                         "VALUES (@HoaDonBanId, @SanPhamId, @SoLuong, @DonGia)";

            using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@HoaDonBanId", currentHoaDonId);
                cmd.Parameters.AddWithValue("@SanPhamId", Convert.ToInt32(row.Cells["SanPhamId"].Value));
                cmd.Parameters.AddWithValue("@SoLuong", Convert.ToInt32(row.Cells["SoLuong"].Value));
                cmd.Parameters.AddWithValue("@DonGia", Convert.ToDecimal(row.Cells["DonGia"].Value));
                cmd.ExecuteNonQuery();
            }
        }


        private void btnHuyHoaDon_Click(object sender, EventArgs e)
        {
            btnThemMoi_Click(sender, e);
        }

        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            currentHoaDonId = -1;
            txtMaHoaDon.Clear();
            dtpNgayBan.Value = DateTime.Now;
            txtKhachHangId.Clear();
            txtNhanVienBan.Clear();
            txtTongTien.Text = "0";
            dgvChiTiet.Rows.Clear();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
