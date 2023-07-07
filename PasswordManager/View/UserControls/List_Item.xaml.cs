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

        private string colorShowed;
        private string text;

        public string Color
        {
            get { return colorShowed; }
            set
            {
                colorShowed = value;
                circle.Fill = Functions.get_color(colorShowed);
                txtBox.Foreground = Functions.get_color(colorShowed);
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
