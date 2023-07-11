using PasswordManager.View.UserControls;
using System;
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
        private static string filePath = $"../../Data/{Functions.Username}_data.txt";

        public Dashboard()
        {
            InitializeComponent();


            UpdateDisplayedItems();
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
                        item.PanelClick += PanelClick;


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

                BlurEffect blurEffect = new BlurEffect();
                blurEffect.Radius = 8;

                mainGrid.Effect = blurEffect;


                EditMenu menu = new EditMenu();
                menu.webpageEditBox.textBox.Text = clickedItem.WebpageTxt;
                menu.usernameEditBox.textBox.Text = clickedItem.GmailTxt;
                menu.itemIndex = clickedItem.Index;

                menu.ShowDialog();

                mainGrid.Effect = null;
                UpdateDisplayedItems();
            }
        }

        private void Item_RemoveButtonClicked(object sender, EventArgs e)
        {
            if (sender is PasswordListItem clickedItem)
            {
                int itemIndex = clickedItem.Index;
                PopUpWindow popUp = new PopUpWindow();

                popUp.Message = $"Sure you want to delete?";
                popUp.Answer = "YesNo";
                popUp.ShowDialog();


                if (popUp.clickedItem == "yes")
                {
                    Functions.DeleteItemAtIndex(itemIndex);
                    UpdateDisplayedItems();
                }

                popUp.Close();
            }
        }

        private void PanelClick(object sender, EventArgs e)
        {
            if (sender is PasswordListItem clickedItem)
            {
                string username = Functions.Username;
                string web = clickedItem.WebpageTxt;
                string email = clickedItem.GmailTxt;

                string[] data = { username, web, email };
                PopUpWindow.data = data;

                PopUpWindow popUp = new PopUpWindow();

                popUp.Answer = "ShowCopy";
                popUp.ShowDialog();


                popUp.Close();
            }
        }
    }
}
