using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace PasswordManager
{
    internal class SecurityProcesses
    {
        private static string usersFile = "Data/users.txt";

        // Change this to use a secure key or generate with loops and make it more complex
        private static readonly byte[] Salt = Encoding.ASCII.GetBytes("exampleSalt");

        // Create User
        public static bool Check_Valid_Username(string username)
        {
            try
            {
                using (StreamReader reader = new StreamReader(usersFile))
                {
                    string line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.Contains(username))
                        {
                            return false;
                        }
                    }
                }
            }
            catch { }

            return true;
        }

        public static void Create_User(string username, string password)
        {
            password = EncryptPassword(password);

            string line = $"{username};{password}";

            if (!Directory.Exists("Data"))
            {
                Directory.CreateDirectory("Data");
            }
            if (!File.Exists(usersFile))
            {
                File.Create(usersFile);
            }

            if (File.Exists(usersFile))
            {
                using (StreamWriter writer = File.AppendText(usersFile))
                {
                    writer.WriteLine(line);
                }
            }

            File.Create($"Data/{username}_data.txt");
            File.Create($"Data/settings_{username}.txt");
        }


        // Login
        public static string Login(string username, string password)
        {
            bool usernameExists = false;
            password = EncryptPassword(password);

            try
            {
                using (StreamReader reader = new StreamReader(usersFile))
                {
                    string line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.Contains(username))
                        {
                            usernameExists = true;

                            string[] parts = line.Split(';');
                            if (parts.Length == 2)
                            {
                                if (parts[1] == password)
                                {
                                    return "valid";
                                }
                                else
                                {
                                    return "Wrong password";
                                }
                            }
                        }
                    }
                }
            }
            catch { return "Username not found";  }

            if (!usernameExists)
            {
                return "Username not found";
            }

            return "Unknow Error";
        }


        // Encryptation
        public static string Generate_Password()
        {
            Random random = new Random();

            string password = "";
            string[] Characters =
            {
                "ABCDEFGHIJKLMNOPQRSTUVWXYZ",
                "abcdefghijklmnopqrstuvwxyz",
                "0123456789",
                "!@#$%^&*()-=_+[]{}|:,.<>/?"
            };

            int i = 0;
            int len = random.Next(8, 15);
            while (i < len)
            {
                int a = random.Next(Characters.Length);
                int b = random.Next(Characters[a].Length);
                password += Characters[a][b];

                i++;
            }

            return password;
        }

        public static string EncryptPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();

                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }


        private static string Encrypt(string plainText)
        {
            string key = GenerateKey();
            byte[] encryptedBytes;

            using (AesManaged aes = new AesManaged())
            {
                Rfc2898DeriveBytes keyDerivation = new Rfc2898DeriveBytes(key, Salt);
                aes.Key = keyDerivation.GetBytes(aes.KeySize / 8);
                aes.IV = keyDerivation.GetBytes(aes.BlockSize / 8);

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }
                        encryptedBytes = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(encryptedBytes);
        }
        public static string Decrypt(string encryptedText)
        {
            string key = GenerateKey();
            byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
            string plainText;

            using (AesManaged aes = new AesManaged())
            {
                Rfc2898DeriveBytes keyDerivation = new Rfc2898DeriveBytes(key, Salt);
                aes.Key = keyDerivation.GetBytes(aes.KeySize / 8);
                aes.IV = keyDerivation.GetBytes(aes.BlockSize / 8);

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(encryptedBytes))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader(cryptoStream))
                        {
                            plainText = streamReader.ReadToEnd();
                        }
                    }
                }
            }

            return plainText;
        }

        private static string GenerateKey()
        {
            // Change this to use a secure key or generate with loops and make it more complex
            string key = "exampleKey";

            return key;
        }


        // Save and get passwords
        public static bool Save_Password(string web, string email, string password)
        {
            string fileName = $"Data/{Functions.Username}_data.txt";
            if (!File.Exists(fileName))
            {
                File.Create(fileName);
            }

            try
            {
                DateTime currentDate = DateTime.Today;
                string yearMonthDay = currentDate.ToString("yyyy-MM-dd");


                string line = $"{web};{email};{yearMonthDay};{password}";
                line = Encrypt(line);

                using (StreamWriter writer = File.AppendText(fileName))
                {
                    writer.WriteLine(line);
                }

                return true;
            }catch { return false; }
        }
        public static string Get_Password(string web, string email)
        {
            string fileName = $"Data/{Functions.Username}_data.txt";

            string[] lines = File.ReadAllLines(fileName);
            foreach (string line in lines)
            {
                string cleanLine = Decrypt(line);
                string[] fields = cleanLine.Split(';');
                if (fields[0] == web && fields[1] == email)
                {
                    return fields[3];
                }

            }
            return null;
        }
    }
}
