using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.ActionPerformers
{
    class CreateNewFolder : AbstractActionPerformerBehavior
    {
        public override void Do(ActionPerformerArgs actionPerformerArgs)
        {
            PanelSet panelSet = (PanelSet)actionPerformerArgs.Sender;

            PopupInput popupInput = new PopupInput(panelSet);
            popupInput.Render();
            string newName = popupInput.UserInputResult;

            var currentPath = panelSet.FocusedListView.Current.FullName + "\\" + newName;

            Directory.CreateDirectory(currentPath);

            Extensions.RefreshFocusedPanel(panelSet);
        }
    }
}
