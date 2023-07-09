using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace PasswordManager.View.UserControls
{
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
                    txtBlock.FontWeight = FontWeights.Bold;
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
                imgIcon.Source = new BitmapImage(new Uri($"../../Images/{iconImage}.png", UriKind.Relative));
            }
        }

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Hand;
            txtBlock.FontWeight = FontWeights.Bold;
        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            if (highlight != 1)
            {
                Mouse.OverrideCursor = Cursors.Arrow;
                txtBlock.FontWeight = FontWeights.Normal;
            }
        }
    }
}
