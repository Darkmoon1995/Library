using LibraryTRY3.Library;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LibraryTRY3.RandomUsercontrol
{
    public partial class MainMenuItem : UserControl
    {
        private bool IsRestoreOn;
        private string AdmminsUsernames = "null";

        public MainMenuItem()
        {
            InitializeComponent();
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Initiate window drag
            Window.GetWindow(this).DragMove();
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).WindowState = WindowState.Minimized;
        }

        private void Restore_Click(object sender, RoutedEventArgs e)
        {
            if (IsRestoreOn == false)
            {
                Window.GetWindow(this).WindowState = WindowState.Normal;
                IsRestoreOn = true;
            }
            else if (IsRestoreOn == true)
            {
                Window.GetWindow(this).WindowState = WindowState.Maximized;
                IsRestoreOn = false;
            }
            else
            {
                MessageBoxResult Result = MessageBox.Show("Please contact the Dev.Team. The application is going to close.", "IsRestoreOn isn't true or false", MessageBoxButton.OK, MessageBoxImage.Error);
                if (Result == MessageBoxResult.OK)
                {
                    Window.GetWindow(this).Close();
                }
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
        }

        private void SettingOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
        }

        private void AddUserOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            Window NewUser = new NewUser(AdmminsUsernames);
            NewUser.Show();
            Window.GetWindow(this).Close();
        }

        private void AddBooks_Click(object sender, RoutedEventArgs e)
        {
            Window AddBooks = new Add_Books(AdmminsUsernames);
            AddBooks.Show();
            Window.GetWindow(this).Close();
        }

        private void Changelibrary_Click(object sender, RoutedEventArgs e)
        {
            Window ChangeLibrary = new ChangeLibrary();
            ChangeLibrary.Show();
            Window.GetWindow(this).Close();
        }

        private void booksdetails_Click(object sender, RoutedEventArgs e)
        {
            Window Bookdetails = new Bookdetails(AdmminsUsernames);
            Bookdetails.Show();
            Window.GetWindow(this).Close();
        }

        private string titlemainmenu;
        public string TitleMainName
        {
            get { return titlemainmenu; }
            set
            {
                titlemainmenu = value;
                MainTitle.Text = titlemainmenu;
            }
        }

        private string adminusername;
        public string AdminUsername
        {
            get { return adminusername; }
            set
            {
                adminusername = value;
                AdminShowerTextBlock.Text = adminusername;
            }
        }
    }
}
