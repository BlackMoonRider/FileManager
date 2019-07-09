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
        public List<ListView<FileSystemInfo>> Panels { get; set; }
        public ListViewItem<FileSystemInfo> CurrentItemToOperateOn { get; set; }
        public ListView<FileSystemInfo> FocusedPanel
        {
            get
            {
                ListView<FileSystemInfo> focusedPanel = null;

                foreach (var panel in Panels)
                {
                    if (panel.Focused)
                    {
                        focusedPanel = panel;
                        break;
                    }
                }

                return focusedPanel;
            }
        }

        public IActionPerformerBehavior ActionPerformer { get; private set; }
        public Actions CurrentAction;

        public PanelSet(int numberOfPanels)
        {
            Panels = new List<ListView<FileSystemInfo>>();

            for (int i = 0; i < numberOfPanels; i++)
            {
                ListView<FileSystemInfo> listView = new ListView<FileSystemInfo>(10, 2, 20, i);
                Panels.Add(listView);
                listView.Current = new DirectoryInfo("C:\\");
                if (i == 0)
                    Panels[i].Focused = true;
                listView.Items = GetItems(Panels[i]);
                ActionPerformer = new NoAction();
            }

            //Panels[0].Focused = true;
        }

        public ListView<FileSystemInfo> FocusedListView => GetFocusedListView();

        public ListView<FileSystemInfo> GetFocusedListView()
        {
            foreach (var listView in Panels)
            {
                if (listView.Focused)
                    return listView;
            }
            return null;
        }

        public List<ListViewItem<FileSystemInfo>> GetItems(ListView<FileSystemInfo> listView)
        {
            var current = (DirectoryInfo)listView.Current;

            return current
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
            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                case ConsoleKey.DownArrow:
                    FocusedPanel.Update(key);
                    break;
                default:
                    ActionPerformerArgs args = new ActionPerformerArgs(key, this);
                    ActionPerformer = ActionPerformer.GetActionPerformer(args);
                    ActionPerformer.Do(args);
                    break;
            }

            
        }
    }
}
