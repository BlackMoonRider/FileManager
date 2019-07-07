using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    //enum Actions
    //{
    //    Copy,
    //    Cut,
    //}
    [Obsolete]
    class CopyCutEventArgs : EventArgs
    {
        public ListViewItem listViewItem;
        public Actions action;
        
        public CopyCutEventArgs(ListViewItem listViewItem, Actions action)
        {
            this.listViewItem = listViewItem;
            this.action = action;
        }
    }
}
