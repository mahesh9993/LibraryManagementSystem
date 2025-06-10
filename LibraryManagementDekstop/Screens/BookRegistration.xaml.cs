using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DesktopApplication.Screens
{
    public partial class BookRegistration : Window
    {
        private readonly Home home;
        public BookRegistration(Home home)
        {
            InitializeComponent();
            this.home = home;
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

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            string isbn = ISBNTextBox.Text.Trim();
            string title = TitleTextBox.Text.Trim();
            string author = AuthorTextBox.Text.Trim();
            string publisher = PublisherTextBox.Text.Trim();
            string copiesText = CopiesTextBox.Text.Trim();
            string? category = (CategoryComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString();

            if (string.IsNullOrEmpty(isbn) || string.IsNullOrEmpty(title) ||
                string.IsNullOrEmpty(author) || string.IsNullOrEmpty(publisher) ||
                string.IsNullOrEmpty(copiesText) || string.IsNullOrEmpty(category))
            {
                MessageBox.Show("Please fill in all the fields.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(copiesText, out int copies) || copies < 1 || copies > 10)
            {
                MessageBox.Show("Please enter a number between 1 and 10 for the number of copies.", "Invalid Copies", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            MessageBox.Show($"Book Registered!\n\nISBN: {isbn}\nTitle: {title}\nAuthor: {author}\nPublisher: {publisher}\nCategory: {category}\nCopies: {copies}", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            home.Show();
            this.Close();
        }
    }
}
