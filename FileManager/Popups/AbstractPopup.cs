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
        protected int offsetX, offsetY, height, width;
        protected readonly string header;
        private ConsoleColor savedForegroundColor, savedBackgroundColor, newForegroundColor, newBackgroundColor;
        public IActionPerformerBehavior ActionPerformer { get; protected set; }

        public AbstractPopup(int height, int width, string header, 
            ConsoleColor newForegroundColor = ConsoleColor.DarkMagenta,
            ConsoleColor newBackgroundColor = ConsoleColor.DarkCyan)
        {

            this.height = height;
            this.width = width;
            this.header = header;
            this.newForegroundColor = newForegroundColor;
            this.newBackgroundColor = newBackgroundColor;

            offsetX = Console.WindowWidth / 2 - width / 2;
            offsetY = Console.WindowHeight / 2 - height / 2;
        }

        public AbstractPopup(int height, int width, int offsetX, int offsetY, string header,
            ConsoleColor newForegroundColor = ConsoleColor.DarkMagenta,
            ConsoleColor newBackgroundColor = ConsoleColor.DarkCyan)
        {

            this.height = height;
            this.width = width;
            this.header = header;
            this.newForegroundColor = newForegroundColor;
            this.newBackgroundColor = newBackgroundColor;

            this.offsetX = offsetX;
            this.offsetY = offsetY;
        }

        public AbstractPopup(string header,
            ConsoleColor newForegroundColor = ConsoleColor.DarkMagenta,
            ConsoleColor newBackgroundColor = ConsoleColor.DarkCyan)
        {
            this.header = header;
            this.newForegroundColor = newForegroundColor;
            this.newBackgroundColor = newBackgroundColor;

            width = 50;
            height = 13;

            offsetX = Console.WindowWidth / 2 - width / 2;
            offsetY = Console.WindowHeight / 2 - height / 2;
        }

        virtual public void Render()
        {
            string background = "".PadRight(width, ' ');

            SaveBackgroundColors();

            Console.ForegroundColor = newForegroundColor;
            Console.BackgroundColor = newBackgroundColor;

            for (int i = 0; i < height; i++)
            {
                Console.CursorTop = offsetY + i;
                Console.CursorLeft = offsetX;
                Console.WriteLine(background);
            }

            offsetX++;
            Console.CursorTop = offsetY;
            Console.CursorLeft = offsetX;
            Console.WriteLine(header.NormalizeString(width - 1));

            offsetY += 2;
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
