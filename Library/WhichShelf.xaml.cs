using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LibraryTRY3.Library
{
    /// <summary>
    /// Interaction logic for WhichShelf.xaml
    /// </summary>
    public partial class WhichShelf : Window
    {
        private bool IsRestoreOn = false; //For Moving The Screen
        private int NumberOfShelf;
        public WhichShelf(int numberOfShelf)
        {
            InitializeComponent();
            NumberOfShelf = numberOfShelf;
            switch (NumberOfShelf)
            {
                case 0:
                    Zero.Background = Brushes.Red;
                    break;
                case 1:
                    one.Background = Brushes.Red;

                    break;
                case 2:
                    two.Background = Brushes.Red;

                    break;
                case 3:
                    three.Background = Brushes.Red;

                    break;
                case 4:
                    four.Background = Brushes.Red;

                    break;
                case 5:
                    Five.Background = Brushes.Red;

                    break;
                default:
                    break;
            }
        }
        //Menu Bar buttons/Clicks
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

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        //be able to move the window
        private void Window_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
