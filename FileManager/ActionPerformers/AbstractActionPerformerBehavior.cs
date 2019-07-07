using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileManager;

namespace FileManager.ActionPerformers
{
    abstract class AbstractActionPerformerBehavior : IActionPerformerBehavior
    {
        abstract public void Do(ActionPerformerArgs actionPerformerArgs);

        public IActionPerformerBehavior GetActionPerformer(ActionPerformerArgs actionPerformerArgs)
        {
            ConsoleKeyInfo key = actionPerformerArgs.Key;
            ListView listView = actionPerformerArgs.PanelSet.FocusedListView;

            switch (key.Key)
            {
                case ConsoleKey.UpArrow when listView.SelectedIndex != 0:
                    return new MoveCursorUp();
                case ConsoleKey.DownArrow when listView.SelectedIndex < listView.Items.Count - 1:
                    return new MoveCursorDown();
                case ConsoleKey.Enter:
                    return new OpenFileFolder();
                case ConsoleKey.RightArrow:
                    return new ChoosePreviousPanel();
                case ConsoleKey.LeftArrow:
                    return new ChooseNextPanel();
                case ConsoleKey.F1:
                    return new Copy();
                case ConsoleKey.F2:
                    return new Cut();
                case ConsoleKey.F3:
                    return new Paste();
                default:
                    return new NoAction();
            }
        }
    }
}
