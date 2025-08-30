namespace BanHang
{
    partial class CommonMenuStrip
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MenuStrip menuStrip1;

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
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            editToolStripMenuItem = new ToolStripMenuItem();
            cutToolStripMenuItem = new ToolStripMenuItem();
            pasteToolStripMenuItem = new ToolStripMenuItem();
            viewToolStripMenuItem = new ToolStripMenuItem();
            refreshToolStripMenuItem = new ToolStripMenuItem();
            sảnPhẩmToolStripMenuItem = new ToolStripMenuItem();
            kháchHàngToolStripMenuItem = new ToolStripMenuItem();
            khoHàngToolStripMenuItem = new ToolStripMenuItem();
            hóaĐơnNhậpToolStripMenuItem = new ToolStripMenuItem();
            hóaĐơnBánToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, editToolStripMenuItem, viewToolStripMenuItem, helpToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(8, 3, 0, 3);
            menuStrip1.Size = new Size(1067, 30);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(88, 24);
            fileToolStripMenuItem.Text = "Hệ Thống";
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(130, 26);
            exitToolStripMenuItem.Text = "Thoát";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // editToolStripMenuItem
            // 
            editToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { cutToolStripMenuItem, pasteToolStripMenuItem });
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.Size = new Size(88, 24);
            editToolStripMenuItem.Text = "Giao Dịch";
            // 
            // cutToolStripMenuItem
            // 
            cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            cutToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.X;
            cutToolStripMenuItem.ShowShortcutKeys = false;
            cutToolStripMenuItem.Size = new Size(224, 26);
            cutToolStripMenuItem.Text = "Bán Hàng";
            // 
            // pasteToolStripMenuItem
            // 
            pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            pasteToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.V;
            pasteToolStripMenuItem.ShowShortcutKeys = false;
            pasteToolStripMenuItem.Size = new Size(224, 26);
            pasteToolStripMenuItem.Text = "Nhập Hàng";
            pasteToolStripMenuItem.Click += pasteToolStripMenuItem_Click;
            // 
            // viewToolStripMenuItem
            // 
            viewToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { refreshToolStripMenuItem, sảnPhẩmToolStripMenuItem, kháchHàngToolStripMenuItem, khoHàngToolStripMenuItem, hóaĐơnNhậpToolStripMenuItem, hóaĐơnBánToolStripMenuItem });
            viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            viewToolStripMenuItem.Size = new Size(75, 24);
            viewToolStripMenuItem.Text = "Quản Lý";
            // 
            // refreshToolStripMenuItem
            // 
            refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            refreshToolStripMenuItem.ShortcutKeys = Keys.F5;
            refreshToolStripMenuItem.ShowShortcutKeys = false;
            refreshToolStripMenuItem.Size = new Size(224, 26);
            refreshToolStripMenuItem.Text = "Loại Sản Phẩm";
            refreshToolStripMenuItem.Click += refreshToolStripMenuItem_Click;
            // 
            // sảnPhẩmToolStripMenuItem
            // 
            sảnPhẩmToolStripMenuItem.Name = "sảnPhẩmToolStripMenuItem";
            sảnPhẩmToolStripMenuItem.Size = new Size(224, 26);
            sảnPhẩmToolStripMenuItem.Text = "Sản Phẩm";
            sảnPhẩmToolStripMenuItem.Click += sảnPhẩmToolStripMenuItem_Click;
            // 
            // kháchHàngToolStripMenuItem
            // 
            kháchHàngToolStripMenuItem.Name = "kháchHàngToolStripMenuItem";
            kháchHàngToolStripMenuItem.Size = new Size(224, 26);
            kháchHàngToolStripMenuItem.Text = "Khách Hàng";
            kháchHàngToolStripMenuItem.Click += kháchHàngToolStripMenuItem_Click;
            // 
            // khoHàngToolStripMenuItem
            // 
            khoHàngToolStripMenuItem.Name = "khoHàngToolStripMenuItem";
            khoHàngToolStripMenuItem.Size = new Size(224, 26);
            khoHàngToolStripMenuItem.Text = "Kho Hàng";
            // 
            // hóaĐơnNhậpToolStripMenuItem
            // 
            hóaĐơnNhậpToolStripMenuItem.Name = "hóaĐơnNhậpToolStripMenuItem";
            hóaĐơnNhậpToolStripMenuItem.Size = new Size(224, 26);
            hóaĐơnNhậpToolStripMenuItem.Text = "Hóa Đơn Nhập";
            // 
            // hóaĐơnBánToolStripMenuItem
            // 
            hóaĐơnBánToolStripMenuItem.Name = "hóaĐơnBánToolStripMenuItem";
            hóaĐơnBánToolStripMenuItem.Size = new Size(224, 26);
            hóaĐơnBánToolStripMenuItem.Text = "Hóa Đơn Bán";
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(90, 24);
            helpToolStripMenuItem.Text = "Giới Thiệu";
            // 
            // CommonMenuStrip
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(menuStrip1);
            Margin = new Padding(4, 5, 4, 5);
            Name = "CommonMenuStrip";
            Size = new Size(1067, 37);
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem sảnPhẩmToolStripMenuItem;
        private ToolStripMenuItem kháchHàngToolStripMenuItem;
        private ToolStripMenuItem khoHàngToolStripMenuItem;
        private ToolStripMenuItem hóaĐơnNhậpToolStripMenuItem;
        private ToolStripMenuItem hóaĐơnBánToolStripMenuItem;
    }
}