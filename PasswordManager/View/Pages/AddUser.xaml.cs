using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace PasswordManager.View.Pages
{
    /// <summary>
    /// Interaction logic for AddUser.xaml
    /// </summary>
    public partial class AddUser : UserControl
    {
        public AddUser()
        {
            InitializeComponent();
            lbluppercase.Color = "#000000";
            lbllowercase.Color = "#000000";
            lblnumber.Color = "#000000";
            lblsymbol.Color = "#000000";
        }

        private void Password_PasswordChanged(object sender, RoutedEventArgs e)
        {

            string red = "#ff0000";
            string green = "#00ff00";
            string black = "#000000";
            string password = lblpassword.passwordBox.Password;

            if (password != "" || password is null)
            {
                if (Regex.IsMatch(password, @"[A-Z]"))
                {
                    lbluppercase.Color = green;
                }
                else { lbluppercase.Color = red; }
                if (Regex.IsMatch(password, @"[a-z]"))
                {
                    lbllowercase.Color = green;
                }
                else { lbllowercase.Color = red; }
                if (Regex.IsMatch(password, @"\d"))
                {
                    lblnumber.Color = green;
                }
                else { lblnumber.Color = red; }
                if (Regex.IsMatch(password, @"[\W_]"))
                {
                    lblsymbol.Color = green;
                }
                else { lblsymbol.Color = red; }
            }
            else
            {
                lbluppercase.Color = black;
                lbllowercase.Color = black;
                lblnumber.Color = black;
                lblsymbol.Color = black;

            }


        }


        private void CreateUser_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            bool securePassword = true;
            string username = Username.textBox.Text;
            string password1 = lblpassword.passwordBox.Password;
            string password2 = PasswordConfirm.passwordBox.Password;

            if (username == "")
            {
                MessageBox.Show("Write a username", "Caution", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (password1 == "" || password2 == "")
            {
                MessageBox.Show("Dont leave blank password", "Caution", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (password1 != password2)
            {
                MessageBox.Show("Passwords don't match", "Caution", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                if (!Regex.IsMatch(password1, @"[A-Z]"))
                {
                    securePassword = false;
                }
                if (!Regex.IsMatch(password1, @"[a-z]"))
                {
                    securePassword = false;
                }
                if (!Regex.IsMatch(password1, @"\d"))
                {
                    securePassword = false;
                }
                if (!Regex.IsMatch(password1, @"[\W_]"))
                {
                    securePassword = false;
                }

                if (!securePassword)
                {
                    MessageBox.Show("Check password security", "Caution", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    string[] args = new string[] { username, password1};

                    string output = Functions.python_execution("check_username", args);
                    
                    if (output.Contains("valid"))
                    {
                        Functions.python_execution("add_user", args);
                        var mainWindow = Window.GetWindow(this) as MainWindow;
                        mainWindow?.load_page("login");
                    }
                    else
                    {
                        MessageBox.Show("Username already registered", "Caution", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
        }


        private void backButton_click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            mainWindow?.load_page("login");
        }
    }
}
