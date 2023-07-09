using System.Windows.Controls;
using System.Windows.Input;

namespace PasswordManager.View.UserControls
{
    public partial class PasswordListItem : UserControl
    {

        private int index;
        private string webpagetxt;
        private string gmailtxt;
        private string datetxt;

        public int Index
        {
            get { return index; }
            set
            {
                index = value;
                if ((index % 2) == 0)
                {
                    panel.Background = Functions.get_color("white");
                }
                else
                {
                    panel.Background = Functions.get_color("#d5d5d5");
                }
            }
        }
        public string WebpageTxt
        {
            get { return webpagetxt; }
            set
            {
                webpagetxt = value;
                webpagelbl.Text = webpagetxt;
            }
        }
        public string GmailTxt
        {
            get { return gmailtxt; }
            set
            {
                gmailtxt = value;
                emaillbl.Text = gmailtxt;
            }
        }
        public string DateTxt
        {
            get { return datetxt; }
            set
            {
                datetxt = value;
                datelbl.Text = datetxt;
            }
        }


        public PasswordListItem()
        {
            InitializeComponent();
        }

        private void StackPanel_MouseEnter(object sender, MouseEventArgs e)
        {
            panel.Background = Functions.get_color("#b2b2b2");
        }

        private void StackPanel_MouseLeave(object sender, MouseEventArgs e)
        {
            if ((index % 2)  == 0)
            {
                panel.Background = Functions.get_color("white");
            }
            else
            {
                panel.Background = Functions.get_color("#d5d5d5");
            }
        }
    }
}
