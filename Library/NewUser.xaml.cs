using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SystemMedia = System.Windows.Media;
using LibraryTRY3.Data;
using LibraryTRY3.Models;
using System.Windows.Media;

namespace LibraryTRY3.Library
{
    public partial class NewUser : Window
    {
        private bool IsRestoreOn = false; // For moving the screen
        private string AdmminsUsernames; // For giving to the next window
        private int borderCounter = 0; // Counter for naming TextBlocks
        private int stackPanelCounter = 0; // Counter for naming StackPanels

        public NewUser(string AdminUsername)
        {
            try
            {
                InitializeComponent();
                AdmminsUsernames = AdminUsername;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Menu Bar buttons/Clicks
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
            else if (IsRestoreOn == true)
            {
                WindowState = WindowState.Maximized;
                IsRestoreOn = false;
            }
            else
            {
                MessageBoxResult Result = MessageBox.Show("please Contact the Dev.Team. The application is going to close.", "IsRestoreOn Isnt true or false", MessageBoxButton.OK, MessageBoxImage.Error);
                if (Result == MessageBoxResult.OK) { Close(); }
            }
        }


        private void SearchingForUserDetail_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var context = new MyDbContext())
                {
                    var users = context.User
                        .Where(u => u.UserName.Contains(SearchingBetterTextBlock.EnteredUsername))
                        .Select(u => u.UserName)
                        .ToList();

                    SearchUsersComboBox.Items.Clear();
                    foreach (var user in users)
                    {
                        SearchUsersComboBox.Items.Add(user);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddingANewUser_Click(object sender, RoutedEventArgs e)
        {
            if (Username.EnteredUsername.Length > 4 && Class.EnteredUsername.Length > 4 && Number.EnteredUsername.Length > 9)
            {
                MessageBoxResult Result = MessageBox.Show($"Are You Sure You Want To add The User {Username.EnteredUsername}?", "Sure?", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                if (Result == MessageBoxResult.OK)
                {
                    try
                    {
                        using (var context = new MyDbContext())
                        {
                            var user = new User
                            {
                                UserName = Username.EnteredUsername,
                                Class = Class.EnteredUsername,
                                Number = Number.EnteredUsername,
                                AdminName = AdmminsUsernames
                            };

                            context.User.Add(user);
                            context.SaveChanges();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    MessageBox.Show($"You added {Username.EnteredUsername} successfully", "Nice", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Username, Class, and Number must be valid", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RemovingUser_Click(object sender, RoutedEventArgs e)
        {
            string selectedItem = (string)SearchUsersComboBox.SelectedItem;
            if (selectedItem == null)
            {
                MessageBox.Show("You Must Select Item Before (In The Search Box Below)", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MessageBoxResult Result = MessageBox.Show($"Are You Sure You Want To Remove The User: {selectedItem}?", "Sure?", MessageBoxButton.OKCancel, MessageBoxImage.Information);
            if (Result == MessageBoxResult.OK)
            {
                try
                {
                    using (var context = new MyDbContext())
                    {
                        var user = context.User.FirstOrDefault(u => u.UserName == selectedItem);
                        if (user != null)
                        {
                            context.User.Remove(user);
                            context.SaveChanges();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void ResetingTheBetterTextbox_Click(object sender, RoutedEventArgs e)
        {
            Username.EnteredUsername = null;
            Class.EnteredUsername = null;
            Number.EnteredUsername = null;
        }

        private void SearchUsersComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedItem = (string)SearchUsersComboBox.SelectedItem;
            try
            {
                using (var context = new MyDbContext())
                {
                    var users = context.Borrow.Where(u => u.UserThatGotTheBook == selectedItem).ToList();
                    if (users.Any())
                    {
                        UserBooksDetailStackPanel.Children.Clear();

                        foreach (var user in users)
                        {
                            // Create StackPanel for each user
                            StackPanel userStackPanel = new StackPanel
                            {
                                Orientation = Orientation.Horizontal,
                                Margin = new Thickness(5)
                            };

                            TextBlock userNameTextBlock = new TextBlock
                            {
                                Background = new SolidColorBrush(Colors.White),
                                Width = 88,
                                Height = 40,
                                Text = $"{user.UserThatGotTheBook}",
                                Margin = new Thickness(2)
                            };

                            TextBlock classTextBlock = new TextBlock
                            {

                                Background = new SolidColorBrush(Colors.White),
                                Width = 88,
                                Height = 40,
                                Text = $"{user.BookName}",
                                Margin = new Thickness(2)

                            };

                            TextBlock numberTextBlock = new TextBlock
                            {
                                Background = new SolidColorBrush(Colors.White),
                                Width = 88,
                                Height = 40,
                                Text = $"{user.DateBookTaken}",
                                Margin = new Thickness(2)
                            };

                            TextBlock adminNameTextBlock = new TextBlock
                            {
                                Background = new SolidColorBrush(Colors.White),
                                Width = 88,
                                Height = 40,
                                Text = $"{user.DateBookGivenBack}",
                                Margin = new Thickness(2)
                            };

                            Button userButton = new Button
                            {
                                Width = 88,
                                Height = 40,
                                Content = "Action Button",
                                Margin = new Thickness(2)
                            };

                            userStackPanel.Children.Add(userNameTextBlock);
                            userStackPanel.Children.Add(classTextBlock);
                            userStackPanel.Children.Add(numberTextBlock);
                            userStackPanel.Children.Add(adminNameTextBlock);
                            userStackPanel.Children.Add(userButton);


                            UserBooksDetailStackPanel.Children.Add(userStackPanel);
                        }
                        StackPanel ChangingUserControlPanel = new StackPanel
                        {
                            Orientation = Orientation.Horizontal,
                            Margin = new Thickness(5)
                        };


                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        

        private void GiveBack_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new MyDbContext())
            {
                var JustCreateRandomUser = new Borrow
                {
                    UserThatGotTheBook = Username.EnteredUsername,
                    BookName = Username.EnteredUsername,
                    DateBookGivenBack = DateTime.Today,
                    DateBookTaken = DateTime.Today,

                };

                context.Borrow.Add(JustCreateRandomUser);
                context.SaveChanges();
            }
        }
    }
}
