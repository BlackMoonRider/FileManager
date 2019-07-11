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
        public static ulong DirectorySize(this DirectoryInfo directoryInfo)
        {
            ulong size = 0;
            FileInfo[] fileInfos = directoryInfo.GetFiles();

            foreach (FileInfo file in fileInfos)
                size += (ulong)file.Length;

            DirectoryInfo[] directoryInfos = directoryInfo.GetDirectories();

            foreach (DirectoryInfo directory in directoryInfos)
                size += directory.DirectorySize();

            return size;
        }

        public static string PrintAsNormalizedSize(this long bytes) => PrintAsNormalizedSize((ulong)bytes);

        public static string PrintAsNormalizedSize(this ulong bytes)
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
    }
}
