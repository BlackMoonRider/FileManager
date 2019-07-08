using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    class ListView : AbstractListView<ListViewItem<FileSystemInfo>>
    {
        public ListView(int offsetX, int offsetY, int height, int offsetXMultiplier) 
            : base(offsetX, offsetY, height, offsetXMultiplier)
        { }

        override public void Clean()
        {
            scroll = 0;
            selectedIndex = previouslySelectedIndex = 0;
            isRendered = false;
            for (int i = 0; i < Items.Count; i++)
            {
                Console.CursorLeft = offsetX;
                Console.CursorTop = i + offsetY;
                Items[i].Clean(ColumnWidths, i, offsetX, offsetY);
            }
        }

        override public void Render()
        {
            base.Render();

            for (int i = 0; i < Math.Min(height, Items.Count); i++)
            {
                int elementIndex = i + scroll;

                if (isRendered && elementIndex != previouslySelectedIndex && elementIndex != selectedIndex)
                    continue;

                var item = Items[elementIndex];
                var savedForegroundColor = Console.ForegroundColor;
                var savedBackgroundColor = Console.BackgroundColor;
                if (elementIndex == selectedIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = Focused ? ConsoleColor.White : ConsoleColor.DarkGray;
                }
                Console.CursorLeft = offsetX;
                Console.CursorTop = i + offsetY;
                item.Render(ColumnWidths, i, offsetX, offsetY);

                Console.ForegroundColor = savedForegroundColor;
                Console.BackgroundColor = savedBackgroundColor;
            }
            isRendered = true;
        }
    }
}
