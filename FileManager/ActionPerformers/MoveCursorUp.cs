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
            ListView<FileSystemInfo> listView = (ListView<FileSystemInfo>)actionPerformerArgs.Sender;

            if (listView.SelectedIndex != 0)
                listView.SelectedIndex--;
        }
    }
}
