namespace BanHang
{
    partial class FrmLoaiSanPham
    {
        private System.ComponentModel.IContainer components = null;

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
            dgvLoaiSP = new DataGridView();
            lblTenLoai = new Label();
            txtTenLoai = new TextBox();
            lblMoTa = new Label();
            txtMoTa = new TextBox();
            btnThem = new Button();
            btnSua = new Button();
            btnXoa = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvLoaiSP).BeginInit();
            SuspendLayout();
            // 
            // dgvLoaiSP
            // 
            dgvLoaiSP.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLoaiSP.Location = new Point(12, 150);
            dgvLoaiSP.Name = "dgvLoaiSP";
            dgvLoaiSP.RowHeadersWidth = 51;
            dgvLoaiSP.RowTemplate.Height = 24;
            dgvLoaiSP.Size = new Size(800, 250);
            dgvLoaiSP.TabIndex = 0;
            // 
            // lblTenLoai
            // 
            lblTenLoai.AutoSize = true;
            lblTenLoai.Location = new Point(12, 20);
            lblTenLoai.Name = "lblTenLoai";
            lblTenLoai.Size = new Size(67, 20);
            lblTenLoai.TabIndex = 1;
            lblTenLoai.Text = "Tên Loại:";
            // 
            // txtTenLoai
            // 
            txtTenLoai.Location = new Point(100, 17);
            txtTenLoai.Name = "txtTenLoai";
            txtTenLoai.Size = new Size(250, 27);
            txtTenLoai.TabIndex = 2;
            // 
            // lblMoTa
            // 
            lblMoTa.AutoSize = true;
            lblMoTa.Location = new Point(12, 60);
            lblMoTa.Name = "lblMoTa";
            lblMoTa.Size = new Size(51, 20);
            lblMoTa.TabIndex = 3;
            lblMoTa.Text = "Mô tả:";
            // 
            // txtMoTa
            // 
            txtMoTa.Location = new Point(100, 57);
            txtMoTa.Multiline = true;
            txtMoTa.Name = "txtMoTa";
            txtMoTa.Size = new Size(250, 60);
            txtMoTa.TabIndex = 4;
            // 
            // btnThem
            // 
            btnThem.Location = new Point(732, 20);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(80, 30);
            btnThem.TabIndex = 5;
            btnThem.Text = "Thêm";
            btnThem.UseVisualStyleBackColor = true;
            // 
            // btnSua
            // 
            btnSua.Location = new Point(732, 60);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(80, 30);
            btnSua.TabIndex = 6;
            btnSua.Text = "Sửa";
            btnSua.UseVisualStyleBackColor = true;
            // 
            // btnXoa
            // 
            btnXoa.Location = new Point(732, 97);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(80, 30);
            btnXoa.TabIndex = 7;
            btnXoa.Text = "Xóa";
            btnXoa.UseVisualStyleBackColor = true;
            // 
            // FrmLoaiSanPham
            // 
            ClientSize = new Size(830, 550);
            Controls.Add(btnXoa);
            Controls.Add(btnSua);
            Controls.Add(btnThem);
            Controls.Add(txtMoTa);
            Controls.Add(lblMoTa);
            Controls.Add(txtTenLoai);
            Controls.Add(lblTenLoai);
            Controls.Add(dgvLoaiSP);
            Name = "FrmLoaiSanPham";
            Text = "Quản lý Loại Sản Phẩm";
            ((System.ComponentModel.ISupportInitialize)dgvLoaiSP).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.DataGridView dgvLoaiSP;
        private System.Windows.Forms.Label lblTenLoai;
        private System.Windows.Forms.TextBox txtTenLoai;
        private System.Windows.Forms.Label lblMoTa;
        private System.Windows.Forms.TextBox txtMoTa;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
    }
}
