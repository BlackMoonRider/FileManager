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
            PanelSet panelSet = (PanelSet)actionPerformerArgs.Sender;

            var parent = Directory.GetParent(Directory.GetParent(panelSet.FocusedListView.SelectedItem.Item.FullName).FullName)
                ?? new DirectoryInfo(Path.GetPathRoot(panelSet.FocusedListView.SelectedItem.Item.FullName));

            panelSet.FocusedListView.Current = parent;

            Extensions.RefreshFocusedPanel(panelSet);
        }
    }
}
