namespace BanHang
{
    partial class FrmNhapHang
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblMaPhieu;
        private System.Windows.Forms.TextBox txtMaPhieu;
        private System.Windows.Forms.Label lblNgayNhap;
        private System.Windows.Forms.DateTimePicker dtpNgayNhap;
        private System.Windows.Forms.Label lblNhaCungCap;
        private System.Windows.Forms.TextBox txtNhaCungCap;
        private System.Windows.Forms.Label lblNhanVienNhap;
        private System.Windows.Forms.TextBox txtNhanVienNhap;
        private System.Windows.Forms.Label lblTongTien;
        private System.Windows.Forms.TextBox txtTongTien;
        private System.Windows.Forms.Label lblGhiChu;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.DataGridView dgvChiTiet;
        private System.Windows.Forms.Button btnThemSanPham;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Button btnXoa;
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
            lblMaPhieu = new Label();
            txtMaPhieu = new TextBox();
            lblNgayNhap = new Label();
            dtpNgayNhap = new DateTimePicker();
            lblNhaCungCap = new Label();
            txtNhaCungCap = new TextBox();
            lblNhanVienNhap = new Label();
            txtNhanVienNhap = new TextBox();
            lblTongTien = new Label();
            txtTongTien = new TextBox();
            lblGhiChu = new Label();
            txtGhiChu = new TextBox();
            dgvChiTiet = new DataGridView();
            btnThemSanPham = new Button();
            btnLuu = new Button();
            btnXoa = new Button();
            btnThoat = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvChiTiet).BeginInit();
            SuspendLayout();
            // 
            // lblMaPhieu
            // 
            lblMaPhieu.AutoSize = true;
            lblMaPhieu.Location = new Point(20, 20);
            lblMaPhieu.Name = "lblMaPhieu";
            lblMaPhieu.Size = new Size(74, 20);
            lblMaPhieu.TabIndex = 0;
            lblMaPhieu.Text = "Mã phiếu:";
            // 
            // txtMaPhieu
            // 
            txtMaPhieu.Location = new Point(142, 16);
            txtMaPhieu.Name = "txtMaPhieu";
            txtMaPhieu.Size = new Size(202, 27);
            txtMaPhieu.TabIndex = 1;
            // 
            // lblNgayNhap
            // 
            lblNgayNhap.AutoSize = true;
            lblNgayNhap.Location = new Point(350, 19);
            lblNgayNhap.Name = "lblNgayNhap";
            lblNgayNhap.Size = new Size(84, 20);
            lblNgayNhap.TabIndex = 2;
            lblNgayNhap.Text = "Ngày nhập:";
            // 
            // dtpNgayNhap
            // 
            dtpNgayNhap.Location = new Point(460, 19);
            dtpNgayNhap.Name = "dtpNgayNhap";
            dtpNgayNhap.Size = new Size(200, 27);
            dtpNgayNhap.TabIndex = 3;
            // 
            // lblNhaCungCap
            // 
            lblNhaCungCap.AutoSize = true;
            lblNhaCungCap.Location = new Point(20, 55);
            lblNhaCungCap.Name = "lblNhaCungCap";
            lblNhaCungCap.Size = new Size(103, 20);
            lblNhaCungCap.TabIndex = 4;
            lblNhaCungCap.Text = "Nhà cung cấp:";
            // 
            // txtNhaCungCap
            // 
            txtNhaCungCap.Location = new Point(142, 49);
            txtNhaCungCap.Name = "txtNhaCungCap";
            txtNhaCungCap.Size = new Size(518, 27);
            txtNhaCungCap.TabIndex = 5;
            // 
            // lblNhanVienNhap
            // 
            lblNhanVienNhap.AutoSize = true;
            lblNhanVienNhap.Location = new Point(20, 90);
            lblNhanVienNhap.Name = "lblNhanVienNhap";
            lblNhanVienNhap.Size = new Size(115, 20);
            lblNhanVienNhap.TabIndex = 6;
            lblNhanVienNhap.Text = "Nhân viên nhập:";
            // 
            // txtNhanVienNhap
            // 
            txtNhanVienNhap.Location = new Point(144, 87);
            txtNhanVienNhap.Name = "txtNhanVienNhap";
            txtNhanVienNhap.Size = new Size(200, 27);
            txtNhanVienNhap.TabIndex = 7;
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
            txtTongTien.Size = new Size(200, 27);
            txtTongTien.TabIndex = 9;
            txtTongTien.Text = "0";
            // 
            // lblGhiChu
            // 
            lblGhiChu.AutoSize = true;
            lblGhiChu.Location = new Point(20, 125);
            lblGhiChu.Name = "lblGhiChu";
            lblGhiChu.Size = new Size(61, 20);
            lblGhiChu.TabIndex = 10;
            lblGhiChu.Text = "Ghi chú:";
            // 
            // txtGhiChu
            // 
            txtGhiChu.Location = new Point(142, 122);
            txtGhiChu.Multiline = true;
            txtGhiChu.Name = "txtGhiChu";
            txtGhiChu.Size = new Size(518, 40);
            txtGhiChu.TabIndex = 11;
            // 
            // dgvChiTiet
            // 
            dgvChiTiet.AllowUserToAddRows = false;
            dgvChiTiet.AllowUserToDeleteRows = false;
            dgvChiTiet.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvChiTiet.ColumnHeadersHeight = 29;
            dgvChiTiet.Location = new Point(20, 180);
            dgvChiTiet.Name = "dgvChiTiet";
            dgvChiTiet.RowHeadersVisible = false;
            dgvChiTiet.RowHeadersWidth = 51;
            dgvChiTiet.Size = new Size(640, 220);
            dgvChiTiet.TabIndex = 12;
            // 
            // btnThemSanPham
            // 
            btnThemSanPham.Location = new Point(20, 420);
            btnThemSanPham.Name = "btnThemSanPham";
            btnThemSanPham.Size = new Size(120, 30);
            btnThemSanPham.TabIndex = 13;
            btnThemSanPham.Text = "Thêm sản phẩm";
            btnThemSanPham.UseVisualStyleBackColor = true;
            btnThemSanPham.Click += btnThemSanPham_Click;
            // 
            // btnLuu
            // 
            btnLuu.Location = new Point(400, 420);
            btnLuu.Name = "btnLuu";
            btnLuu.Size = new Size(80, 30);
            btnLuu.TabIndex = 14;
            btnLuu.Text = "Lưu";
            btnLuu.UseVisualStyleBackColor = true;
            btnLuu.Click += btnLuu_Click;
            // 
            // btnXoa
            // 
            btnXoa.Location = new Point(490, 420);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(80, 30);
            btnXoa.TabIndex = 15;
            btnXoa.Text = "Xóa";
            btnXoa.UseVisualStyleBackColor = true;
            btnXoa.Click += btnXoa_Click;
            // 
            // btnThoat
            // 
            btnThoat.Location = new Point(580, 420);
            btnThoat.Name = "btnThoat";
            btnThoat.Size = new Size(80, 30);
            btnThoat.TabIndex = 16;
            btnThoat.Text = "Thoát";
            btnThoat.UseVisualStyleBackColor = true;
            btnThoat.Click += btnThoat_Click;
            // 
            // FrmNhapHang
            // 
            ClientSize = new Size(700, 553);
            Controls.Add(lblMaPhieu);
            Controls.Add(txtMaPhieu);
            Controls.Add(lblNgayNhap);
            Controls.Add(dtpNgayNhap);
            Controls.Add(lblNhaCungCap);
            Controls.Add(txtNhaCungCap);
            Controls.Add(lblNhanVienNhap);
            Controls.Add(txtNhanVienNhap);
            Controls.Add(lblTongTien);
            Controls.Add(txtTongTien);
            Controls.Add(lblGhiChu);
            Controls.Add(txtGhiChu);
            Controls.Add(dgvChiTiet);
            Controls.Add(btnThemSanPham);
            Controls.Add(btnLuu);
            Controls.Add(btnXoa);
            Controls.Add(btnThoat);
            Name = "FrmNhapHang";
            Text = "Quản lý nhập hàng";
            Load += FrmNhapHang_Load;
            ((System.ComponentModel.ISupportInitialize)dgvChiTiet).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}