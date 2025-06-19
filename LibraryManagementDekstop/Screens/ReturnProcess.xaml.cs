using DesktopApplication.Screens;
using LibraryManagementDekstop.Models;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace LibraryManagementDesktop.Services
{
    public partial class ReturnProcess : Window
    {
        public class BorrowedBook
        {
            public int BookCopyId { get; set; }
            public DateTime BorrowDate { get; set; }
            public DateTime DueDate { get; set; }
            public decimal Fine => (DateTime.Today - DueDate).Days > 0 ?
                                  (DateTime.Today - DueDate).Days * 10m : 0m;
        }

        private ObservableCollection<BorrowedBook> borrowedBooks = new();
        private decimal totalFine = 0m;

        public ReturnProcess()
        {
            InitializeComponent();
            dgBorrowedBooks.ItemsSource = borrowedBooks;
            SetupDataGridColumns();
        }

        private void SetupDataGridColumns()
        {
            dgBorrowedBooks.Columns.Clear();

            dgBorrowedBooks.Columns.Add(new DataGridTextColumn
            {
                Header = "Book Copy ID",
                Binding = new Binding("BookCopyId"),
                Width = 100
            });

            dgBorrowedBooks.Columns.Add(new DataGridTextColumn
            {
                Header = "Borrow Date",
                Binding = new Binding("BorrowDate") { StringFormat = "yyyy-MM-dd" },
                Width = 120
            });

            dgBorrowedBooks.Columns.Add(new DataGridTextColumn
            {
                Header = "Due Date",
                Binding = new Binding("DueDate") { StringFormat = "yyyy-MM-dd" },
                Width = 120
            });

            dgBorrowedBooks.Columns.Add(new DataGridTextColumn
            {
                Header = "Fine",
                Binding = new Binding("Fine") { StringFormat = "C" },
                Width = 80
            });

            var buttonColumn = new DataGridTemplateColumn
            {
                Header = "Action",
                Width = 100
            };

            var buttonTemplate = new FrameworkElementFactory(typeof(Button));
            buttonTemplate.SetValue(Button.ContentProperty, "Return");
            buttonTemplate.SetValue(Button.TagProperty, new Binding("."));
            buttonTemplate.AddHandler(Button.ClickEvent, new RoutedEventHandler(ReturnBook_Click));

            buttonColumn.CellTemplate = new DataTemplate
            {
                VisualTree = buttonTemplate
            };

            dgBorrowedBooks.Columns.Add(buttonColumn);
        }

        private async void LoadBooks_Click(object sender, RoutedEventArgs e)
        {
            borrowedBooks.Clear();
            totalFine = 0m;
            txtTotalFine.Text = "$0.00";

            if (!int.TryParse(txtUserId.Text.Trim(), out int userId))
            {
                MessageBox.Show("Please enter a valid numeric User ID.");
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

                var userModel = new BookReturnModel
                {
                    userID = userId
                };

                var json = JsonSerializer.Serialize(userModel);
                var requestContent = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("api/Loan/GetLoansByUser", requestContent);

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();

                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    var result = JsonSerializer.Deserialize<ApiResponse>(responseBody, options);

                    if (result == null || result.data == null || result.data.Count == 0)
                    {
                        MessageBox.Show("No books found for this user.");
                        return;
                    }

                    foreach (var loan in result.data)
                    {
                        borrowedBooks.Add(new BorrowedBook
                        {
                            BookCopyId = loan.bookCopyID,
                            BorrowDate = loan.loanDate,
                            DueDate = loan.returnDate
                        });
                    }

                    UpdateTotalFine();
                }
                else
                {
                    MessageBox.Show($"API request failed: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading books: {ex.Message}");
            }
        }

        private async void ReturnBook_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (sender is Button button && button.Tag is BorrowedBook borrowedBook)
                {
                    int bookCopyID = borrowedBook.BookCopyId;

                    var handler = new HttpClientHandler
                    {
                        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                    };

                    using var client = new HttpClient(handler);
                    client.BaseAddress = new Uri("https://localhost:7034/");


                    var deleteModel = new BookReturnModel()
                    {
                        bookCopyID = bookCopyID
                    };

                    var json = JsonSerializer.Serialize(deleteModel);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");


                    var response = await client.PostAsync("api/Loan/ReturnBookDelete", content);

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Book Returned successfully!");

                    }
                    else
                    {
                        MessageBox.Show($"Failed to book return: {response.StatusCode}");
                    }
                }
                else
                {
                    MessageBox.Show("Invalid BookCopyID.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void UpdateTotalFine()
        {
            totalFine = borrowedBooks.Sum(book => book.Fine);
            txtTotalFine.Text = totalFine.ToString("C");
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Home home = new();
            home.Show();
            this.Close();
        }
    }
}
