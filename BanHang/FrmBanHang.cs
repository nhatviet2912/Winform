using System.Data;
using System.Data.SQLite;
using System.Drawing.Printing;

namespace BanHang
{
    public partial class FrmBanHang : Form
    {
        private DataTable dt;
        private CommonMenuStrip commonMenu;
        private TableLayoutPanel tableLayout;
        private int currentHoaDonId = -1;
        private string tongtien = "0";
        private PrintDocument printDocument;

        public FrmBanHang()
        {
            InitializeComponent();
            InitializeCommonMenu();
            SetupTableLayout();
            printDocument = new PrintDocument();
            printDocument.PrintPage += PrintDocument_PrintPage;

            txtMaHoaDon.ReadOnly = true;
            txtMaHoaDon.Text = GenerateMa();
            dgvChiTiet.CellFormatting += dgvChiTiet_CellFormatting;

            LoadKhachHang();
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
            var frm = new FrmThemSanPham(false);
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
                                var sanPhamId = InsertChiTietHoaDon(conn, row);
                                int soLuong = Convert.ToInt32(row.Cells["SoLuong"].Value);
                                UpdateTonKho(conn, sanPhamId, soLuong);
                            }

                            // Commit transaction nếu không có lỗi
                            trans.Commit();
                            ClearInput();
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

        private void Print()
        {
            try
            {
                // Lấy setting máy in
                printDocument.PrinterSettings.PrinterName = Properties.Settings.Default.PrinterName;

                // Áp dụng hướng in
                if (Properties.Settings.Default.Orientation == "Landscape")
                    printDocument.DefaultPageSettings.Landscape = true;
                else
                    printDocument.DefaultPageSettings.Landscape = false;

                // Áp dụng margin
                printDocument.DefaultPageSettings.Margins = new Margins(
                    Properties.Settings.Default.MarginLeft,
                    Properties.Settings.Default.MarginRight,
                    Properties.Settings.Default.MarginTop,
                    Properties.Settings.Default.MarginBottom
                );

                // Nếu có chọn khổ giấy
                if (!string.IsNullOrEmpty(Properties.Settings.Default.PaperSize))
                {
                    foreach (PaperSize ps in printDocument.PrinterSettings.PaperSizes)
                    {
                        if (ps.PaperName == Properties.Settings.Default.PaperSize)
                        {
                            printDocument.DefaultPageSettings.PaperSize = ps;
                            break;
                        }
                    }
                }

                // In trực tiếp
                printDocument.Print();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi in hóa đơn: " + ex.Message);
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
                    string updateSql = "UPDATE TonKho SET SoLuong = SoLuong - @SoLuong WHERE SanPhamId = @SanPhamId";
                    using (var updateCmd = new SQLiteCommand(updateSql, conn))
                    {
                        updateCmd.Parameters.AddWithValue("@SoLuong", soLuong);
                        updateCmd.Parameters.AddWithValue("@SanPhamId", sanPhamId);
                        updateCmd.ExecuteNonQuery();
                    }
                }
            }
        }

        private void LoadKhachHang()
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                string sql = "SELECT Id, TenKhachHang FROM KhachHang";
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        // Thêm option rỗng vào đầu danh sách
                        DataRow row = dt.NewRow();
                        row["Id"] = DBNull.Value;       // hoặc 0 nếu bạn muốn
                        row["TenKhachHang"] = "";       // hiển thị rỗng
                        dt.Rows.InsertAt(row, 0);

                        comboBox1.DataSource = dt;
                        comboBox1.DisplayMember = "TenKhachHang";
                        comboBox1.ValueMember = "Id";
                    }
                }
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
                cmd.Parameters.AddWithValue("@KhachHangId", Convert.ToInt32(comboBox1.SelectedValue));
                cmd.Parameters.AddWithValue("@NhanVienBan", txtNhanVienBan.Text);
                cmd.Parameters.AddWithValue("@TongTien", Convert.ToDecimal(tongtien));
                currentHoaDonId = Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        private int InsertChiTietHoaDon(SQLiteConnection conn, DataGridViewRow row)
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
            string sql = "INSERT INTO ChiTietHoaDonBan (HoaDonBanId, SanPhamId, SoLuong, DonGia) " +
                         "VALUES (@HoaDonBanId, @SanPhamId, @SoLuong, @DonGia)";

            using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@HoaDonBanId", currentHoaDonId);
                cmd.Parameters.AddWithValue("@SanPhamId", sanPhamId);
                cmd.Parameters.AddWithValue("@SoLuong", Convert.ToInt32(row.Cells["SoLuong"].Value));
                cmd.Parameters.AddWithValue("@DonGia", Convert.ToDecimal(row.Cells["DonGia"].Value));
                cmd.ExecuteNonQuery();
            }

            return sanPhamId;
        }


        private void btnHuyHoaDon_Click(object sender, EventArgs e)
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

        private void ClearInput()
        {
            currentHoaDonId = -1;
            txtMaHoaDon.Text = GenerateMa();
            dtpNgayBan.Value = DateTime.Now;
            comboBox1.SelectedIndex = 0;
            txtNhanVienBan.Clear();
            txtTongTien.Text = "0";
            dgvChiTiet.Rows.Clear();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private string GenerateMa()
        {
            using (var conn = DatabaseHelper.GetConnection())
            using (var cmd = new SQLiteCommand("SELECT MAX(MaHoaDon) FROM HoaDonBan", conn))
            {
                object result = cmd.ExecuteScalar();

                if (result != DBNull.Value && result != null)
                {
                    string lastCode = result.ToString(); // VD: "SP0005"
                    string numberPart = new string(lastCode.SkipWhile(c => !char.IsDigit(c)).ToArray()); // lấy "0005"

                    if (int.TryParse(numberPart, out int number))
                    {
                        int nextId = number + 1;
                        return "MHDB" + nextId.ToString("D4"); // SP0006
                    }
                }

                // Nếu chưa có dữ liệu thì trả về SP0001
                return "MHDB0001";
            }
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Font font = new Font("Arial", 10);
            Font fontBold = new Font("Arial", 10, FontStyle.Bold);
            float y = 20;

            // ====== Tiêu đề ======
            e.Graphics.DrawString("HÓA ĐƠN BÁN HÀNG", new Font("Arial", 14, FontStyle.Bold), Brushes.Black, 200, y);
            y += 40;

            // ====== Ngày giờ ======
            e.Graphics.DrawString("Ngày: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm"), font, Brushes.Black, 20, y);
            y += 30;

            // ====== Định nghĩa cột ======
            int xTenSP = 20;
            int xSL = 250;
            int xDonGia = 320;
            int xThanhTien = 450;
            int rowHeight = 25;

            // Header bảng
            e.Graphics.DrawString("Sản phẩm", fontBold, Brushes.Black, xTenSP, y);
            e.Graphics.DrawString("Số Lượng", fontBold, Brushes.Black, xSL, y);
            e.Graphics.DrawString("Đơn giá", fontBold, Brushes.Black, xDonGia, y);
            e.Graphics.DrawString("Thành tiền", fontBold, Brushes.Black, xThanhTien, y);

            // Kẻ line dưới header
            y += rowHeight;
            e.Graphics.DrawLine(Pens.Black, xTenSP, y, 600, y);

            decimal tongTien = 0;

            // ====== Lặp chi tiết hóa đơn ======
            foreach (DataGridViewRow row in dgvChiTiet.Rows)
            {
                if (row.IsNewRow) continue;

                string tenSP = row.Cells["TenSP"].Value.ToString();
                int soLuong = Convert.ToInt32(row.Cells["SoLuong"].Value);
                decimal donGia = Convert.ToDecimal(row.Cells["DonGia"].Value);
                decimal thanhTien = soLuong * donGia;

                tongTien += thanhTien;

                // In dữ liệu
                e.Graphics.DrawString(tenSP, font, Brushes.Black, xTenSP, y);
                e.Graphics.DrawString(soLuong.ToString(), font, Brushes.Black, xSL, y);
                e.Graphics.DrawString(donGia.ToString("N0") + "đ", font, Brushes.Black, xDonGia, y);
                e.Graphics.DrawString(thanhTien.ToString("N0") + "đ", font, Brushes.Black, xThanhTien, y);

                // Kẻ line dưới từng dòng
                y += rowHeight;
                e.Graphics.DrawLine(Pens.LightGray, xTenSP, y, 600, y);
            }

            // ====== Tổng cộng ======
            y += 10;
            e.Graphics.DrawString("TỔNG CỘNG: " + tongTien.ToString("N0") + "đ", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, xThanhTien - 50, y);

            // ====== Ký tên ======
            y += 60;
            e.Graphics.DrawString("Người bán hàng", font, Brushes.Black, xTenSP, y);
            e.Graphics.DrawString("Người mua hàng", font, Brushes.Black, 400, y);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dgvChiTiet.Rows.Count <= 1)
            {
                MessageBox.Show("Không có dữ liệu để in!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Print();
        }
    }
}
