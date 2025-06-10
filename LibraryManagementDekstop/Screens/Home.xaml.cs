using LibraryManagementDekstop.Interfaces;
using LibraryManagementDekstop.Screens;
using LibraryManagementDekstop.Services;
using LibraryManagementDesktop.Services;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DesktopApplication.Screens
{
    public partial class Home : Window
    {
        private readonly UserRegistration userRegistration;
        private readonly BookRegistration bookRegistration;

        public Home(UserRegistration userRegistration)
        {
            InitializeComponent();
            LoadBooks();

            this.userRegistration = userRegistration;
        }

        private void LoadBooks()
        {
            List<Book> books = new List<Book>
            {
                new Book { Id = 1, BookName = "Madol Duuwa", Author = "Martin Wickramasinghe", Quantity = 6, StatusColor = Colors.Red },
                new Book { Id = 2, BookName = "Grade 10 Maths", Author = "Master Guide", Quantity = 5, StatusColor = Colors.LimeGreen },
                new Book { Id = 3, BookName = "Tamil Language", Author = "Master Guide", Quantity = 2, StatusColor = Colors.LimeGreen }
            };

            BookGrid.ItemsSource = books;
        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            //UserRegistration userRegistration = new UserRegistration(userService);
            //userRegistration.Show();

            //var userRegistration = service.GetUserDetails<>
            userRegistration.Show();
        }


        private void AddBooks_Click(object sender, RoutedEventArgs e)
        {
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

    public class Book
    {
        public int Id { get; set; }
        public string BookName { get; set; }
        public string Author { get; set; }
        public int Quantity { get; set; }
        public Color StatusColor { get; set; }
    }
}
