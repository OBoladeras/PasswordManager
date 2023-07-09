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
    }
}
