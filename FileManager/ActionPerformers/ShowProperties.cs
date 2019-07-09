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
            PanelSet panelSet = (PanelSet)actionPerformerArgs.Sender;

            var sourceInfo = panelSet.FocusedListView.SelectedItem.Item;
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
                stringBuilder.AppendLine("Size:\t\t" + fileInfo.Length.NormalizeSize());

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
                stringBuilder.AppendLine("Size:\t\t" + directoryInfo.DirectorySize().NormalizeSize());
                stringBuilder.AppendLine("Files:\t\t" + Directory.GetFiles(sourceInfo.FullName).Count());
                stringBuilder.AppendLine("Folders:\t\t" + Directory.GetDirectories(sourceInfo.FullName).Count());

                info = stringBuilder.ToString();
            }

            PopupMessage popupMessage = new PopupMessage(panelSet, info);
            popupMessage.Render();

            Extensions.RefreshScreen(panelSet);
        }
    }
}
