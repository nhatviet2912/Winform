using System.Data;
using System.Data.SQLite;

namespace BanHang
{
    public partial class FrmNhapHang : Form
    {
        private DataTable dt;
        private CommonMenuStrip commonMenu;
        private TableLayoutPanel tableLayout;
        private int currentPhieuNhapId = -1;
        private string tongtien = "0";

        public FrmNhapHang()
        {
            InitializeComponent();
            InitializeCommonMenu();
            SetupTableLayout();

            txtMaPhieu.ReadOnly = true;
            txtMaPhieu.Text = GenerateMa();
            dgvChiTiet.CellFormatting += dgvChiTiet_CellFormatting;
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
            var frm = new FrmThemSanPham(true);
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

        private void dgvChiTiet_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var colName = dgvChiTiet.Columns[e.ColumnIndex].Name;
            if (colName == "DonGia" || colName == "ThanhTien")
            {
                if (e.Value == null || e.Value == DBNull.Value) return;

                // Nếu giá trị đã là decimal
                if (e.Value is decimal dec)
                {
                    e.Value = dec.ToString("N0") + " VNĐ";
                    e.FormattingApplied = true;
                }
                else
                {
                    // thử parse nếu vô tình là string số
                    if (decimal.TryParse(e.Value.ToString(), out decimal tmp))
                    {
                        e.Value = tmp.ToString("N0") + " VNĐ";
                        e.FormattingApplied = true;
                    }
                }
            }
        }

        private void TinhTongTien()
        {
            decimal tong = 0;
            foreach (DataGridViewRow row in dgvChiTiet.Rows)
            {
                tong += Convert.ToDecimal(row.Cells["ThanhTien"].Value);
            }
            txtTongTien.Text = tong.ToString("N0") + " VNĐ";
            tongtien = tong.ToString();
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
                            var sanPhamId = InsertChiTietPhieuNhap(conn, row);
                            int soLuong = Convert.ToInt32(row.Cells["SoLuong"].Value);
                            UpdateTonKho(conn, sanPhamId, soLuong);
                        }

                        // Commit transaction nếu tất cả các thao tác thành công
                        trans.Commit();
                        ClearInput();
                        dgvChiTiet.Rows.Clear();
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

        private void ClearInput()
        {
            currentPhieuNhapId = -1;
            txtMaPhieu.Text = GenerateMa();
            dtpNgayNhap.Value = DateTime.Now;
            txtNhaCungCap.Clear();
            txtNhanVienNhap.Clear();
            txtTongTien.Text = "0";
            txtGhiChu.Clear();
            dgvChiTiet.Rows.Clear();
        }

        private void InsertPhieuNhap(SQLiteConnection conn)
        {
            string sql = "INSERT INTO PhieuNhapKho (MaPhieu, NgayNhap, NhaCungCap, NhanVienNhap, TongTien, GhiChu) " +
                         "VALUES (@MaPhieu, @NgayNhap, @NhaCungCap, @NhanVienNhap, @TongTien, @GhiChu); " +
                         "SELECT last_insert_rowid();";

            using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@MaPhieu", txtMaPhieu.Text);
                cmd.Parameters.AddWithValue("@NgayNhap", dtpNgayNhap.Value);
                cmd.Parameters.AddWithValue("@NhaCungCap", txtNhaCungCap.Text);
                cmd.Parameters.AddWithValue("@NhanVienNhap", txtNhanVienNhap.Text);
                cmd.Parameters.AddWithValue("@TongTien", Convert.ToDecimal(tongtien));
                cmd.Parameters.AddWithValue("@GhiChu", txtGhiChu.Text);

                currentPhieuNhapId = Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        private void UpdateTonKho(SQLiteConnection conn, int sanPhamId, int soLuong)
        {
            // Kiểm tra đã có tồn kho cho sản phẩm chưa
            string checkSql = "SELECT SoLuong FROM TonKho WHERE SanPhamId = @SanPhamId";
            using (var checkCmd = new SQLiteCommand(checkSql, conn))
            {
                checkCmd.Parameters.AddWithValue("@SanPhamId", sanPhamId);
                var result = checkCmd.ExecuteScalar();

                if (result != null) // đã có -> update
                {
                    string updateSql = "UPDATE TonKho SET SoLuong = SoLuong + @SoLuong WHERE SanPhamId = @SanPhamId";
                    using (var updateCmd = new SQLiteCommand(updateSql, conn))
                    {
                        updateCmd.Parameters.AddWithValue("@SoLuong", soLuong);
                        updateCmd.Parameters.AddWithValue("@SanPhamId", sanPhamId);
                        updateCmd.ExecuteNonQuery();
                    }
                }
                else // chưa có -> insert mới
                {
                    string insertSql = "INSERT INTO TonKho (SanPhamId, SoLuong) VALUES (@SanPhamId, @SoLuong)";
                    using (var insertCmd = new SQLiteCommand(insertSql, conn))
                    {
                        insertCmd.Parameters.AddWithValue("@SanPhamId", sanPhamId);
                        insertCmd.Parameters.AddWithValue("@SoLuong", soLuong);
                        insertCmd.ExecuteNonQuery();
                    }
                }
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
                cmd.Parameters.AddWithValue("@TongTien", Convert.ToDecimal(tongtien));
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

        private int InsertChiTietPhieuNhap(SQLiteConnection conn, DataGridViewRow row)
        {
            string maSP = row.Cells["SanPhamId"].Value?.ToString();

            if (string.IsNullOrEmpty(maSP))
                throw new Exception("Mã sản phẩm không được để trống");

            // Query lấy SanPhamId theo MaSP
            int sanPhamId;
            string sqlGetId = "SELECT Id FROM SanPham WHERE MaSanPham = @MaSP LIMIT 1";
            using (SQLiteCommand cmdGet = new SQLiteCommand(sqlGetId, conn))
            {
                cmdGet.Parameters.AddWithValue("@MaSP", maSP);
                object result = cmdGet.ExecuteScalar();
                if (result == null)
                    throw new Exception($"Không tìm thấy sản phẩm với mã: {maSP}");
                sanPhamId = Convert.ToInt32(result);
            }
            string sql = "INSERT INTO ChiTietPhieuNhap (PhieuNhapKhoId, SanPhamId, SoLuong, DonGiaNhap) " +
                         "VALUES (@PhieuNhapKhoId, @SanPhamId, @SoLuong, @DonGiaNhap)";

            using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@PhieuNhapKhoId", currentPhieuNhapId);
                cmd.Parameters.AddWithValue("@SanPhamId", sanPhamId);
                cmd.Parameters.AddWithValue("@SoLuong", Convert.ToInt32(row.Cells["SoLuong"].Value));
                cmd.Parameters.AddWithValue("@DonGiaNhap", Convert.ToDecimal(row.Cells["DonGiaNhap"].Value));
                cmd.ExecuteNonQuery();
            }

            return sanPhamId;
        }


        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvChiTiet.CurrentRow == null) return;

            var result = MessageBox.Show("Bạn có chắc chắn muốn xóa sản phẩm này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    dgvChiTiet.Rows.RemoveAt(dgvChiTiet.CurrentRow.Index);
                    TinhTongTien();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message);
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private string GenerateMa()
        {
            using (var conn = DatabaseHelper.GetConnection())
            using (var cmd = new SQLiteCommand("SELECT MAX(MaPhieu) FROM PhieuNhapKho", conn))
            {
                object result = cmd.ExecuteScalar();

                if (result != DBNull.Value && result != null)
                {
                    string lastCode = result.ToString(); // VD: "SP0005"
                    string numberPart = new string(lastCode.SkipWhile(c => !char.IsDigit(c)).ToArray()); // lấy "0005"

                    if (int.TryParse(numberPart, out int number))
                    {
                        int nextId = number + 1;
                        return "MP" + nextId.ToString("D4"); // SP0006
                    }
                }

                // Nếu chưa có dữ liệu thì trả về SP0001
                return "MP0001";
            }
        }
    }
}
