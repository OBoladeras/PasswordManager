using PasswordManager.View.UserControls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Effects;


namespace PasswordManager.View.Pages
{
    public partial class Dashboard : UserControl
    {

        private int currentPage = 1;
        private int itemIndex = 0;
        private static string filePath = $"../../Data/{Functions.Username}_data.txt";

        public Dashboard()
        {
            InitializeComponent();

            load_settings();

            UpdateDisplayedItems();
        }

        private void load_settings()
        {
            editMenu.Background = Functions.Complementary;
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

            try
            {
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

                        // Event handle
                        item.EditButtonClicked += Item_EditButtonClicked;
                        item.RemoveButtonClicked += Item_RemoveButtonClicked;


                        if (i >= startIndex && i <= endIndex)
                        {
                            mainStackPanel.Children.Add(item);
                        }

                        i++;
                    }
                }
            }
            catch
            {
                Console.WriteLine("Password File not Found");
            }
        }


        private void leftArrowButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage = currentPage - 1;
                UpdateDisplayedItems();
            }
        }

        private void rightArrowButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            currentPage = currentPage + 1;
            UpdateDisplayedItems();


            if (mainStackPanel.Children.Count == 1)
            {
                currentPage = currentPage - 1;
                UpdateDisplayedItems();
            }
        }


        private void Item_EditButtonClicked(object sender, EventArgs e)
        {
            if (sender is PasswordListItem clickedItem)
            {
                itemIndex = clickedItem.Index;

                BlurEffect blurEffect = new BlurEffect();
                blurEffect.Radius = 8;

                leftMenu.Effect = blurEffect;
                rightPart.Effect = blurEffect;

                editMenu.Visibility = Visibility.Visible;

                webpageEditBox.textBox.Text = clickedItem.WebpageTxt;
                usernameEditBox.textBox.Text = clickedItem.GmailTxt;
            }
        }

        private void Item_RemoveButtonClicked(object sender, EventArgs e)
        {
            if (sender is PasswordListItem clickedItem)
            {
                int itemIndex = clickedItem.Index;

                MessageBoxResult confirm = MessageBox.Show($"Delete {clickedItem.WebpageTxt} password?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (confirm == MessageBoxResult.Yes)
                {
                    DeleteItemAtIndex(itemIndex);
                }
            }
        }

        public void DeleteItemAtIndex(int index)
        {
            string[] lines = File.ReadAllLines(filePath);

            if (index >= 0 && index < lines.Length)
            {
                List<string> linesList = new List<string>(lines);
                linesList.RemoveAt(index);

                File.WriteAllLines(filePath, linesList);
            }
        }


        // Pop Up window
        private void closePopUpButton_MouseEnter(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Hand;
        }

        private void closePopUpButton_MouseLeave(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Arrow;
        }

        private void closePopUpButton_Click(object sender, MouseButtonEventArgs e)
        {
            editMenu.Visibility = Visibility.Hidden;
            leftMenu.Effect = null;
            rightPart.Effect = null;
        }

        private void updatePasswordBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MessageBoxResult answer = MessageBox.Show("Sure you want to update the password?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            
            if (answer == MessageBoxResult.Yes)
            {
                DeleteItemAtIndex(itemIndex);

                string username = Functions.Username;
                string web = webpageEditBox.textBox.Text;
                string email = usernameEditBox.textBox.Text;
                string password = passwordEditBox.passwordBox.Password;

                string[] variables = { username, web, email, password };
                Functions.python_execution("save_password", variables);
            }
        }
    }
}
