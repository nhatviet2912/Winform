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
                // Ẩn form hiện tại
                currentForm.Hide();

                // Tạo form mới và mở dưới dạng hộp thoại modal
                FrmLoaiSanPham frmLoaiSanPham = new FrmLoaiSanPham();
                frmLoaiSanPham.ShowDialog();

                // Đảm bảo form hiện tại sẽ bị đóng sau khi form mới đóng
                if (!currentForm.IsDisposed)
                {
                    currentForm.Close();
                }
            }
        }

        private void sảnPhẩmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form currentForm = this.FindForm();
            if (currentForm != null)
            {
                // Ẩn form hiện tại
                currentForm.Hide();

                // Tạo form mới và mở dưới dạng hộp thoại modal
                FrmSanPham frmSanPham = new FrmSanPham();
                frmSanPham.ShowDialog();

                // Đảm bảo form hiện tại sẽ bị đóng sau khi form mới đóng
                if (!currentForm.IsDisposed)
                {
                    currentForm.Close();
                }
            }
        }

        private void kháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form currentForm = this.FindForm();
            if (currentForm != null)
            {
                // Ẩn form hiện tại
                currentForm.Hide();

                // Tạo form mới và mở dưới dạng hộp thoại modal
                FrmKhachHang frmKhachHang = new FrmKhachHang();
                frmKhachHang.ShowDialog();

                // Đảm bảo form hiện tại sẽ bị đóng sau khi form mới đóng
                if (!currentForm.IsDisposed)
                {
                    currentForm.Close();
                }
            }
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form currentForm = this.FindForm();
            if (currentForm != null)
            {
                // Ẩn form hiện tại
                currentForm.Hide();

                // Tạo form mới và mở dưới dạng hộp thoại modal
                FrmNhapHang frmNhapHang = new FrmNhapHang();
                frmNhapHang.ShowDialog();

                // Đảm bảo form hiện tại sẽ bị đóng sau khi form mới đóng
                if (!currentForm.IsDisposed)
                {
                    currentForm.Close();
                }
            }
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form currentForm = this.FindForm();
            if (currentForm != null)
            {
                // Ẩn form hiện tại
                currentForm.Hide();

                // Tạo form mới và mở dưới dạng hộp thoại modal
                FrmBanHang frmBanHang = new FrmBanHang();
                frmBanHang.ShowDialog();

                // Đảm bảo form hiện tại sẽ bị đóng sau khi form mới đóng
                if (!currentForm.IsDisposed)
                {
                    currentForm.Close();
                }
            }
        }
    }
}
