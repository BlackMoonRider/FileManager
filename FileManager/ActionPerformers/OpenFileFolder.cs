using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.ActionPerformers
{
    class OpenFileFolder : AbstractActionPerformerBehavior
    {
        public override void Do(ActionPerformerArgs actionPerformerArgs)
        {
            PanelSet panelSet = (PanelSet)actionPerformerArgs.Sender;

            ListView<FileSystemInfo> listView = panelSet.FocusedListView;
            FileSystemInfo info = listView.SelectedItem.Item;
            if (info is FileInfo file)
                Process.Start(file.FullName);
            else if (info is DirectoryInfo directoryInfo)
            {
                listView.Clean();
                panelSet.FocusedListView.Current = directoryInfo;
                listView.Items = panelSet.GetItems(panelSet.FocusedPanel);
            }
        }
    }
}
