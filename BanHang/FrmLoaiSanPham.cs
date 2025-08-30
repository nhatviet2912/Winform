using System.Data;
using System.Data.SQLite;

namespace BanHang
{
    public partial class FrmLoaiSanPham : Form
    {
        private DataTable dt;

        private CommonMenuStrip commonMenu;
        private TableLayoutPanel tableLayout;

        public FrmLoaiSanPham()
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
                using (var adapter = new SQLiteDataAdapter("SELECT * FROM LoaiSanPham", conn))
                {
                    dt = new DataTable();
                    adapter.Fill(dt);
                    dgvLoaiSP.DataSource = dt;
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
                using (var conn = DatabaseHelper.GetConnection())
                using (var cmd = new SQLiteCommand(
                    "INSERT INTO LoaiSanPham(TenLoai, MoTa) VALUES(@TenLoai, @MoTa)", conn))
                {
                    cmd.Parameters.AddWithValue("@TenLoai", txtTenLoai.Text);
                    cmd.Parameters.AddWithValue("@MoTa", txtMoTa.Text);
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
            if (dgvLoaiSP.CurrentRow != null)
            {
                int id = Convert.ToInt32(dgvLoaiSP.CurrentRow.Cells["Id"].Value);

                try
                {
                    using (var conn = DatabaseHelper.GetConnection())
                    using (var cmd = new SQLiteCommand(
                        "UPDATE LoaiSanPham " +
                        "SET TenLoai=@TenLoai, MoTa=@MoTa, NgayCapNhat=CURRENT_TIMESTAMP " +
                        "WHERE Id=@Id", conn))
                    {
                        cmd.Parameters.AddWithValue("@TenLoai", txtTenLoai.Text);
                        cmd.Parameters.AddWithValue("@MoTa", txtMoTa.Text);
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
            if (dgvLoaiSP.CurrentRow != null)
            {
                int id = Convert.ToInt32(dgvLoaiSP.CurrentRow.Cells["Id"].Value);

                try
                {
                    using (var conn = DatabaseHelper.GetConnection())
                    using (var cmd = new SQLiteCommand(
                        "DELETE FROM LoaiSanPham WHERE Id=@Id", conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);
                        cmd.ExecuteNonQuery();
                    }

                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa dữ liệu: " + ex.Message);
                }
            }
        }

        private void dgvLoaiSanPham_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvLoaiSP.CurrentRow != null)
            {
                txtTenLoai.Text = dgvLoaiSP.CurrentRow.Cells["TenLoai"].Value?.ToString();
                txtMoTa.Text = dgvLoaiSP.CurrentRow.Cells["MoTa"].Value?.ToString();
            }
        }
    }
}
