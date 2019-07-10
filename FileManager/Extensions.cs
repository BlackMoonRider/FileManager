using System;
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
            ulong size = 0;
            FileInfo[] fileInfos = directoryInfo.GetFiles();

            foreach (FileInfo f in fileInfos)
                size += (ulong)f.Length;

            DirectoryInfo[] directoryInfos = directoryInfo.GetDirectories();

            foreach (DirectoryInfo d in directoryInfos)
                size += DirectorySize(d);

            return size;
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

        public static string NormalizeStringLength(this string inputString, int maxLength)
        {
            string[] inputLines = inputString.Split(new string[] { "\r\n" }, StringSplitOptions.None);

            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < inputLines.Length; i++)
            {
                if (inputLines[i].Length < maxLength)
                {
                    if (i == inputLines.Length - 1)
                        stringBuilder.Append(inputLines[i].Trim().PadRight(maxLength, ' '));
                    else
                        stringBuilder.AppendLine(inputLines[i].Trim().PadRight(maxLength, ' '));
                }
                else
                {
                    if (i == inputLines.Length - 1)
                        stringBuilder.Append(inputLines[i].Trim().Substring(0, maxLength - 4) + "... ");
                    else
                        stringBuilder.AppendLine(inputLines[i].Trim().Substring(0, maxLength - 4) + "... ");

                }
            }

            var tmp = stringBuilder.ToString();

            return stringBuilder.ToString();
        }

        public static void RefreshScreen(PanelSet panelSet)
        {
            Console.Clear();

            foreach (var panel in panelSet.Panels)
            {
                panel.Clean();
                panel.Items = panelSet.GetItems(panel);
                panel.Render();
            }

            RenderLegend(panelSet);
        }

        public static void RefreshFocusedPanel(PanelSet panelSet)
        {
            foreach (var panel in panelSet.Panels)
            {
                if (panel.Focused)
                {
                    panel.Clean();
                    panel.Items = panelSet.GetItems(panel);
                    panel.Render();
                }
            }

            RenderLegend(panelSet);
        }

        private static void RenderLegend(PanelSet panelSet)
        {
            PopupSticker legend = new PopupSticker(1, Console.WindowWidth, 0, 47, panelSet, String.Empty,
                " F1 Copy | F2 Rename | F3 Cut | F4 Paste | F5 Root | F6 Properties | F7 New Folder | F8 Drives ");

            legend.Render();
        }
    }
}
