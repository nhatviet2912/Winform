using System.Data;
using System.Data.SQLite;

namespace BanHang
{
    public partial class FrmLoaiSanPham : Form
    {
        private DataTable dt;

        private CommonMenuStrip commonMenu;
        private TableLayoutPanel tableLayout;
        bool isLoading = false;

        public FrmLoaiSanPham()
        {
            InitializeComponent();
            InitializeCommonMenu();
            SetupTableLayout();
            this.Load += FrmLoaiSanPham_Load;
            this.btnThem.Click += btnThem_Click;
            this.btnSua.Click += btnSua_Click;
            this.btnXoa.Click += btnXoa_Click;
        }

        private void FrmLoaiSanPham_Load(object sender, EventArgs e)
        {
            LoadData();
            if (dgvLoaiSP.Columns["Id"] != null)
                dgvLoaiSP.Columns["Id"].Visible = false;
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

        private void ClearInput ()
        {
            txtTenLoai.Clear();
            txtMoTa.Clear();
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

                    var columnHeaders = new Dictionary<string, string>
                    {
                        { "TenLoai", "Tên Loại" },
                        { "MoTa", "Mô Tả" },
                        { "NgayTao", "Ngày Tạo" },
                        { "NgayCapNhat", "Ngày Cập Nhật" }
                    };

                    SetColumnHeaders(dgvLoaiSP, columnHeaders);

                    if (dgvLoaiSP.Columns.Contains("NgayTao"))
                        dgvLoaiSP.Columns["NgayTao"].DefaultCellStyle.Format = "dd/MM/yyyy";

                    if (dgvLoaiSP.Columns.Contains("NgayCapNhat"))
                        dgvLoaiSP.Columns["NgayCapNhat"].DefaultCellStyle.Format = "dd/MM/yyyy";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load dữ liệu: " + ex.Message);
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
            if (!ValidateInput()) return;
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
                    ClearInput();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa dữ liệu: " + ex.Message);
                }
            }
        }

        private void dgvLoaiSP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) { 
                var row = dgvLoaiSP.Rows[e.RowIndex]; 
                txtTenLoai.Text = Convert.ToString(row.Cells["TenLoai"].Value); 
                txtMoTa.Text = Convert.ToString(row.Cells["MoTa"].Value); 
            } 
        }

        private bool ValidateInput()
        {
            // Kiểm tra Tên loại
            if (string.IsNullOrWhiteSpace(txtTenLoai.Text))
            {
                MessageBox.Show("Vui lòng nhập Tên loại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenLoai.Focus();
                return false;
            }

            return true;
        }

    }
}
