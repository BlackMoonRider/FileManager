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

            PanelSet panelSet = new PanelSet(2);

            Extensions.RefreshScreen(panelSet);

            while (true)
            {
                var key = Console.ReadKey();
                panelSet.Update(key);
                foreach (ListView listView in panelSet.Panels)
                {
                    listView.Render();
                }
            }
        }
    }
}
