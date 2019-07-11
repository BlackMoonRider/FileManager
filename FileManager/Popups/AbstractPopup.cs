using FileManager.ActionPerformers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    abstract class AbstractPopup
    {
        public int OffsetX { get; protected set; }
        public int OffsetY { get; protected set; }
        public int Height { get; protected set; }
        public int Width { get; protected set; }
        protected readonly string header;
        private ConsoleColor savedForegroundColor, savedBackgroundColor, newForegroundColor, newBackgroundColor;
        public IActionPerformerBehavior ActionPerformer { get; protected set; }

        public AbstractPopup(int height, int width, int offsetX, int offsetY, string header,
            ConsoleColor newForegroundColor = ConsoleColor.DarkMagenta,
            ConsoleColor newBackgroundColor = ConsoleColor.DarkCyan)
        {

            this.Height = height;
            this.Width = width;
            this.header = header;
            this.newForegroundColor = newForegroundColor;
            this.newBackgroundColor = newBackgroundColor;

            this.OffsetX = offsetX;
            this.OffsetY = offsetY;
        }

        public AbstractPopup(string header, int height = 13, int width = 50,
            ConsoleColor newForegroundColor = ConsoleColor.DarkMagenta,
            ConsoleColor newBackgroundColor = ConsoleColor.DarkCyan)
        {
            this.header = header;
            this.newForegroundColor = newForegroundColor;
            this.newBackgroundColor = newBackgroundColor;

            Width = width;
            Height = height;

            OffsetX = Console.WindowWidth / 2 - Width / 2;
            OffsetY = Console.WindowHeight / 2 - Height / 2;
        }

        virtual public void Render()
        {
            string background = "".PadRight(Width, ' ');

            SaveBackgroundColors();

            Console.ForegroundColor = newForegroundColor;
            Console.BackgroundColor = newBackgroundColor;

            for (int i = 0; i < Height; i++)
            {
                Console.CursorTop = OffsetY + i;
                Console.CursorLeft = OffsetX;
                Console.WriteLine(background);
            }

            OffsetX++;
            Console.CursorTop = OffsetY;
            Console.CursorLeft = OffsetX;
            Console.WriteLine(header.NormalizeStringLength(Width - 1));

            OffsetY += 2;
        }

        protected void SaveBackgroundColors()
        {
            savedForegroundColor = Console.ForegroundColor;
            savedBackgroundColor = Console.BackgroundColor;
        }

        protected void RestoreBackgroundColors()
        {
            Console.ForegroundColor = savedForegroundColor;
            Console.BackgroundColor = savedBackgroundColor;
        }
    }
}
