using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.ActionPerformers
{
    class MoveCursorUp : AbstractActionPerformerBehavior
    {
        public override void Do(ActionPerformerArgs args)
        {
            args.ListView.SelectedIndex--;
        }
    }
}
