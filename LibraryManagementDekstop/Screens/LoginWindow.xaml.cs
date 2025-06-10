using DesktopApplication.Screens;
using System.Windows;

namespace LibraryApp.Screens
{
    public partial class LoginWindow : Window
    {
        private readonly Home home;
        public LoginWindow(Home home)
        {
            InitializeComponent();
            txtUsername.Text = "admin";
            this.home = home;
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
                home.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid username or password.",
                                "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
