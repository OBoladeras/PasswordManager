using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PasswordManager.View.Pages
{
    public partial class Login : UserControl
    {
        public Login()
        {
            InitializeComponent();
        }


        private void enterButton_click(object sender, MouseButtonEventArgs e)
        {
            check_login();
        }

        private void Username_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Password.passwordBox.Focus();
            }
        }

        private void Password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                check_login();
            }
        }

        private void createAccountButton_click(object sender, MouseButtonEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            mainWindow?.load_page("addUser");
        }


        private void check_login()
        {
            string filename = "check_login";
            string username = Username.textBox.Text;
            string password = Password.passwordBox.Password;
            string[] args = { username, password };
            string result = "";

            result = Functions.python_execution(filename, args);

            if (result.Contains("valid"))
            {
                Functions.Username = username;

                load_settings();

                var mainWindow = Window.GetWindow(this) as MainWindow;
                mainWindow?.load_page("dashboard");
            }
            else if (result.Contains("error1"))
            {
                MessageBox.Show("Invalid Password", "error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (result.Contains("error2"))
            {
                MessageBox.Show("Invalid Username", "error", MessageBoxButton.OK, MessageBoxImage.Warning);
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

            if (primary == "#6C63FF")
            {
                Functions.ColorSelectedIndex = 1;
            }
            else if (primary == "#00BFA6")
            {
                Functions.ColorSelectedIndex = 2;
            }
            else if (primary == "#F50057")
            {
                Functions.ColorSelectedIndex = 3;
            }
        }
    }
}
