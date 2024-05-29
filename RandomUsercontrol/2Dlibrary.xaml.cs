using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.EntityFrameworkCore;
using LibraryTRY3.Data;
using LibraryTRY3.Models;

namespace LibraryTRY3.RandomUsercontrol
{
    public partial class _2Dlibrary : UserControl
    {
        private readonly MyDbContext _context;
        private List<(Button button, int value)> buttonList = new List<(Button button, int value)>();

        public _2Dlibrary()
        {
            InitializeComponent();
            _context = new MyDbContext(); // Initialize the context
            LoadData();
        }

        // Trigger layout update
        public void Refresh()
        {
            ClearContent();
            LoadData();
            UpdateLayout();
        }

        // Clear all existing content from the grid
        private void ClearContent()
        {
            HelloWorld.Children.Clear();
            buttonList.Clear();
        }

        // Searching for the shelf
        public void SearchForShelf(int shelfNumber)
        {
            string buttonName = "Shelf" + shelfNumber;

            Button button = HelloWorld.Children.OfType<Button>().FirstOrDefault(b => b.Name == buttonName);

            if (button != null)
            {
                button.Background = Brushes.Red;
                button.InvalidateVisual();
            }
        }

        private void LoadData()
        {
            try
            {
                var structures = _context.LibraryStructures.ToList();

                foreach (var structure in structures)
                {
                    int calculatedValue = structure.Row + structure.Column * 10;
                    CreateButton(structure.Name, structure.Row, structure.Column, calculatedValue);
                }

                buttonList.Sort((a, b) => a.value.CompareTo(b.value));

                for (int i = 0; i < buttonList.Count; i++)
                {
                    buttonList[i].button.Content += $"{i + 1}";
                    buttonList[i].button.Name = $"Shelf{i + 1}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CreateButton(string name, int rowlocation, int columnlocation, int calculatedValue)
        {
            Button JustStyleButton = new Button();

            JustStyleButton.Name = "Shelf" + calculatedValue.ToString();

            JustStyleButton.Style = (Style)FindResource("StyleManagment");

            Grid.SetRow(JustStyleButton, rowlocation);
            Grid.SetColumn(JustStyleButton, columnlocation);

            HelloWorld.Children.Add(JustStyleButton);

            buttonList.Add((JustStyleButton, calculatedValue));
        }

        private void Shelf_Click(object sender, RoutedEventArgs e)
        {
            // Handle the shelf click event if needed
        }
    }
}
