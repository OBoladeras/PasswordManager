using PasswordManager.View.Pages;
using System;
using System.IO;
using System.Windows;

namespace PasswordManager
{
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

            load_settings();

            load_page("login");
        }

        public void load_page(string pageName)
        {
            if (pageName == "login")
            {
                Login loginControl = new Login();
                contentControl.Content = loginControl;
            }
            else if (pageName == "addUser")
            {
                AddUser addUSerControl = new AddUser();
                contentControl.Content = addUSerControl;
            }
            else if (pageName == "main" || pageName == "dashboard")
            {
                Dashboard mainPage = new Dashboard();
                contentControl.Content = mainPage;
            }
            else if (pageName == "save")
            {
                Save mainPage = new Save();
                contentControl.Content = mainPage;

            }
            else
                    {
                Console.WriteLine("Page not found");
            }
        }

        private void load_settings()
        {
            // Default settings
            string primary = "#6699ff";
            string secundary = "#b3ccff";
            string complementary = "#6699ff";
            string textColor = "white";


            // Get data
            try
            {
                string filePath = $"../../Data/settings_{Functions.Username}.txt";
                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    string[] items = line.Split(';');
                    if (items[0] == "colors" || items.Length == 5)
                    {
                        primary = items[1];
                        secundary = items[2];
                        complementary = items[3];
                        textColor = items[4];
                    }
                }
            }
            catch
            {
                Console.WriteLine("Settings file not found");
            }            

            // Save data in cache
            Functions.PrimaryColor = Functions.get_color(primary);
            Functions.SecundaryColor = Functions.get_color(secundary);
            Functions.Complementary = Functions.get_color(complementary);
            Functions.TextColor = Functions.get_color(textColor);
        }
    }
}
