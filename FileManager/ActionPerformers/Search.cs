using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.ActionPerformers
{
    class Search : AbstractActionPerformerBehavior
    {
        public override void Do(ActionPerformerArgs actionPerformerArgs)
        {
            PanelSet panelSet = (PanelSet)actionPerformerArgs.Sender;
            PopupList popupList = new PopupList("Search result:");
            PopupInput popupInput = new PopupInput(panelSet, "Enter the name of a file or directory:", "Search");

            popupInput.Render();
            var userInput = popupInput.UserInputResult;

            popupList.ListView = new ListView<FileSystemInfo>(popupList.OffsetX, popupList.OffsetY, popupList.Height, 0,
                popupList.BackgroundColor, popupList.ForegroundColor);

            popupList.ListView.Current = panelSet.FocusedPanel.Current;
            panelSet.Modal = popupList;
            popupList.ListView.Items = panelSet.GetItems(popupList.ListView.Current).Where(i => i.Item.Name.Contains(userInput)).ToList();

            if (popupList.ListView.Items.Count > 0)
                popupList.ListView.Current = panelSet.Modal.ListView.Items[0].Item;
            else
                return;

            popupList.Render();
        }
    }
}
