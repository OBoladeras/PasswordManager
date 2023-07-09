using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PasswordManager.View.UserControls
{
    public partial class LeftMenu : UserControl
    {

        public static int index { get; set; }

        public LeftMenu()
        {
            InitializeComponent();

            set_settings();
        }

        private void dashboardButton_click(object sender, MouseButtonEventArgs e)
        {
            index = 0;

            var mainWindow = Window.GetWindow(this) as MainWindow;
            mainWindow?.load_page("dashboard");
        }

        private void saveButton_click(object sender, MouseButtonEventArgs e)
        {
            index = 1;

            var mainWindow = Window.GetWindow(this) as MainWindow;
            mainWindow?.load_page("save");
        }

        private void settingsButton_click(object sender, MouseButtonEventArgs e)
        {
            index = 2;

            var mainWindow = Window.GetWindow(this) as MainWindow;
            mainWindow?.load_page("settings");
        }

        private void exitbutton_click(object sender, MouseButtonEventArgs e)
        {
            Functions.Username = "";

            var mainWindow = Window.GetWindow(this) as MainWindow;
            mainWindow?.load_page("login");
        }


        private void set_settings()
        {
            username.Text = Functions.Username;
            mainGrid.Background = Functions.PrimaryColor;
            divider.Fill = Functions.TextColor;
            dashboardbtn.Foreground = Functions.TextColor;
            savebtn.Foreground = Functions.TextColor;
            settingsbtn.Foreground = Functions.TextColor;
            exitButton.Foreground = Functions.TextColor;

            if (index == 0)
            {
                dashboardbtn.HighLight = 1;
            }
            else if (index == 1)
            {
                savebtn.HighLight = 1;
            }
            else if (index == 2)
            {
                settingsbtn.HighLight = 1;
            }
        }
    }
}
