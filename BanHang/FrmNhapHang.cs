using System.Data.SQLite;

namespace BanHang
{
    public partial class FrmNhapHang : Form
    {
        private string connectionString = "Data Source=database.db;Version=3;";
        private int currentPhieuNhapId = -1; // -1: phiếu mới

        public FrmNhapHang()
        {
            InitializeComponent();
        }

        private void FrmNhapHang_Load(object sender, EventArgs e)
        {
            // Tạo cột cho DataGridView
            dgvChiTiet.Columns.Add("SanPhamId", "Mã SP");
            dgvChiTiet.Columns.Add("TenSP", "Tên sản phẩm");
            dgvChiTiet.Columns.Add("SoLuong", "Số lượng");
            dgvChiTiet.Columns.Add("DonGiaNhap", "Đơn giá");
            dgvChiTiet.Columns.Add("ThanhTien", "Thành tiền");

            dgvChiTiet.AllowUserToAddRows = false;
        }

        private void btnThemSanPham_Click(object sender, EventArgs e)
        {
            //var frm = new FrmThemSanPham();
            //if (frm.ShowDialog() == DialogResult.OK)
            //{
            //    string maSP = frm.MaSP;
            //    string tenSP = frm.TenSP;
            //    int soLuong = frm.SoLuong;
            //    decimal donGia = frm.DonGia;
            //    decimal thanhTien = soLuong * donGia;

            //    dgvChiTiet.Rows.Add(maSP, tenSP, soLuong, donGia, thanhTien);
            //    TinhTongTien();
            //}
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

        // 🟢 Thêm mới phiếu
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

        // 🟢 Lưu phiếu nhập
        private void btnLuu_Click(object sender, EventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                SQLiteTransaction trans = conn.BeginTransaction();

                try
                {
                    if (currentPhieuNhapId == -1) // insert mới
                    {
                        string sql = "INSERT INTO PhieuNhapKho (MaPhieu, NgayNhap, NhaCungCap, NhanVienNhap, TongTien, GhiChu) " +
                                     "VALUES (@MaPhieu, @NgayNhap, @NhaCungCap, @NhanVienNhap, @TongTien, @GhiChu); SELECT last_insert_rowid();";
                        SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@MaPhieu", txtMaPhieu.Text);
                        cmd.Parameters.AddWithValue("@NgayNhap", dtpNgayNhap.Value);
                        cmd.Parameters.AddWithValue("@NhaCungCap", txtNhaCungCap.Text);
                        cmd.Parameters.AddWithValue("@NhanVienNhap", txtNhanVienNhap.Text);
                        cmd.Parameters.AddWithValue("@TongTien", Convert.ToDecimal(txtTongTien.Text));
                        cmd.Parameters.AddWithValue("@GhiChu", txtGhiChu.Text);
                        currentPhieuNhapId = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                    else // update
                    {
                        string sql = "UPDATE PhieuNhapKho SET MaPhieu=@MaPhieu, NgayNhap=@NgayNhap, NhaCungCap=@NhaCungCap, " +
                                     "NhanVienNhap=@NhanVienNhap, TongTien=@TongTien, GhiChu=@GhiChu, NgayCapNhat=CURRENT_TIMESTAMP " +
                                     "WHERE Id=@Id";
                        SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@MaPhieu", txtMaPhieu.Text);
                        cmd.Parameters.AddWithValue("@NgayNhap", dtpNgayNhap.Value);
                        cmd.Parameters.AddWithValue("@NhaCungCap", txtNhaCungCap.Text);
                        cmd.Parameters.AddWithValue("@NhanVienNhap", txtNhanVienNhap.Text);
                        cmd.Parameters.AddWithValue("@TongTien", Convert.ToDecimal(txtTongTien.Text));
                        cmd.Parameters.AddWithValue("@GhiChu", txtGhiChu.Text);
                        cmd.Parameters.AddWithValue("@Id", currentPhieuNhapId);
                        cmd.ExecuteNonQuery();

                        // xóa chi tiết cũ
                        new SQLiteCommand("DELETE FROM ChiTietPhieuNhap WHERE PhieuNhapKhoId=@Id", conn)
                        { Parameters = { new SQLiteParameter("@Id", currentPhieuNhapId) } }.ExecuteNonQuery();
                    }

                    // insert chi tiết
                    foreach (DataGridViewRow row in dgvChiTiet.Rows)
                    {
                        if (row.IsNewRow) continue;
                        string sqlCT = "INSERT INTO ChiTietPhieuNhap (PhieuNhapKhoId, SanPhamId, SoLuong, DonGiaNhap) " +
                                       "VALUES (@PhieuNhapKhoId, @SanPhamId, @SoLuong, @DonGiaNhap)";
                        SQLiteCommand cmdCT = new SQLiteCommand(sqlCT, conn);
                        cmdCT.Parameters.AddWithValue("@PhieuNhapKhoId", currentPhieuNhapId);
                        cmdCT.Parameters.AddWithValue("@SanPhamId", Convert.ToInt32(row.Cells["SanPhamId"].Value));
                        cmdCT.Parameters.AddWithValue("@SoLuong", Convert.ToInt32(row.Cells["SoLuong"].Value));
                        cmdCT.Parameters.AddWithValue("@DonGiaNhap", Convert.ToDecimal(row.Cells["DonGiaNhap"].Value));
                        cmdCT.ExecuteNonQuery();
                    }

                    trans.Commit();
                    MessageBox.Show("Lưu phiếu nhập thành công!");
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        // 🟢 Xóa phiếu nhập
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (currentPhieuNhapId != -1)
            {
                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    new SQLiteCommand("DELETE FROM ChiTietPhieuNhap WHERE PhieuNhapKhoId=@Id", conn)
                    { Parameters = { new SQLiteParameter("@Id", currentPhieuNhapId) } }.ExecuteNonQuery();

                    new SQLiteCommand("DELETE FROM PhieuNhapKho WHERE Id=@Id", conn)
                    { Parameters = { new SQLiteParameter("@Id", currentPhieuNhapId) } }.ExecuteNonQuery();

                    MessageBox.Show("Đã xóa phiếu nhập!");
                    btnThemMoi_Click(sender, e);
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
