using System.Windows.Controls;
using System.Windows.Media;

namespace PasswordManager.View.UserControls
{
    /// <summary>
    /// Interaction logic for List_Item.xaml
    /// </summary>
    public partial class List_Item : UserControl
    {
        public List_Item()
        {
            InitializeComponent();
        }

        private Brush get_color(string hex)
        {
            BrushConverter brushConverter = new BrushConverter();
            Brush brush = (Brush)brushConverter.ConvertFrom(hex);

            return brush;
        }

        private string colorShowed;
        private string text;

        public string Color
        {
            get { return colorShowed; }
            set
            {
                colorShowed = value;
                circle.Fill = get_color(colorShowed);
                txtBox.Foreground = get_color(colorShowed);
            }
        }
        public string Text
        {
            get { return text; }
            set
            {
                text = value;
                txtBox.Text = text;
            }
        }
    }
}
