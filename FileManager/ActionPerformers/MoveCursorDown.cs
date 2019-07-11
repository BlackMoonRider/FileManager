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
            ListView<FileSystemInfo> listView = (ListView<FileSystemInfo>)actionPerformerArgs.Sender;

            if (listView.SelectedIndex < listView.Items.Count - 1)
                listView.SelectedIndex++;
        }
    }
}
