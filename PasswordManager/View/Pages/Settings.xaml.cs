using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PasswordManager.View.Pages
{
    public partial class Settings : UserControl
    {

        string[] colorsGlobal;

        public Settings()
        {
            InitializeComponent();

            load_settings();
        }


        private void purpleBox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string[] colors = get_other_colors("#6C63FF");
            set_colors(colors, 1);

            reload();
        }

        private void greenBox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string[] colors = get_other_colors("#00BFA6");
            set_colors(colors, 2);

            reload();
        }

        private void magentaBox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string[] colors = get_other_colors("#F50057");
            set_colors(colors, 3);

            reload();
        }

        private void customColorBox_textChanged(object sender, EventArgs e)
        {
            string customColor = customColorBox.textBox.Text;
            bool isHexadecimal = false;
            if (customColor.Length == 7)
            {
                string clear = customColor.Substring(1);
                isHexadecimal = int.TryParse(clear, System.Globalization.NumberStyles.HexNumber, null, out _);
            }


            if (customColor.StartsWith("#") && isHexadecimal)
            {
                string[] colors = get_other_colors(customColor);
                if (customColor == "#6C63FF")
                {
                    set_colors(colors, 1);
                }
                else if (customColor == "#00BFA6")
                {
                    set_colors(colors, 2);
                }
                else if (customColor == "#F50057")
                {
                    set_colors(colors, 3);
                }
                else
                {
                    set_colors(colors, 0);
                }
                Functions.CustomColor = customColor;

                reload();
            }
        }

        private void saveButton_click(object sender, MouseButtonEventArgs e)
        {
            PopUpWindow popUp = new PopUpWindow();

            try
            {
                string settingsFilePath = $"Data/settings_{Functions.Username}.txt";
                string lineToAppend = $"colors;{colorsGlobal[0]};{colorsGlobal[1]};{colorsGlobal[2]};{colorsGlobal[3]}";

                if (File.Exists(settingsFilePath))
                {
                    List<string> lines = File.ReadAllLines(settingsFilePath)
                                             .Where(line => !line.Contains("colors"))
                                             .ToList();

                    lines.Add(lineToAppend);

                    File.WriteAllLines(settingsFilePath, lines);
                }
                else
                {
                    File.Create(settingsFilePath);
                    File.WriteAllText(settingsFilePath, lineToAppend);
                }

                popUp.Message = "Saved succesfully";
                popUp.ShowDialog();
            }
            catch
            {
                popUp.Message = "Error saving settings";
                popUp.ShowDialog();
            }

            popUp.Close();
        }

        private void deleteButton_Click(object sender, MouseButtonEventArgs e)
        {
            PopUpWindow popUp = new PopUpWindow();
            popUp.Message = "Sure you want to delete?";
            popUp.Answer = "YesNo";
            popUp.ShowDialog();

            if (popUp.clickedItem == "yes")
            {
                string[] argv = { Functions.Username };

                string userDataFile = $"Data/{Functions.Username}_data.txt";
                string settingsFile = $"Data/settings_{Functions.Username}.txt";
                string usersFile = "Data/users.txt";

                string[] files = { userDataFile, settingsFile };

                foreach (string file in files)
                {
                    try
                    {
                        File.Delete(file);
                    }
                    catch { Console.WriteLine($"File not found: {file}"); }
                }
                DeleteLineContainingWord(usersFile, Functions.Username);

                Functions.Username = "";

                var mainWindow = Window.GetWindow(this) as MainWindow;
                mainWindow?.load_page("login");
            }
        }

        private void DeleteLineContainingWord(string filePath, string wordToDelete)
        {
            try
            {
                string[] lines = File.ReadAllLines(filePath);

                string tempFilePath = filePath + ".tmp";

                using (StreamWriter writer = new StreamWriter(tempFilePath))
                {
                    foreach (string line in lines)
                    {
                        if (!line.Contains(wordToDelete))
                        {
                            writer.WriteLine(line);
                        }
                    }
                }

                File.Delete(filePath);
                File.Move(tempFilePath, filePath);
            }
            catch (IOException ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
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

            int softerRed = (int)((255 - red) * reductionPercentage) + red;
            int softerGreen = (int)((255 - green) * reductionPercentage) + green;
            int softerBlue = (int)((255 - blue) * reductionPercentage) + blue;

            string softerColor = string.Format("#{0:X2}{1:X2}{2:X2}", softerRed, softerGreen, softerBlue);
            string complementaryColor = string.Format("#{0:X2}{1:X2}{2:X2}", complementaryRed, complementaryGreen, complementaryBlue);


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

        private void set_colors(string[] colors, int selectedColor)
        {
            Functions.PrimaryColor = Functions.get_color(colors[0]);
            Functions.SecundaryColor = Functions.get_color(colors[1]);
            Functions.Complementary = Functions.get_color(colors[2]);
            Functions.TextColor = Functions.get_color(colors[3]);

            Functions.ColorSelectedIndex = selectedColor;

            colorsGlobal = colors;
        }


        private void reload()
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            mainWindow?.load_page("settings");
        }

        private void load_settings()
        {
            if (Functions.ColorSelectedIndex == 1)
            {
                purpleBox.Selected = true;
                greenBox.Selected = false;
                magentaBox.Selected = false;

                customColorBox.textBox.Text = "#6C63FF";
            }
            else if (Functions.ColorSelectedIndex == 2)
            {
                purpleBox.Selected = false;
                greenBox.Selected = true;
                magentaBox.Selected = false;

                customColorBox.textBox.Text = "#00BFA6";
            }
            else if (Functions.ColorSelectedIndex == 3)
            {
                purpleBox.Selected = false;
                greenBox.Selected = false;
                magentaBox.Selected = true;

                customColorBox.textBox.Text = "#F50057";
            }

            customColorBox.textBox.Text = Functions.CustomColor;
        }

    }
}
