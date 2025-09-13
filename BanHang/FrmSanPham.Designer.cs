namespace BanHang
{
    partial class FrmSanPham
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
            dgvSanPham = new DataGridView();
            lblMaSP = new Label();
            txtMaSP = new TextBox();
            lblTenSP = new Label();
            txtTenSP = new TextBox();
            lblMoTa = new Label();
            txtMoTa = new TextBox();
            lblGia = new Label();
            txtGia = new TextBox();
            lblDonViTinh = new Label();
            txtDonViTinh = new TextBox();
            lblLoaiSP = new Label();
            cboLoaiSP = new ComboBox();
            lblThuongHieu = new Label();
            txtThuongHieu = new TextBox();
            lblXuatXu = new Label();
            txtXuatXu = new TextBox();
            btnThem = new Button();
            btnSua = new Button();
            btnXoa = new Button();
            txtSearch = new TextBox();
            label1 = new Label();
            btnSeach = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvSanPham).BeginInit();
            SuspendLayout();
            // 
            // dgvSanPham
            // 
            dgvSanPham.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSanPham.Location = new Point(12, 280);
            dgvSanPham.Name = "dgvSanPham";
            dgvSanPham.RowHeadersWidth = 51;
            dgvSanPham.RowTemplate.Height = 24;
            dgvSanPham.Size = new Size(800, 250);
            dgvSanPham.TabIndex = 0;
            dgvSanPham.CellClick += dgvSanPham_CellClick;
            // 
            // lblMaSP
            // 
            lblMaSP.AutoSize = true;
            lblMaSP.Location = new Point(12, 20);
            lblMaSP.Name = "lblMaSP";
            lblMaSP.Size = new Size(115, 20);
            lblMaSP.TabIndex = 1;
            lblMaSP.Text = "Mã sản phẩm * :";
            // 
            // txtMaSP
            // 
            txtMaSP.Location = new Point(133, 17);
            txtMaSP.Name = "txtMaSP";
            txtMaSP.Size = new Size(200, 27);
            txtMaSP.TabIndex = 2;
            // 
            // lblTenSP
            // 
            lblTenSP.AutoSize = true;
            lblTenSP.Location = new Point(12, 55);
            lblTenSP.Name = "lblTenSP";
            lblTenSP.Size = new Size(117, 20);
            lblTenSP.TabIndex = 3;
            lblTenSP.Text = "Tên sản phẩm * :";
            // 
            // txtTenSP
            // 
            txtTenSP.Location = new Point(133, 52);
            txtTenSP.Name = "txtTenSP";
            txtTenSP.Size = new Size(200, 27);
            txtTenSP.TabIndex = 4;
            // 
            // lblMoTa
            // 
            lblMoTa.AutoSize = true;
            lblMoTa.Location = new Point(12, 90);
            lblMoTa.Name = "lblMoTa";
            lblMoTa.Size = new Size(51, 20);
            lblMoTa.TabIndex = 5;
            lblMoTa.Text = "Mô tả:";
            // 
            // txtMoTa
            // 
            txtMoTa.Location = new Point(133, 87);
            txtMoTa.Multiline = true;
            txtMoTa.Name = "txtMoTa";
            txtMoTa.Size = new Size(200, 50);
            txtMoTa.TabIndex = 6;
            // 
            // lblGia
            // 
            lblGia.AutoSize = true;
            lblGia.Location = new Point(12, 150);
            lblGia.Name = "lblGia";
            lblGia.Size = new Size(77, 20);
            lblGia.TabIndex = 7;
            lblGia.Text = "Giá bán * :";
            // 
            // txtGia
            // 
            txtGia.Location = new Point(133, 147);
            txtGia.Name = "txtGia";
            txtGia.Size = new Size(200, 27);
            txtGia.TabIndex = 8;
            txtGia.TextChanged += txtGia_TextChanged;
            // 
            // lblDonViTinh
            // 
            lblDonViTinh.AutoSize = true;
            lblDonViTinh.Location = new Point(12, 185);
            lblDonViTinh.Name = "lblDonViTinh";
            lblDonViTinh.Size = new Size(84, 20);
            lblDonViTinh.TabIndex = 9;
            lblDonViTinh.Text = "Đơn vị tính:";
            // 
            // txtDonViTinh
            // 
            txtDonViTinh.Location = new Point(133, 184);
            txtDonViTinh.Name = "txtDonViTinh";
            txtDonViTinh.Size = new Size(200, 27);
            txtDonViTinh.TabIndex = 10;
            // 
            // lblLoaiSP
            // 
            lblLoaiSP.AutoSize = true;
            lblLoaiSP.Location = new Point(12, 220);
            lblLoaiSP.Name = "lblLoaiSP";
            lblLoaiSP.Size = new Size(108, 20);
            lblLoaiSP.TabIndex = 11;
            lblLoaiSP.Text = "Loại sản phẩm:";
            // 
            // cboLoaiSP
            // 
            cboLoaiSP.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLoaiSP.FormattingEnabled = true;
            cboLoaiSP.Location = new Point(133, 220);
            cboLoaiSP.Name = "cboLoaiSP";
            cboLoaiSP.Size = new Size(200, 28);
            cboLoaiSP.TabIndex = 12;
            // 
            // lblThuongHieu
            // 
            lblThuongHieu.AutoSize = true;
            lblThuongHieu.Location = new Point(350, 20);
            lblThuongHieu.Name = "lblThuongHieu";
            lblThuongHieu.Size = new Size(95, 20);
            lblThuongHieu.TabIndex = 13;
            lblThuongHieu.Text = "Thương hiệu:";
            // 
            // txtThuongHieu
            // 
            txtThuongHieu.Location = new Point(450, 17);
            txtThuongHieu.Name = "txtThuongHieu";
            txtThuongHieu.Size = new Size(200, 27);
            txtThuongHieu.TabIndex = 14;
            // 
            // lblXuatXu
            // 
            lblXuatXu.AutoSize = true;
            lblXuatXu.Location = new Point(350, 55);
            lblXuatXu.Name = "lblXuatXu";
            lblXuatXu.Size = new Size(62, 20);
            lblXuatXu.TabIndex = 15;
            lblXuatXu.Text = "Xuất xứ:";
            // 
            // txtXuatXu
            // 
            txtXuatXu.Location = new Point(450, 52);
            txtXuatXu.Name = "txtXuatXu";
            txtXuatXu.Size = new Size(200, 27);
            txtXuatXu.TabIndex = 16;
            // 
            // btnThem
            // 
            btnThem.Location = new Point(680, 15);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(100, 30);
            btnThem.TabIndex = 17;
            btnThem.Text = "Thêm";
            btnThem.UseVisualStyleBackColor = true;
            // 
            // btnSua
            // 
            btnSua.Location = new Point(680, 55);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(100, 30);
            btnSua.TabIndex = 18;
            btnSua.Text = "Sửa";
            btnSua.UseVisualStyleBackColor = true;
            // 
            // btnXoa
            // 
            btnXoa.Location = new Point(680, 95);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(100, 30);
            btnXoa.TabIndex = 19;
            btnXoa.Text = "Xóa";
            btnXoa.UseVisualStyleBackColor = true;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(450, 220);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(200, 27);
            txtSearch.TabIndex = 20;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(362, 223);
            label1.Name = "label1";
            label1.Size = new Size(70, 20);
            label1.TabIndex = 21;
            label1.Text = "Tìm kiếm";
            // 
            // btnSeach
            // 
            btnSeach.Location = new Point(680, 220);
            btnSeach.Name = "btnSeach";
            btnSeach.Size = new Size(94, 29);
            btnSeach.TabIndex = 22;
            btnSeach.Text = "Tìm";
            btnSeach.UseVisualStyleBackColor = true;
            btnSeach.Click += btnSeach_Click;
            // 
            // FrmSanPham
            // 
            ClientSize = new Size(830, 603);
            Controls.Add(btnSeach);
            Controls.Add(label1);
            Controls.Add(txtSearch);
            Controls.Add(btnXoa);
            Controls.Add(btnSua);
            Controls.Add(btnThem);
            Controls.Add(txtXuatXu);
            Controls.Add(lblXuatXu);
            Controls.Add(txtThuongHieu);
            Controls.Add(lblThuongHieu);
            Controls.Add(cboLoaiSP);
            Controls.Add(lblLoaiSP);
            Controls.Add(txtDonViTinh);
            Controls.Add(lblDonViTinh);
            Controls.Add(txtGia);
            Controls.Add(lblGia);
            Controls.Add(txtMoTa);
            Controls.Add(lblMoTa);
            Controls.Add(txtTenSP);
            Controls.Add(lblTenSP);
            Controls.Add(txtMaSP);
            Controls.Add(lblMaSP);
            Controls.Add(dgvSanPham);
            Name = "FrmSanPham";
            Text = "Quản lý Sản Phẩm";
            ((System.ComponentModel.ISupportInitialize)dgvSanPham).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private void DgvSanPham_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private System.Windows.Forms.DataGridView dgvSanPham;
        private System.Windows.Forms.Label lblMaSP;
        private System.Windows.Forms.TextBox txtMaSP;
        private System.Windows.Forms.Label lblTenSP;
        private System.Windows.Forms.TextBox txtTenSP;
        private System.Windows.Forms.Label lblMoTa;
        private System.Windows.Forms.TextBox txtMoTa;
        private System.Windows.Forms.Label lblGia;
        private System.Windows.Forms.TextBox txtGia;
        private System.Windows.Forms.Label lblDonViTinh;
        private System.Windows.Forms.TextBox txtDonViTinh;
        private System.Windows.Forms.Label lblLoaiSP;
        private System.Windows.Forms.ComboBox cboLoaiSP;
        private System.Windows.Forms.Label lblThuongHieu;
        private System.Windows.Forms.TextBox txtThuongHieu;
        private System.Windows.Forms.Label lblXuatXu;
        private System.Windows.Forms.TextBox txtXuatXu;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private TextBox txtSearch;
        private Label label1;
        private Button btnSeach;
    }
}
