using Microsoft.Win32;

namespace AnLH_EyeProtected
{
    public class RegistryHelper
    {
        public static bool WriteToCurrentUserSetting(string currentUsername, string key, string data)
        {
            bool output = false;
             
            try
            {
                using (RegistryKey myUserCustomRegistry = Registry.CurrentUser.CreateSubKey(key))
                {
                    myUserCustomRegistry.SetValue(currentUsername, data);
                    output = true;
                }
                
            }
            catch
            {
                output = false;
            }
            return output;
        }


        public static bool WriteToAutoRunOnStartWindwosRegistry(string applicationName, string applicationPath)
        {
            bool output = false;

            try
            {
                using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
                {
                    registryKey.SetValue(applicationName, applicationPath);
                    output = true;
                }
            }
            catch 
            {
                output = false;
            }
            
            return output;
        }
        public static string ReadFromAutoRunOnStartWindwosRegistry(string applicationName)
        {
            string output = null;

            using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                registryKey.GetValue(applicationName);
                output = registryKey.GetValue(applicationName).ToString();
            }
            return output;
        }

        public static bool DeleteToAutoRunOnStartWindwosRegistry(string applicationName)
        {
            bool output = false;

            try
            {
                using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
                {
                    registryKey.DeleteValue(applicationName);
                    output = true;
                }
            }
            catch { }
           
            return output;
        }


        public static string ReadFromCurrentUserSetting(string currentUsername, string key)
        {
            string output = null;
            try
            {
                using (RegistryKey MyRegistryKey = Registry.CurrentUser.OpenSubKey(key))
                {
                    output = MyRegistryKey.GetValue(currentUsername).ToString();
                }
            }
            catch { }
            return output;
        }
    }
}
