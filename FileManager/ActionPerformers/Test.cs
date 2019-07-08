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
            string message = @"Hello
world
many lines";
            PopupList popup = new PopupList(actionPerformerArgs.PanelSet, message);

            popup.Render();
        }
    }
}
