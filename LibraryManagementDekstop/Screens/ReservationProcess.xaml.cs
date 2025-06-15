using DesktopApplication.Screens;
using LibraryManagementDekstop.Models;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows;

namespace LibraryManagementDekstop.Screens
{
    public partial class ReservationProcess : Window
    {
        public ReservationProcess()
        {
            InitializeComponent();
        }
        private async void ReserveBook_Click(object sender, RoutedEventArgs e)
        {
            string userNumber = UserNumberTextBox.Text.Trim();
            string selectedTitle = BookIdTextBox.Text.Trim();

            if (string.IsNullOrEmpty(userNumber) || string.IsNullOrEmpty(selectedTitle))
            {
                MessageBox.Show("Please enter borrower ID and select a book title.");
                return;
            }

            ReservationStatusTextBlock.Text = $"Book '{selectedTitle}' Reserved successfully.";
            try

            {
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };

                using var client = new HttpClient(handler);

                client.BaseAddress = new Uri("https://localhost:7034/");

                var saveModel = new ReservationDetailInputModel()

                {
                    UserNumber = userNumber,
                    BookNumber = selectedTitle,
                    CreatedBy = 1,

                };

                var json = JsonSerializer.Serialize(saveModel);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("api/Reservation/ReserveBooks", content);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("User registered successfully!");
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
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Home home = new();
            home.Show();
            this.Close();
        }

    }
}
