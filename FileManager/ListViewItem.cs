using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    class ListViewItem
    {
        private readonly string[] columns;
        public FileSystemInfo State { get; set; }

        public ListViewItem(FileSystemInfo fileSystemInfo, params string[] columns)
        {
            State = fileSystemInfo;
            this.columns = columns;
        }

        public void Render(List<int> columnsWidth, int elementIndex, int listViewX, int listViewY)
        {
            for (int i = 0; i < columns.Length; i++)
            {
                Console.CursorLeft = listViewX + columnsWidth.Take(i).Sum();
                Console.CursorTop = elementIndex + listViewY;
                Console.Write(GetStringWithLength(columns[i], columnsWidth[i]));
            }
        }

        private string GetStringWithLength(string v1, int maxLength)
        {
            if (v1.Length < maxLength)
                return v1.PadRight(maxLength, ' ');
            else
                return v1.Substring(0, maxLength - 5) + "...";
        }

        public void Clean(List<int> columnWidths, int i, int offsetX, int offsetY)
        {
            Console.CursorLeft = offsetX;
            Console.CursorTop = i + offsetY;
            Console.Write(new string(' ', columnWidths.Sum()));
        }
    }
}
