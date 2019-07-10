using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.ActionPerformers
{
    class Cut : AbstractActionPerformerBehavior
    {
        public override void Do(ActionPerformerArgs actionPerformerArgs)
        {
            PanelSet panelSet = (PanelSet)actionPerformerArgs.Sender;

            panelSet.CurrentItemToOperateOn = panelSet.FocusedListView.SelectedItem;
            panelSet.CurrentAction = Actions.Cut;
        }
    }
}
