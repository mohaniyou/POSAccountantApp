using System;
using System.Windows.Forms;
using POSAccountantApp.Database;

namespace POSAccountantApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            try
            {
                // Initialize database first
                SqlConnectionHelper.InitializeDatabase();

                // Create and show login form
                var loginForm = new LoginForm();
                Application.Run(loginForm);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Application Error: {ex.Message}\n\nDetails: {ex.InnerException?.Message}", 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
