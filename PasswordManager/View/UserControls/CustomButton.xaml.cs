using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

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
        private string icon;

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
        public string Icon
        {
            get { return icon; }
            set
            {
                icon = value;
                string imagePath = $"pack://application:,,,/Images/{icon}.png";
                placeHolderImage.Source = new BitmapImage(new Uri(imagePath));
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
                if (Functions.PrimaryColor != null)
                {
                    button_obj.Background = Functions.PrimaryColor;
                }
                else
                {
                    button_obj.Background = Functions.get_color("blue");
                }
            }
            else
            {
                button_obj.Background = Functions.get_color(primary);
            }
        }
    }
}
