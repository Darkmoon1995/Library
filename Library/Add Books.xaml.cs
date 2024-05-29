using LibraryTRY3.Data;
using LibraryTRY3.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace LibraryTRY3.Library
{
    public partial class Add_Books : Window
    {
        private bool IsRestoreOn = false;
        private string AdmminsUsernames;
        private readonly MyDbContext _context;

        public Add_Books(string AdminUsername)
        {
            InitializeComponent();
            AdmminsUsernames = AdminUsername;

            var options = new DbContextOptionsBuilder<MyDbContext>()
                .UseSqlite("Data Source=MyLocalDatabase.db")
                .Options;
            _context = new MyDbContext(options);
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Restore_Click(object sender, RoutedEventArgs e)
        {
            if (IsRestoreOn == false)
            {
                WindowState = WindowState.Normal;
                IsRestoreOn = true;
            }
            else
            {
                WindowState = WindowState.Maximized;
                IsRestoreOn = false;
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void BookDetails_Click(object sender, RoutedEventArgs e)
        {
            Window addBooks = new Bookdetails(AdmminsUsernames);
            addBooks.Show();
            Close();
        }

        private void AddUserOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            Window newUser = new NewUser(AdmminsUsernames);
            newUser.Show();
            Close();
        }

        private void ConfirmAdding_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult addingSure = MessageBox.Show($"Are You Sure You Want To Add The Book Called: {NameOfBookaADD.EnteredUsername}", "Sure", MessageBoxButton.OKCancel, MessageBoxImage.Information);
            if (addingSure == MessageBoxResult.OK)
            {
                if (NameOfBookaADD.EnteredUsername.Length >= 3 && LocationADD.EnteredUsername.Length == 3)
                {
                    try
                    {
                        if (!int.TryParse(LocationADD.EnteredUsername, out _))
                        {
                            MessageBox.Show("The Location Of the book Must be numbers", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        var newBook = new BooksDetail
                        {
                            Name = NameOfBookaADD.EnteredUsername,
                            Location = LocationADD.EnteredUsername,
                            Taken = false,
                            
                            Author = AuthorADD.EnteredUsername ?? string.Empty,  // Default to empty string if null
                            Publisher = PublisherAdd.EnteredUsername ?? string.Empty, 
                            YearOfPublication = int.Parse(TheYearADD.EnteredUsername),
                            UserAdminAdded = AdmminsUsernames
                        };

                        _context.BooksDetails.Add(newBook);
                        _context.SaveChanges();
                        MessageBox.Show("You Added The book successfully", "Nice", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        var innerException = ex.InnerException;
                        while (innerException != null)
                        {
                            MessageBox.Show($"An error occurred: {innerException.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            innerException = innerException.InnerException;
                        }
                        MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("There Must Be At Least 3 Characters In The Name And The Location Must Be 3 Characters", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }



        private string SaveTheOriginalNameBook;

        private void SearchingChanging_Click(object sender, RoutedEventArgs e)
        {
            string selectedItem = SearchingChaned.EnteredUsername;

            try
            {
                var book = _context.BooksDetails.SingleOrDefault(b => b.Name == selectedItem);

                if (book == null)
                {
                    MessageBox.Show($"A book with the name {selectedItem} doesn't exist in the database. Please search the book first.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                TakenByChanging.EnteredUsername = book.TakenBy;
                AuthorChanging.EnteredUsername = book.Author;
                PublisherChanging.EnteredUsername = book.Publisher;
                NameChanging.EnteredUsername = book.Name;
                YearofpublishChanging.EnteredUsername = book.YearOfPublication.ToString();
                AdminWhoUsedIT.EnteredUsername = book.UserAdminAdded;
                LocationChanging.EnteredUsername = book.Location;
                SaveTheOriginalNameBook = book.Name;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ConfirmChanging_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult areYouSureChanged = MessageBox.Show($"Are You Sure You Want To Change The Book Called: {SaveTheOriginalNameBook} To {NameChanging.EnteredUsername}", "Sure", MessageBoxButton.OKCancel, MessageBoxImage.Information);
            if (areYouSureChanged == MessageBoxResult.OK)
            {
                try
                {
                    if (NameChanging.EnteredUsername.Length >= 3)
                    {
                        var book = _context.BooksDetails.SingleOrDefault(b => b.Name == SaveTheOriginalNameBook);
                        if (book == null)
                        {
                            MessageBox.Show($"A book with the name {SaveTheOriginalNameBook} doesn't exist in the database.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        if (!int.TryParse(LocationChanging.EnteredUsername, out _))
                        {
                            MessageBox.Show("The Location Of the book Must be numbers", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        book.Name = NameChanging.EnteredUsername;
                        book.Location = LocationChanging.EnteredUsername;
                        book.TakenBy = TakenByChanging.EnteredUsername;
                        book.Author = AuthorChanging.EnteredUsername;
                        book.Publisher = PublisherChanging.EnteredUsername;
                        book.YearOfPublication = int.Parse(YearofpublishChanging.EnteredUsername);
                        book.UserAdminAdded = AdminWhoUsedIT.EnteredUsername;

                        _context.SaveChanges();
                        MessageBox.Show($"The Book Changed As: {SaveTheOriginalNameBook} To {NameChanging.EnteredUsername}", "Thanks", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("The Name Must Be 3 Or More Characters", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while updating book details: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void DeketeBooks_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete the book?", "Deleting The Book", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if (result == MessageBoxResult.OK)
            {
                try
                {
                    var book = _context.BooksDetails.SingleOrDefault(b => b.Name == SaveTheOriginalNameBook);
                    if (book == null)
                    {
                        MessageBox.Show($"A book with the name {SaveTheOriginalNameBook} doesn't exist in the database.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    _context.BooksDetails.Remove(book);
                    _context.SaveChanges();
                    MessageBox.Show("The Book Was Deleted Successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
