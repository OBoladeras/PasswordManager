using System;
using System.Diagnostics;
using System.Windows.Media;

namespace PasswordManager
{
    internal class Functions
    {
        // Cache variables
        public static string Username { get; set; }

        public static Brush PrimaryColor { get; set; }
        public static Brush SecundaryColor { get; set; }
        public static Brush TextColor { get; set; }
        public static Brush Complementary { get; set; }



        // Custom color 1 = #6C63FF
        // Custom color 2 = #00BFA6
        // Custom color 3 = #F50057


        public static string python_execution(string script, string[] variables)
        {
            Process process = new Process();

            process.StartInfo.FileName = "python";

            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            process.StartInfo.Arguments = $"PythonFiles/{script}.py";
            process.StartInfo.Arguments = $"{basePath}/../../PythonFiles/{script}.py";

            string arguments = string.Join(" ", variables);

            process.StartInfo.Arguments += $" {arguments}";

            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;

            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            Console.WriteLine("Python output:");
            Console.WriteLine(output);
            return output;
        }

        public static string check_dependences()
        {
            return "";
        }


        public static Brush get_color(string hex)
        {
            BrushConverter brushConverter = new BrushConverter();
            Brush brush = (Brush)brushConverter.ConvertFrom(hex);

            return brush;
        }
    }
}
