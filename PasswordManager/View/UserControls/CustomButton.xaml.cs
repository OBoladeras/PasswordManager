using System.Windows.Controls;
using System.Windows.Input;

namespace PasswordManager.View.UserControls
{
    public partial class CustomButton : UserControl
    {
        public CustomButton()
        {
            InitializeComponent();

            load_settings();
        }


        private string text;
        private int textsize;
        private string primary;
        private string secundary;

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

        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            if (secundary == null || secundary == "")
            {
                button_obj.Background = Functions.SecundaryColor;
            }
            else
            {
                button_obj.Background = Functions.get_color(secundary);
            }
            Mouse.OverrideCursor = Cursors.Hand;
        }

        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            if (primary == null || primary == "")
            {
                button_obj.Background = Functions.PrimaryColor;
            }
            else
            {
                button_obj.Background = Functions.get_color(primary);
            }
            Mouse.OverrideCursor = Cursors.Arrow;
        }

        private void load_settings()
        {
            if (primary == null || primary == "")
            {
                button_obj.Background = Functions.PrimaryColor;
            }
            else
            {
                button_obj.Background = Functions.get_color(primary);
            }
        }
    }
}
