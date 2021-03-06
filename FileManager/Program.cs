using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Console.WindowHeight = 50;
            Console.BufferHeight = Console.WindowHeight;
            Console.BufferWidth = Console.BufferWidth;

            PanelSet panelSet = new PanelSet(2);

            panelSet.RefreshScreen();

            while (true)
            {
                var key = Console.ReadKey();
                try
                {
                    panelSet.Update(key);
                }
                catch (Exception ex)
                {
                    var popup = new PopupMessage(panelSet, $"This operation cannot be performed.\r\n{ex.Message}", "Error");
                    popup.Render();
                }

                panelSet.Render();
            }
        }
    }
}
