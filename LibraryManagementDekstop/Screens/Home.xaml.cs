using LibraryApp.Screens;
using LibraryManagementDekstop.Interfaces;
using LibraryManagementDekstop.Models;
using LibraryManagementDekstop.Screens;
using LibraryManagementDekstop.Services;
using LibraryManagementDesktop.Services;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DesktopApplication.Screens
{
    public partial class Home : Window
    {
        public Home()
        {
            InitializeComponent();
            LoadBooks();
        }
        private async void LoadBooks()
        {
            try
            {
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };

                using var client = new HttpClient(handler);
                client.BaseAddress = new Uri("https://localhost:7034/");

                var response = await client.GetAsync("api/Book/GetAllBooks");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();

                    var apiResponse = JsonSerializer.Deserialize<ApiResponse<List<Book>>>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    if (apiResponse != null && apiResponse.Data != null)
                    {
                        BookGrid.ItemsSource = apiResponse.Data;
                    }
                    else
                    {
                        MessageBox.Show("No books found.");
                    }
                }
                else
                {
                    MessageBox.Show($"Failed to load books: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }


        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            UserRegistration userRegistration = new();
            userRegistration.Show();
            this.Close();
        }

        private void AddBooks_Click(object sender, RoutedEventArgs e)
        {
            BookRegistration bookRegistration = new();
            bookRegistration.Show();
            this.Close();
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            ReturnProcess returnProcess = new();
            returnProcess.Show();
            this.Close();
        }

        private void Reservation_Click(object sender, RoutedEventArgs e)
        {
            ReservationProcess reservationProcess = new();
            reservationProcess.Show();
            this.Close();
        }
        private void LoanProcess_Click(object sender, RoutedEventArgs e)
        {
            LoanProcess loanProcess = new();
            loanProcess.Show();
            this.Close();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new();

            loginWindow.Show();
            this.Close();
        }

        //private void Search_Click(object sender, RoutedEventArgs e)
        //{
        //    MessageBox.Show($"Searching: {SearchBox.Text}");
        //}

        //private void SearchBox_GotFocus(object sender, RoutedEventArgs e)
        //{
        //    if (SearchBox.Text == "Search books")
        //    {
        //        SearchBox.Text = "";
        //        SearchBox.Foreground = Brushes.Black;
        //    }
        //}

        //private void SearchBox_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    if (string.IsNullOrWhiteSpace(SearchBox.Text))
        //    {
        //        SearchBox.Text = "Search books";
        //        SearchBox.Foreground = Brushes.Gray;
        //    }
        //}
    }
}
