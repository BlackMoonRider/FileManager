﻿using System;
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
        private int panelCount;
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
            }

            Panels[0].Focused = true;
        }

        private static void View_Selected(object sender, EventArgs e)
        {
            ListView listView = (ListView)sender;
            var info = listView.SelectedItem.State;
            if (info is FileInfo file)
                Process.Start(file.FullName);
            else if (info is DirectoryInfo directoryInfo)
            {
                listView.Clean();
                listView.Items = GetItems(directoryInfo.FullName);
            }
        }

        private static List<ListViewItem> GetItems(string path)
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
    }
}
