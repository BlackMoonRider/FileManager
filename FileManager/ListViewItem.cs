using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    class ListViewItem<T> : AbstarctListViewItem<T>
    {
        public ListViewItem(T item, params string[] columns) : base (item, columns) { }

        override public void Render(List<int> columnsWidth, int elementIndex, int listViewX, int listViewY)
        {
            for (int i = 0; i < columns.Length; i++)
            {
                Console.CursorLeft = listViewX + columnsWidth.Take(i).Sum();
                Console.CursorTop = elementIndex + listViewY;
                Console.Write(Extensions.NormalizeStringLength(columns[i], columnsWidth[i]));
            }
        }
    }
}
