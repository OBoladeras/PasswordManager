using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PasswordManager.View.Pages
{
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

        private void passwordlbl_text_changed(object sender, RoutedEventArgs e)
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

        private void CreateUser_Click(object sender, MouseButtonEventArgs e)
        {
            CreateUser();
        }

        private void backButton_click(object sender, MouseButtonEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            mainWindow?.load_page("login");
        }

        private void Username_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                lblpassword.Focus();
            }
        }

        private void lblpassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                PasswordConfirm.Focus();
            }
        }

        private void PasswordConfirm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                CreateUser();

            }
        }


        private void CreateUser()
        {
            PopUpWindow popUp = new PopUpWindow();

            bool securePassword = true;
            string username = Username.textBox.Text;
            string password1 = lblpassword.passwordBox.Password;
            string password2 = PasswordConfirm.passwordBox.Password;

            if (username == "")
            {
                popUp.Message = "Write a username";
                popUp.ShowDialog();
            }
            else if (password1 == "" || password2 == "")
            {
                popUp.Message = "Dont leave blank password";
                popUp.ShowDialog();
            }
            else if (password1 != password2)
            {
                popUp.Message = "Passwords don't match";
                popUp.ShowDialog();
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
                    popUp.Message = "Check password security";
                    popUp.ShowDialog();
                }
                else
                {
                    if (SecurityProcesses.Check_Valid_Username(username))
                    {
                        SecurityProcesses.Create_User(username, password1);

                        var mainWindow = Window.GetWindow(this) as MainWindow;
                        mainWindow?.load_page("login");
                    }
                    else
                    {
                        popUp.Message = "Username already registered";
                        popUp.ShowDialog();
                    }
                }
            }

            popUp.Close();
        }
    }
}
