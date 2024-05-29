using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LibraryTRY3.RandomUsercontrol
{
    /// <summary>
    /// Interaction logic for BetterTextbox.xaml
    /// </summary>
    public partial class BetterTextbox : UserControl
    {
        public BetterTextbox()
        {
            InitializeComponent();
        }
        private String placeholder;

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

        private void txtInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (String.IsNullOrEmpty(txtInput.Text))
            {
                tbPlaceHolder.Visibility = Visibility.Visible;
            }
            else
            {
                tbPlaceHolder.Visibility = Visibility.Collapsed;
            }
        }
        public string EnteredUsername
        {
            set
            {
                txtInput.Text = value;
            }
            get
            {
                return txtInput.Text;
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
    }
}
