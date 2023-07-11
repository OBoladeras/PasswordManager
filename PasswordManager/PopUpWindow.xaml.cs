using System.Windows;

namespace PasswordManager
{
    public partial class PopUpWindow : Window
    {

        public string clickedItem;

        public PopUpWindow()
        {
            InitializeComponent();
        }

        private string message;
        private string answer;

        public string Message
        {
            get { return message; }
            set
            {
                message = value;
                messageBox.Text = message;
            }
        }
        public string Answer
        {
            get { return answer; }
            set
            {
                answer = value;
                if (answer == "YesNo")
                {
                    Yes.Visibility = Visibility.Visible;
                    No.Visibility = Visibility.Visible;
                    Ok.Visibility = Visibility.Hidden;
                }
                else
                {
                    Yes.Visibility = Visibility.Hidden;
                    No.Visibility = Visibility.Hidden;
                    Ok.Visibility = Visibility.Visible;
                }
            }
        }

        private void Yes_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            clickedItem = "yes";
            Close();
        }
        private void No_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            clickedItem = "no";
            Close();
        }
        private void Ok_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            clickedItem = "ok";
            Close();
        }
    }
}
