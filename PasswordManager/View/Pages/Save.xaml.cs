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
            PopUpWindow popUp = new PopUpWindow();

            string username = Functions.Username;
            string web = webpageLbl.textBox.Text;
            string email = usernameLbl.textBox.Text;
            string password = passwordLbl.passwordBox.Password;
            string[] variables = { username, web, email, password };

            if (web == null || web == "")
            {
                popUp.Message = "Write a web";
                popUp.ShowDialog();
                webpageLbl.textBox.Focus();
            }
            else if (email == null || email == "")
            {
                popUp.Message = "Write a email or username";
                popUp.ShowDialog();
                usernameLbl.textBox.Focus();
            }
            else if (password == null ||password == "")
            {
                popUp.Message = "Write a password";
                popUp.ShowDialog();
                passwordLbl.passwordBox.Focus();
            }
            else
            {
                string status = Functions.python_execution("save_password", variables);
                if (status.Contains("valid"))
                {
                    popUp.Message = "Password Saved";
                }
                else
                {
                    popUp.Message = "Error saving the password";
                }

                popUp.ShowDialog();

                webpageLbl.textBox.Text = "";
                usernameLbl.textBox.Text = "";
                passwordLbl.passwordBox.Password = "";
                passwordLbl.placeHolder.Visibility = Visibility.Visible;
                webpageLbl.textBox.Focus();

                popUp.Close();
            }
        }

        private void webpageLbl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                usernameLbl.Focus();
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
