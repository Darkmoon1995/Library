using System.Linq;
using System.Windows;
using System.Windows.Input;
using LibraryTRY3.Data;
using LibraryTRY3.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryTRY3
{
    public partial class MainWindow : Window
    {
        private bool IsRestoreOn = false;
        private string ReturnUsername;
        private string ReturnPassword;

        public MainWindow()
        {
            InitializeComponent();
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
            else if (IsRestoreOn == true)
            {
                WindowState = WindowState.Maximized;
                IsRestoreOn = false;
            }
            else
            {
                MessageBoxResult Result = MessageBox.Show("Please contact the Dev.Team. The application is going to close.", "IsRestoreOn isn't true or false", MessageBoxButton.OK, MessageBoxImage.Error);
                if (Result == MessageBoxResult.OK) { Close(); }
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

        private void CheckUser_Click(object sender, RoutedEventArgs e)
        {
            ReturnUsername = UserReturn.EnteredUsername;
            ReturnPassword = PasswordReturn.EnteredPassword;

            try
            {
                var optionsBuilder = new DbContextOptionsBuilder<MyDbContext>();
                optionsBuilder.UseSqlite("Data Source=MyLocalDatabase.db");

                using (var context = new MyDbContext(optionsBuilder.Options))
                {
                    var count = context.Admins
                        .Count(a => a.Username == ReturnUsername && a.Password == ReturnPassword && a.Accepted);

                    if (count > 0)
                    {
                        Library.ChangeLibrary newWindow = new Library.ChangeLibrary();
                        newWindow.Show();
                        this.Close(); // Close the current window
                    }
                    else
                    {
                        MessageBox.Show("Invalid username or password (Please Wait To be Confirmed).");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void ChangeToregister_Click(object sender, RoutedEventArgs e)
        {
            Login.Visibility = Visibility.Hidden;
            Register.Visibility = Visibility.Visible;
            MenuBarTextBlock.Text = "Register";
        }

        private void ChangeToLogin_Click(object sender, RoutedEventArgs e)
        {
            Login.Visibility = Visibility.Visible;
            Register.Visibility = Visibility.Hidden;
            MenuBarTextBlock.Text = "Login";
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (PasswordRegister.EnteredPassword == PasswordConfirm.EnteredPassword)
                {
                    if (PasswordRegister.EnteredPassword.Length >= 5 && UserRegister.EnteredUsername.Length >= 5)
                    {
                        var optionsBuilder = new DbContextOptionsBuilder<MyDbContext>();
                        optionsBuilder.UseSqlite("Data Source=MyLocalDatabase.db");

                        using (var context = new MyDbContext(optionsBuilder.Options))
                        {
                            var newAdmin = new Admin
                            {
                                Username = UserRegister.EnteredUsername,
                                Password = PasswordRegister.EnteredPassword, // Consider hashing the password
                                Accepted = true
                            };

                            context.Admins.Add(newAdmin);
                            context.SaveChanges();
                        }

                        MessageBox.Show("Registered successfully. Please try to login when you are confirmed.", "Register", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    }
                    else
                    {
                        MessageBox.Show("Username and password must be at least 5 characters.", "Not Enough Characters", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("The repeated password is not the same as the password. If this keeps happening, contact the admins.", "Check The Password Again", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
                if (ex.InnerException != null)
                {
                    MessageBox.Show($"Inner Exception: {ex.InnerException.Message}");
                }
            }
        }

        private void ForgotThePassword_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("I didn't want to make this, so ignore it. Or contact the admins.", "Forgot Password", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
