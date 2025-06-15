using LibraryManagementDekstop.Interfaces;
using ApplicationCore.Models;
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

        //private void LoadBooks()
        //{
        //    List<Book> books = new List<Book>
        //    {
        //        new Book { Id = 1, BookName = "Madol Duuwa", Author = "Martin Wickramasinghe", Quantity = 6, StatusColor = Colors.Red },
        //        new Book { Id = 2, BookName = "Grade 10 Maths", Author = "Master Guide", Quantity = 5, StatusColor = Colors.LimeGreen },
        //        new Book { Id = 3, BookName = "Tamil Language", Author = "Master Guide", Quantity = 2, StatusColor = Colors.LimeGreen }
        //    };

        //    BookGrid.ItemsSource = books;
        //}

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
            UserRegistration userRegistration = new UserRegistration();
            userRegistration.Show();
        }

        private void AddBooks_Click(object sender, RoutedEventArgs e)
        {
            BookRegistration bookRegistration = new BookRegistration();
            bookRegistration.Show();
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            ReturnProcess returnProcess = new ReturnProcess();
            returnProcess.Show();
        }

        private void Reservation_Click(object sender, RoutedEventArgs e)
        {
            ReservationProcess reservationProcess = new ReservationProcess();
            reservationProcess.Show();
        }
        private void LoanProcess_Click(object sender, RoutedEventArgs e)
        {
            LoanProcess loanProcess = new LoanProcess();
            loanProcess.Show();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Searching: {SearchBox.Text}");
        }

        private void SearchBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (SearchBox.Text == "Search books")
            {
                SearchBox.Text = "";
                SearchBox.Foreground = Brushes.Black;
            }
        }

        private void SearchBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SearchBox.Text))
            {
                SearchBox.Text = "Search books";
                SearchBox.Foreground = Brushes.Gray;
            }
        }
    }
}
