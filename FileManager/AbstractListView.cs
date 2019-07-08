﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    abstract class AbstractListView<T>
    {
        public List<int> ColumnWidths { get; set; }
        public List<T> Items { get; set; }
        protected readonly int offsetX, offsetY, height;
        protected bool isRendered;
        protected int scroll;
        protected int selectedIndex;
        protected int previouslySelectedIndex;

        public int SelectedIndex
        {
            get => selectedIndex;
            set
            {
                previouslySelectedIndex = selectedIndex;
                selectedIndex = value;
            }
        }

        public T SelectedItem => Items[SelectedIndex]; // TODO: Fix copying to an empty folder

        public bool Focused { get; set; }

        public AbstractListView(int offsetX, int offsetY, int height, int offsetXMultiplier)
        {
            ColumnWidths = new List<int> { 32, 10, 10 };

            this.offsetX = offsetX + offsetXMultiplier * ColumnWidths.Sum() +
                (offsetXMultiplier > 0 ? 2 : 0);
            this.offsetY = offsetY;

            this.height = height;
        }

        abstract public void Clean();

        virtual public void Render()
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
        }
    }
}
