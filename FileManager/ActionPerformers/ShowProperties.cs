using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.ActionPerformers
{
    class ShowProperties : AbstractActionPerformerBehavior
    {
        public override void Do(ActionPerformerArgs actionPerformerArgs)
        {
            var sourceInfo = actionPerformerArgs.PanelSet.FocusedListView.SelectedItem.State;
            StringBuilder stringBuilder = new StringBuilder();

            string info = String.Empty;
            int readOnly = ((int)(sourceInfo.Attributes) & (int)FileAttributes.ReadOnly);

            if (sourceInfo is FileInfo fileInfo)
            {
                stringBuilder.AppendLine("Name:\t\t" + sourceInfo.Name);
                stringBuilder.AppendLine("Parent direcotry:\t" + Path.GetDirectoryName(sourceInfo.FullName));
                stringBuilder.AppendLine("Root direcotry:\t" + Path.GetPathRoot(sourceInfo.FullName));
                stringBuilder.AppendLine("Read-only:\t" + ((readOnly == 1) ? "true" : "false"));
                stringBuilder.AppendLine("Last read time:\t" + sourceInfo.LastAccessTime);
                stringBuilder.AppendLine("Last write time:\t" + sourceInfo.LastWriteTime);
                stringBuilder.AppendLine("Size:\t\t" + fileInfo.Length);

                info = stringBuilder.ToString();
            }

            else if (sourceInfo is DirectoryInfo directoryInfo)
            {
                stringBuilder.AppendLine("Name:\t\t" + sourceInfo.Name);
                stringBuilder.AppendLine("Parent direcotry:\t" + Path.GetDirectoryName(sourceInfo.FullName));
                stringBuilder.AppendLine("Root direcotry:\t" + Path.GetPathRoot(sourceInfo.FullName));
                stringBuilder.AppendLine("Read-only:\t" + ((readOnly == 1) ? "true" : "false"));
                stringBuilder.AppendLine("Last read time:\t" + sourceInfo.LastAccessTime);
                stringBuilder.AppendLine("Last write time:\t" + sourceInfo.LastWriteTime);
                stringBuilder.AppendLine("Size:\t\t" + directoryInfo.DirectorySize());
                stringBuilder.AppendLine("Files:\t\t" + Directory.GetDirectories(sourceInfo.FullName).Count());
                stringBuilder.AppendLine("Folders:\t\t" + Directory.GetFiles(sourceInfo.FullName).Count());

                info = stringBuilder.ToString();
            }

            PopupMessage popupMessage = new PopupMessage(actionPerformerArgs.PanelSet, info);
            popupMessage.Render();

            Extensions.RefreshScreen(actionPerformerArgs.PanelSet);
        }
    }
}
