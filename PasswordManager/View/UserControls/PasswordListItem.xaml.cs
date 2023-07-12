using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PasswordManager.View.UserControls
{
    public partial class PasswordListItem : UserControl
    {

        public event EventHandler EditButtonClicked;
        public event EventHandler RemoveButtonClicked;
        public event EventHandler PanelClick;

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

        // Visual
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

        private void webpagelbl_MouseEnter(object sender, MouseEventArgs e)
        {
            webpagelbl.Foreground = Functions.get_color("blue");
            Mouse.OverrideCursor = Cursors.Hand;
        }

        private void webpagelbl_MouseLeave(object sender, MouseEventArgs e)
        {
            webpagelbl.Foreground = Functions.get_color("black");
            Mouse.OverrideCursor = Cursors.Arrow;
        }

        private void editButton_MouseEnter(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Hand;
        }

        private void editButton_MouseLeave(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Arrow;
        }

        private void removeButton_MouseEnter(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Hand;
        }

        private void removeButton_MouseLeave(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Arrow;
        }


        private void webpagelbl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Process.Start($"http://{webpagetxt}");
            }
            catch
            {
                PopUpWindow popUp = new PopUpWindow();
                popUp.Message = "Error opening the webpage";
                popUp.ShowDialog();
            }
        }

        private void editButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            EditButtonClicked?.Invoke(this, EventArgs.Empty);
        }

        private void removeButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            RemoveButtonClicked?.Invoke(this, EventArgs.Empty);
        }

        private void panel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PanelClick?.Invoke(this, e);
        }

        private void Rectangle_MouseEnter(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Hand;
        }

        private void Rectangle_MouseLeave(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Arrow;
        }
    }
}
