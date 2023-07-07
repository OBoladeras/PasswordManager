using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace PasswordManager.View.UserControls
{
    /// <summary>
    /// Interaction logic for CustomButton.xaml
    /// </summary>
    public partial class CustomButton : UserControl
    {
        public CustomButton()
        {
            InitializeComponent();
        }


        private string primary;
        private string secundary;
        private string text;
        private int textsize;

        public string Primary
        {
            get { return primary; }
            set
            {
                primary = value;
                button_obj.Background = Functions.get_color(primary);
            }
        }
        public string Secundary
        {
            get { return secundary; }
            set
            {
                secundary = value;
            }
        }
        public string Text
        {
            get { return text; }
            set
            {
                text = value;
                placeHolder.Text = text;
            }
        }
        public int TextSize
        {
            get { return textsize; }
            set
            {
                textsize = value;
                placeHolder.FontSize = textsize;
            }
        }

        private void Border_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            button_obj.Background = Functions.get_color(secundary);
            Mouse.OverrideCursor = Cursors.Hand;
        }

        private void Border_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            button_obj.Background = Functions.get_color(primary);
            Mouse.OverrideCursor = Cursors.Arrow;
        }
    }
}
