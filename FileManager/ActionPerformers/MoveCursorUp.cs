using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileManager.ActionPerformers
{
    class MoveCursorUp : AbstractActionPerformerBehavior
    {
        public override void Do(ActionPerformerArgs actionPerformerArgs)
        {
            dynamic listView = actionPerformerArgs.Sender as AbstractListView<ListViewItem<FileSystemInfo>>;

            if (listView == null)
                listView = actionPerformerArgs.Sender as AbstractListView<ListViewItem<DirectoryInfo>>;

            if (listView.SelectedIndex != 0)
                listView.SelectedIndex--;
        }
    }
}
