using System.Collections.Generic;
using System.IO;
using System.Windows.Media;

namespace PasswordManager
{
    internal class Functions
    {
        // Cache variables
        public static string Username { get; set; }

        public static Brush PrimaryColor { get; set; }
        public static Brush SecundaryColor { get; set; }
        public static Brush Complementary { get; set; }
        public static Brush TextColor { get; set; }

        public static string CustomColor { get; set; }

        public static int ColorSelectedIndex { get; set; }


        public static Brush get_color(string hex)
        {
            BrushConverter brushConverter = new BrushConverter();
            Brush brush = (Brush)brushConverter.ConvertFrom(hex);

            return brush;
        }


        public static void DeleteItemAtIndex(int index)
        {
            string filePath = $"Data/{Username}_data.txt";
            
            string[] lines = File.ReadAllLines(filePath);

            if (index >= 0 && index < lines.Length)
            {
                List<string> linesList = new List<string>(lines);
                linesList.RemoveAt(index);

                File.WriteAllLines(filePath, linesList);
            }
        }
    }
}
