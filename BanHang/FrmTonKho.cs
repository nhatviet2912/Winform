using System.Data;
using System.Data.SQLite;

namespace BanHang
{
    public partial class FrmTonKho : Form
    {
        private DataTable dt;
        private CommonMenuStrip commonMenu;
        private TableLayoutPanel tableLayout;
        public FrmTonKho()
        {
            InitializeComponent();
            InitializeCommonMenu();
            SetupTableLayout();
            LoadTonKho();
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

        private void LoadTonKho(string keyword = "")
        {
            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    string sql = @"SELECT 
                               tk.Id, 
                               sp.TenSanPham AS [Tên sản phẩm], 
                               sp.ThuongHieu AS [Thương hiệu],
                               sp.XuatXu AS [Xuất xứ],
                               sp.MoTa AS [Mô tả],
                               tk.SoLuong AS [Số lượng tồn]
                           FROM TonKho tk
                           INNER JOIN SanPham sp ON tk.SanPhamId = sp.Id";

                    if (!string.IsNullOrWhiteSpace(keyword))
                    {
                        sql += " WHERE sp.TenSanPham LIKE @kw ";
                    }
                    using (var cmd = new SQLiteCommand(sql, conn))
                    {
                        if (!string.IsNullOrWhiteSpace(keyword))
                            cmd.Parameters.AddWithValue("@kw", "%" + keyword + "%");

                        using (var adapter = new SQLiteDataAdapter(cmd))
                        {
                            dt = new DataTable();
                            adapter.Fill(dt);
                            dgvTonKho.DataSource = dt;
                        }
                    }
                }

                // Ẩn cột Id
                if (dgvTonKho.Columns["Id"] != null)
                    dgvTonKho.Columns["Id"].Visible = false;

                // Format số lượng tồn
                if (dgvTonKho.Columns.Contains("Số lượng tồn"))
                {
                    dgvTonKho.Columns["Số lượng tồn"].DefaultCellStyle.Format = "N0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load dữ liệu: " + ex.Message,
                                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadTonKho(txtSearch.Text);
        }
    }
}
