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
        public ListView<DirectoryInfo> ListView { get; set; }

        public PopupList(string header = "List") : base(header) { }

        public override void Render()
        {
            base.Render();

            ListView.SetOffsetY(OffsetY);

            ListView.Render();

            RestoreBackgroundColors();
        }
    }
}
