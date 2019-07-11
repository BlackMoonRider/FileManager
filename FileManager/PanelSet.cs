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
        public PopupList Modal { get; set; }
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
                listView.Items = GetItems(Panels[i]);
                ActionPerformer = new NoAction();
            }
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
            DirectoryInfo current = (DirectoryInfo)listView.Current;

            try
            {
                return current
                    .GetFileSystemInfos()
                    .Select(
                    lvi => new ListViewItem<FileSystemInfo>(
                    lvi,
                    lvi.Name,
                    lvi is DirectoryInfo dir ? "<dir>" : lvi.Extension,
                    lvi is FileInfo file ? Extensions.PrintAsNormalizedSize(file.Length) : ""))
                    .ToList();
            }
            catch (UnauthorizedAccessException ex)
            {
                var parent = Directory.GetParent(listView.Current.FullName)
                ?? new DirectoryInfo(Path.GetPathRoot(listView.Current.FullName));

                listView.Current = parent;

                var popup = new PopupMessage(this, $"Access denied.", "Error");
                popup.Render();
                
                current = parent;

                return GetItems(listView);
            }
            catch
            {
                throw;
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
                        FocusedPanel.Current = new DirectoryInfo(Modal.ListView.SelectedItem.Item.FullName);
                        Modal = null;
                        Extensions.RefreshScreen(this);
                    }
                    break;
                case ConsoleKey.Escape:
                    if (Modal == null)
                        goto default;
                    else
                    {
                        Modal = null;
                        Extensions.RefreshScreen(this);
                    }
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
