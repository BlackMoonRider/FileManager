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
            popupList.ListView.Focused = true;
            popupList.ListView.ColumnWidths = new List<int>() { 30, 10, 10 };
            popupList.ListView.Current = panelSet.FocusedPanel.Current;
            panelSet.Modal = popupList;

            var tmp = GetAllFilesAndFolders((DirectoryInfo)popupList.ListView.Current);
            popupList.ListView.Items = GetAllFilesAndFolders((DirectoryInfo)popupList.ListView.Current).Where(i => i.Item.Name.Contains(userInput)).ToList();

            if (popupList.ListView.Items.Count > 0)
                popupList.ListView.Current = panelSet.Modal.ListView.Items[0].Item;
            else
                return;

            popupList.Render();
        }

        private List<ListViewItem<FileSystemInfo>> GetAllFilesAndFolders(DirectoryInfo directoryInfo)
        {
            List<ListViewItem<FileSystemInfo>> filesAndFolders = new List<ListViewItem<FileSystemInfo>>();

            FileInfo[] fileInfos = directoryInfo.GetFiles();
            filesAndFolders.AddRange(
                fileInfos
                .Select(f => new ListViewItem<FileSystemInfo>(f, f.Name, f.Extension, f.Length.PrintAsNormalizedSize()))
                .ToList());

            DirectoryInfo[] directoryInfos = directoryInfo.GetDirectories();
            filesAndFolders.AddRange(
                directoryInfos
                .Select(d => new ListViewItem<FileSystemInfo>(d, d.Name, "<dir>", ""))
                .ToList());

            foreach (DirectoryInfo directory in directoryInfos)
            {
                try
                {
                    filesAndFolders.AddRange(GetAllFilesAndFolders(directory));
                }
                catch (UnauthorizedAccessException)
                {
                    continue;
                }
            }

            return filesAndFolders;
        }
    }
}
