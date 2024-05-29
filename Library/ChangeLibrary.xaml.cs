using LibraryTRY3.Data;
using LibraryTRY3.Models;
using LibraryTRY3.RandomUsercontrol;
using Microsoft.EntityFrameworkCore;
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
    /// Interaction logic for ChangeLibrary.xaml
    /// </summary>
    public partial class ChangeLibrary : Window
    {
       
        private readonly MyDbContext _context;
        public ChangeLibrary()
        {
            InitializeComponent();
            _context = new MyDbContext(); 

        }

        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
     //Hosele Nadaram Check Konam Age Row Kar mikone Ya na chon bayad beshmoram :D


            try
            {
                int row = int.Parse(RowLocation.EnteredUsername);
                int column = int.Parse(columnLocation.EnteredUsername);

                // Check if a button already exists at the specified location
                bool buttonExists = _context.LibraryStructures.Any(s => s.Row == row && s.Column == column); //MER30 RandomGUY :D

                if (buttonExists)
                {
                    MessageBox.Show("A button already exists at this location.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    var structure = new LibraryStructure
                    {
                        Name = NameContext.EnteredUsername,
                        Row = row,
                        Column = column
                    };
                    _context.LibraryStructures.Add(structure);
                    _context.SaveChanges();
                    library2DControl.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RemoveAll_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               
                var allStructures = _context.LibraryStructures.ToList();

                _context.LibraryStructures.RemoveRange(allStructures);
                _context.SaveChanges();

                library2DControl.Refresh();

                MessageBox.Show("Table was reset successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }

            MessageBox.Show("Table Was Reseted successfully.");

            }
            
        }

       
    }

