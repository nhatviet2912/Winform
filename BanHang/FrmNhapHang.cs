using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace BanHang
{
    public partial class FrmNhapHang : Form
    {
        private DataTable dt;
        private CommonMenuStrip commonMenu;
        private TableLayoutPanel tableLayout;
        private int currentPhieuNhapId = -1;

        public FrmNhapHang()
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

        private void FrmNhapHang_Load(object sender, EventArgs e)
        {
            dgvChiTiet.Columns.Add("SanPhamId", "Mã Sản Phẩm");
            dgvChiTiet.Columns.Add("TenSP", "Tên sản phẩm");
            dgvChiTiet.Columns.Add("SoLuong", "Số lượng");
            dgvChiTiet.Columns.Add("DonGiaNhap", "Đơn giá");
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

        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            currentPhieuNhapId = -1;
            txtMaPhieu.Clear();
            txtNhaCungCap.Clear();
            txtNhanVienNhap.Clear();
            txtGhiChu.Clear();
            txtTongTien.Text = "0";
            dgvChiTiet.Rows.Clear();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    SQLiteTransaction trans = conn.BeginTransaction();
                    try
                    {
                        // Kiểm tra nếu là phiếu nhập mới hay cập nhật
                        if (currentPhieuNhapId == -1)
                        {
                            InsertPhieuNhap(conn);
                        }
                        else
                        {
                            UpdatePhieuNhap(conn);
                            DeleteChiTietPhieuNhap(conn);
                        }

                        // Thêm chi tiết phiếu nhập
                        foreach (DataGridViewRow row in dgvChiTiet.Rows)
                        {
                            if (row.IsNewRow) continue;
                            InsertChiTietPhieuNhap(conn, row);
                        }

                        // Commit transaction nếu tất cả các thao tác thành công
                        trans.Commit();
                        MessageBox.Show("Lưu phiếu nhập thành công!");
                    }
                    catch (Exception ex)
                    {
                        // Rollback nếu có lỗi
                        trans.Rollback();
                        MessageBox.Show("Lỗi khi lưu: " + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi ở mức độ cao hơn
                MessageBox.Show("Lỗi không xác định: " + ex.Message);
            }
        }

        private void InsertPhieuNhap(SQLiteConnection conn)
        {
            string sql = "INSERT INTO PhieuNhapKho (MaPhieu, NgayNhap, NhaCungCap, NhanVienNhap, TongTien, GhiChu) " +
                         "VALUES (@MaPhieu, @NgayNhap, @NhaCungCap, @NhanVienNhap, @TongTien, @GhiChu); SELECT last_insert_rowid();";

            using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@MaPhieu", txtMaPhieu.Text);
                cmd.Parameters.AddWithValue("@NgayNhap", dtpNgayNhap.Value);
                cmd.Parameters.AddWithValue("@NhaCungCap", txtNhaCungCap.Text);
                cmd.Parameters.AddWithValue("@NhanVienNhap", txtNhanVienNhap.Text);
                cmd.Parameters.AddWithValue("@TongTien", Convert.ToDecimal(txtTongTien.Text));
                cmd.Parameters.AddWithValue("@GhiChu", txtGhiChu.Text);

                currentPhieuNhapId = Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        private void UpdatePhieuNhap(SQLiteConnection conn)
        {
            string sql = "UPDATE PhieuNhapKho SET MaPhieu=@MaPhieu, NgayNhap=@NgayNhap, NhaCungCap=@NhaCungCap, " +
                         "NhanVienNhap=@NhanVienNhap, TongTien=@TongTien, GhiChu=@GhiChu, NgayCapNhat=CURRENT_TIMESTAMP " +
                         "WHERE Id=@Id";

            using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@MaPhieu", txtMaPhieu.Text);
                cmd.Parameters.AddWithValue("@NgayNhap", dtpNgayNhap.Value);
                cmd.Parameters.AddWithValue("@NhaCungCap", txtNhaCungCap.Text);
                cmd.Parameters.AddWithValue("@NhanVienNhap", txtNhanVienNhap.Text);
                cmd.Parameters.AddWithValue("@TongTien", Convert.ToDecimal(txtTongTien.Text));
                cmd.Parameters.AddWithValue("@GhiChu", txtGhiChu.Text);
                cmd.Parameters.AddWithValue("@Id", currentPhieuNhapId);
                cmd.ExecuteNonQuery();
            }
        }

        private void DeleteChiTietPhieuNhap(SQLiteConnection conn)
        {
            string sql = "DELETE FROM ChiTietPhieuNhap WHERE PhieuNhapKhoId=@Id";
            using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Id", currentPhieuNhapId);
                cmd.ExecuteNonQuery();
            }
        }

        private void InsertChiTietPhieuNhap(SQLiteConnection conn, DataGridViewRow row)
        {
            string sql = "INSERT INTO ChiTietPhieuNhap (PhieuNhapKhoId, SanPhamId, SoLuong, DonGiaNhap) " +
                         "VALUES (@PhieuNhapKhoId, @SanPhamId, @SoLuong, @DonGiaNhap)";

            using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@PhieuNhapKhoId", currentPhieuNhapId);
                cmd.Parameters.AddWithValue("@SanPhamId", Convert.ToInt32(row.Cells["SanPhamId"].Value));
                cmd.Parameters.AddWithValue("@SoLuong", Convert.ToInt32(row.Cells["SoLuong"].Value));
                cmd.Parameters.AddWithValue("@DonGiaNhap", Convert.ToDecimal(row.Cells["DonGiaNhap"].Value));
                cmd.ExecuteNonQuery();
            }
        }


        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (currentPhieuNhapId == -1)
            {
                MessageBox.Show("Chưa có phiếu nhập để xóa!");
                return;
            }

            var result = MessageBox.Show("Bạn có chắc chắn muốn xóa phiếu nhập này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (var conn = DatabaseHelper.GetConnection())  // Sử dụng DatabaseHelper để lấy kết nối
                    {
                        using (SQLiteTransaction trans = conn.BeginTransaction())
                        {
                            try
                            {
                                // Xóa chi tiết phiếu nhập
                                DeleteChiTietPhieuNhap(conn);

                                // Xóa phiếu nhập
                                DeletePhieuNhap(conn);

                                // Commit transaction nếu không có lỗi
                                trans.Commit();
                                MessageBox.Show("Đã xóa phiếu nhập thành công!");

                                // Thực hiện hành động thêm mới sau khi xóa
                                btnThemMoi_Click(sender, e);
                            }
                            catch (Exception ex)
                            {
                                // Rollback nếu có lỗi trong transaction
                                trans.Rollback();
                                MessageBox.Show("Lỗi khi xóa: " + ex.Message);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message);
                }
            }
        }

        private void DeletePhieuNhap(SQLiteConnection conn)
        {
            string sql = "DELETE FROM PhieuNhapKho WHERE Id=@Id";
            using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Id", currentPhieuNhapId);
                cmd.ExecuteNonQuery();
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
