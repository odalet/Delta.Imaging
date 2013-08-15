using System;
using System.IO;
using Microsoft.Win32;
using System.Diagnostics;


namespace Delta.CertXplorer.ApplicationModel
{
    public static class AutoStart
    {
        public static bool RegisterApplication()
        {
            return RegisterApplication(GetCurrentApplicationPath());
        }

        public static bool RegisterApplication(string path)
        {
            return RegisterApplication(path,
                Path.GetFileNameWithoutExtension(path));
        }

        public static bool RegisterApplication(string path, string key)
        {
            return RegisterApplication(path, key, true);
        }

        public static bool RegisterApplication(string path, string key, bool localMachine)
        {
            try
            {
                RegistryKey root = localMachine ? Registry.LocalMachine : Registry.CurrentUser;
                RegistryKey rk = root.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);

                if (rk.GetValue(key) != null) rk.DeleteValue(key);
                rk.SetValue(key, path, RegistryValueKind.String);
            }
            catch (Exception ex)
            {
                This.Logger.Error(string.Format("Unable to register autorun for application {0} (path is {1}",
                    key, path), ex);
                return false;
            }

            return true;
        }

        public static bool UnregisterApplication()
        {
            return UnregisterApplication(GetCurrentApplicationKey());
        }

        public static bool UnregisterApplication(string key)
        {
            // We unregister by deleteing a key both in HKLM and HKCU - if found.
            try
            {
                RegistryKey rk = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
                if (rk.GetValue(key) != null) rk.DeleteValue(key);

                rk = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
                if (rk.GetValue(key) != null) rk.DeleteValue(key);
            }
            catch (Exception ex)
            {
                This.Logger.Error(string.Format("Unable to unregister autorun for application {0}", key), ex);
                return false;
            }

            return true;
        }

        public static bool IsRegisteredApplication()
        {
            return IsRegisteredApplication(GetCurrentApplicationKey());
        }

        public static bool IsRegisteredApplication(string key)
        {
            // We unregister by deleteing a key both in HKLM and HKCU - if found.
            try
            {
                RegistryKey rk = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
                if (rk.GetValue(key) != null) return true;

                rk = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
                if (rk.GetValue(key) != null) return true;
            }
            catch (Exception ex)
            {
                This.Logger.Error(string.Format("Unable to unregister autorun for application {0}", key), ex);
                return false;
            }

            return false;
        }

        private static string GetCurrentApplicationPath()
        {
            var p = Process.GetCurrentProcess();
            return RemoveVSHost(p.MainModule.FileName);
        }

        private static string GetCurrentApplicationKey()
        {
            return Path.GetFileNameWithoutExtension(GetCurrentApplicationPath());
        }

        private static string RemoveVSHost(string original)
        {
            if (original.Contains(".vshost"))
            {
                int index = original.LastIndexOf(".vshost");
                int len = ".vshost".Length;
                return string.Concat(original.Substring(0, index), original.Substring(index + len));
            }
            else return original;
        }
    }
}
