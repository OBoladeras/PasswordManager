using PasswordManager.View.Pages;
using System;
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
            // Handle this
            Functions.PrimaryColor = Functions.get_color("#6699ff");
            Functions.SecundaryColor = Functions.get_color("#b3ccff");
            Functions.TextColor = Functions.get_color("white");
        }
    }
}
