using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    class PopupMessage : AbstractPopup
    {
        private PanelSet panelSet;

        public PopupMessage(PanelSet panelSet, string header = "Info") : base(header)
        {
            this.panelSet = panelSet;
        }

        public override void Render()
        {
            base.Render();

            var lines = header.Split(new string[] { "\r\n" }, StringSplitOptions.None);

            for (int i = 0; i < lines.Length; i++)
            {
                Console.CursorTop = offsetY + i;
                Console.CursorLeft = offsetX + 1;
                Console.WriteLine(lines[i]);
            }

            Console.ReadKey();

            RestoreBackgroundColors();

            Extensions.RefreshScreen(panelSet);
        }
    }
}
