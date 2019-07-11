using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.ActionPerformers
{
    class NavigateToRoot : AbstractActionPerformerBehavior
    {
        public override void Do(ActionPerformerArgs actionPerformerArgs)
        {
            PanelSet panelSet = (PanelSet)actionPerformerArgs.Sender;

            var root = Path.GetPathRoot(panelSet.FocusedListView.SelectedItem.Item.FullName);

            panelSet.FocusedListView.Current = new DirectoryInfo(root);

            panelSet.RefreshFocusedPanel();
        }
    }
}
