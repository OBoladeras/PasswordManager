using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PasswordManager.View.Pages
{
    public partial class Save : UserControl
    {
        public Save()
        {
            InitializeComponent();

            username.Text = Functions.Username;
        }

        // Menu Items Handle
        private void dashboard_click(object sender, MouseButtonEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            mainWindow?.load_page("dashboard");
        }

        private void exitbutton_click(object sender, MouseButtonEventArgs e)
        {
            Functions.Username = "";

            var mainWindow = Window.GetWindow(this) as MainWindow;
            mainWindow?.load_page("login");
        }


        // Page Handle
        private void randomPasswordButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string[] variables = { };

            string password = Functions.python_execution("generate_password", variables);
            passwordLbl.passwordBox.Password = password;
            passwordLbl.placeHolder.Visibility = Visibility.Hidden;
        }

        private void saveButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string username = Functions.Username;
            string web = webpageLbl.textBox.Text;
            string email = usernameLbl.textBox.Text;
            string password = passwordLbl.passwordBox.Password;
            string[] variables = { username, web, email, password };

            Functions.python_execution("save_password", variables);

            webpageLbl.textBox.Text = "";
            usernameLbl.textBox.Text = "";
            passwordLbl.passwordBox.Password = "";
        }

        private void webpageLbl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                username.Focus();
            }
        }

        private void usernameLbl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                passwordLbl.Focus();
            }
        }
    }
}
