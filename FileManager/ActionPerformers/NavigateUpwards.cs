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

            var parent = Directory.GetParent(panelSet.FocusedListView.Current.FullName)
                ?? new DirectoryInfo(Path.GetPathRoot(panelSet.FocusedListView.Current.FullName));

            panelSet.FocusedListView.Current = parent;

            Extensions.RefreshFocusedPanel(panelSet);
        }
    }
}
