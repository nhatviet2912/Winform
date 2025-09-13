namespace BanHang
{
    partial class FrmBanHang
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblMaHoaDon;
        private System.Windows.Forms.TextBox txtMaHoaDon;
        private System.Windows.Forms.Label lblNgayBan;
        private System.Windows.Forms.DateTimePicker dtpNgayBan;
        private System.Windows.Forms.Label lblKhachHangId;
        private System.Windows.Forms.Label lblNhanVienBan;
        private System.Windows.Forms.TextBox txtNhanVienBan;
        private System.Windows.Forms.Label lblTongTien;
        private System.Windows.Forms.TextBox txtTongTien;
        private System.Windows.Forms.DataGridView dgvChiTiet;
        private System.Windows.Forms.Button btnThemSanPham;
        private System.Windows.Forms.Button btnLuuHoaDon;
        private System.Windows.Forms.Button btnHuyHoaDon;
        private System.Windows.Forms.Button btnThoat;

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
            lblMaHoaDon = new Label();
            txtMaHoaDon = new TextBox();
            lblNgayBan = new Label();
            dtpNgayBan = new DateTimePicker();
            lblKhachHangId = new Label();
            lblNhanVienBan = new Label();
            txtNhanVienBan = new TextBox();
            lblTongTien = new Label();
            txtTongTien = new TextBox();
            dgvChiTiet = new DataGridView();
            btnThemSanPham = new Button();
            btnLuuHoaDon = new Button();
            btnHuyHoaDon = new Button();
            btnThoat = new Button();
            comboBox1 = new ComboBox();
            btnPrint = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvChiTiet).BeginInit();
            SuspendLayout();
            // 
            // lblMaHoaDon
            // 
            lblMaHoaDon.AutoSize = true;
            lblMaHoaDon.Location = new Point(20, 20);
            lblMaHoaDon.Name = "lblMaHoaDon";
            lblMaHoaDon.Size = new Size(92, 20);
            lblMaHoaDon.TabIndex = 0;
            lblMaHoaDon.Text = "Mã hóa đơn:";
            // 
            // txtMaHoaDon
            // 
            txtMaHoaDon.Location = new Point(136, 19);
            txtMaHoaDon.Name = "txtMaHoaDon";
            txtMaHoaDon.Size = new Size(200, 27);
            txtMaHoaDon.TabIndex = 1;
            // 
            // lblNgayBan
            // 
            lblNgayBan.AutoSize = true;
            lblNgayBan.Location = new Point(350, 17);
            lblNgayBan.Name = "lblNgayBan";
            lblNgayBan.Size = new Size(76, 20);
            lblNgayBan.TabIndex = 2;
            lblNgayBan.Text = "Ngày bán:";
            // 
            // dtpNgayBan
            // 
            dtpNgayBan.Location = new Point(460, 17);
            dtpNgayBan.Name = "dtpNgayBan";
            dtpNgayBan.Size = new Size(200, 27);
            dtpNgayBan.TabIndex = 3;
            // 
            // lblKhachHangId
            // 
            lblKhachHangId.AutoSize = true;
            lblKhachHangId.Location = new Point(20, 55);
            lblKhachHangId.Name = "lblKhachHangId";
            lblKhachHangId.Size = new Size(112, 20);
            lblKhachHangId.TabIndex = 4;
            lblKhachHangId.Text = "Mã khách hàng:";
            // 
            // lblNhanVienBan
            // 
            lblNhanVienBan.AutoSize = true;
            lblNhanVienBan.Location = new Point(20, 90);
            lblNhanVienBan.Name = "lblNhanVienBan";
            lblNhanVienBan.Size = new Size(107, 20);
            lblNhanVienBan.TabIndex = 6;
            lblNhanVienBan.Text = "Nhân viên bán:";
            // 
            // txtNhanVienBan
            // 
            txtNhanVienBan.Location = new Point(136, 85);
            txtNhanVienBan.Name = "txtNhanVienBan";
            txtNhanVienBan.Size = new Size(200, 27);
            txtNhanVienBan.TabIndex = 7;
            // 
            // lblTongTien
            // 
            lblTongTien.AutoSize = true;
            lblTongTien.Location = new Point(350, 90);
            lblTongTien.Name = "lblTongTien";
            lblTongTien.Size = new Size(75, 20);
            lblTongTien.TabIndex = 8;
            lblTongTien.Text = "Tổng tiền:";
            // 
            // txtTongTien
            // 
            txtTongTien.Location = new Point(460, 87);
            txtTongTien.Name = "txtTongTien";
            txtTongTien.ReadOnly = true;
            txtTongTien.Size = new Size(140, 27);
            txtTongTien.TabIndex = 9;
            txtTongTien.Text = "0";
            // 
            // dgvChiTiet
            // 
            dgvChiTiet.AllowUserToAddRows = false;
            dgvChiTiet.AllowUserToDeleteRows = false;
            dgvChiTiet.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvChiTiet.ColumnHeadersHeight = 29;
            dgvChiTiet.Location = new Point(20, 130);
            dgvChiTiet.Name = "dgvChiTiet";
            dgvChiTiet.RowHeadersVisible = false;
            dgvChiTiet.RowHeadersWidth = 51;
            dgvChiTiet.Size = new Size(640, 200);
            dgvChiTiet.TabIndex = 10;
            // 
            // btnThemSanPham
            // 
            btnThemSanPham.Location = new Point(20, 340);
            btnThemSanPham.Name = "btnThemSanPham";
            btnThemSanPham.Size = new Size(143, 30);
            btnThemSanPham.TabIndex = 11;
            btnThemSanPham.Text = "Thêm sản phẩm";
            btnThemSanPham.UseVisualStyleBackColor = true;
            btnThemSanPham.Click += btnThemSanPham_Click;
            // 
            // btnLuuHoaDon
            // 
            btnLuuHoaDon.Location = new Point(400, 340);
            btnLuuHoaDon.Name = "btnLuuHoaDon";
            btnLuuHoaDon.Size = new Size(80, 30);
            btnLuuHoaDon.TabIndex = 12;
            btnLuuHoaDon.Text = "Lưu";
            btnLuuHoaDon.UseVisualStyleBackColor = true;
            btnLuuHoaDon.Click += btnLuuHoaDon_Click;
            // 
            // btnHuyHoaDon
            // 
            btnHuyHoaDon.Location = new Point(490, 340);
            btnHuyHoaDon.Name = "btnHuyHoaDon";
            btnHuyHoaDon.Size = new Size(80, 30);
            btnHuyHoaDon.TabIndex = 13;
            btnHuyHoaDon.Text = "Xóa";
            btnHuyHoaDon.UseVisualStyleBackColor = true;
            btnHuyHoaDon.Click += btnHuyHoaDon_Click;
            // 
            // btnThoat
            // 
            btnThoat.Location = new Point(580, 340);
            btnThoat.Name = "btnThoat";
            btnThoat.Size = new Size(80, 30);
            btnThoat.TabIndex = 14;
            btnThoat.Text = "Thoát";
            btnThoat.UseVisualStyleBackColor = true;
            btnThoat.Click += btnThoat_Click;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(136, 55);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(200, 28);
            comboBox1.TabIndex = 15;
            // 
            // btnPrint
            // 
            btnPrint.Location = new Point(300, 340);
            btnPrint.Name = "btnPrint";
            btnPrint.Size = new Size(94, 29);
            btnPrint.TabIndex = 16;
            btnPrint.Text = "In Hóa Đơn";
            btnPrint.UseVisualStyleBackColor = true;
            btnPrint.Click += btnPrint_Click;
            // 
            // FrmBanHang
            // 
            ClientSize = new Size(700, 453);
            Controls.Add(btnPrint);
            Controls.Add(comboBox1);
            Controls.Add(lblMaHoaDon);
            Controls.Add(txtMaHoaDon);
            Controls.Add(lblNgayBan);
            Controls.Add(dtpNgayBan);
            Controls.Add(lblKhachHangId);
            Controls.Add(lblNhanVienBan);
            Controls.Add(txtNhanVienBan);
            Controls.Add(lblTongTien);
            Controls.Add(txtTongTien);
            Controls.Add(dgvChiTiet);
            Controls.Add(btnThemSanPham);
            Controls.Add(btnLuuHoaDon);
            Controls.Add(btnHuyHoaDon);
            Controls.Add(btnThoat);
            Name = "FrmBanHang";
            Text = "Quản lý bán hàng";
            Load += FrmBanHang_Load;
            ((System.ComponentModel.ISupportInitialize)dgvChiTiet).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
        private ComboBox comboBox1;
        private Button btnPrint;
    }
}
