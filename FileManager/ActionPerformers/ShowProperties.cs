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

            stringBuilder.AppendLine("Name:               " + sourceInfo.Name);
            stringBuilder.AppendLine("Parent direcotry:   " + Path.GetDirectoryName(sourceInfo.FullName));
            stringBuilder.AppendLine("Root direcotry:     " + Path.GetPathRoot(sourceInfo.FullName));
            stringBuilder.AppendLine("Read-only:          " + ((readOnly == 1) ? "true" : "false"));
            stringBuilder.AppendLine("Last read time:     " + sourceInfo.LastAccessTime);
            stringBuilder.AppendLine("Last write time:    " + sourceInfo.LastWriteTime);

            if (sourceInfo is FileInfo fileInfo)
            {
                stringBuilder.AppendLine("Size:               " + fileInfo.Length.PrintAsNormalizedSize());
            }

            else if (sourceInfo is DirectoryInfo directoryInfo)
            {
                stringBuilder.AppendLine("Size:               " + directoryInfo.DirectorySize().PrintAsNormalizedSize());
                stringBuilder.AppendLine("Files:              " + Directory.GetFiles(sourceInfo.FullName).Length);
                stringBuilder.AppendLine("Folders:            " + Directory.GetDirectories(sourceInfo.FullName).Length);
            }

            info = stringBuilder.ToString();

            PopupMessage popupMessage = new PopupMessage(panelSet, info, "Properties");
            popupMessage.Render();

            panelSet.RefreshScreen();
        }
    }
}
