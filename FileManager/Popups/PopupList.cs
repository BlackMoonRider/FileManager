using FileManager.ActionPerformers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    class PopupList : AbstractPopup
    {
        public ListView<DirectoryInfo> ListView;

        public PopupList(int height, int width, string header) : base(height, width, header)
        {
            Builder();
        }

        public PopupList(string header = "List") : base(header)
        {
            Builder();
        }

        protected void Builder()
        {
            ListView = new ListView<DirectoryInfo>(offsetX, offsetY, height, 0);
            ListView.Focused = true;
            ListView.ColumnWidths = new List<int>() { 6, 14, 10 };
            ListView.Items = DriveInfo.GetDrives()
                .Where(d => d.IsReady)
                .Select(d => new ListViewItem<DirectoryInfo>(d.RootDirectory, d.RootDirectory.FullName, d.VolumeLabel, Extensions.NormalizeSize(d.TotalSize)))
                .ToList();

            ActionPerformer = new NoAction();
        }

        public override void Render()
        {
            base.Render();

            ListView.Render();

            RestoreBackgroundColors();
        }

        public void Update(ConsoleKeyInfo key)
        {
            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                case ConsoleKey.DownArrow:
                    ListView.Update(key);
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
