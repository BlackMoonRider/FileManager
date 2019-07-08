using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    class PopupMessage
    {
        private readonly int offsetX, offsetY, height, width;
        private readonly string header;
        // private bool isRendered;
        private PanelSet panelSet;

        public PopupMessage(int offsetX, int offsetY, int height, int width, PanelSet panelSet, string header)
        {
            this.offsetX = offsetX;
            this.offsetY = offsetY;

            this.height = height;
            this.width = width;

            this.panelSet = panelSet;
            this.header = header;
        }

        public PopupMessage(PanelSet panelSet, string header)
        {
            this.panelSet = panelSet;
            this.header = header;

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

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.BackgroundColor = ConsoleColor.DarkCyan;

            for (int i = 0; i < height; i++)
            {
                Console.CursorTop = offsetY + i;
                Console.CursorLeft = offsetX;
                Console.WriteLine(background);
            }

            var lines = header.Split(new string[] { "\r\n" }, StringSplitOptions.None);

            for (int i = 0; i < lines.Length; i++)
            {
                Console.CursorTop = offsetY + i;
                Console.CursorLeft = offsetX + 1;
                Console.WriteLine(lines[i]);
            }

            Console.ReadKey();

            Console.ForegroundColor = savedForegroundColor;
            Console.BackgroundColor = savedBackgroundColor;

            Extensions.RefreshScreen(panelSet);
        }
    }
}
