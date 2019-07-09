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
            var root = Path.GetPathRoot(actionPerformerArgs.PanelSet.FocusedListView.SelectedItem.Item.FullName);

            actionPerformerArgs.PanelSet.FocusedListView.Current = new DirectoryInfo(root);

            Extensions.RefreshFocusedPanel(actionPerformerArgs.PanelSet);
        }
    }
}
