using System;
using System.Diagnostics;

namespace PasswordManager.Properties
{
    internal class Functions
    {

        public static string python_execution(string script, string[] variables)
        {
            Process process = new Process();

            process.StartInfo.FileName = "python";

            process.StartInfo.Arguments = $"PythonFiles/{script}";

            string arguments = string.Join(" ", variables);

            process.StartInfo.Arguments += $" {arguments}";

            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;

            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            Console.WriteLine(output);
            return output;
        }

        public static string check_dependences()
        {
            return "";
        }

    }
}
