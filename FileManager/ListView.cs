﻿using System;
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
        private bool isRendered;

        private int selectedIndex;

        private int scroll;
        private readonly int offsetX, offsetY, height;
        public int SelectedIndex
        {
            get => selectedIndex;
            set
            {
                previouslySelectedIndex = selectedIndex;
                selectedIndex = value;
            }
        }
        private int previouslySelectedIndex;
        public ListViewItem SelectedItem => Items[SelectedIndex];
        public bool Focused { get; set; }

        public ListView(int offsetX, int offsetY, int height)
        {
            this.offsetX = offsetX;
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

        public void UpdateSelectedIndex(ConsoleKeyInfo key)
        {
            if (key.Key == ConsoleKey.UpArrow && SelectedIndex != 0)
                SelectedIndex--;
            else if (key.Key == ConsoleKey.DownArrow && SelectedIndex < Items.Count - 1)
                SelectedIndex++;

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
            else if (key.Key == ConsoleKey.Enter)
                Selected(this, EventArgs.Empty);
        }

        public event EventHandler Selected;
    }
}