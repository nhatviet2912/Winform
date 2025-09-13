namespace BanHang
{
    partial class FrmTonKho
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView dgvTonKho;

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
            lblSearch = new Label();
            txtSearch = new TextBox();
            btnSearch = new Button();
            dgvTonKho = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvTonKho).BeginInit();
            SuspendLayout();
            // 
            // lblSearch
            // 
            lblSearch.AutoSize = true;
            lblSearch.Location = new Point(23, 27);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(73, 20);
            lblSearch.TabIndex = 0;
            lblSearch.Text = "Tìm kiếm:";
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(114, 23);
            txtSearch.Margin = new Padding(3, 4, 3, 4);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(285, 27);
            txtSearch.TabIndex = 1;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(423, 21);
            btnSearch.Margin = new Padding(3, 4, 3, 4);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(103, 33);
            btnSearch.TabIndex = 2;
            btnSearch.Text = "Tìm";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // dgvTonKho
            // 
            dgvTonKho.AllowUserToAddRows = false;
            dgvTonKho.AllowUserToDeleteRows = false;
            dgvTonKho.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTonKho.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTonKho.Location = new Point(23, 80);
            dgvTonKho.Margin = new Padding(3, 4, 3, 4);
            dgvTonKho.Name = "dgvTonKho";
            dgvTonKho.ReadOnly = true;
            dgvTonKho.RowHeadersWidth = 51;
            dgvTonKho.RowTemplate.Height = 25;
            dgvTonKho.Size = new Size(686, 204);
            dgvTonKho.TabIndex = 3;
            // 
            // FrmTonKho
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(743, 507);
            Controls.Add(dgvTonKho);
            Controls.Add(btnSearch);
            Controls.Add(txtSearch);
            Controls.Add(lblSearch);
            Margin = new Padding(3, 4, 3, 4);
            Name = "FrmTonKho";
            Text = "Quản lý kho hàng";
            ((System.ComponentModel.ISupportInitialize)dgvTonKho).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
