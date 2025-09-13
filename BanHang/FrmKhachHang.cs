using System.Data;
using System.Data.SQLite;
using System.Text.RegularExpressions;

namespace BanHang
{
    public partial class FrmKhachHang : Form
    {
        private DataTable dt;
        private CommonMenuStrip commonMenu;
        private TableLayoutPanel tableLayout;

        public FrmKhachHang()
        {
            InitializeComponent();
            InitializeCommonMenu();
            SetupTableLayout();
            LoadData();
            LoadGioiTinh();

            this.btnThem.Click += btnThem_Click;
            this.btnSua.Click += btnSua_Click;
            this.btnXoa.Click += btnXoa_Click;

            txtMaCode.ReadOnly = true;
            txtMaCode.Text = GenerateMa();
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

        private void LoadData(string keyword = "")
        {
            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    string query = @"SELECT * FROM KhachHang";
                    if (!string.IsNullOrWhiteSpace(keyword))
                    {
                        query += " WHERE TenKhachHang LIKE @keyword OR DienThoai LIKE @keyword";
                    }

                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        if (!string.IsNullOrWhiteSpace(keyword))
                        {
                            cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");
                        }

                        using (var adapter = new SQLiteDataAdapter(cmd))
                        {
                            dt = new DataTable();
                            adapter.Fill(dt);
                            dgvKhachHang.DataSource = dt;

                            if (dgvKhachHang.Columns.Contains("Id"))
                                dgvKhachHang.Columns["Id"].Visible = false;

                            if (dgvKhachHang.Columns.Contains("TrangThai"))
                                dgvKhachHang.Columns["TrangThai"].Visible = false;

                            var columnHeaders = new Dictionary<string, string>
                    {
                        { "MaCode", "Mã Khách Hàng" },
                        { "TenKhachHang", "Tên Khách Hàng" },
                        { "DiaChi", "Địa Chỉ" },
                        { "DienThoai", "Điện Thoại" },
                        { "Email", "Email" },
                        { "GioiTinh", "Giới Tính" },
                        { "NgayTao", "Ngày Tạo" },
                        { "NgayCapNhat", "Ngày Cập Nhật" }
                    };

                            SetColumnHeaders(dgvKhachHang, columnHeaders);

                            if (dgvKhachHang.Columns.Contains("NgayTao"))
                                dgvKhachHang.Columns["NgayTao"].DefaultCellStyle.Format = "dd/MM/yyyy";

                            if (dgvKhachHang.Columns.Contains("NgayCapNhat"))
                                dgvKhachHang.Columns["NgayCapNhat"].DefaultCellStyle.Format = "dd/MM/yyyy";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load dữ liệu: " + ex.Message);
            }
        }


        private void LoadGioiTinh()
        {
            var gioiTinhList = new List<KeyValuePair<int, string>>
            {
                new KeyValuePair<int, string>(0, ""),       // Rỗng
                new KeyValuePair<int, string>(1, "Nam"),
                new KeyValuePair<int, string>(2, "Nữ"),
                new KeyValuePair<int, string>(3, "Khác")
            };

            cbGioiTinh.DataSource = gioiTinhList;
            cbGioiTinh.DisplayMember = "Value";  // Hiển thị tên
            cbGioiTinh.ValueMember = "Key";      // Giá trị thật
        }

        private void SetColumnHeaders(DataGridView dgv, Dictionary<string, string> headers)
        {
            foreach (var kvp in headers)
            {
                if (dgv.Columns.Contains(kvp.Key))
                    dgv.Columns[kvp.Key].HeaderText = kvp.Value;
            }
        }

        private string GenerateMa()
        {
            using (var conn = DatabaseHelper.GetConnection())
            using (var cmd = new SQLiteCommand("SELECT MAX(MaCode) FROM KhachHang", conn))
            {
                object result = cmd.ExecuteScalar();

                if (result != DBNull.Value && result != null)
                {
                    string lastCode = result.ToString(); // VD: "SP0005"
                    string numberPart = new string(lastCode.SkipWhile(c => !char.IsDigit(c)).ToArray()); // lấy "0005"

                    if (int.TryParse(numberPart, out int number))
                    {
                        int nextId = number + 1;
                        return "KH" + nextId.ToString("D4"); // SP0006
                    }
                }

                // Nếu chưa có dữ liệu thì trả về SP0001
                return "KH0001";
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateInput()) return;
                string sql = "INSERT INTO KhachHang(MaCode, TenKhachHang, DiaChi, DienThoai, Email, GioiTinh) " +
                             "VALUES(@MaCode, @TenKhachHang, @DiaChi, @DienThoai, @Email, @GioiTinh)";
                using (var conn = DatabaseHelper.GetConnection())
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaCode", txtMaCode.Text);
                    cmd.Parameters.AddWithValue("@TenKhachHang", txtTenKH.Text);
                    cmd.Parameters.AddWithValue("@DiaChi", txtDiaChi.Text);
                    cmd.Parameters.AddWithValue("@DienThoai", txtDienThoai.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@GioiTinh", cbGioiTinh.SelectedIndex);
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
            if (!ValidateInput()) return;
            if (dgvKhachHang.CurrentRow != null)
            {
                int id = Convert.ToInt32(dgvKhachHang.CurrentRow.Cells["Id"].Value);
                string sql = "UPDATE KhachHang SET MaCode=@MaCode, TenKhachHang=@TenKhachHang, DiaChi=@DiaChi, DienThoai=@DienThoai, Email=@Email, GioiTinh=@GioiTinh, NgayCapNhat=CURRENT_TIMESTAMP WHERE Id=@Id";
                try
                {
                    using (var conn = DatabaseHelper.GetConnection())
                    using (var cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaCode", txtMaCode.Text);
                        cmd.Parameters.AddWithValue("@TenKhachHang", txtTenKH.Text);
                        cmd.Parameters.AddWithValue("@DiaChi", txtDiaChi.Text);
                        cmd.Parameters.AddWithValue("@DienThoai", txtDienThoai.Text);
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                        cmd.Parameters.AddWithValue("@GioiTinh", cbGioiTinh.SelectedIndex);
                        cmd.Parameters.AddWithValue("@Id", id);
                        cmd.ExecuteNonQuery();
                    }

                    LoadData();
                    ClearInput();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi sửa dữ liệu: " + ex.Message);
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa khách hàng này không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
                );
            if (result == DialogResult.Yes)
            {
                if (dgvKhachHang.CurrentRow != null)
                {
                    int id = Convert.ToInt32(dgvKhachHang.CurrentRow.Cells["Id"].Value);
                    string sql = "DELETE FROM KhachHang WHERE Id=@Id";
                    try
                    {
                        using (var conn = DatabaseHelper.GetConnection())
                        using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                        {
                            cmd.Parameters.AddWithValue("@Id", id);
                            cmd.ExecuteNonQuery();
                            LoadData();
                            ClearInput();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi sửa dữ liệu: " + ex.Message);
                    }
                }
            }
        }

        private void ClearInput()
        {
            txtMaCode.Text = GenerateMa();
            txtTenKH.Clear();
            txtDiaChi.Clear();
            txtDienThoai.Clear();
            txtEmail.Clear();
            cbGioiTinh.SelectedIndex = -1;
        }

        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtMaCode.Text = dgvKhachHang.CurrentRow.Cells["MaCode"].Value.ToString();
                txtTenKH.Text = dgvKhachHang.CurrentRow.Cells["TenKhachHang"].Value.ToString();
                txtDiaChi.Text = dgvKhachHang.CurrentRow.Cells["DiaChi"].Value.ToString();
                txtDienThoai.Text = dgvKhachHang.CurrentRow.Cells["DienThoai"].Value.ToString();
                txtEmail.Text = dgvKhachHang.CurrentRow.Cells["Email"].Value.ToString();
                cbGioiTinh.SelectedIndex = Convert.ToInt32(dgvKhachHang.CurrentRow.Cells["GioiTinh"].Value);
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtMaCode.Text))
            {
                MessageBox.Show("Vui lòng nhập mã sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtTenKH.Text))
            {
                MessageBox.Show("Vui lòng nhập tên khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (cbGioiTinh.SelectedValue == null || Convert.ToInt32(cbGioiTinh.SelectedValue) == 0)
            {
                MessageBox.Show("Vui lòng chọn giới tính!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDiaChi.Text))
            {
                MessageBox.Show("Vui lòng nhập địa chỉ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDienThoai.Text))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Vui lòng nhập email!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else
            {
                // Regex cơ bản kiểm tra email
                string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                if (!Regex.IsMatch(txtEmail.Text, pattern))
                {
                    MessageBox.Show("Email không đúng định dạng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            return true;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData(txtSearch.Text);
        }
    }
}
