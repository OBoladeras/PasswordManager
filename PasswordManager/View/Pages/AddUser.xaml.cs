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

        private void CustomButton_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }

        private void backButton_click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            mainWindow?.load_page("login");
        }
    }
}
