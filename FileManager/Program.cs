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
            PopupSticker legend = new PopupSticker(1, Console.WindowWidth, panelSet, String.Empty,
                " F1 Copy | F2 Rename | F3 Cut | F4 Paste | F5 Root | F6 Properties | F8 Drive ");

            Extensions.RefreshScreen(panelSet);
            legend.Render();

            while (true)
            {
                var key = Console.ReadKey();
                try
                {
                    panelSet.Update(key);
                }
                catch (Exception ex)
                {
                    var exception = ex;
                    var popup = new PopupMessage(panelSet, "This operation cannot be \r\nperformed.", "Error"); // TODO: Impelement saving of the current path (to avoid the "Access denied" error)
                    popup.Render();
                }
                
                foreach (ListView<FileSystemInfo> listView in panelSet.Panels)
                {
                    listView.Render();
                }
            }
        }
    }
}
