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

            if (sourceInfo is FileInfo file)
            {
                if (action == Actions.Copy)
                {
                    var fileToCopy = sourceInfo.FullName;
                    var fileToPaste = Path.GetDirectoryName(senderInfo.FullName) + "\\" + Path.GetFileName(sourceInfo.FullName);

                    File.Copy(fileToCopy, fileToPaste);
                }

                else if (action == Actions.Cut)
                {
                    var fileToCopy = sourceInfo.FullName;
                    var fileToPaste = Path.GetDirectoryName(senderInfo.FullName) + "\\" + Path.GetFileName(sourceInfo.FullName);
                    File.Move(fileToCopy, fileToPaste);
                }
            }

            else if (sourceInfo is DirectoryInfo directoryInfo)
            {
                if (action == Actions.Copy)
                {
                    var folderToCopy = sourceInfo.FullName;
                    var folderToPaste = Path.GetDirectoryName(senderInfo.FullName) + "\\" + sourceInfo.Name;

                    Extensions.DirectoryCopy(folderToCopy, folderToPaste);
                }

                else if (action == Actions.Cut)
                {
                    var folderToCopy = sourceInfo.FullName;
                    var folderToPaste = Path.GetDirectoryName(senderInfo.FullName) + "\\" + sourceInfo.Name;

                    Directory.Move(folderToCopy, folderToPaste);
                }
            }

            foreach (var panel in actionPerformerArgs.PanelSet.Panels)
            {
                panel.Clean();
                panel.Items = actionPerformerArgs.PanelSet.GetItems(Path.GetDirectoryName(panel.SelectedItem.State.FullName));
                panel.Render();
            }
        }
    }
}
