using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    class ListView
    {
        public List<int> ColumnWidths { get; set; }
        public List<ListViewItem> Items { get; set; }
        private readonly int offsetX, offsetY, height;
        private bool isRendered;
        private int scroll;
        private int selectedIndex;
        private int previouslySelectedIndex;

        public int SelectedIndex
        {
            get => selectedIndex;
            set
            {
                previouslySelectedIndex = selectedIndex;
                selectedIndex = value;
            }
        }

        public ListViewItem SelectedItem => Items[SelectedIndex];

        public bool Focused { get; set; }

        public ListView(int offsetX, int offsetY, int height, int offsetXMultiplier)
        {
            ColumnWidths = new List<int> { 32, 10, 10 };

            this.offsetX = offsetX + offsetXMultiplier * ColumnWidths.Sum() + 
                (offsetXMultiplier > 0 ? 2 : 0);
            this.offsetY = offsetY;

            this.height = height; 
        }

        public void Clean()
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

        public void Render()
        {
            if (selectedIndex > height + scroll - 1)
            {
                scroll++;
                isRendered = false;
            }
            else if (selectedIndex < scroll)
            {
                scroll--;
                isRendered = false;
            }

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
                    Console.BackgroundColor = ConsoleColor.White;
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
