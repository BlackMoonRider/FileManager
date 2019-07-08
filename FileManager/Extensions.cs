﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    static class Extensions
    {
        public static void DirectoryCopy(string sourceName, string destinationName, bool copySubDirs = true)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(sourceName);

            if (!directoryInfo.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceName);
            }

            DirectoryInfo[] dirs = directoryInfo.GetDirectories();

            if (!Directory.Exists(destinationName))
            {
                Directory.CreateDirectory(destinationName);
            }

            FileInfo[] files = directoryInfo.GetFiles();
            foreach (FileInfo file in files)
            {
                string tempPath = Path.Combine(destinationName, file.Name);
                file.CopyTo(tempPath, false);
            }

            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string tempPath = Path.Combine(destinationName, subdir.Name);
                    DirectoryCopy(subdir.FullName, tempPath, copySubDirs);
                }
            }
        }

        public static ulong DirectorySize(this DirectoryInfo directoryInfo)
        {
            dynamic fileSystemObject = Activator.CreateInstance(Type.GetTypeFromProgID("Scripting.FileSystemObject"));
            dynamic folder = fileSystemObject.GetFolder(directoryInfo.FullName);
            return (ulong)folder.size;
        }

        public static string NormalizeSize(this long bytes) => NormalizeSize((ulong)bytes);
        public static string NormalizeSize(this int bytes) => NormalizeSize((ulong)bytes);

        public static string NormalizeSize(this ulong bytes)
        {
            if (bytes < 1024)
                return $"{bytes} Byte";

            ulong kbytes = bytes / 1024;

            if (kbytes < 1024)
                return $"{kbytes} KB";

            ulong mbytes = kbytes / 1024;

            if (mbytes < 1024)
                return $"{mbytes} MB";

            ulong gbytes = mbytes / 1024;

            if (gbytes < 1024)
                return $"{gbytes} GB";

            ulong tbytes = gbytes / 1024;

            return $"{kbytes} TB";
        }

        public static void RefreshScreen(PanelSet panelSet)
        {
            Console.Clear();

            foreach (var panel in panelSet.Panels)
            {
                panel.Clean();
                panel.Items = panelSet.GetItems(Path.GetDirectoryName(panel.SelectedItem.State.FullName));
                panel.Render();
            }
        }
    }
}
