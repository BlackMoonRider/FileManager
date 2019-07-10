using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    class PopupInput : AbstractPopup
    {
        private PanelSet panelSet;
        private string message;

        public string UserInputResult { get; private set; }

        public PopupInput(PanelSet panelSet, string message, string header = "Input") : base(header)
        {
            this.panelSet = panelSet;
            this.message = message;
        }

        public override void Render()
        {
            base.Render();

            string newName = String.Empty;

            while (NameIsValid(newName))
            {
                Console.CursorTop = offsetY + 1;
                Console.CursorLeft = offsetX + 1;
                Console.WriteLine(message.NormalizeStringLength(width - 2));

                Console.CursorTop = offsetY + 3;
                Console.CursorLeft = offsetX + 1;
                newName = Console.ReadLine();
            }

            UserInputResult = newName;

            RestoreBackgroundColors();

            Extensions.RefreshScreen(panelSet);
        }

        private bool NameIsValid(string name) // TODO: Add all real Windows names constraints 
        {
            if (String.IsNullOrWhiteSpace(name))
                return true;
            else
                return false;
        }
    }
}
