using System.Windows;
using System.Windows.Controls;

namespace LibraryManagementDekstop.Screens
{
    public partial class UserRegistration : Window
    {
        public UserRegistration()
        {
            InitializeComponent();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            string userNumber = UserNumberTextBox.Text.Trim();
            string name = NameTextBox.Text.Trim();
            string nic = NICTextBox.Text.Trim();
            string address = AddressTextBox.Text.Trim();
            string sex = MaleRadioButton.IsChecked == true ? "Male" :
                         FemaleRadioButton.IsChecked == true ? "Female" : "Not specified";

            var selectedType = RegistrationTypeComboBox.SelectedItem as ComboBoxItem;
            string registrationType = selectedType?.Content.ToString() ?? "Not selected";

            if (string.IsNullOrWhiteSpace(userNumber) ||
                string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(nic) ||
                string.IsNullOrWhiteSpace(address) ||
                registrationType == "Not selected")
            {
                MessageBox.Show("Please fill all fields correctly.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            MessageBox.Show("User registered successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
