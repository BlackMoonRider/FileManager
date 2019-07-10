using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    abstract class AbstarctListViewItem<T>
    {
        protected readonly string[] columns;
        public T Item { get; set; }

        public AbstarctListViewItem(T item, params string[] columns)
        {
            Item = item;
            this.columns = columns;
        }

        abstract public void Render(List<int> columnsWidth, int elementIndex, int listViewX, int listViewY);

        public void Clean(List<int> columnWidths, int i, int offsetX, int offsetY)
        {
            Console.CursorLeft = offsetX;
            if (i + offsetY < Console.BufferHeight)
                Console.CursorTop = i + offsetY;
            Console.Write(new string(' ', columnWidths.Sum()));
        }
    }
}
