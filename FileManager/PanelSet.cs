using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileManager.ActionPerformers;

namespace FileManager
{
    class PanelSet
    {
        public List<ListView> Panels { get; set; }
        public ListViewItem<FileSystemInfo> CurrentItemToOperateOn { get; set; }
        public IActionPerformerBehavior ActionPerformer { get; private set; }
        public Actions CurrentAction;

        public PanelSet(int numberOfPanels)
        {
            Panels = new List<ListView>();

            for (int i = 0; i < numberOfPanels; i++)
            {
                ListView listView = new ListView(10, 2, 20, i);
                Panels.Add(listView);
                listView.Items = GetItems("C:\\");
                ActionPerformer = new NoAction();
            }

            Panels[0].Focused = true;
        }

        public ListView FocusedListView => GetFocusedListView();

        public ListView GetFocusedListView()
        {
            foreach (var listView in Panels)
            {
                if (listView.Focused)
                    return listView;
            }
            return null;
        }

        public List<ListViewItem<FileSystemInfo>> GetItems(string path)
        {
            return new DirectoryInfo(path)
                .GetFileSystemInfos()
                .Select(
                lvi => new ListViewItem<FileSystemInfo>(
                lvi,
                lvi.Name,
                lvi is DirectoryInfo dir ? "<dir>" : lvi.Extension,
                lvi is FileInfo file ? file.Length.ToString() : ""))
                .ToList();
        }

        public void Update(ConsoleKeyInfo key)
        {
            ActionPerformerArgs args = new ActionPerformerArgs(key, this);
            ActionPerformer = ActionPerformer.GetActionPerformer(args);
            ActionPerformer.Do(args);
        }
    }
}
