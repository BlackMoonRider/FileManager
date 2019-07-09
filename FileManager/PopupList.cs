using FileManager.ActionPerformers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    class PopupList
    {
        private readonly int offsetX, offsetY, height, width;
        private readonly string header;
        // private bool isRendered;
        public readonly ListView<DirectoryInfo> ListView;
        // private readonly DriveInfo[] driveInfos = DriveInfo.GetDrives();

        public IActionPerformerBehavior ActionPerformer { get; private set; }

        public PopupList(int offsetX, int offsetY, int height, int width, string header)
        {
            this.offsetX = offsetX;
            this.offsetY = offsetY;

            this.height = height;
            this.width = width;

            this.header = header;

            ListView = new ListView<DirectoryInfo>(offsetX, offsetX, height, 1);
            ActionPerformer = new NoAction();
        }

        public PopupList(string header)
        {
            this.header = header;

            width = 30;
            height = 10;

            offsetX = Console.WindowWidth / 2 - width / 2;
            offsetY = Console.WindowHeight / 2 - height / 2;

            ListView = new ListView<DirectoryInfo>(offsetX, offsetX, height, 1);
            ListView.Focused = true;
            ListView.Items = DriveInfo.GetDrives().Where(d => d.IsReady).Select(d => new ListViewItem<DirectoryInfo>(d.RootDirectory, d.RootDirectory.FullName)).ToList();

            ActionPerformer = new NoAction();
        }

        public void Render()
        {
            string background = "".PadRight(width, ' ');

            var savedForegroundColor = Console.ForegroundColor;
            var savedBackgroundColor = Console.BackgroundColor;

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.BackgroundColor = ConsoleColor.DarkCyan;

            for (int i = 0; i < height; i++)
            {
                Console.CursorTop = offsetY + i;
                Console.CursorLeft = offsetX;
                Console.WriteLine(background);
            }

            var lines = header.Split(new string[] { "\r\n" }, StringSplitOptions.None);

            //for (int i = 0; i < listView.Items.Count; i++)
            //{
            //    Console.CursorTop = offsetY + i;
            //    Console.CursorLeft = offsetX + 1;
            //    Console.WriteLine(listView.Items[i].Item.FullName);
            //}

            ListView.Render();

            //Console.ReadKey();

            Console.ForegroundColor = savedForegroundColor;
            Console.BackgroundColor = savedBackgroundColor;
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
