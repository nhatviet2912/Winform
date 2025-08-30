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

                cboLoaiSP.DataSource = dtLoai;
                cboLoaiSP.DisplayMember = "TenLoai";
                cboLoaiSP.ValueMember = "Id";
            }
        }

        private void LoadData()
        {
            using (var conn = DatabaseHelper.GetConnection())
            using (var adapter = new SQLiteDataAdapter("SELECT * FROM SanPham", conn))
            {
                dt = new DataTable();
                adapter.Fill(dt);
                dgvSanPham.DataSource = dt;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            using (var conn = DatabaseHelper.GetConnection())
            using (var cmd = new SQLiteCommand(@"
        INSERT INTO SanPham
        (MaSanPham, TenSanPham, MoTa, GiaBan, DonViTinh, LoaiSanPhamId, ThuongHieu, XuatXu, TrangThai)
        VALUES(@Ma, @Ten, @MoTa, @Gia, @DonVi, @Loai, @ThuongHieu, @XuatXu, @TrangThai)", conn))
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
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvSanPham.CurrentRow != null)
            {
                int id = Convert.ToInt32(dgvSanPham.CurrentRow.Cells["Id"].Value);

                using (var conn = DatabaseHelper.GetConnection())
                using (var cmd = new SQLiteCommand(@"
            UPDATE SanPham SET 
                MaSanPham=@Ma, TenSanPham=@Ten, MoTa=@MoTa, GiaBan=@Gia, DonViTinh=@DonVi,
                LoaiSanPhamId=@Loai, ThuongHieu=@ThuongHieu, XuatXu=@XuatXu, TrangThai=@TrangThai,
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
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvSanPham.CurrentRow != null)
            {
                int id = Convert.ToInt32(dgvSanPham.CurrentRow.Cells["Id"].Value);

                using (var conn = DatabaseHelper.GetConnection())
                using (var cmd = new SQLiteCommand("DELETE FROM SanPham WHERE Id=@Id", conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }

                LoadData();
            }
        }


        private void dgvSanPham_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSanPham.CurrentRow != null)
            {
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
    }
}
