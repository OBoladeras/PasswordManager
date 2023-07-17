using System.Windows;
using System.Windows.Input;

namespace PasswordManager
{
    public partial class EditMenu : Window
    {

        public int itemIndex;

        public EditMenu()
        {
            InitializeComponent();

            editMenu.Background = Functions.Complementary;
        }

        // Pop Up window
        private void closePopUpButton_MouseEnter(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Hand;
        }

        private void closePopUpButton_MouseLeave(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Arrow;
        }

        private void updatePasswordBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PopUpWindow popUp = new PopUpWindow();

            string username = Functions.Username;
            string web = webpageEditBox.textBox.Text;
            string email = usernameEditBox.textBox.Text;
            string password = passwordEditBox.passwordBox.Password;

            if (web == "" || web == null)
            {
                popUp.Message = "All labels must be filled";
                popUp.ShowDialog();
            }
            else if (email == "" || email == null)
            {
                popUp.Message = "All labels must be filled";
                popUp.ShowDialog();
            }
            else if (password == "" || password == null)
            {
                popUp.Message = "Write a password";
                popUp.ShowDialog();
            }
            else
            {
                popUp.Message = "Update password?";
                popUp.Answer = "YesNo";
                popUp.ShowDialog();

                if (popUp.clickedItem == "yes")
                {
                    Functions.DeleteItemAtIndex(itemIndex);
                    SecurityProcesses.Save_Password(web, email, password);

                    Close();
                }
            }

            popUp.Close();
        }
    }
}
