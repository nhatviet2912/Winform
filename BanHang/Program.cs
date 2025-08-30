using System.Diagnostics;

namespace BanHang
{
    internal static class Program
    {
        private static Process backgroundProcess;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            DatabaseHelper.InitDatabase();
            FrmLogin loginForm = new FrmLogin();
            Application.Run(loginForm);
            if (loginForm.IsDisposed) // Kiểm tra nếu form đã đóng
            {
                // Dừng các process ngầm (nếu có)
                StopBackgroundProcesses();

                // Thoát ứng dụng
                Application.Exit();
            }
        }

        private static void StopBackgroundProcesses()
        {
            // Giả sử bạn đang chạy một process nền
            if (backgroundProcess != null && !backgroundProcess.HasExited)
            {
                backgroundProcess.Kill(); // Dừng process
            }
        }
    }
}