﻿using PasswordManager.View.UserControls;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace PasswordManager.View.Pages
{
    public partial class Dashboard : UserControl
    {

        private int currentPage = 1;
        private static string filePath = $"../../Data/{Functions.Username}_data.txt";

        public Dashboard()
        {
            InitializeComponent();

            username.Text = Functions.Username;
            UpdateDisplayedItems();

        }

        // Menu Otems Handle
        private void exitbutton_click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Functions.Username = "";

            var mainWindow = Window.GetWindow(this) as MainWindow;
            mainWindow?.load_page("login");
        }
        private void saveButton_click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            mainWindow?.load_page("save");
        }


        // List Items Handle
        private void DeleteChildItems()
        {
            for (int i = mainStackPanel.Children.Count - 1; i >= 0; i--)
            {
                var childItem = mainStackPanel.Children[i] as FrameworkElement;
                if (childItem.Name != "TitleItem")
                {
                    mainStackPanel.Children.RemoveAt(i);
                }
            }
        }

        private void UpdateDisplayedItems()
        {

            int itemsPerPage = 9;

            int startIndex = (currentPage - 1) * itemsPerPage;
            int endIndex = startIndex + itemsPerPage - 1;

            DeleteChildItems();

            int i = 0;

            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                string[] fields = line.Split(';');
                if (fields.Length >= 3)
                {
                    string webpage = fields[0];
                    string username = fields[1];
                    string date = fields[2];

                    PasswordListItem item = new PasswordListItem();
                    item.Index = i;
                    item.WebpageTxt = webpage;
                    item.GmailTxt = username;
                    item.DateTxt = date;

                    if (i >= startIndex && i <= endIndex)
                    {
                        mainStackPanel.Children.Add(item);
                    }

                    i++;
                }
            }
        }


        private void leftArrowButton_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage = currentPage - 1;
                UpdateDisplayedItems();
            }
        }

        private void rightArrowButton_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            currentPage = currentPage + 1;
            UpdateDisplayedItems();


            if (mainStackPanel.Children.Count == 1)
            {
                currentPage = currentPage - 1;
                UpdateDisplayedItems();
            }
        }
    }
}