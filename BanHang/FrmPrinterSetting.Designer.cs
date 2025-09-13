using System.Windows.Forms;

namespace BanHang
{
    partial class FrmPrinterSetting
    {
        private System.ComponentModel.IContainer components = null;
        private ComboBox cboPrinters;
        private Label lblPrinter;
        private Button btnSave;
        private Button btnCancel;

        private Label lblMarginTop;
        private Label lblMarginBottom;
        private Label lblMarginLeft;
        private Label lblMarginRight;
        private NumericUpDown numMarginTop;
        private NumericUpDown numMarginBottom;
        private NumericUpDown numMarginLeft;
        private NumericUpDown numMarginRight;

        private Label lblPaperSize;
        private ComboBox cboPaperSize;

        private Label lblOrientation;
        private ComboBox cboOrientation;

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
            cboPrinters = new ComboBox();
            lblPrinter = new Label();
            btnSave = new Button();
            btnCancel = new Button();
            lblMarginTop = new Label();
            lblMarginBottom = new Label();
            lblMarginLeft = new Label();
            lblMarginRight = new Label();
            numMarginTop = new NumericUpDown();
            numMarginBottom = new NumericUpDown();
            numMarginLeft = new NumericUpDown();
            numMarginRight = new NumericUpDown();
            lblPaperSize = new Label();
            cboPaperSize = new ComboBox();
            lblOrientation = new Label();
            cboOrientation = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)numMarginTop).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numMarginBottom).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numMarginLeft).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numMarginRight).BeginInit();
            SuspendLayout();
            // 
            // cboPrinters
            // 
            cboPrinters.Location = new Point(150, 20);
            cboPrinters.Name = "cboPrinters";
            cboPrinters.Size = new Size(200, 28);
            cboPrinters.TabIndex = 1;
            // 
            // lblPrinter
            // 
            lblPrinter.Location = new Point(20, 20);
            lblPrinter.Name = "lblPrinter";
            lblPrinter.Size = new Size(100, 23);
            lblPrinter.TabIndex = 0;
            lblPrinter.Text = "Chọn máy in:";
            // 
            // btnSave
            // 
            btnSave.Location = new Point(150, 320);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 30);
            btnSave.TabIndex = 14;
            btnSave.Text = "Lưu";
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(250, 320);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 30);
            btnCancel.TabIndex = 15;
            btnCancel.Text = "Hủy";
            btnCancel.Click += btnCancel_Click;
            // 
            // lblMarginTop
            // 
            lblMarginTop.Location = new Point(20, 140);
            lblMarginTop.Name = "lblMarginTop";
            lblMarginTop.Size = new Size(100, 23);
            lblMarginTop.TabIndex = 6;
            lblMarginTop.Text = "Lề trên (mm):";
            // 
            // lblMarginBottom
            // 
            lblMarginBottom.Location = new Point(20, 180);
            lblMarginBottom.Name = "lblMarginBottom";
            lblMarginBottom.Size = new Size(100, 23);
            lblMarginBottom.TabIndex = 8;
            lblMarginBottom.Text = "Lề dưới (mm):";
            // 
            // lblMarginLeft
            // 
            lblMarginLeft.Location = new Point(20, 220);
            lblMarginLeft.Name = "lblMarginLeft";
            lblMarginLeft.Size = new Size(100, 23);
            lblMarginLeft.TabIndex = 10;
            lblMarginLeft.Text = "Lề trái (mm):";
            // 
            // lblMarginRight
            // 
            lblMarginRight.Location = new Point(20, 260);
            lblMarginRight.Name = "lblMarginRight";
            lblMarginRight.Size = new Size(100, 23);
            lblMarginRight.TabIndex = 12;
            lblMarginRight.Text = "Lề phải (mm):";
            // 
            // numMarginTop
            // 
            numMarginTop.Location = new Point(150, 140);
            numMarginTop.Name = "numMarginTop";
            numMarginTop.Size = new Size(120, 27);
            numMarginTop.TabIndex = 7;
            // 
            // numMarginBottom
            // 
            numMarginBottom.Location = new Point(150, 180);
            numMarginBottom.Name = "numMarginBottom";
            numMarginBottom.Size = new Size(120, 27);
            numMarginBottom.TabIndex = 9;
            // 
            // numMarginLeft
            // 
            numMarginLeft.Location = new Point(150, 220);
            numMarginLeft.Name = "numMarginLeft";
            numMarginLeft.Size = new Size(120, 27);
            numMarginLeft.TabIndex = 11;
            // 
            // numMarginRight
            // 
            numMarginRight.Location = new Point(150, 260);
            numMarginRight.Name = "numMarginRight";
            numMarginRight.Size = new Size(120, 27);
            numMarginRight.TabIndex = 13;
            // 
            // lblPaperSize
            // 
            lblPaperSize.Location = new Point(20, 60);
            lblPaperSize.Name = "lblPaperSize";
            lblPaperSize.Size = new Size(100, 23);
            lblPaperSize.TabIndex = 2;
            lblPaperSize.Text = "Khổ giấy:";
            // 
            // cboPaperSize
            // 
            cboPaperSize.Items.AddRange(new object[] { "A4", "A5", "80mm", "58mm" });
            cboPaperSize.Location = new Point(150, 60);
            cboPaperSize.Name = "cboPaperSize";
            cboPaperSize.Size = new Size(200, 28);
            cboPaperSize.TabIndex = 3;
            // 
            // lblOrientation
            // 
            lblOrientation.Location = new Point(20, 100);
            lblOrientation.Name = "lblOrientation";
            lblOrientation.Size = new Size(100, 23);
            lblOrientation.TabIndex = 4;
            lblOrientation.Text = "Hướng in:";
            // 
            // cboOrientation
            // 
            cboOrientation.Items.AddRange(new object[] { "Portrait", "Landscape" });
            cboOrientation.Location = new Point(150, 100);
            cboOrientation.Name = "cboOrientation";
            cboOrientation.Size = new Size(200, 28);
            cboOrientation.TabIndex = 5;
            // 
            // FrmPrinterSetting
            // 
            ClientSize = new Size(400, 400);
            Controls.Add(lblPrinter);
            Controls.Add(cboPrinters);
            Controls.Add(lblPaperSize);
            Controls.Add(cboPaperSize);
            Controls.Add(lblOrientation);
            Controls.Add(cboOrientation);
            Controls.Add(lblMarginTop);
            Controls.Add(numMarginTop);
            Controls.Add(lblMarginBottom);
            Controls.Add(numMarginBottom);
            Controls.Add(lblMarginLeft);
            Controls.Add(numMarginLeft);
            Controls.Add(lblMarginRight);
            Controls.Add(numMarginRight);
            Controls.Add(btnSave);
            Controls.Add(btnCancel);
            Name = "FrmPrinterSetting";
            Text = "Printer Settings";
            ((System.ComponentModel.ISupportInitialize)numMarginTop).EndInit();
            ((System.ComponentModel.ISupportInitialize)numMarginBottom).EndInit();
            ((System.ComponentModel.ISupportInitialize)numMarginLeft).EndInit();
            ((System.ComponentModel.ISupportInitialize)numMarginRight).EndInit();
            ResumeLayout(false);
        }
    }
}
