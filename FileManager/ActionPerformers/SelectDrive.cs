using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.ActionPerformers
{
    class SelectDrive : AbstractActionPerformerBehavior
    {
        public override void Do(ActionPerformerArgs actionPerformerArgs)
        {
            PopupList popupList = new PopupList("Select drive:");
            ((PanelSet)actionPerformerArgs.Sender).Modal = popupList;
            popupList.Render();
        }
    }
}
