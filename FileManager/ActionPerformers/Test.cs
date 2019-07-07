using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.ActionPerformers
{
    class Test : AbstractActionPerformerBehavior
    {
        public override void Do(ActionPerformerArgs actionPerformerArgs)
        {
            PopupInput popupInput = new PopupInput(actionPerformerArgs.PanelSet);

            popupInput.Render();
        }
    }
}
