using System;
using System.Windows;
using PasswordManager.View.Pages;

namespace PasswordManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

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
            else if (pageName == "main")
            {
                // MainControl loginControl = new MainControl();
                // contentControl.Content = loginControl;
            }
        }


        public void LoginUserControl_LoginButtonClicked(object sender, EventArgs e)
        {
            load_page("main");

        }
    }
}
