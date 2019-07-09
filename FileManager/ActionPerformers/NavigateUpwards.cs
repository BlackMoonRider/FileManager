using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.ActionPerformers
{
    class NavigateUpwards : AbstractActionPerformerBehavior
    {
        public override void Do(ActionPerformerArgs actionPerformerArgs)
        {
            var parent = Directory.GetParent(Directory.GetParent(actionPerformerArgs.PanelSet.FocusedListView.SelectedItem.Item.FullName).FullName)
                ?? new DirectoryInfo(Path.GetPathRoot(actionPerformerArgs.PanelSet.FocusedListView.SelectedItem.Item.FullName));

            actionPerformerArgs.PanelSet.FocusedListView.Current = parent;

            Extensions.RefreshFocusedPanel(actionPerformerArgs.PanelSet);
        }
    }
}
