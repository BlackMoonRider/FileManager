using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.ActionPerformers
{
    class Paste : AbstractActionPerformerBehavior
    {
        public override void Do(ActionPerformerArgs actionPerformerArgs)
        {
            FileSystemInfo senderInfo = actionPerformerArgs.PanelSet.FocusedListView.SelectedItem.State;
            FileSystemInfo sourceInfo = actionPerformerArgs.PanelSet.CurrentItemToOperateOn.State;

            var action = actionPerformerArgs.PanelSet.CurrentAction;

            var fileToCopy = sourceInfo.FullName;
            var fileToPaste = Path.GetDirectoryName(senderInfo.FullName) + "\\" + Path.GetFileName(sourceInfo.FullName);

            var folderToCopy = sourceInfo.FullName;
            var folderToPaste = Path.GetDirectoryName(senderInfo.FullName) + "\\" + sourceInfo.Name;

            if (sourceInfo is FileInfo file)
            {
                if (action == Actions.Copy)
                    File.Copy(fileToCopy, fileToPaste);

                else if (action == Actions.Cut)
                    File.Move(fileToCopy, fileToPaste);
            }

            else if (sourceInfo is DirectoryInfo directoryInfo)
            {
                if (action == Actions.Copy)
                    Extensions.DirectoryCopy(folderToCopy, folderToPaste);

                else if (action == Actions.Cut)
                    Directory.Move(folderToCopy, folderToPaste);
            }

            Extensions.RefreshScreen(actionPerformerArgs.PanelSet);
        }
    }
}
