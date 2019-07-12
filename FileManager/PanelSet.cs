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
        public List<ListView<FileSystemInfo>> Panels { get; private set; }
        public PopupList Modal { get; set; }
        public ListViewItem<FileSystemInfo> CurrentItemToOperateOn { get; set; }
        public ListView<FileSystemInfo> FocusedPanel => Panels.First(p => p.Focused);
        public IActionPerformerBehavior ActionPerformer { get; private set; }
        public Actions CurrentAction { get; set; }

        public PanelSet(int numberOfPanels)
        {
            Panels = new List<ListView<FileSystemInfo>>();

            for (int i = 0; i < numberOfPanels; i++)
            {
                ListView<FileSystemInfo> listView = new ListView<FileSystemInfo>(10, 2, 43, i);
                Panels.Add(listView);
                listView.Current = new DirectoryInfo(
                    DriveInfo.GetDrives()
                    .Where(d => d.IsReady).ToList()
                    .First()
                    .Name);
                if (i == 0)
                    Panels[i].Focused = true;
                listView.Items = GetItems(Panels[i].Current);
            }

            ActionPerformer = new NoAction();
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

        public List<ListViewItem<FileSystemInfo>> GetItems(FileSystemInfo listViewCurrent)
        {
            DirectoryInfo current = Modal == null ? (DirectoryInfo)listViewCurrent : (DirectoryInfo)Modal.ListView.Current;

            try
            {
                return current
                    .GetFileSystemInfos()
                    .Select(
                    lvi => new ListViewItem<FileSystemInfo>(
                    lvi,
                    lvi.Name,
                    lvi is DirectoryInfo ? "<dir>" : lvi.Extension,
                    lvi is FileInfo file ? Utility.BytesToStringAsNormalizedSize(file.Length) : ""))
                    .ToList();
            }
            catch (UnauthorizedAccessException)
            {
                var parent = Directory.GetParent(listViewCurrent.FullName)
                    ?? new DirectoryInfo(Path.GetPathRoot(listViewCurrent.FullName));

                foreach (var panel in Panels)
                {
                    if (panel.Focused)
                        panel.Current = parent;
                }

                var popup = new PopupMessage(this, "Access denied.", "Error");
                popup.Render();

                return GetItems(parent);
            }
        }

        public void Update(ConsoleKeyInfo key)
        {
            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                case ConsoleKey.DownArrow:
                    if (Modal == null)
                        FocusedPanel.Update(key);
                    else
                    {
                        Modal.ListView.Update(key);
                        Modal.ListView.Render();
                    }
                    break;
                case ConsoleKey.Enter:
                    if (Modal == null)
                        goto default;
                    else
                    {
                        FileSystemInfo fileSystemInfo = Modal.ListView.SelectedItem.Item;

                        if (fileSystemInfo is FileInfo file)
                        {
                            Process.Start(file.FullName);
                            FocusedPanel.Current = new DirectoryInfo(Path.GetPathRoot(fileSystemInfo.FullName));
                        }
                        else
                            FocusedPanel.Current = new DirectoryInfo(fileSystemInfo.FullName);

                        Modal = null;
                        RefreshScreen();
                    }
                    break;
                case ConsoleKey.Escape:
                    if (Modal != null)
                    {
                        Modal = null;
                        RefreshScreen();
                    }
                    break;
                default:
                    ActionPerformerArgs args = new ActionPerformerArgs(key, this);
                    ActionPerformer = ActionPerformer.GetActionPerformer(args);
                    ActionPerformer.Do(args);
                    break;
            }
        }

        public void RefreshScreen()
        {
            Console.Clear();

            foreach (var panel in Panels)
            {
                panel.Clean();
                panel.Items = Modal == null ? GetItems(panel.Current) : GetItems(Modal.ListView.Current);
                panel.Render();
            }

            RenderLegend(this);
        }

        public void RefreshFocusedPanel()
        {
            foreach (var panel in Panels)
            {
                if (panel.Focused)
                {
                    panel.Clean();
                    panel.Items = Modal == null ? GetItems(panel.Current) : GetItems(Modal.ListView.Current);
                    panel.Render();
                }
            }

            RenderLegend(this);
        }

        private void RenderLegend(PanelSet panelSet)
        {
            PopupSticker legend = new PopupSticker(1, Console.WindowWidth, 0, 47, panelSet, String.Empty,
                " F1 Copy | F2 Rename | F3 Cut | F4 Paste | F5 Root | F6 Properties | F7 New Folder | F8 Drives | F9 Search ");

            legend.Render();
        }

        public void Render()
        {
            foreach (ListView<FileSystemInfo> listView in Panels)
                listView.Render();
        }
    }
}
