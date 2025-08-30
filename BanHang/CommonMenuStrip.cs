namespace BanHang
{
    public partial class CommonMenuStrip : UserControl
    {
        public event EventHandler<string> MenuItemClicked;

        public CommonMenuStrip()
        {
            InitializeComponent();
        }

        // Thuộc tính để truy cập MenuStrip từ bên ngoài
        public MenuStrip MainMenu
        {
            get { return menuStrip1; }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form currentForm = this.FindForm();
            if (currentForm != null)
            {
                currentForm.Hide();
                FrmLoaiSanPham frmLoaiSanPham = new FrmLoaiSanPham();
                frmLoaiSanPham.ShowDialog();
                currentForm.Close(); // Đóng form cũ sau khi form mới đóng
            }
        }

        private void sảnPhẩmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form currentForm = this.FindForm();
            if (currentForm != null)
            {
                currentForm.Hide();
                FrmSanPham frmSanPham = new FrmSanPham();
                frmSanPham.ShowDialog();
                currentForm.Close();
            }
        }

        private void kháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form currentForm = this.FindForm();
            if (currentForm != null)
            {
                currentForm.Hide();
                FrmKhachHang frmKhachHang = new FrmKhachHang();
                frmKhachHang.ShowDialog();
                currentForm.Close();
            }
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form currentForm = this.FindForm();
            if (currentForm != null)
            {
                currentForm.Hide();
                FrmNhapHang frmNhapHang = new FrmNhapHang();
                frmNhapHang.ShowDialog();
                currentForm.Close();
            }
        }
    }
}
