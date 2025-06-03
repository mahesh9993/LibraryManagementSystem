using System.Collections.Generic;
using System.Windows;

namespace LibraryManagementDekstop.Screens
{
    public partial class ReservationProcess : Window
    {
        public ReservationProcess()
        {
            InitializeComponent();
        }
        private void ReserveBook_Click(object sender, RoutedEventArgs e)
        {
            string borrowerId = BorrowerIdTextBox.Text.Trim();
            string selectedTitle = BookIdTextBox.Text.Trim();

            if (string.IsNullOrEmpty(borrowerId) || string.IsNullOrEmpty(selectedTitle)) 
            {
                MessageBox.Show("Please enter borrower ID and select a book title.");
                return;
            }

            ReservationStatusTextBlock.Text = $"Book '{selectedTitle}' reserved successfully for Borrower {borrowerId}.";
        }
    }
}
