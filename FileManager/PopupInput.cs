using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    class PopupInput
    {
        private readonly int offsetX, offsetY, height, width;
        private bool isRendered;

        public PopupInput(int offsetX, int offsetY, int height, int width = 10)
        {
            this.offsetX = offsetX;
            this.offsetY = offsetY;

            this.height = height;
            this.width = width;
        }

        public PopupInput()
        {
            width = 30;
            height = 10;

            offsetX = Console.WindowWidth / 2 - width / 2;
            offsetY = Console.WindowHeight / 2 - height / 2;
        }

        public void Render()
        {
            string background = "".PadRight(width, ' ');

            var savedForegroundColor = Console.ForegroundColor;
            var savedBackgroundColor = Console.BackgroundColor;

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.BackgroundColor = ConsoleColor.DarkGreen;

            for (int i = 0; i < height; i++)
            {
                Console.CursorTop = offsetY + i;
                Console.CursorLeft = offsetX;
                Console.WriteLine(background);
            }

            Console.ForegroundColor = savedForegroundColor;
            Console.BackgroundColor = savedBackgroundColor;
        }
    }
}
