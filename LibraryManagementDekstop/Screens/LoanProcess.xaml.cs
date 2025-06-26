using DesktopApplication.Screens;
using LibraryManagementDekstop.Models;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Linq;

namespace LibraryManagementDekstop.Screens
{
    public partial class LoanProcess : Window
    {
        public LoanProcess()
        {
            InitializeComponent();
        }

        private int borrowedBooksCount = 0;
        private bool hasOverdue = false;
        private bool isCopyBorrowable = false;

        private async void CheckBorrower_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string borrowerNumber = BorrowerIdTextBox.Text.Trim();

                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };
                using var client = new HttpClient(handler);
                client.BaseAddress = new Uri("https://localhost:7034/");

                var response = await client.GetAsync($"api/Loan/GetLoanDetailsByUser?userNumber={borrowerNumber}");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();

                    var apiResponse = JsonSerializer.Deserialize<ApiResponse<List<LoanDetailModel>>>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    if (apiResponse != null && apiResponse.Data.Count > 0)
                    {
                        borrowedBooksCount = apiResponse.Data[0].LoanCount;
                        hasOverdue = apiResponse.Data[0].HasOverdue;
                        if (hasOverdue)
                        {
                            BorrowerStatusTextBlock.Text = "Borrower has overdue books. Cannot borrow more.";
                        }
                        else
                        {
                            BorrowerStatusTextBlock.Text = borrowedBooksCount < 5 ? $"Borrower is eligible. Currently borrowed: {borrowedBooksCount}/5" : $"Borrower is not eligible. Currently borrowed: {borrowedBooksCount}/5";
                        }
                        
                    }
                    else
                    {
                        MessageBox.Show(this, "Failed to get loan details");
                    }
                }
                else
                {
                    MessageBox.Show(this, "Failed to load loans");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Error: {ex.Message}");
            }

            ConfirmLoanButton.IsEnabled = false;
            ReturnDateTextBlock.Text = "";
            CopyStatusTextBlock.Text = "";
        }

        private async void CheckCopy_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                string copyNumber = CopyIdTextBox.Text.Trim();

                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };
                using var client = new HttpClient(handler);
                client.BaseAddress = new Uri("https://localhost:7034/");

                var response = await client.GetAsync($"api/Loan/CheckBookAvailability?bookCopyNumber={copyNumber}");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();

                    var apiResponse = JsonSerializer.Deserialize<ApiResponse<List<BookAvailabilityOutputModel>>>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    if (apiResponse != null && apiResponse.Data.Count > 0)
                    {
                        isCopyBorrowable = !apiResponse.Data[0].IsReference;
                        CopyStatusTextBlock.Text = apiResponse.Data[0].IsReference ? "This is a reference-only copy. Not borrowable." : "Available to Borrow.";
                        ConfirmLoanButton.IsEnabled = isCopyBorrowable;
                    }
                    else
                    {
                        MessageBox.Show(this, "Failed to get loan details");
                    }
                }
                else
                {
                    MessageBox.Show(this, "Failed to load loans");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Error: {ex.Message}");
            }
        }

        private async void ConfirmLoanButton_Click(object sender, RoutedEventArgs e)
        {
            if (isCopyBorrowable)
            {
                string borrowerNumber = BorrowerIdTextBox.Text.Trim();
                string copyNumber = CopyIdTextBox.Text.Trim();

                if (string.IsNullOrWhiteSpace(BorrowerIdTextBox.Text) || string.IsNullOrWhiteSpace(CopyIdTextBox.Text))
                {
                    MessageBox.Show(this, "Please fill in all fields.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
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

                    var saveModel = new LoanModel()
                    {
                        UserNumber = borrowerNumber,
                        BookCopyNumber = copyNumber,
                        ReturnDate = DateTime.Today.AddDays(14),
                        CreatedBy = 1,
                    };

                    var json = JsonSerializer.Serialize(saveModel);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync("api/Loan/SaveLoan", content);

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show(this, "Loan Saved Successfully!");
                    }
                    else
                    {
                        MessageBox.Show(this, $"Failed to save loan: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, $"Error: {ex.Message}");
                }

                DateTime returnDate = DateTime.Today.AddDays(14);
                ReturnDateTextBlock.Text = $"Loan confirmed! Return date: {returnDate:dd MMM yyyy}";
                ReturnDateTextBlock.Foreground = Brushes.DarkBlue;
            }
        }
        private void CancelLoanButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Home home = new();
            home.Show();
            this.Close();
        }
    }
}
