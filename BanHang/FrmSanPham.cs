using System.Data;
using System.Data.SQLite;

namespace BanHang
{
    public partial class FrmSanPham : Form
    {
        private DataTable dt;
        private CommonMenuStrip commonMenu;
        private TableLayoutPanel tableLayout;

        public FrmSanPham()
        {
            InitializeComponent();
            LoadLoaiSP();
            LoadData();
            InitializeCommonMenu();
            SetupTableLayout();

            this.btnThem.Click += btnThem_Click;
            this.btnSua.Click += btnSua_Click;
            this.btnXoa.Click += btnXoa_Click;

            txtMaSP.ReadOnly = true;
            txtMaSP.Text = GenerateMaSanPham();
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

        private void LoadLoaiSP()
        {
            using (var conn = DatabaseHelper.GetConnection())
            using (var cmd = new SQLiteCommand("SELECT Id, TenLoai FROM LoaiSanPham", conn))
            using (var reader = cmd.ExecuteReader())
            {
                DataTable dtLoai = new DataTable();
                dtLoai.Load(reader);

                // Thêm option rỗng ở dòng đầu tiên
                DataRow row = dtLoai.NewRow();
                row["Id"] = 0;              // giá trị ảo
                row["TenLoai"] = " ";       // hoặc "Chọn loại sản phẩm"
                dtLoai.Rows.InsertAt(row, 0);

                cboLoaiSP.DataSource = dtLoai;
                cboLoaiSP.DisplayMember = "TenLoai";
                cboLoaiSP.ValueMember = "Id";

                // Mặc định chọn option rỗng
                cboLoaiSP.SelectedIndex = 0;
            }
        }

        private void LoadData()
        {
            var query = @"SELECT  
SanPham.Id AS Id,  
SanPham.MaSanPham,  
SanPham.TenSanPham,  
LoaiSanPham.TenLoai AS LoaiSanPhamTen,
SanPham.MoTa AS MoTa,  
SanPham.GiaBan,  
SanPham.DonViTinh,  
SanPham.ThuongHieu,  
SanPham.XuatXu,  
SanPham.NgayTao, 
SanPham.LoaiSanPhamId, 
SanPham.NgayCapNhat
FROM SanPham
LEFT JOIN LoaiSanPham 
ON SanPham.LoaiSanPhamId = LoaiSanPham.Id";
            using (var conn = DatabaseHelper.GetConnection())
            using (var adapter = new SQLiteDataAdapter(query, conn))
            {
                dt = new DataTable();
                adapter.Fill(dt);
                dgvSanPham.DataSource = dt;

                if (dgvSanPham.Columns.Contains("Id"))
                {
                    dgvSanPham.Columns["Id"].Visible = false;
                }

                if (dgvSanPham.Columns.Contains("LoaiSanPhamId"))
                {
                    dgvSanPham.Columns["LoaiSanPhamId"].Visible = false;
                }

                var columnHeaders = new Dictionary<string, string>
                {
                    { "MaSanPham", "Mã Sản Phẩm" },
                    { "TenSanPham", "Tên Sản Phẩm" },
                    { "MoTa", "Mô Tả" },
                    { "GiaBan", "Giá Bán" },
                    { "DonViTinh", "Đơn Vị Tính" },
                    { "LoaiSanPhamTen", "Loại Sản Phẩm" },
                    { "ThuongHieu", "Thương Hiệu" },
                    { "XuatXu", "Xuất Xứ" },
                    { "TrangThai", "Trạng Thái" },
                    { "NgayTao", "Ngày Tạo" },
                    { "NgayCapNhat", "Ngày Cập Nhật" }
                };

                SetColumnHeaders(dgvSanPham, columnHeaders);

                if (dgvSanPham.Columns.Contains("GiaBan"))
                {
                    dgvSanPham.Columns["GiaBan"].DefaultCellStyle.Format = "N0"; // 1,000,000
                }

                if (dgvSanPham.Columns.Contains("NgayTao"))
                    dgvSanPham.Columns["NgayTao"].DefaultCellStyle.Format = "dd/MM/yyyy";

                if (dgvSanPham.Columns.Contains("NgayCapNhat"))
                    dgvSanPham.Columns["NgayCapNhat"].DefaultCellStyle.Format = "dd/MM/yyyy";

                // Gắn sự kiện để thêm " VNĐ"
                dgvSanPham.CellFormatting += dgvSanPham_CellFormatting;
            }
        }

        private void txtGia_TextChanged(object sender, EventArgs e)
        {
            if (txtGia.Text == "") return;

            // Lấy vị trí con trỏ hiện tại
            int selectionStart = txtGia.SelectionStart;

            // Loại bỏ dấu phân cách cũ
            string raw = txtGia.Text.Replace(",", "").Replace(".", "");

            if (decimal.TryParse(raw, out decimal value))
            {
                txtGia.Text = value.ToString("N0");
                // Đặt lại vị trí con trỏ sau khi format
                txtGia.SelectionStart = Math.Min(selectionStart, txtGia.Text.Length);
            }
        }

        private string GenerateMaSanPham()
        {
            using (var conn = DatabaseHelper.GetConnection())
            using (var cmd = new SQLiteCommand("SELECT MAX(MaSanPham) FROM SanPham", conn))
            {
                object result = cmd.ExecuteScalar();

                if (result != DBNull.Value && result != null)
                {
                    string lastCode = result.ToString(); // VD: "SP0005"
                    string numberPart = new string(lastCode.SkipWhile(c => !char.IsDigit(c)).ToArray()); // lấy "0005"

                    if (int.TryParse(numberPart, out int number))
                    {
                        int nextId = number + 1;
                        return "SP" + nextId.ToString("D4"); // SP0006
                    }
                }

                // Nếu chưa có dữ liệu thì trả về SP0001
                return "SP0001";
            }
        }

        private void dgvSanPham_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvSanPham.Columns[e.ColumnIndex].Name == "GiaBan" && e.Value != null && e.Value != DBNull.Value)
            {
                if (decimal.TryParse(e.Value.ToString(), out decimal gia))
                {
                    e.Value = gia.ToString("N0") + " VNĐ";
                    e.FormattingApplied = true;
                }
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

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateInput()) return;
                using (var conn = DatabaseHelper.GetConnection())
                using (var cmd = new SQLiteCommand(@"
                INSERT INTO SanPham
                (MaSanPham, TenSanPham, MoTa, GiaBan, DonViTinh, LoaiSanPhamId, ThuongHieu, XuatXu)
                VALUES(@Ma, @Ten, @MoTa, @Gia, @DonVi, @Loai, @ThuongHieu, @XuatXu)", conn))
                {
                    cmd.Parameters.AddWithValue("@Ma", txtMaSP.Text);
                    cmd.Parameters.AddWithValue("@Ten", txtTenSP.Text);
                    cmd.Parameters.AddWithValue("@MoTa", txtMoTa.Text);
                    cmd.Parameters.AddWithValue("@Gia", decimal.TryParse(txtGia.Text, out var gia) ? gia : 0);
                    cmd.Parameters.AddWithValue("@DonVi", txtDonViTinh.Text);
                    cmd.Parameters.AddWithValue("@Loai", cboLoaiSP.SelectedValue);
                    cmd.Parameters.AddWithValue("@ThuongHieu", txtThuongHieu.Text);
                    cmd.Parameters.AddWithValue("@XuatXu", txtXuatXu.Text);

                    cmd.ExecuteNonQuery();
                }

                LoadData();
                ClearInput();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm dữ liệu: " + ex.Message);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateInput()) return;
                if (dgvSanPham.CurrentRow != null)
                {
                    int id = Convert.ToInt32(dgvSanPham.CurrentRow.Cells["Id"].Value);

                    using (var conn = DatabaseHelper.GetConnection())
                    using (var cmd = new SQLiteCommand(@"
                    UPDATE SanPham SET 
                        MaSanPham=@Ma, TenSanPham=@Ten, MoTa=@MoTa, GiaBan=@Gia, DonViTinh=@DonVi,
                        LoaiSanPhamId=@Loai, ThuongHieu=@ThuongHieu, XuatXu=@XuatXu,
                        NgayCapNhat=CURRENT_TIMESTAMP
                    WHERE Id=@Id", conn))
                    {
                        cmd.Parameters.AddWithValue("@Ma", txtMaSP.Text);
                        cmd.Parameters.AddWithValue("@Ten", txtTenSP.Text);
                        cmd.Parameters.AddWithValue("@MoTa", txtMoTa.Text);
                        cmd.Parameters.AddWithValue("@Gia", decimal.TryParse(txtGia.Text, out var gia) ? gia : 0);
                        cmd.Parameters.AddWithValue("@DonVi", txtDonViTinh.Text);
                        cmd.Parameters.AddWithValue("@Loai", cboLoaiSP.SelectedValue);
                        cmd.Parameters.AddWithValue("@ThuongHieu", txtThuongHieu.Text);
                        cmd.Parameters.AddWithValue("@XuatXu", txtXuatXu.Text);
                        cmd.Parameters.AddWithValue("@Id", id);

                        cmd.ExecuteNonQuery();
                    }

                    LoadData();
                    ClearInput();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm dữ liệu: " + ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                var result = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa sản phẩm này không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(dgvSanPham.CurrentRow.Cells["Id"].Value);

                    using (var conn = DatabaseHelper.GetConnection())
                    using (var cmd = new SQLiteCommand("DELETE FROM SanPham WHERE Id=@Id", conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);
                        cmd.ExecuteNonQuery();
                    }

                    LoadData();
                    ClearInput();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm dữ liệu: " + ex.Message);
            }
        }

        private void dgvSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvSanPham.Rows[e.RowIndex];
                txtMaSP.Text = dgvSanPham.CurrentRow.Cells["MaSanPham"].Value?.ToString();
                txtTenSP.Text = dgvSanPham.CurrentRow.Cells["TenSanPham"].Value?.ToString();
                txtMoTa.Text = dgvSanPham.CurrentRow.Cells["MoTa"].Value?.ToString();
                txtGia.Text = dgvSanPham.CurrentRow.Cells["GiaBan"].Value?.ToString();
                txtDonViTinh.Text = dgvSanPham.CurrentRow.Cells["DonViTinh"].Value?.ToString();
                cboLoaiSP.SelectedValue = dgvSanPham.CurrentRow.Cells["LoaiSanPhamId"].Value;
                txtThuongHieu.Text = dgvSanPham.CurrentRow.Cells["ThuongHieu"].Value?.ToString();
                txtXuatXu.Text = dgvSanPham.CurrentRow.Cells["XuatXu"].Value?.ToString();
            }
        }

        private void ClearInput()
        {
            var text = GenerateMaSanPham();
            txtMaSP.Text = GenerateMaSanPham();
            txtTenSP.Clear();
            txtMoTa.Clear();
            txtGia.Clear();
            txtDonViTinh.Clear();
            cboLoaiSP.SelectedIndex = 0;
            txtThuongHieu.Clear();
            txtXuatXu.Clear();
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtMaSP.Text))
            {
                MessageBox.Show("Lỗi mã sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtTenSP.Text))
            {
                MessageBox.Show("Xin mời nhập tên sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (cboLoaiSP.SelectedValue == null || Convert.ToInt32(cboLoaiSP.SelectedValue) == 0)
            {
                MessageBox.Show("Vui lòng chọn loại sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtGia.Text))
            {
                MessageBox.Show("Xin mời nhập giá bán!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            string giaText = txtGia.Text.Replace(",", "").Replace(".", "");

            if (!int.TryParse(giaText, out int gia) || gia <= 0)
            {
                MessageBox.Show("Giá bán phải là số nguyên dương!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
    }
}
