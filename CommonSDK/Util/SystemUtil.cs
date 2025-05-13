using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace CommonSDK.Util
{
    public struct ApplicationInfo
    {
        public string InstallLocation;

        public string DisplayName;

        public string UninstallString;

        public static ApplicationInfo Create(string installLocation, string displayName, string uninstallString)
        {
            ApplicationInfo info = new()
            {
                InstallLocation = installLocation,
                DisplayName = displayName,
                UninstallString = uninstallString
            };
            return info;
        }
    }

    public class SystemUtil
    {
        public static bool IsWindowPlatform()
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
        }

        public static bool IsLinuxPlatform()
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
        }

        public static OSPlatform GetPlatform()
        {
            if (IsWindowPlatform())
            {
                return OSPlatform.Windows;
            }
            else if (IsLinuxPlatform())
            {
                return OSPlatform.Linux;
            }

            return OSPlatform.OSX;
        }


        public static List<ApplicationInfo> GetApplicationList()
        {
#pragma warning disable CA1416 // 验证平台兼容性
            if (!IsWindowPlatform())
            {
                throw new PlatformNotSupportedException("The api only support windows!");
            }

            using RegistryKey? key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall", false) ?? throw new ExternalException("The registry key is null!");

            List<ApplicationInfo> infoList = [];
            
            foreach (String keyName in key.GetSubKeyNames())
            {
                RegistryKey? subkey = key.OpenSubKey(keyName);
                var displayName = subkey?.GetValue("DisplayName") as string;
                var installLocation = subkey?.GetValue("InstallLocation") as string;
                var uninstallString = subkey?.GetValue("UninstallString") as string;
                if (string.IsNullOrEmpty(displayName))
                    continue;

                if (string.IsNullOrEmpty(installLocation))
                    continue;

                if (string.IsNullOrEmpty(uninstallString))
                    continue;

                ApplicationInfo info = ApplicationInfo.Create(installLocation, displayName, uninstallString);
                infoList.Add(info);

                Console.WriteLine(displayName.ToLower() + "  " + installLocation?.ToLower());
            }
#pragma warning restore CA1416 // 验证平台兼容性
            
            return infoList;
        }

        public static ApplicationInfo GetApplicationInfo(string applicationName)
        {
#pragma warning disable CA1416 // 验证平台兼容性
            if (!IsWindowPlatform())
            {
                throw new PlatformNotSupportedException("The api only support windows!");
            }

            using RegistryKey? key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall", false) ?? throw new ExternalException("The registry key is null!");

            List<ApplicationInfo> infoList = [];

            foreach (String keyName in key.GetSubKeyNames())
            {
                RegistryKey? subkey = key.OpenSubKey(keyName);
                var displayName = subkey?.GetValue("DisplayName") as string;
                var installLocation = subkey?.GetValue("InstallLocation") as string;
                var uninstallString = subkey?.GetValue("UninstallString") as string;
                if (string.IsNullOrEmpty(displayName))
                    continue;

                if (string.IsNullOrEmpty(installLocation))
                    continue;

                if (string.IsNullOrEmpty(uninstallString))
                    continue;

                if (displayName.Contains(applicationName, StringComparison.CurrentCultureIgnoreCase))
                {
                    return ApplicationInfo.Create(installLocation, displayName, uninstallString);
                }
            }
#pragma warning restore CA1416 // 验证平台兼容性

            throw new FileNotFoundException("It can't find application info");
        }

        private static bool IsFile(string path)
        {
            return File.Exists(path);
        }

        private static bool IsDirectory(string path)
        {
            return Directory.Exists(path);
        }
        

        public static string GetFileIfExist(string path, string name)
        {
            ArgumentNullException.ThrowIfNullOrEmpty(path);
            ArgumentNullException.ThrowIfNullOrWhiteSpace(path);
            ArgumentNullException.ThrowIfNullOrEmpty(name);
            ArgumentNullException.ThrowIfNullOrWhiteSpace(name);

            if (!IsDirectory(path))
            {
                return string.Empty;
            }

            var fileList = Directory.GetFiles(path);

            for (int i = 0; i < fileList.Length; i++)
            {
                if (fileList[i].Contains(name))
                {
                    return Path.Combine(Path.GetFullPath(path), name);
                }
            }

            string[] dirList = Directory.GetDirectories(path);
            string filePath = string.Empty;

            for (int i = 0; i < dirList.Length; i++) 
            {
                filePath = GetFileIfExist(dirList[i], name);
            }

            return filePath;
        }
    }
}
