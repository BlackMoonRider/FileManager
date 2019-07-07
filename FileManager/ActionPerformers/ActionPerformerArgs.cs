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
        public ListView ListView;
        public ListViewItem ListViewItem;
        public Actions Action;

        public ActionPerformerArgs(ConsoleKeyInfo key, ListView listView)
        {
            Key = key;
            ListView = listView;
        }

        public ActionPerformerArgs(ConsoleKeyInfo key, ListView listView, ListViewItem listViewItem, Actions action) 
            : this (key, listView)
        {
            ListViewItem = listViewItem;
            Action = action;
        }
    }
}