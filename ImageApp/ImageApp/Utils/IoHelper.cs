using System;
using System.IO;

namespace ImageApp.Utils
{
    public static class IoHelper
    {
        /// <summary>
        /// C:\Users\(username)\AppData\Local\<see cref="Constants.AppName"/>
        /// </summary>
        public static readonly string LocalAppDataPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            Constants.AppName);

        /// <summary>
        /// Attempts to create a directory if it does not already exist
        /// </summary>
        /// <param name="path">Path to the directory</param>
        /// <returns>true if successful, or if the directory was already there</returns>
        public static bool TryCreateDirectoryIfNotExists(string path)
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                return Directory.Exists(path);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Attempts to write the specified text to the specified path (overwriting if already there)
        /// </summary>
        /// <param name="path">Path to write to</param>
        /// <param name="text">Text to write</param>
        /// <returns>true if successful</returns>
        public static bool TryWriteFile(string path, string text)
        {
            try
            {
                File.WriteAllText(path, text);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Attempts to append the specified text to the file at the specified path
        /// </summary>
        /// <param name="path">Path to the file</param>
        /// <param name="text">Text to append</param>
        /// <returns>true if successful</returns>
        public static bool TryAppendToFile(string path, string text)
        {
            try
            {
                File.AppendAllText(path, text);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
