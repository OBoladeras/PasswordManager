using System.Windows;

namespace PasswordManager
{
    public partial class PopUpWindow : Window
    {
        public static string[] data { get; set; }

        public string clickedItem;
        private bool closeShow = false;
        private bool closeCopy = false;

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
                    Show.Visibility = Visibility.Hidden;
                    Copy.Visibility = Visibility.Hidden;
                }
                else if (answer == "ShowCopy")
                {
                    Yes.Visibility = Visibility.Hidden;
                    No.Visibility = Visibility.Hidden;
                    Ok.Visibility = Visibility.Hidden;
                    Show.Visibility = Visibility.Visible;
                    Copy.Visibility = Visibility.Visible;

                    messageBox.Text = "*******";
                }
                else        
                {
                    Yes.Visibility = Visibility.Hidden;
                    No.Visibility = Visibility.Hidden;
                    Ok.Visibility = Visibility.Visible;
                    Show.Visibility = Visibility.Hidden;
                    Copy.Visibility = Visibility.Hidden;
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

        private void Show_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!closeShow)
            {
                string password = Functions.python_execution("get_password", data);

                messageBox.Text = password;
                closeShow = true;
                Show.Text = "Close";

                if (closeCopy)
                {
                    Yes.Visibility = Visibility.Hidden;
                    No.Visibility = Visibility.Hidden;
                    Ok.Visibility = Visibility.Visible;
                    Show.Visibility = Visibility.Hidden;
                    Copy.Visibility = Visibility.Hidden;

                    Ok.Text = "Close";
                }
            }
            else { Close(); }
        }
        private void Copy_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!closeCopy)
            {
                string password = Functions.python_execution("get_password", data);

                Clipboard.SetText(password);
                closeCopy = true;
                Copy.Text = "Close";

                if (closeShow)
                {
                    Yes.Visibility = Visibility.Hidden;
                    No.Visibility = Visibility.Hidden;
                    Ok.Visibility = Visibility.Visible;
                    Show.Visibility = Visibility.Hidden;
                    Copy.Visibility = Visibility.Hidden;

                    Ok.Text = "Close";
                }
            }
            else { Close(); }
        }

        private void Ok_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            clickedItem = "ok";
            Close();
        }
    }
}
