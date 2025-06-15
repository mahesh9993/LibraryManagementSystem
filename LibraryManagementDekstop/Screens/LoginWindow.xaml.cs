using DesktopApplication.Screens;
using System.Windows;
using System.Windows.Input;

namespace LibraryApp.Screens
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            txtUsername.Text = "admin"; // Optional: Prefill
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Password;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter both username and password.",
                                "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (username == "admin" && password == "1234")
            {
                Home home = new();
                home.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid username or password.",
                                "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Input_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Login_Click(null, null);
            }
        }
    }
}
