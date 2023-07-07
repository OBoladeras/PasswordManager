using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace PasswordManager.View.UserControls
{
    /// <summary>
    /// Interaction logic for MenuItem.xaml
    /// </summary>
    public partial class MenuItem : UserControl
    {
        public MenuItem()
        {
            InitializeComponent();
        }

        private int highlight;
        private string text;
        private string iconImage;

        public int HighLight
        {
            get { return highlight; }
            set
            {
                highlight = value;
                if (highlight == 1)
                {
                    txtBlock.Foreground = Functions.get_color("white");
                }
                else
                {
                    txtBlock.Foreground = Functions.get_color("gray");
                }
            }
        }
        public string Text
        {
            get { return text; }
            set
            {
                text = value;
                txtBlock.Text = text;
            }
        }
        public string IconImage
        {
            get { return iconImage; }
            set
            {
                iconImage = value;
                imgIcon.Source = new BitmapImage(new Uri($"../../Image/{iconImage}", UriKind.Relative));
            }
        }

        private void Grid_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            txtBlock.Foreground = Functions.get_color("white");
        }

        private void Grid_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            txtBlock.Foreground = Functions.get_color("gray");
        }
    }
}
