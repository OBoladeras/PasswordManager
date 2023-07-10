using System.Windows.Controls;
using System.Windows.Input;

namespace PasswordManager.View.UserControls
{
    public partial class PasswordInputBox : UserControl
    {
        public PasswordInputBox()
        {
            InitializeComponent();
        }

        private string placeholder;

        public string PlaceHolder
        {
            get { return placeholder; }
            set
            {
                placeholder = value;
                placeHolder.Text = placeholder;
            }
        }


        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            passwordBox.Focus();

            if (passwordBox.Password == "")
            {
                placeHolder.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                placeHolder.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        private void textBox_IsKeyboardFocusedChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            if (passwordBox.IsKeyboardFocused)
            {
                placeHolder.Visibility = System.Windows.Visibility.Hidden;
            }
            else
            {
                if (passwordBox.Password == "")
                {
                    placeHolder.Visibility = System.Windows.Visibility.Visible;
                }
            }
        }

        private void passwordBox_MouseEnter(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.IBeam;
        }

        private void passwordBox_MouseLeave(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Arrow;
        }
    }
}
