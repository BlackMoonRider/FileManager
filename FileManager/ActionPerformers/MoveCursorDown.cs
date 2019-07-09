using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.ActionPerformers
{
    class MoveCursorDown : AbstractActionPerformerBehavior
    {
        public override void Do(ActionPerformerArgs actionPerformerArgs)
        {
            dynamic listView = actionPerformerArgs.Sender as AbstractListView<ListViewItem<FileSystemInfo>>;

            if (listView == null)
                listView = actionPerformerArgs.Sender as AbstractListView<ListViewItem<DirectoryInfo>>;

            if (listView.SelectedIndex < listView.Items.Count - 1)
                listView.SelectedIndex++;
        }
    }
}
