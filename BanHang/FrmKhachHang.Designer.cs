namespace BanHang
{
    partial class FrmKhachHang
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblMaCode;
        private System.Windows.Forms.Label lblTenKH;
        private System.Windows.Forms.Label lblDiaChi;
        private System.Windows.Forms.Label lblDienThoai;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblGioiTinh;
        private System.Windows.Forms.TextBox txtMaCode;
        private System.Windows.Forms.TextBox txtTenKH;
        private System.Windows.Forms.TextBox txtDiaChi;
        private System.Windows.Forms.TextBox txtDienThoai;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.ComboBox cbGioiTinh;
        private System.Windows.Forms.DataGridView dgvKhachHang;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;

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
            lblMaCode = new Label();
            lblTenKH = new Label();
            lblDiaChi = new Label();
            lblDienThoai = new Label();
            lblEmail = new Label();
            lblGioiTinh = new Label();
            txtMaCode = new TextBox();
            txtTenKH = new TextBox();
            txtDiaChi = new TextBox();
            txtDienThoai = new TextBox();
            txtEmail = new TextBox();
            cbGioiTinh = new ComboBox();
            dgvKhachHang = new DataGridView();
            btnThem = new Button();
            btnSua = new Button();
            btnXoa = new Button();
            label1 = new Label();
            txtSearch = new TextBox();
            btnSearch = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvKhachHang).BeginInit();
            SuspendLayout();
            // 
            // lblMaCode
            // 
            lblMaCode.Location = new Point(20, 30);
            lblMaCode.Name = "lblMaCode";
            lblMaCode.Size = new Size(134, 23);
            lblMaCode.TabIndex = 0;
            lblMaCode.Text = "Mã Khách Hàng:";
            // 
            // lblTenKH
            // 
            lblTenKH.Location = new Point(20, 65);
            lblTenKH.Name = "lblTenKH";
            lblTenKH.Size = new Size(134, 23);
            lblTenKH.TabIndex = 2;
            lblTenKH.Text = "Tên Khách Hàng:";
            // 
            // lblDiaChi
            // 
            lblDiaChi.Location = new Point(20, 100);
            lblDiaChi.Name = "lblDiaChi";
            lblDiaChi.Size = new Size(100, 23);
            lblDiaChi.TabIndex = 4;
            lblDiaChi.Text = "Địa chỉ:";
            // 
            // lblDienThoai
            // 
            lblDienThoai.Location = new Point(20, 135);
            lblDienThoai.Name = "lblDienThoai";
            lblDienThoai.Size = new Size(100, 23);
            lblDienThoai.TabIndex = 6;
            lblDienThoai.Text = "Điện thoại:";
            // 
            // lblEmail
            // 
            lblEmail.Location = new Point(20, 170);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(100, 23);
            lblEmail.TabIndex = 8;
            lblEmail.Text = "Email:";
            // 
            // lblGioiTinh
            // 
            lblGioiTinh.Location = new Point(20, 205);
            lblGioiTinh.Name = "lblGioiTinh";
            lblGioiTinh.Size = new Size(100, 23);
            lblGioiTinh.TabIndex = 10;
            lblGioiTinh.Text = "Giới tính:";
            // 
            // txtMaCode
            // 
            txtMaCode.Location = new Point(186, 26);
            txtMaCode.Name = "txtMaCode";
            txtMaCode.Size = new Size(287, 27);
            txtMaCode.TabIndex = 1;
            // 
            // txtTenKH
            // 
            txtTenKH.Location = new Point(186, 59);
            txtTenKH.Name = "txtTenKH";
            txtTenKH.Size = new Size(287, 27);
            txtTenKH.TabIndex = 3;
            // 
            // txtDiaChi
            // 
            txtDiaChi.Location = new Point(186, 92);
            txtDiaChi.Name = "txtDiaChi";
            txtDiaChi.Size = new Size(287, 27);
            txtDiaChi.TabIndex = 5;
            // 
            // txtDienThoai
            // 
            txtDienThoai.Location = new Point(186, 125);
            txtDienThoai.Name = "txtDienThoai";
            txtDienThoai.Size = new Size(287, 27);
            txtDienThoai.TabIndex = 7;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(186, 166);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(287, 27);
            txtEmail.TabIndex = 9;
            // 
            // cbGioiTinh
            // 
            cbGioiTinh.Items.AddRange(new object[] { "Nam", "Nữ", "Khác" });
            cbGioiTinh.Location = new Point(186, 202);
            cbGioiTinh.Name = "cbGioiTinh";
            cbGioiTinh.Size = new Size(287, 28);
            cbGioiTinh.TabIndex = 11;
            // 
            // dgvKhachHang
            // 
            dgvKhachHang.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvKhachHang.Location = new Point(20, 282);
            dgvKhachHang.MultiSelect = false;
            dgvKhachHang.Name = "dgvKhachHang";
            dgvKhachHang.RowHeadersWidth = 51;
            dgvKhachHang.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvKhachHang.Size = new Size(600, 200);
            dgvKhachHang.TabIndex = 16;
            dgvKhachHang.CellClick += dgvKhachHang_CellClick;
            // 
            // btnThem
            // 
            btnThem.Location = new Point(545, 30);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(75, 28);
            btnThem.TabIndex = 12;
            btnThem.Text = "Thêm";
            // 
            // btnSua
            // 
            btnSua.Location = new Point(545, 65);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(75, 28);
            btnSua.TabIndex = 13;
            btnSua.Text = "Sửa";
            // 
            // btnXoa
            // 
            btnXoa.Location = new Point(545, 100);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(75, 28);
            btnXoa.TabIndex = 14;
            btnXoa.Text = "Xóa";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(20, 243);
            label1.Name = "label1";
            label1.Size = new Size(70, 20);
            label1.TabIndex = 17;
            label1.Text = "Tìm kiếm";
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(186, 240);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(287, 27);
            txtSearch.TabIndex = 18;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(545, 240);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(75, 29);
            btnSearch.TabIndex = 19;
            btnSearch.Text = "Tìm";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // FrmKhachHang
            // 
            ClientSize = new Size(650, 553);
            Controls.Add(btnSearch);
            Controls.Add(txtSearch);
            Controls.Add(label1);
            Controls.Add(lblMaCode);
            Controls.Add(txtMaCode);
            Controls.Add(lblTenKH);
            Controls.Add(txtTenKH);
            Controls.Add(lblDiaChi);
            Controls.Add(txtDiaChi);
            Controls.Add(lblDienThoai);
            Controls.Add(txtDienThoai);
            Controls.Add(lblEmail);
            Controls.Add(txtEmail);
            Controls.Add(lblGioiTinh);
            Controls.Add(cbGioiTinh);
            Controls.Add(btnThem);
            Controls.Add(btnSua);
            Controls.Add(btnXoa);
            Controls.Add(dgvKhachHang);
            Name = "FrmKhachHang";
            Text = "Quản lý Khách Hàng";
            ((System.ComponentModel.ISupportInitialize)dgvKhachHang).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
        private Label label1;
        private TextBox txtSearch;
        private Button btnSearch;
    }
}
