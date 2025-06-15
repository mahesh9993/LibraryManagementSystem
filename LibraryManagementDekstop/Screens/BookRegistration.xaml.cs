using LibraryManagementDekstop.Models;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Text.Json;
using System.Text;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Xml.Linq;

namespace DesktopApplication.Screens
{
    public partial class BookRegistration : Window
    {
        private List<Book> _bookCategories = new List<Book>();

        public BookRegistration()
        {
            InitializeComponent();
            LoadCategories(); // Load categories when window initializes
        }

        private async void LoadCategories()
        {
            try
            {
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };

                using var client = new HttpClient(handler);
                client.BaseAddress = new Uri("https://localhost:7034/");

                var response = await client.GetAsync("api/Book/GetAllBookCategories");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var apiResponse = JsonSerializer.Deserialize<ApiResponse<List<Book>>>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    if (apiResponse != null && apiResponse.Data != null)
                    {
                        _bookCategories = apiResponse.Data;
                        CategoryComboBox.ItemsSource = _bookCategories;
                        CategoryComboBox.DisplayMemberPath = "BookCategoryName";
                        CategoryComboBox.SelectedValuePath = "BookCategoryID";
                    }
                }
                else
                {
                    MessageBox.Show("Failed to load book categories.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading categories: {ex.Message}");
            }
        }

        private void SubmitISBN_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(ISBNTextBox.Text))
            {
                DetailsPanel.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Please enter a valid ISBN.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void CopiesTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !int.TryParse(e.Text, out _);
        }

        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ISBNTextBox.Text) ||
                string.IsNullOrWhiteSpace(TitleTextBox.Text) ||
                string.IsNullOrWhiteSpace(AuthorTextBox.Text) ||
                string.IsNullOrWhiteSpace(PublisherTextBox.Text) ||
                string.IsNullOrWhiteSpace(CopiesTextBox.Text) ||
                CategoryComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(CopiesTextBox.Text, out int copies) || copies < 1 || copies > 10)
            {
                MessageBox.Show("Please enter a valid number of copies (1-10).", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
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

                var selectedCategory = (Book)CategoryComboBox.SelectedItem;
                var book = new Book
                {
                    ISBN = ISBNTextBox.Text.Trim(),
                    Title = TitleTextBox.Text.Trim(),
                    Author = AuthorTextBox.Text.Trim(),
                    Publisher = PublisherTextBox.Text.Trim(),
                    NoofCopies = copies,
                    BookCategoryID = selectedCategory.BookCategoryID,
                    BookCategoryName = selectedCategory.BookCategoryName,
                    CreatedBy = 1
                };

                var json = JsonSerializer.Serialize(book);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("api/Book/RegisterBook", content);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Book registered successfully!");
                    Home home = new();
                    home.Show();
                    this.Close();
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Failed to register book: {response.StatusCode}\n{errorContent}");
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