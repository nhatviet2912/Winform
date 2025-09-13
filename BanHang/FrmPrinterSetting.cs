using System;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace BanHang
{
    public partial class FrmPrinterSetting : Form
    {
        public FrmPrinterSetting()
        {
            InitializeComponent(); // cái này gọi đến Designer
            this.Load += new System.EventHandler(this.FrmPrinterSetting_Load);
        }

        private void FrmPrinterSetting_Load(object sender, EventArgs e)
        {
            // Load lại setting đã lưu
            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                cboPrinters.Items.Add(printer);
            }

            // Load máy in đã lưu trong Settings
            string savedPrinter = Properties.Settings.Default.PrinterName;
            if (!string.IsNullOrEmpty(savedPrinter) && cboPrinters.Items.Contains(savedPrinter))
            {
                cboPrinters.SelectedItem = savedPrinter;
            }
            else if (cboPrinters.Items.Count > 0)
            {
                cboPrinters.SelectedIndex = 0; // chọn máy in đầu tiên nếu chưa có
            }

            cboPaperSize.SelectedItem = Properties.Settings.Default.PaperSize;
            cboOrientation.SelectedItem = Properties.Settings.Default.Orientation;

            numMarginTop.Value = Properties.Settings.Default.MarginTop;
            numMarginBottom.Value = Properties.Settings.Default.MarginBottom;
            numMarginLeft.Value = Properties.Settings.Default.MarginLeft;
            numMarginRight.Value = Properties.Settings.Default.MarginRight;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Lưu thông tin máy in
                Properties.Settings.Default.PrinterName = cboPrinters.SelectedItem?.ToString() ?? "";

                // Lưu khổ giấy
                Properties.Settings.Default.PaperSize = cboPaperSize.SelectedItem?.ToString() ?? "";

                // Lưu hướng in
                Properties.Settings.Default.Orientation = cboOrientation.SelectedItem?.ToString() ?? "Portrait";

                // Lưu margin
                Properties.Settings.Default.MarginTop = (int)numMarginTop.Value;
                Properties.Settings.Default.MarginBottom = (int)numMarginBottom.Value;
                Properties.Settings.Default.MarginLeft = (int)numMarginLeft.Value;
                Properties.Settings.Default.MarginRight = (int)numMarginRight.Value;

                // Ghi lại setting
                Properties.Settings.Default.Save();

                MessageBox.Show("Đã lưu cài đặt máy in!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                Form currentForm = this.FindForm();
                if (currentForm != null)
                {
                    // Ẩn form hiện tại
                    currentForm.Hide();

                    // Tạo form mới và mở dưới dạng hộp thoại modal
                    FrmLoaiSanPham frmLoaiSanPham = new FrmLoaiSanPham();
                    frmLoaiSanPham.ShowDialog();

                    // Đảm bảo form hiện tại sẽ bị đóng sau khi form mới đóng
                    if (!currentForm.IsDisposed)
                    {
                        currentForm.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu cài đặt: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Form currentForm = this.FindForm();
            if (currentForm != null)
            {
                // Ẩn form hiện tại
                currentForm.Hide();

                // Tạo form mới và mở dưới dạng hộp thoại modal
                FrmLoaiSanPham frmLoaiSanPham = new FrmLoaiSanPham();
                frmLoaiSanPham.ShowDialog();

                // Đảm bảo form hiện tại sẽ bị đóng sau khi form mới đóng
                if (!currentForm.IsDisposed)
                {
                    currentForm.Close();
                }
            }
        }
    }
}
