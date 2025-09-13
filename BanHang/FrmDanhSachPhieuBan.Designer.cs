namespace BanHang
{
    partial class FrmDanhSachPhieuBan
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvPhieuNhap;
        private System.Windows.Forms.Button btnChiTiet;

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
            dgvPhieuNhap = new DataGridView();
            btnChiTiet = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvPhieuNhap).BeginInit();
            SuspendLayout();
            // 
            // dgvPhieuNhap
            // 
            dgvPhieuNhap.AllowUserToAddRows = false;
            dgvPhieuNhap.AllowUserToDeleteRows = false;
            dgvPhieuNhap.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvPhieuNhap.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPhieuNhap.Location = new Point(20, 60);
            dgvPhieuNhap.MultiSelect = false;
            dgvPhieuNhap.Name = "dgvPhieuNhap";
            dgvPhieuNhap.ReadOnly = true;
            dgvPhieuNhap.RowHeadersVisible = false;
            dgvPhieuNhap.RowHeadersWidth = 51;
            dgvPhieuNhap.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPhieuNhap.Size = new Size(740, 308);
            dgvPhieuNhap.TabIndex = 0;
            // 
            // btnChiTiet
            // 
            btnChiTiet.Location = new Point(20, 358);
            btnChiTiet.Name = "btnChiTiet";
            btnChiTiet.Size = new Size(100, 30);
            btnChiTiet.TabIndex = 4;
            btnChiTiet.Text = "Xem chi tiết";
            btnChiTiet.UseVisualStyleBackColor = true;
            btnChiTiet.Click += btnChiTiet_Click;
            // 
            // FrmDanhSachPhieuBan
            // 
            ClientSize = new Size(784, 461);
            Controls.Add(btnChiTiet);
            Controls.Add(dgvPhieuNhap);
            Name = "FrmDanhSachPhieuBan";
            Text = "Danh sách bán hàng";
            Load += FrmDanhSachPhieuNhap_Load;
            ((System.ComponentModel.ISupportInitialize)dgvPhieuNhap).EndInit();
            ResumeLayout(false);
        }
    }
}
