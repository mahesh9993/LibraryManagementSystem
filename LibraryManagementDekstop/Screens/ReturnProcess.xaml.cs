using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace LibraryManagementDesktop.Services
{
    public partial class ReturnProcess : Window
    {
        public class BorrowedBook
        {
            public int BookId { get; set; }
            public string Title { get; set; }
            public DateTime BorrowDate { get; set; }
            public DateTime DueDate { get; set; }

            public decimal Fine
            {
                get
                {
                    int overdueDays = (DateTime.Today - DueDate).Days;
                    return overdueDays > 0 ? overdueDays * 10m : 0m;
                }
            }
        }

        private ObservableCollection<BorrowedBook> borrowedBooks = new();

        private decimal totalFine = 0m;

        public ReturnProcess()
        {
            InitializeComponent();
            dgBorrowedBooks.ItemsSource = borrowedBooks;
        }

        private void LoadBooks_Click(object sender, RoutedEventArgs e)
        {
            borrowedBooks.Clear();
            totalFine = 0m;
            txtTotalFine.Text = "$0.00";

            string userId = txtUserId.Text.Trim();
            if (string.IsNullOrEmpty(userId))
            {
                MessageBox.Show("Please enter a valid User ID.");
                return;
            }

            if (userId == "U001")
            {
                borrowedBooks.Add(new BorrowedBook
                {
                    BookId = 101,
                    Title = "C# Programming",
                    BorrowDate = DateTime.Today.AddDays(-15),
                    DueDate = DateTime.Today.AddDays(-3)
                });
                borrowedBooks.Add(new BorrowedBook
                {
                    BookId = 102,
                    Title = "Database Design",
                    BorrowDate = DateTime.Today.AddDays(-10),
                    DueDate = DateTime.Today.AddDays(5)
                });
            }
            else
            {
                MessageBox.Show("User not found or no borrowed books.");
            }

            UpdateTotalFine();
        }

        private void ReturnBook_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is BorrowedBook book)
            {
                borrowedBooks.Remove(book);

                MessageBox.Show($"Book '{book.Title}' returned successfully.");

                UpdateTotalFine();

                if (borrowedBooks.Count == 0)
                {
                    MessageBox.Show("All books returned.");
                    txtUserId.Clear();
                }
            }
        }

        private void UpdateTotalFine()
        {
            totalFine = 0m;
            foreach (var book in borrowedBooks)
            {
                totalFine += book.Fine;
            }
            txtTotalFine.Text = totalFine.ToString("C");
        }
    }
}
