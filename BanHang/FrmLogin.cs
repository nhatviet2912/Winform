namespace BanHang
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click_1(object sender, EventArgs e)
        {
            string user = txtUsername.Text.Trim();
            string pass = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu!", "Thông báo");
                return;
            }

            // Ví dụ kiểm tra tạm thời (sau này thay bằng DB)
            if (user == "admin" && pass == "123")
            {
                MessageBox.Show("Đăng nhập thành công!", "Thông báo");
                this.Hide();
                FrmLoaiSanPham frmLoaiSanPham = new FrmLoaiSanPham();
                frmLoaiSanPham.ShowDialog();
            }
            else
            {
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!", "Lỗi");
            }
        }
    }
}
