using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    enum Action
    {
        Copy,
        Cut,
    }
    class CopyCutEventArgs : EventArgs
    {
        public ListViewItem listViewItem;
        public Action action;

        public CopyCutEventArgs(ListViewItem listViewItem, Action action)
        {
            this.listViewItem = listViewItem;
            this.action = action;
        }
    }
}
