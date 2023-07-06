using System.Windows.Controls;

namespace PasswordManager.View.UserControls
{
    /// <summary>
    /// Interaction logic for InputBox.xaml
    /// </summary>
    public partial class InputBox : UserControl
    {
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
    }
}
