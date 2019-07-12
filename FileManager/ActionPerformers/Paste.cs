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
            PanelSet panelSet = (PanelSet)actionPerformerArgs.Sender;

            FileSystemInfo senderInfo = panelSet.FocusedListView.Current;
            FileSystemInfo sourceInfo = panelSet.CurrentItemToOperateOn.Item;

            var action = panelSet.CurrentAction;

            var source = sourceInfo.FullName;
            var destination = senderInfo.FullName + "\\" + sourceInfo.Name;

            if (sourceInfo is FileInfo)
            {
                if (action == Actions.Copy)
                    File.Copy(source, destination);

                else if (action == Actions.Cut)
                    File.Move(source, destination);
            }

            else if (sourceInfo is DirectoryInfo)
            {
                if (action == Actions.Copy)
                    Utility.DirectoryCopy(source, destination);

                else if (action == Actions.Cut)
                    Directory.Move(source, destination);
            }

            if (action == Actions.Copy)
                panelSet.RefreshFocusedPanel();

            else if (action == Actions.Cut)
                panelSet.RefreshScreen();
        }
    }
}
