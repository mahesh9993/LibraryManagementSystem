using System.Collections.Generic;
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

        private void LoadBooks()
        {
            List<Book> books = new List<Book>
            {
                new Book { Id = 1, BookName = "Madol Duuwa", Author = "Martin Wickramasinghe", Quantity = 6, StatusColor = Colors.Red },
                new Book { Id = 2, BookName = "Grade 10 Maths", Author = "Master Guide", Quantity = 5, StatusColor = Colors.LimeGreen },
                new Book { Id = 3, BookName = "Tamil Language", Author = "Master Guide", Quantity = 2, StatusColor = Colors.Blue }
            };

            BookGrid.ItemsSource = books;
        }

        // Navigation button handlers
        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Add User Clicked");
        }

        private void AddBooks_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Add Books Clicked");
        }

        private void Reservation_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Reservation Clicked");
        }

        private void LoanProcess_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Loan Process Clicked");
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

    public class Book
    {
        public int Id { get; set; }
        public string BookName { get; set; }
        public string Author { get; set; }
        public int Quantity { get; set; }
        public Color StatusColor { get; set; }
    }
}
