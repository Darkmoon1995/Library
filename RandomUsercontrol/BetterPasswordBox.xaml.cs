using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Channels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LibraryTRY3.RandomUsercontrol
{

    public partial class BetterPasswordBox : UserControl
    {
        private bool PasswordIsHidden = true;
        public BetterPasswordBox()
        {
            InitializeComponent();
        }
        private String placeholder;
        // Place Holder 
        public String Placeholder
        {
            get { return placeholder; }
            set
            {
                placeholder = value;
                tbPlaceHolder.Text = placeholder;

            }
        }


        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            txtInput.Clear();
            txtInput.Focus();
        }

        private void txtInput_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(txtInput.Password))
            {
                tbPlaceHolder.Visibility = Visibility.Visible;
            }
            else
            {
                tbPlaceHolder.Visibility = Visibility.Collapsed;
            }
        }
        private void txtInputTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (String.IsNullOrEmpty(txtInputTextbox.Text))
            {
                tbPlaceHolder.Visibility = Visibility.Visible;
            }
            else
            {
                tbPlaceHolder.Visibility = Visibility.Collapsed;
            }
        }
        //Show Password
        private void PasswordShower_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordIsHidden == true)
            {
                PasswordShower.Content = "Hide";
                PasswordIsHidden = false;
                txtInputTextbox.Visibility = Visibility.Visible;
                txtInput.Visibility = Visibility.Hidden;
                txtInputTextbox.Text = txtInput.Password;

            }
            else if (PasswordIsHidden == false)
            {
                PasswordIsHidden = true;
                PasswordShower.Content = "Show";
                txtInputTextbox.Visibility = Visibility.Hidden;
                txtInput.Visibility = Visibility.Visible;
                txtInput.Password = txtInputTextbox.Text;
            }
            else { MessageBox.Show("Error", "There Was An Error Please Relunch the Application Again", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
        //Get Password Or Username
        public string EnteredPassword
        {
            get
            {
                if (PasswordIsHidden)
                    return txtInput.Password;
                else
                    return txtInputTextbox.Text;
            }
        }

        private void txtInput_GotFocus(object sender, RoutedEventArgs e)
        {
            txtInput.BorderBrush = Brushes.DarkBlue;

        }

        private void txtInput_LostFocus(object sender, RoutedEventArgs e)
        {
            txtInput.BorderBrush = Brushes.Transparent;

        }



        private void txtInputTextbox_GotFocus(object sender, RoutedEventArgs e)
        {
            txtInput.BorderBrush = Brushes.DarkBlue;

        }

        private void txtInputTextbox_LostFocus(object sender, RoutedEventArgs e)
        {
            txtInput.BorderBrush = Brushes.Transparent;

        }
    } 
}
