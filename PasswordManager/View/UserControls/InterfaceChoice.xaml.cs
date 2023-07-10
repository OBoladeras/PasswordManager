using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PasswordManager.View.UserControls
{
    public partial class InterfaceChoice : UserControl
    {
        public InterfaceChoice()
        {
            InitializeComponent();

        }

        private int index;
        private bool selected;

        public int Index
        {
            get { return index; }
            set
            {
                index = value;
                set_color();
            }
        }
        public bool Selected
        {
            get { return selected; }
            set
            { selected = value; }
        }

        private void set_color()
        {
            Brush color;
            Brush black = Functions.get_color("black");
            backgroundImage.Source = new BitmapImage(new Uri($"../../Images/color{index}.png", UriKind.Relative));

            if (index == 1)
            {
                colorName.Text = "Purple";
                color = Functions.get_color("#6C63FF");
            }
            else if (index == 2)
            {
                colorName.Text = "Green";
                color = Functions.get_color("#00BFA6");
            }
            else
            {
                colorName.Text = "Magenta";
                color = Functions.get_color("#F50057");
            }

            if (selected == true)
            {
                bigBorder.BorderBrush = color;
                smallBorder.BorderBrush = color;

                checkBoxCircle.Background = color;
                checkBoxCircle.BorderBrush = color;
                checkBoxCircle.BorderThickness = new System.Windows.Thickness(0);

            }
            else
            {
                bigBorder.BorderThickness = new System.Windows.Thickness(1);
                smallBorder.BorderThickness = new System.Windows.Thickness(1);
                bigBorder.BorderBrush = black;
                smallBorder.BorderBrush = black;

                checkBoxCircle.Background = null;
                checkBoxCircle.BorderBrush = black;
                checkBoxCircle.BorderThickness = new System.Windows.Thickness(2);
            }
        }

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Hand;
        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Arrow;
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
