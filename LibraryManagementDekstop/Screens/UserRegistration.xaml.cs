using LibraryManagementDekstop.Interfaces;
using ApplicationCore.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;

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
                gender == "Not specified" ||
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

            try
            {
                var jsonContent = JsonConvert.SerializeObject(saveModel);
                var httpContent = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

                var response = await client.PostAsync("saveUser", httpContent);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("User registered successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Registration failed: {error}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
