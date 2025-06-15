using DesktopApplication.Screens;
using LibraryManagementDekstop.Interfaces;
using LibraryManagementDekstop.Models;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace LibraryManagementDekstop.Screens
{
    public partial class UserRegistration : Window
    {
        HttpClient client = new HttpClient();
        public UserRegistration()
        {
            client.BaseAddress = new Uri("https://localhost:7034/api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
                );
            InitializeComponent();
        }

        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            string userNumber = UserNumberTextBox.Text.Trim();
            string fname = FNameTextBox.Text.Trim();
            string lname = LNameTextBox.Text.Trim();
            string nic = NICTextBox.Text.Trim();
            string address = AddressTextBox.Text.Trim();
            string gender = MaleRadioButton.IsChecked == true ? "Male" :
                            FemaleRadioButton.IsChecked == true ? "Female" : "Not specified";

            var selectedType = RegistrationTypeComboBox.SelectedItem as ComboBoxItem;
            string registrationType = selectedType?.Content.ToString() ?? "Not selected";

            if (string.IsNullOrWhiteSpace(userNumber) ||
                string.IsNullOrWhiteSpace(fname) ||
                string.IsNullOrWhiteSpace(lname) ||
                string.IsNullOrWhiteSpace(nic) ||
                string.IsNullOrWhiteSpace(address) ||
                gender == "Not specified" ||
                registrationType == "Not selected")
            {
                MessageBox.Show("Please fill all fields correctly.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };

                using var client = new HttpClient(handler);
                client.BaseAddress = new Uri("https://localhost:7034/");

                var saveModel = new UserSaveModel()
                {
                    UserNumber = userNumber,
                    FirstName = fname,
                    LastName = lname,
                    NIC = nic,
                    Gender = gender,
                    Address = address,
                    CreatedBy = 1,
                    IsActive = true,
                    UserType = registrationType
                };

                var json = JsonSerializer.Serialize(saveModel);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("api/User/SaveUser", content);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("User registered successfully!");
                    Home home = new();
                    home.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show($"Failed to register user: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Home home = new();
            home.Show();
            this.Close();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Home home = new();
            home.Show();
            this.Close();
        }
    }
}
