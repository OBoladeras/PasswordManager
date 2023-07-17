using System;
using System.Windows;
using System.Windows.Input;

namespace PasswordManager
{
    public partial class PopUpWindow : Window
    {
        public static string[] data { get; set; }

        public string clickedItem;
        private bool closeSee = false;
        private bool closeCopy = false;

        
        public PopUpWindow()
        {
            InitializeComponent();

            // Blur main window
            var blurEffect = new System.Windows.Media.Effects.BlurEffect();
            blurEffect.Radius = 8;

            var mainWindow = Application.Current.MainWindow;
            if (mainWindow != null)
            {
                mainWindow.Effect = blurEffect;
            }

            this.Closed += PopUpWindow_Closed;
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
                    See.Visibility = Visibility.Hidden;
                    Copy.Visibility = Visibility.Hidden;
                }
                else if (answer == "SeeCopy")
                {
                    Yes.Visibility = Visibility.Hidden;
                    No.Visibility = Visibility.Hidden;
                    Ok.Visibility = Visibility.Hidden;
                    See.Visibility = Visibility.Visible;
                    Copy.Visibility = Visibility.Visible;

                    messageBox.Text = "*******";
                }
                else        
                {
                    Yes.Visibility = Visibility.Hidden;
                    No.Visibility = Visibility.Hidden;
                    Ok.Visibility = Visibility.Visible;
                    See.Visibility = Visibility.Hidden;
                    Copy.Visibility = Visibility.Hidden;
                }
            }
        }

        private void Yes_Click(object sender, MouseButtonEventArgs e)
        {
            clickedItem = "yes";
            Close();
        }
        private void No_Click(object sender, MouseButtonEventArgs e)
        {
            clickedItem = "no";
            Close();
        }

        private void See_Click(object sender, MouseButtonEventArgs e)
        {
            if (!closeSee)
            {
                string password = SecurityProcesses.Get_Password(data[0], data[1]);

                messageBox.Text = password;
                closeSee = true;
                See.Text = "Close";

                if (closeCopy)
                {
                    Yes.Visibility = Visibility.Hidden;
                    No.Visibility = Visibility.Hidden;
                    Ok.Visibility = Visibility.Visible;
                    See.Visibility = Visibility.Hidden;
                    Copy.Visibility = Visibility.Hidden;

                    Ok.Text = "Close";
                }
            }
            else { Close(); }
        }
        private void Copy_Click(object sender, MouseButtonEventArgs e)
        {
            if (!closeCopy)
            {
                string password = SecurityProcesses.Get_Password(data[0], data[1]);

                Clipboard.SetText(password);
                closeCopy = true;
                Copy.Text = "Close";

                if (closeSee)
                {
                    Yes.Visibility = Visibility.Hidden;
                    No.Visibility = Visibility.Hidden;
                    Ok.Visibility = Visibility.Visible;
                    See.Visibility = Visibility.Hidden;
                    Copy.Visibility = Visibility.Hidden;

                    Ok.Text = "Close";
                }
            }
            else { Close(); }
        }

        private void Ok_Click(object sender, MouseButtonEventArgs e)
        {
            clickedItem = "ok";
            Close();
        }


        private void PopUpWindow_Closed(object sender, EventArgs e)
        {
            var mainWindow = Application.Current.MainWindow;
            if (mainWindow != null)
            {
                mainWindow.Effect = null;
            }
        }
    }
}
