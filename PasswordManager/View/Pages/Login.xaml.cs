using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;

namespace PasswordManager.View.Pages
{
    public partial class Login : UserControl
    {
        public Login()
        {
            InitializeComponent();

            Functions.PrimaryColor = Functions.get_color("#6699ff");
            Functions.SecundaryColor = Functions.get_color("#b3ccff");            
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
            PopUpWindow popUp = new PopUpWindow();
            
            string username = Username.textBox.Text;
            string password = Password.passwordBox.Password;
            string result;

            if (username == "" || username == null)
            {
                popUp.Message = "Write a username";
                popUp.ShowDialog();
            }
            else if (password == "" || password == null)
            {
                popUp.Message = "Write a password";
                popUp.ShowDialog();
            }
            else
            {
                result = SecurityProcesses.Login(username, password);

                if (result.Contains("valid"))
                {
                    Functions.Username = username;
                    // CHeck if files exist and if not creates them
                    if (!File.Exists($"Data/{username}_data.txt"))
                    {
                        File.Create($"Data/{username}_data.txt");
                    }
                    if (!File.Exists($"Data/settings_{username}.txt"))
                    {
                        File.Create($"Data/settings_{username}.txt");
                    }

                    load_settings();

                    var mainWindow = Window.GetWindow(this) as MainWindow;
                    mainWindow?.load_page("dashboard");
                }
                else
                {
                    popUp.Message = result;
                    popUp.ShowDialog();
                }
            }

            popUp.Close();
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
