using PasswordManager.Properties;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PasswordManager.View.Pages
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : UserControl
    {
        public Login()
        {
            InitializeComponent();
        }

        private void enterButton_click(object sender, MouseButtonEventArgs e)
        {
            string filename = "check_login.py";
            string username = Username.textBox.Text;
            string password = Password.passwordBox.Password;
            string[] args = { username, password };

            string result = Functions.python_execution(filename, args);

            if (result.Contains("True"))
            {
                var mainWindow = Window.GetWindow(this) as MainWindow;
                mainWindow?.load_page("main");
            }
        }

        private void createAccountButton_click(object sender, MouseButtonEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            mainWindow?.load_page("addUser");
        }
    }
}
