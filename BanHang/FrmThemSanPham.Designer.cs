namespace BanHang
{
    partial class FrmThemSanPham
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblMaSP;
        private System.Windows.Forms.Label lblTenSP;
        private System.Windows.Forms.TextBox txtTenSP;
        private System.Windows.Forms.Label lblSoLuong;
        private System.Windows.Forms.TextBox txtSoLuong;
        private System.Windows.Forms.Label lblDonGia;
        private System.Windows.Forms.TextBox txtDonGia;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Button btnHuy;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblMaSP = new Label();
            lblTenSP = new Label();
            txtTenSP = new TextBox();
            lblSoLuong = new Label();
            txtSoLuong = new TextBox();
            lblDonGia = new Label();
            txtDonGia = new TextBox();
            btnLuu = new Button();
            btnHuy = new Button();
            cboMaSP = new ComboBox();
            SuspendLayout();
            // 
            // lblMaSP
            // 
            lblMaSP.AutoSize = true;
            lblMaSP.Location = new Point(20, 20);
            lblMaSP.Name = "lblMaSP";
            lblMaSP.Size = new Size(53, 20);
            lblMaSP.TabIndex = 0;
            lblMaSP.Text = "Mã SP:";
            // 
            // lblTenSP
            // 
            lblTenSP.AutoSize = true;
            lblTenSP.Location = new Point(20, 60);
            lblTenSP.Name = "lblTenSP";
            lblTenSP.Size = new Size(55, 20);
            lblTenSP.TabIndex = 2;
            lblTenSP.Text = "Tên SP:";
            // 
            // txtTenSP
            // 
            txtTenSP.Location = new Point(100, 60);
            txtTenSP.Name = "txtTenSP";
            txtTenSP.Size = new Size(200, 27);
            txtTenSP.TabIndex = 3;
            // 
            // lblSoLuong
            // 
            lblSoLuong.AutoSize = true;
            lblSoLuong.Location = new Point(20, 100);
            lblSoLuong.Name = "lblSoLuong";
            lblSoLuong.Size = new Size(72, 20);
            lblSoLuong.TabIndex = 4;
            lblSoLuong.Text = "Số lượng:";
            // 
            // txtSoLuong
            // 
            txtSoLuong.Location = new Point(100, 100);
            txtSoLuong.Name = "txtSoLuong";
            txtSoLuong.Size = new Size(200, 27);
            txtSoLuong.TabIndex = 5;
            // 
            // lblDonGia
            // 
            lblDonGia.AutoSize = true;
            lblDonGia.Location = new Point(20, 140);
            lblDonGia.Name = "lblDonGia";
            lblDonGia.Size = new Size(65, 20);
            lblDonGia.TabIndex = 6;
            lblDonGia.Text = "Đơn giá:";
            // 
            // txtDonGia
            // 
            txtDonGia.Location = new Point(100, 140);
            txtDonGia.Name = "txtDonGia";
            txtDonGia.Size = new Size(200, 27);
            txtDonGia.TabIndex = 7;
            txtDonGia.TextChanged += txtGia_TextChanged;
            // 
            // btnLuu
            // 
            btnLuu.Location = new Point(100, 180);
            btnLuu.Name = "btnLuu";
            btnLuu.Size = new Size(75, 38);
            btnLuu.TabIndex = 8;
            btnLuu.Text = "Lưu";
            btnLuu.Click += btnLuu_Click;
            // 
            // btnHuy
            // 
            btnHuy.Location = new Point(200, 180);
            btnHuy.Name = "btnHuy";
            btnHuy.Size = new Size(75, 38);
            btnHuy.TabIndex = 9;
            btnHuy.Text = "Hủy";
            btnHuy.Click += btnHuy_Click;
            // 
            // cboMaSP
            // 
            cboMaSP.FormattingEnabled = true;
            cboMaSP.Location = new Point(100, 20);
            cboMaSP.Name = "cboMaSP";
            cboMaSP.Size = new Size(200, 28);
            cboMaSP.TabIndex = 10;
            // 
            // FrmThemSanPham
            // 
            ClientSize = new Size(350, 230);
            Controls.Add(cboMaSP);
            Controls.Add(lblMaSP);
            Controls.Add(lblTenSP);
            Controls.Add(txtTenSP);
            Controls.Add(lblSoLuong);
            Controls.Add(txtSoLuong);
            Controls.Add(lblDonGia);
            Controls.Add(txtDonGia);
            Controls.Add(btnLuu);
            Controls.Add(btnHuy);
            Name = "FrmThemSanPham";
            Text = "Thêm sản phẩm";
            ResumeLayout(false);
            PerformLayout();
        }
        private ComboBox cboMaSP;
    }
}