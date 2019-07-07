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

            PanelSet panelSet = new PanelSet(2);

            foreach (ListView listView in panelSet.Panels)
            {
                listView.Render();
            }

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
