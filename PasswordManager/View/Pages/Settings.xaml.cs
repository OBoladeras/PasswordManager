using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PasswordManager.View.Pages
{
    public partial class Settings : UserControl
    {

        string filePath = $"../../Data/settings_{Functions.Username}.txt";

        public Settings()
        {
            InitializeComponent();

            load_settings();
        }


        private void purpleBox_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            string[] colors = get_other_colors("#6C63FF");
            set_colors(colors, 1);

            reload();
        }

        private void greenBox_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            string[] colors = get_other_colors("#00BFA6");
            set_colors(colors, 2);

            reload();
        }

        private void magentaBox_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            string[] colors = get_other_colors("#F50057");
            set_colors(colors, 3);

            reload();
        }



        private void set_colors(string[] colors, int selectedColor)
        {
            Functions.PrimaryColor = Functions.get_color(colors[0]);
            Functions.SecundaryColor = Functions.get_color(colors[1]);
            Functions.Complementary = Functions.get_color(colors[2]);
            Functions.TextColor = Functions.get_color(colors[3]);

            Functions.ColorSelectedIndex = selectedColor;

            string lineToAppend = $"colors;{colors[0]};{colors[1]};{colors[2]};{colors[3]}";

            List<string> lines = File.ReadAllLines(filePath)
                .Where(line => !line.Contains("color"))
                .ToList();

            lines.Add(lineToAppend);

            File.WriteAllLines(filePath, lines);
        }

        private string[] get_other_colors(string originalColor)
        {
            int red = Convert.ToInt32(originalColor.Substring(1, 2), 16);
            int green = Convert.ToInt32(originalColor.Substring(3, 2), 16);
            int blue = Convert.ToInt32(originalColor.Substring(5, 2), 16);

            double reductionPercentage = 0.5;
            red = (int)(red * reductionPercentage);
            green = (int)(green * reductionPercentage);
            blue = (int)(blue * reductionPercentage);

            int complementaryRed = 255 - red;
            int complementaryGreen = 255 - green;
            int complementaryBlue = 255 - blue;

            string complementaryColor = string.Format("#{0:X2}{1:X2}{2:X2}", complementaryRed, complementaryGreen, complementaryBlue);
            string softerColor = string.Format("#{0:X2}{1:X2}{2:X2}", red, green, blue);

            double brightness = (red * 0.299 + green * 0.587 + blue * 0.114) / 255;
            double brightnessThreshold = 0.5;

            string textColor;
            if (brightness > brightnessThreshold)
            {
                textColor = "#000000";
            }
            else
            {
                textColor = "#FFFFFF";
            }


            string[] colors = { originalColor, softerColor, complementaryColor, textColor };
            return colors;
        }

        private void load_settings()
        {
            if (Functions.ColorSelectedIndex == 1)
            {
                purpleBox.Selected = true;
                greenBox.Selected = false;
                magentaBox.Selected = false;
            }
            else if (Functions.ColorSelectedIndex == 2)
            {
                purpleBox.Selected = false;
                greenBox.Selected = true;
                magentaBox.Selected = false;
            }
            else if (Functions.ColorSelectedIndex == 3)
            {

                Console.WriteLine("asdddddddddd");

                purpleBox.Selected = false;
                greenBox.Selected = false;
                magentaBox.Selected = true;
            }
        }

        private void reload()
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            mainWindow?.load_page("settings");
        }
    }
}
