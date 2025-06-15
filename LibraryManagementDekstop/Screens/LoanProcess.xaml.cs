using DesktopApplication.Screens;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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

        private void CheckBorrower_Click(object sender, RoutedEventArgs e)
        {
            string borrowerId = BorrowerIdTextBox.Text.Trim();
            if (borrowerId == "123")
            {
                borrowedBooksCount = 3;
                hasOverdue = false;
                BorrowerStatusTextBlock.Text = $"Borrower is eligible. Currently borrowed: {borrowedBooksCount}/5";
            }
            else if (borrowerId == "999")
            {
                borrowedBooksCount = 6;
                hasOverdue = true;
                BorrowerStatusTextBlock.Text = $"Borrower has overdue books. Cannot borrow more.";
            }
            else
            {
                BorrowerStatusTextBlock.Text = $"Borrower not found.";
            }

            ConfirmLoanButton.IsEnabled = false;
            ReturnDateTextBlock.Text = "";
            CopyStatusTextBlock.Text = "";
        }

        private void CheckCopy_Click(object sender, RoutedEventArgs e)
        {
            string copyId = CopyIdTextBox.Text.Trim();
            if (copyId == "REF001")
            {
                isCopyBorrowable = false;
                CopyStatusTextBlock.Text = "This is a reference-only copy. Not borrowable.";
                ConfirmLoanButton.IsEnabled = false;
            }
            else
            {
                isCopyBorrowable = true;
                CopyStatusTextBlock.Text = "Available to Borrow.";
                ConfirmLoanButton.IsEnabled = true;
            }
        }

        private void ConfirmLoanButton_Click(object sender, RoutedEventArgs e)
        {
            if (isCopyBorrowable)
            {
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
