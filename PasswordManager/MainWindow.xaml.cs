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
            if (!Directory.Exists("Data"))
            {
                Directory.CreateDirectory("Data");
            }
            if (!File.Exists("Data/users.txt"))
            {
                File.Create("Data/users.txt");
            }

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
            else if (pageName == "settings")
            {
                Settings mainPage = new Settings();
                contentControl.Content = mainPage;
            }
            else
            {
                Console.WriteLine("Page not found");
            }
        }
    }
}
