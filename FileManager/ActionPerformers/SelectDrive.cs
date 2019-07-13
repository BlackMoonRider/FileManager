using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.ActionPerformers
{
    class SelectDrive : AbstractActionPerformerBehavior
    {
        public override void Do(ActionPerformerArgs actionPerformerArgs)
        {
            PanelSet panelSet = (PanelSet)actionPerformerArgs.Sender;
            PopupList popupList = new PopupList("Select drive:");

            popupList.ListView = new ListView<FileSystemInfo>(popupList.OffsetX, popupList.OffsetY, popupList.Height, 0,
                popupList.BackgroundColor, popupList.ForegroundColor);
            popupList.ListView.Focused = true;
            popupList.ListView.ColumnWidths = new List<int>() { 7, popupList.Width - 17, 10 };
            popupList.ListView.Items = DriveInfo.GetDrives()
                .Where(d => d.IsReady)
                .Select(d => new ListViewItem<FileSystemInfo>(d.RootDirectory, d.RootDirectory.FullName, d.VolumeLabel, Utility.BytesToStringAsNormalizedSize(d.TotalSize)))
                .ToList();

            panelSet.Modal = popupList;
            popupList.Render();
        }
    }
}
