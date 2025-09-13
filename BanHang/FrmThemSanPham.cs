using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace BanHang
{
    public partial class FrmThemSanPham : Form
    {
        public int Id { get; private set; }
        public string MaSP { get; private set; }
        public string TenSP { get; private set; }
        public int SoLuong { get; private set; }
        public decimal DonGia { get; private set; }

        private bool _hasNhapHang;

        public FrmThemSanPham(bool hasNhapHang)
        {
            InitializeComponent();
            this.Load += FrmThemSanPham_Load;
            _hasNhapHang = hasNhapHang;
        }

        private void FrmThemSanPham_Load(object sender, EventArgs e)
        {
            LoadSanPham();
            txtTenSP.ReadOnly = true;
            cboMaSP.Focus();
        }

        private void txtGia_TextChanged(object sender, EventArgs e)
        {
            if (txtDonGia.Text == "") return;

            // Lấy vị trí con trỏ hiện tại
            int selectionStart = txtDonGia.SelectionStart;

            // Loại bỏ dấu phân cách cũ
            string raw = txtDonGia.Text.Replace(",", "").Replace(".", "");

            if (decimal.TryParse(raw, out decimal value))
            {
                txtDonGia.Text = value.ToString("N0");
                // Đặt lại vị trí con trỏ sau khi format
                txtDonGia.SelectionStart = Math.Min(selectionStart, txtDonGia.Text.Length);
            }
        }

        private void LoadSanPham()
        {
            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                using (var cmd = new SQLiteCommand("SELECT Id, MaSanPham, TenSanPham FROM SanPham", conn))
                using (var adapter = new SQLiteDataAdapter(cmd))
                {
                    var dt = new DataTable();
                    adapter.Fill(dt);

                    cboMaSP.DataSource = dt;
                    cboMaSP.DisplayMember = "MaSanPham";  // hiển thị mã
                    cboMaSP.ValueMember = "TenSanPham";   // giá trị = tên
                    cboMaSP.SelectedIndex = -1;
                }

                // Khi chọn mã sp → tự fill tên sp
                cboMaSP.SelectedIndexChanged += (s, e) =>
                {
                    if (cboMaSP.SelectedValue != null)
                        txtTenSP.Text = cboMaSP.SelectedValue.ToString();
                    else
                        txtTenSP.Clear();
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load sản phẩm: " + ex.Message);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            // Lấy giá trị từ form
            MaSP = cboMaSP.Text.Trim();
            TenSP = txtTenSP.Text.Trim();
            SoLuong = int.Parse(txtSoLuong.Text);
            DonGia = decimal.Parse(txtDonGia.Text);

            // Trả kết quả và đóng form
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            // Hủy và đóng form
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(cboMaSP.Text))
            {
                MessageBox.Show("Vui lòng chọn mã sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtSoLuong.Text))
            {
                MessageBox.Show("Xin mời nhập số lượng sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!int.TryParse(txtSoLuong.Text, out int soLuong) || soLuong <= 0)
            {
                MessageBox.Show("Số lượng phải là số nguyên dương!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDonGia.Text))
            {
                MessageBox.Show("Xin mời nhập giá bán!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            string giaText = txtDonGia.Text.Replace(",", "").Replace(".", "");

            if (!int.TryParse(giaText, out int gia) || gia <= 0)
            {
                MessageBox.Show("Giá bán phải là số nguyên dương!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!_hasNhapHang)
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    // Lấy Id của sản phẩm từ bảng SanPham
                    int sanPhamId;
                    string sqlGetId = "SELECT Id FROM SanPham WHERE MaSanPham = @MaSP LIMIT 1";
                    using (var cmd = new SQLiteCommand(sqlGetId, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaSP", cboMaSP.Text);
                        object result = cmd.ExecuteScalar();
                        if (result == null)
                        {
                            MessageBox.Show("Không tìm thấy sản phẩm trong danh mục!", "Thông báo",
                                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                        sanPhamId = Convert.ToInt32(result);
                    }

                    // Lấy số lượng tồn trong bảng TonKho
                    string sqlCheckTonKho = "SELECT SoLuong FROM TonKho WHERE SanPhamId = @SanPhamId LIMIT 1";
                    using (var cmd = new SQLiteCommand(sqlCheckTonKho, conn))
                    {
                        cmd.Parameters.AddWithValue("@SanPhamId", sanPhamId);
                        object result = cmd.ExecuteScalar();

                        if (result == null)
                        {
                            MessageBox.Show("Sản phẩm chưa có trong kho!", "Thông báo",
                                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }

                        int soLuongTon = Convert.ToInt32(result);
                        if (soLuong > soLuongTon)
                        {
                            MessageBox.Show($"Số lượng nhập ({soLuong}) vượt quá tồn kho hiện tại ({soLuongTon})!",
                                            "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                    }
                }
            }

            return true;
        }
    }
}
