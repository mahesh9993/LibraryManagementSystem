using LibraryManagementDekstop.Interfaces;
using LibraryManagementDekstop.Models;
using System.Windows;
using System.Windows.Controls;

namespace LibraryManagementDekstop.Screens
{
    public partial class UserRegistration : Window
    {
        private readonly IUserService userService;

        public UserRegistration(IUserService userService, object userService1)
        {
            InitializeComponent();
            this.userService = userService;
        }

        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            string userNumber = UserNumberTextBox.Text.Trim();
            string name = NameTextBox.Text.Trim();
            string nic = NICTextBox.Text.Trim();
            string address = AddressTextBox.Text.Trim();
            string gender = MaleRadioButton.IsChecked == true ? "Male" :
                            FemaleRadioButton.IsChecked == true ? "Female" : "Not specified";

            var selectedType = RegistrationTypeComboBox.SelectedItem as ComboBoxItem;
            string registrationType = selectedType?.Content.ToString() ?? "Not selected";

            if (string.IsNullOrWhiteSpace(userNumber) ||
                string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(nic) ||
                string.IsNullOrWhiteSpace(address) ||
                string.IsNullOrWhiteSpace(gender) ||
                registrationType == "Not selected")
            {
                MessageBox.Show("Please fill all fields correctly.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var saveModel = new UserSaveModel()
            {
                UserNumber = userNumber,
                UserName = name,
                NIC = nic,
                Gender = gender,
                Address = address,
                CreatedBy = 1,
                UserType = registrationType
            };

            var response = await userService.SaveUserDetails(saveModel);

            if (response)
            {
                MessageBox.Show("User registered successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Failed to register user.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
