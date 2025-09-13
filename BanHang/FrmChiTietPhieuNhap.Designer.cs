namespace BanHang
{
    partial class FrmChiTietPhieuNhap
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
            button1 = new Button();
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
            txtMaPhieu.Location = new Point(167, 17);
            txtMaPhieu.Name = "txtMaPhieu";
            txtMaPhieu.ReadOnly = true;
            txtMaPhieu.Size = new Size(300, 27);
            txtMaPhieu.TabIndex = 1;
            // 
            // lblNgayNhap
            // 
            lblNgayNhap.AutoSize = true;
            lblNgayNhap.Location = new Point(20, 55);
            lblNgayNhap.Name = "lblNgayNhap";
            lblNgayNhap.Size = new Size(84, 20);
            lblNgayNhap.TabIndex = 2;
            lblNgayNhap.Text = "Ngày nhập:";
            // 
            // dtpNgayNhap
            // 
            dtpNgayNhap.Enabled = false;
            dtpNgayNhap.Location = new Point(167, 53);
            dtpNgayNhap.Name = "dtpNgayNhap";
            dtpNgayNhap.Size = new Size(300, 27);
            dtpNgayNhap.TabIndex = 3;
            // 
            // lblNhaCungCap
            // 
            lblNhaCungCap.AutoSize = true;
            lblNhaCungCap.Location = new Point(20, 90);
            lblNhaCungCap.Name = "lblNhaCungCap";
            lblNhaCungCap.Size = new Size(103, 20);
            lblNhaCungCap.TabIndex = 4;
            lblNhaCungCap.Text = "Nhà cung cấp:";
            // 
            // txtNhaCungCap
            // 
            txtNhaCungCap.Location = new Point(167, 86);
            txtNhaCungCap.Name = "txtNhaCungCap";
            txtNhaCungCap.ReadOnly = true;
            txtNhaCungCap.Size = new Size(300, 27);
            txtNhaCungCap.TabIndex = 5;
            // 
            // lblNhanVienNhap
            // 
            lblNhanVienNhap.AutoSize = true;
            lblNhanVienNhap.Location = new Point(20, 125);
            lblNhanVienNhap.Name = "lblNhanVienNhap";
            lblNhanVienNhap.Size = new Size(115, 20);
            lblNhanVienNhap.TabIndex = 6;
            lblNhanVienNhap.Text = "Nhân viên nhập:";
            // 
            // txtNhanVienNhap
            // 
            txtNhanVienNhap.Location = new Point(167, 119);
            txtNhanVienNhap.Name = "txtNhanVienNhap";
            txtNhanVienNhap.ReadOnly = true;
            txtNhanVienNhap.Size = new Size(300, 27);
            txtNhanVienNhap.TabIndex = 7;
            // 
            // lblTongTien
            // 
            lblTongTien.AutoSize = true;
            lblTongTien.Location = new Point(20, 160);
            lblTongTien.Name = "lblTongTien";
            lblTongTien.Size = new Size(75, 20);
            lblTongTien.TabIndex = 8;
            lblTongTien.Text = "Tổng tiền:";
            // 
            // txtTongTien
            // 
            txtTongTien.Location = new Point(167, 152);
            txtTongTien.Name = "txtTongTien";
            txtTongTien.ReadOnly = true;
            txtTongTien.Size = new Size(300, 27);
            txtTongTien.TabIndex = 9;
            // 
            // lblGhiChu
            // 
            lblGhiChu.AutoSize = true;
            lblGhiChu.Location = new Point(20, 195);
            lblGhiChu.Name = "lblGhiChu";
            lblGhiChu.Size = new Size(61, 20);
            lblGhiChu.TabIndex = 10;
            lblGhiChu.Text = "Ghi chú:";
            // 
            // txtGhiChu
            // 
            txtGhiChu.Location = new Point(167, 192);
            txtGhiChu.Multiline = true;
            txtGhiChu.Name = "txtGhiChu";
            txtGhiChu.ReadOnly = true;
            txtGhiChu.Size = new Size(300, 60);
            txtGhiChu.TabIndex = 11;
            // 
            // dgvChiTiet
            // 
            dgvChiTiet.AllowUserToAddRows = false;
            dgvChiTiet.AllowUserToDeleteRows = false;
            dgvChiTiet.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvChiTiet.ColumnHeadersHeight = 29;
            dgvChiTiet.Location = new Point(20, 270);
            dgvChiTiet.Name = "dgvChiTiet";
            dgvChiTiet.ReadOnly = true;
            dgvChiTiet.RowHeadersWidth = 51;
            dgvChiTiet.Size = new Size(700, 250);
            dgvChiTiet.TabIndex = 12;
            // 
            // button1
            // 
            button1.Location = new Point(626, 20);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 13;
            button1.Text = "Thoát";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // FrmChiTietPhieuNhap
            // 
            ClientSize = new Size(760, 550);
            Controls.Add(button1);
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
            Name = "FrmChiTietPhieuNhap";
            Text = "Chi tiết phiếu nhập";
            ((System.ComponentModel.ISupportInitialize)dgvChiTiet).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
        private Button button1;
    }
}
