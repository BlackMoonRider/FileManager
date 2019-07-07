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

            var source = sourceInfo.FullName;
            var destinationFile = Path.GetDirectoryName(senderInfo.FullName) + "\\" + Path.GetFileName(sourceInfo.FullName);
            var destinationFolder = Path.GetDirectoryName(senderInfo.FullName) + "\\" + sourceInfo.Name;

            if (sourceInfo is FileInfo file)
            {
                if (action == Actions.Copy)
                    File.Copy(source, destinationFile);

                else if (action == Actions.Cut)
                    File.Move(source, destinationFile);
            }

            else if (sourceInfo is DirectoryInfo directoryInfo)
            {
                if (action == Actions.Copy)
                    Extensions.DirectoryCopy(source, destinationFolder);

                else if (action == Actions.Cut)
                    Directory.Move(source, destinationFolder);
            }

            Extensions.RefreshScreen(actionPerformerArgs.PanelSet);
        }
    }
}
