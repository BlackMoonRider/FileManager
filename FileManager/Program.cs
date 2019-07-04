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
                foreach (ListView listView in panelSet.Panels)
                {
                    var key = Console.ReadKey();
                    listView.UpdateSelectedIndex(key);
                    listView.Render();
                }

            }
        }


    }
}
