using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace PasswordManager.View.UserControls
{
    public partial class InputBox : UserControl
    {

        public event EventHandler textChanged;

        public InputBox()
        {
            InitializeComponent();
        }

        private string placeholdertext;

        public string PlaceHolderText
        {
            get { return placeholdertext; }
            set
            {
                placeholdertext = value;
                placeHolder.Text = placeholdertext;
            }
        }


        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            textBox.Focus();

            if (textBox.Text == "")
            {
                placeHolder.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                placeHolder.Visibility = System.Windows.Visibility.Hidden;
            }

            textChanged?.Invoke(this, EventArgs.Empty);
        }

        private void textBox_IsKeyboardFocusedChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            if (textBox.IsKeyboardFocused)
            {
                placeHolder.Visibility = System.Windows.Visibility.Hidden;
            }
            else
            {
                if (textBox.Text == "")
                {
                    placeHolder.Visibility = System.Windows.Visibility.Visible;
                }
            }
        }

        private void textBox_MouseEnter(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.IBeam;
        }

        private void textBox_MouseLeave(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Arrow;
        }
    }
}
