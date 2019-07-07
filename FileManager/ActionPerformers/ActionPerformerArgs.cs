using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    enum Actions
    {
        Copy,
        Cut,
    }
    class ActionPerformerArgs
    {
        public ConsoleKeyInfo Key;
        public PanelSet PanelSet;

        public ActionPerformerArgs(ConsoleKeyInfo key, PanelSet panelSet)
        {
            Key = key;
            PanelSet = panelSet;
        }
    }
}