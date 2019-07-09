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
        private string message;

        //public PopupMessage(int height, int width, string header) : base(height, width, header)
        //{ }

        public PopupMessage(PanelSet panelSet, string message, string header = "Info") : base(header)
        {
            this.panelSet = panelSet;
            this.message = message;
        }

        public PopupMessage(string message, string header = "Info") : base(header)
        {
            this.panelSet = panelSet;
            this.message = message;
        }

        public override void Render()
        {
            base.Render();

            var lines = message.Split(new string[] { "\r\n" }, StringSplitOptions.None);

            for (int i = 0; i < lines.Length; i++)
            {
                Console.CursorTop = offsetY + i;
                Console.CursorLeft = offsetX;
                Console.WriteLine(lines[i]);
            }

            Console.ReadKey();

            RestoreBackgroundColors();

            if (panelSet != null)
                Extensions.RefreshScreen(panelSet);
        }
    }
}
