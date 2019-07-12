using FileManager.ActionPerformers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    class ListView<T> : AbstractListView<ListViewItem<T>>
    {
        public T Current { get; set; }

        public ListView(int offsetX, int offsetY, int height, int offsetXMultiplier,
            ConsoleColor newForegroundColor = ConsoleColor.Black,
            ConsoleColor newBackgroundColorFocused = ConsoleColor.White,
            ConsoleColor newBackgroundColorUnfocused = ConsoleColor.DarkGray) 
            : base(offsetX, offsetY, height, offsetXMultiplier, newForegroundColor, newBackgroundColorFocused, newBackgroundColorUnfocused)
        { }

        public override void Clean()
        {
            scroll = 0;
            selectedIndex = previouslySelectedIndex = 0;
            isRendered = false;
            for (int i = 0; i < Items.Count; i++)
            {
                Console.CursorLeft = offsetX;
                if (i + offsetY < Console.BufferHeight)
                    Console.CursorTop = i + offsetY;
                Items[i].Clean(ColumnWidths, i, offsetX, offsetY);
            }
        }

        public override void Render()
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
                    Console.ForegroundColor = ForegroundColor;
                    Console.BackgroundColor = Focused ? BackgroundColorFocused : BackgroundColorUnfocused;
                }
                else
                {
                    Console.ForegroundColor = BackgroundColorFocused;
                    Console.BackgroundColor = ForegroundColor;
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
