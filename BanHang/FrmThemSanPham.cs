using System;
using System.Windows.Forms;

namespace BanHang
{
    public partial class FrmThemSanPham : Form
    {
        public string MaSP { get; private set; }
        public string TenSP { get; private set; }
        public int SoLuong { get; private set; }
        public decimal DonGia { get; private set; }

        public FrmThemSanPham()
        {
            InitializeComponent();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            // Lấy giá trị từ form
            MaSP = txtMaSP.Text.Trim();
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
    }
}
