using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.ActionPerformers
{
    class CreateNewFolder : AbstractActionPerformerBehavior
    {
        public override void Do(ActionPerformerArgs actionPerformerArgs)
        {
            PanelSet panelSet = (PanelSet)actionPerformerArgs.Sender;

            var currentPath = panelSet.FocusedListView.Current.FullName + "\\New Folder";

            Directory.CreateDirectory(currentPath);

            Extensions.RefreshFocusedPanel(panelSet);
        }
    }
}
