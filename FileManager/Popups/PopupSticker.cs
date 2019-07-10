using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    class PopupSticker : AbstractPopup
    {
        private PanelSet panelSet;
        private string message;

        public PopupSticker(int height, int width, PanelSet panelSet, string message, string header = "Info") : base(height, width, header)
        {
            this.panelSet = panelSet;
            this.message = message;
        }

        public PopupSticker(PanelSet panelSet, string message, string header = "Info") : base(header)
        {
            this.panelSet = panelSet;
            this.message = message;
        }

        public PopupSticker(string message, string header = "Info") : base(header)
        {
            this.message = message;
        }

        public override void Render()
        {
            base.Render();

            RestoreBackgroundColors();
        }
    }
}
