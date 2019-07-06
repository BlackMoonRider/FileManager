using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    class PanelSet
    {
        public List<ListView> Panels { get; set; }

        public PanelSet(int numberOfPanels)
        {
            Panels = new List<ListView>();

            for (int i = 0; i < numberOfPanels; i++)
            {
                ListView listView = new ListView(10, 2, 20, i);
                Panels.Add(listView);

                listView.Items = GetItems("C:\\");
                listView.Selected += View_Selected;
                listView.ChooseNextPanel += View_ChooseNextPanel;
                listView.ChoosePreviousPanel += View_ChoosePreviousPanel;
                listView.Paste += View_Paste;
            }

            Panels[0].Focused = true;
        }

        private void View_Selected(object sender, EventArgs e)
        {
            ListView listView = (ListView)sender;
            FileSystemInfo info = listView.SelectedItem.State;
            if (info is FileInfo file)
                Process.Start(file.FullName);
            else if (info is DirectoryInfo directoryInfo)
            {
                listView.Clean();
                listView.Items = GetItems(directoryInfo.FullName);
            }
        }

        private void View_ChooseNextPanel(object sender, EventArgs e)
        {
            for (int i = 0; i < Panels.Count; i++)
            {
                if (Panels[i].Focused)
                {
                    Panels[i].Focused = false;
                    if (i == Panels.Count - 1)
                        Panels[0].Focused = true;
                    else
                        Panels[i + 1].Focused = true;
                    return;
                }
            }
        }

        private void View_ChoosePreviousPanel(object sender, EventArgs e)
        {
            for (int i = 0; i < Panels.Count; i++)
            {
                if (Panels[i].Focused)
                {
                    Panels[i].Focused = false;
                    if (i == 0)
                        Panels[Panels.Count - 1].Focused = true;
                    else
                        Panels[i - 1].Focused = true;
                    return;
                }
            }
        }

        private List<ListViewItem> GetItems(string path)
        {
            return new DirectoryInfo(path)
                .GetFileSystemInfos()
                .Select(
                lvi => new ListViewItem(
                lvi,
                lvi.Name,
                lvi is DirectoryInfo dir ? "<dir>" : lvi.Extension,
                lvi is FileInfo file ? file.Length.ToString() : ""))
                .ToList();
        }

        private void View_Paste(object sender, CopyCutEventArgs e)
        {
            ListView listView = (ListView)sender;
            FileSystemInfo info = listView.SelectedItem.State;

            if (info is FileInfo file)
            {
                if (e.action == Action.Copy)
                {
                    var fileToCopy = e.listViewItem.State.FullName;
                    var fileToPaste = Path.GetDirectoryName(info.FullName) + "\\" + Path.GetFileName(e.listViewItem.State.FullName);

                    File.Copy(fileToCopy, fileToPaste);
                }

                else if (e.action == Action.Cut)
                {
                    var fileToCopy = e.listViewItem.State.FullName;
                    var fileToPaste = Path.GetDirectoryName(info.FullName) + "\\" + Path.GetFileName(e.listViewItem.State.FullName);

                    File.Move(fileToCopy, fileToPaste);
                }

                foreach (var panel in Panels)
                {
                    panel.Clean();
                    panel.Items = GetItems(Path.GetDirectoryName(panel.SelectedItem.State.FullName));
                    panel.Render();
                }

            }
            else if (info is DirectoryInfo directoryInfo)
            {
                if (e.action == Action.Copy)
                {

                }

                else if (e.action == Action.Cut)
                {
                    //var fileToCopy = e.listViewItem.State.FullName;
                    //var fileToPaste = Path.GetDirectoryName(info.FullName);

                    //Directory.Move(fileToCopy, fileToPaste);
                }
                listView.Clean();
                listView.Items = GetItems(directoryInfo.FullName);
            }
        }
    }
}
