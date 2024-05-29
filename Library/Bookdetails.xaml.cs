using LibraryTRY3.RandomUsercontrol;
using LibraryTRY3.Data;
using LibraryTRY3.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LibraryTRY3.Library
{
    public partial class Bookdetails : Window
    {
       
        private string ItemSearched; // For Searching In The books
        private string AdmminsUsernames;

        public Bookdetails(string AdminUsername)
        {
            InitializeComponent();
            AdmminsUsernames = AdminUsername;
            LoadData();
        }

        private void Window_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void LoadData()
        {
            try
            {
                using (var context = new MyDbContext())
                {
                    var bookNames = context.BooksDetails.Select(b => b.Name).ToList();

                    foreach (var name in bookNames)
                    {
                        SearchingBooksComboBox.Items.Add(name);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SearchOfBooksBtn_Click(object sender, RoutedEventArgs e)
        {
            ItemSearched = SearchBox.EnteredUsername;
            IQueryable<BooksDetail> query;

            try
            {
                using (var context = new MyDbContext())
                {
                    if (IsTheBookTaken.IsChecked == true) // IsIt Taken
                    {
                        query = context.BooksDetails.Where(b => b.Name.Contains(ItemSearched) && b.Taken);
                    }
                    else if (IsTheBookTaken.IsChecked == false) // IsItNotTaken
                    {
                        query = context.BooksDetails.Where(b => b.Name.Contains(ItemSearched) && !b.Taken);
                    }
                    else // OtherStuff
                    {
                        query = context.BooksDetails.Where(b => b.Name.Contains(ItemSearched));
                    }

                    SearchingBooksComboBox.Items.Clear();
                    foreach (var book in query.ToList())
                    {
                        SearchingBooksComboBox.Items.Add(book.Name);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SearchingBooksComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedItem = (string)SearchingBooksComboBox.SelectedItem;

            try
            {
                using (var context = new MyDbContext())
                {
                    var book = context.BooksDetails.FirstOrDefault(b => b.Name == selectedItem);

                    if (book != null)
                    {
                        TakenByTextBlock.Text = book.TakenBy ?? "Null";
                        AuthorTextBlock.Text = book.Author ?? "Null";
                        PublisherTextBlock.Text = book.Publisher ?? "Null";
                        NameOfBookTextBlock.Text = book.Name;

                        // Turn Red The location
                        _2Dlibrary _2DlibraryControl = new _2Dlibrary();
                        _2DlibraryControl.SearchForShelf(int.Parse(book.Location.Substring(0, 2)));
                        library2D.Children.Add(_2DlibraryControl);
                        int parsedShelfNumber = int.Parse(book.Location[2].ToString());

                        int shelfnumber = parsedShelfNumber;
                        Window ShelfWindows = new WhichShelf(shelfnumber);
                        ShelfWindows.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the database query
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
