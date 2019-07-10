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
            PopupList popupList = new PopupList("Test");
            ((PanelSet)actionPerformerArgs.Sender).Modal = popupList;
            popupList.Render();
            //popupList.Update(actionPerformerArgs.Key);
        }
    }
}
