using System.Data;
using System.Data.SQLite;

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

        private void LoadData()
        {
            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                using (var adapter = new SQLiteDataAdapter("SELECT * FROM KhachHang", conn))
                {
                    dt = new DataTable();
                    adapter.Fill(dt);
                    dgvKhachHang.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load dữ liệu: " + ex.Message);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm dữ liệu: " + ex.Message);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
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
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi sửa dữ liệu: " + ex.Message);
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
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
                    }
                }
                catch (Exception ex) 
                {
                    MessageBox.Show("Lỗi khi sửa dữ liệu: " + ex.Message);
                }
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaCode.Clear();
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
    }
}
