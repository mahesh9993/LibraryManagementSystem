﻿using DesktopApplication.Screens;
using LibraryManagementDekstop.Models;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using static LibraryManagementDesktop.Services.ReturnProcess;

namespace LibraryManagementDekstop.Screens
{
    public partial class ReservationProcess : Window
    {
        public ReservationProcess()
        {
            InitializeComponent();
            LoadReserveBooks();
        }

        private async void LoadReserveBooks()
        {
            try
            {
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };

                using var client = new HttpClient(handler);
                client.BaseAddress = new Uri("https://localhost:7034/");

                var response = await client.GetAsync("api/Reservation/GetReserveBooks");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();

                    var apiResponse = JsonSerializer.Deserialize<ApiResponse<List<ReserveDetailOutputModel>>>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    if (apiResponse != null && apiResponse.Data != null)
                    {
                        dgReservedBooks.ItemsSource = apiResponse.Data;
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


        private async void ReserveBook_Click(object sender, RoutedEventArgs e)
        {
            string userNumber = UserNumberTextBox.Text.Trim();
            string bookNumber = BookIdTextBox.Text.Trim();

            if (string.IsNullOrEmpty(userNumber) || string.IsNullOrEmpty(bookNumber))
            {
                MessageBox.Show("Please enter borrower ID and select a book title.");
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

                var saveModel = new ReservationDetailInputModel()

                {
                    UserNumber = userNumber,
                    BookNumber = bookNumber,
                    CreatedBy = 1,

                };

                var json = JsonSerializer.Serialize(saveModel);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("api/Reservation/ReserveBooks", content);

                if (response.IsSuccessStatusCode)
                {
                    ReservationStatusTextBlock.Text = $"Book '{bookNumber}' Reserved successfully.";
                    MessageBox.Show(this,"Reservation successfull!");
                    LoadReserveBooks();
                }

                else
                {
                    MessageBox.Show($"Reservation Failed: {response.StatusCode}");
                }

            }

            catch (Exception ex)

            {

                MessageBox.Show($"Error: {ex.Message}");

            }

        }

        private async void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
             
                if (sender is Button button && button.Tag is int bookCopyID)
                {
                    var handler = new HttpClientHandler
                    {
                        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                    };

                    using var client = new HttpClient(handler);
                    client.BaseAddress = new Uri("https://localhost:7034/");

             
                    var deleteModel = new UpdateReserveModel()
                    {
                        BookCopyID = bookCopyID
                    };

                    var json = JsonSerializer.Serialize(deleteModel);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

               
                    var response = await client.PostAsync("api/Reservation/UpdateReservationDetailByBookCopyID", content);

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show(this, "Reservation updated/deleted successfully!");
                        LoadReserveBooks();

                    }
                    else
                    {
                        MessageBox.Show(this, $"Failed to update reservation: {response.StatusCode}");
                    }
                }
                else
                {
                    MessageBox.Show(this, "Invalid BookCopyID.");
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
