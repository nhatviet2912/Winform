using System.Data;
using System.Data.SQLite;
using System.Drawing.Printing;

namespace BanHang
{
    public partial class FrmDanhSachPhieuBan : Form
    {
        private DataTable dt;
        private CommonMenuStrip commonMenu;
        private TableLayoutPanel tableLayout;
        private DataRow _hoaDon;
        private DataTable _chiTiet;
        private PrintDocument printDocument;

        public FrmDanhSachPhieuBan()
        {
            InitializeComponent();
            InitializeCommonMenu();
            SetupTableLayout();

            printDocument = new PrintDocument();
            printDocument.PrintPage += PrintDocument_PrintPage;
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

        private void FrmDanhSachPhieuNhap_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData(string keyword = "")
        {
            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    string sql = @"
                        SELECT h.Id, h.MaHoaDon, h.NgayBan,
                               k.TenKhachHang,
                               h.NhanVienBan, h.TongTien
                        FROM HoaDonBan h
                        LEFT JOIN KhachHang k ON h.KhachHangId = k.Id
                        ORDER BY h.NgayBan DESC";

                    if (!string.IsNullOrWhiteSpace(keyword))
                    {
                        sql += " WHERE MaHoaDon LIKE @kw";
                    }

                    using (var cmd = new SQLiteCommand(sql, conn))
                    {
                        if (!string.IsNullOrWhiteSpace(keyword))
                            cmd.Parameters.AddWithValue("@kw", "%" + keyword + "%");

                        using (var adapter = new SQLiteDataAdapter(cmd))
                        {
                            dt = new DataTable();
                            adapter.Fill(dt);
                            dgvPhieuNhap.DataSource = dt;
                        }
                    }
                }

                // Ẩn cột Id
                if (dgvPhieuNhap.Columns["Id"] != null)
                    dgvPhieuNhap.Columns["Id"].Visible = false;

                var columnHeaders = new Dictionary<string, string>
                {
                    { "MaHoaDon", "Mã Phiếu" },
                    { "NgayBan", "Ngày Bán" },
                    { "TenKhachHang", "Tên Khách Hàng" },
                    { "NhanVienBan", "Nhân Viên Bán" },
                    { "TongTien", "Tổng Tiền" }
                };

                SetColumnHeaders(dgvPhieuNhap, columnHeaders);

                if (dgvPhieuNhap.Columns.Contains("TongTien"))
                {
                    dgvPhieuNhap.Columns["TongTien"].DefaultCellStyle.Format = "N0"; // 1,000,000
                }

                if (dgvPhieuNhap.Columns.Contains("NgayBan"))
                    dgvPhieuNhap.Columns["NgayBan"].DefaultCellStyle.Format = "dd/MM/yyyy";

                // Gắn sự kiện để thêm " VNĐ"
                dgvPhieuNhap.CellFormatting += dgvSanPham_CellFormatting;

                if (!dgvPhieuNhap.Columns.Contains("btnPrint"))
                {
                    DataGridViewButtonColumn btnPrint = new DataGridViewButtonColumn();
                    btnPrint.Name = "btnPrint";
                    btnPrint.HeaderText = "Chức năng";
                    btnPrint.Text = "In";
                    btnPrint.UseColumnTextForButtonValue = true; // luôn hiển thị chữ "In"
                    btnPrint.Width = 60;
                    dgvPhieuNhap.Columns.Add(btnPrint);
                    dgvPhieuNhap.CellContentClick += dgvPhieuNhap_CellContentClick;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load dữ liệu: " + ex.Message);
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

        private void dgvSanPham_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvPhieuNhap.Columns[e.ColumnIndex].Name == "TongTien" && e.Value != null && e.Value != DBNull.Value)
            {
                if (decimal.TryParse(e.Value.ToString(), out decimal gia))
                {
                    e.Value = gia.ToString("N0") + " VNĐ";
                    e.FormattingApplied = true;
                }
            }
        }

        private void btnChiTiet_Click(object sender, EventArgs e)
        {
            if (dgvPhieuNhap.CurrentRow == null) return;

            int id = Convert.ToInt32(dgvPhieuNhap.CurrentRow.Cells["Id"].Value);

            var frm = new FrmChiTietPhieuBan(id);
            frm.ShowDialog();
        }

        private void dgvPhieuNhap_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra có click đúng vào cột btnPrint không
            if (dgvPhieuNhap.Columns[e.ColumnIndex].Name == "btnPrint" && e.RowIndex >= 0)
            {
                int hoaDonId = Convert.ToInt32(dgvPhieuNhap.Rows[e.RowIndex].Cells["Id"].Value);
                var data = GetHoaDonData(hoaDonId);
                _hoaDon = data.hoaDon;
                _chiTiet = data.chiTiet;
                // TODO: gọi hàm in hóa đơn theo Id
                Print();
            }
        }

        private void Print()
        {
            if (_hoaDon == null || _chiTiet == null || _chiTiet.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để in!");
                return;
            }
            try
            {
                // Sử dụng Microsoft Print to PDF để test
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

                // In ra file PDF (sẽ hiện hộp thoại Save As của Windows)
                printDocument.Print();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi in hóa đơn: " + ex.Message);
            }
        }

        private (DataTable chiTiet, DataRow hoaDon) GetHoaDonData(int hoaDonId)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                // Lấy thông tin hóa đơn
                string sqlHoaDon = @"
                    SELECT h.Id, h.MaHoaDon, h.NgayBan, h.NhanVienBan, h.TongTien, k.TenKhachHang
                    FROM HoaDonBan h
                    LEFT JOIN KhachHang k ON h.KhachHangId = k.Id
                    WHERE h.Id = @id";

                DataTable dtHoaDon = new DataTable();
                using (var cmd = new SQLiteCommand(sqlHoaDon, conn))
                {
                    cmd.Parameters.AddWithValue("@id", hoaDonId);
                    using (var adapter = new SQLiteDataAdapter(cmd))
                    {
                        adapter.Fill(dtHoaDon);
                    }
                }

                // Lấy chi tiết hóa đơn
                string sqlChiTiet = @"
                    SELECT c.SoLuong, c.DonGia, (c.SoLuong * c.DonGia) AS ThanhTien,
                           s.TenSanPham
                    FROM ChiTietHoaDonBan c
                    INNER JOIN SanPham s ON c.SanPhamId = s.Id
                    WHERE c.HoaDonBanId = @id";

                DataTable dtChiTiet = new DataTable();
                using (var cmd = new SQLiteCommand(sqlChiTiet, conn))
                {
                    cmd.Parameters.AddWithValue("@id", hoaDonId);
                    using (var adapter = new SQLiteDataAdapter(cmd))
                    {
                        adapter.Fill(dtChiTiet);
                    }
                }

                return (dtChiTiet, dtHoaDon.Rows[0]);
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

            // ====== Thông tin hóa đơn ======
            e.Graphics.DrawString("Mã HĐ: " + _hoaDon["MaHoaDon"], font, Brushes.Black, 20, y);
            y += 20;
            e.Graphics.DrawString("Ngày: " + Convert.ToDateTime(_hoaDon["NgayBan"]).ToString("dd/MM/yyyy HH:mm"), font, Brushes.Black, 20, y);
            y += 20;
            e.Graphics.DrawString("Khách hàng: " + _hoaDon["TenKhachHang"], font, Brushes.Black, 20, y);
            y += 20;
            e.Graphics.DrawString("Nhân viên: " + _hoaDon["NhanVienBan"], font, Brushes.Black, 20, y);
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

            y += rowHeight;
            e.Graphics.DrawLine(Pens.Black, xTenSP, y, 600, y);

            decimal tongTien = 0;

            // ====== Lặp chi tiết hóa đơn ======
            foreach (DataRow row in _chiTiet.Rows)
            {
                string tenSP = row["TenSanPham"].ToString();
                int soLuong = Convert.ToInt32(row["SoLuong"]);
                decimal donGia = Convert.ToDecimal(row["DonGia"]);
                decimal thanhTien = Convert.ToDecimal(row["ThanhTien"]);

                tongTien += thanhTien;

                e.Graphics.DrawString(tenSP, font, Brushes.Black, xTenSP, y);
                e.Graphics.DrawString(soLuong.ToString(), font, Brushes.Black, xSL, y);
                e.Graphics.DrawString(donGia.ToString("N0") + "đ", font, Brushes.Black, xDonGia, y);
                e.Graphics.DrawString(thanhTien.ToString("N0") + "đ", font, Brushes.Black, xThanhTien, y);

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

    }
}
