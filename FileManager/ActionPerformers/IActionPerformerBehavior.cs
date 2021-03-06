﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.ActionPerformers
{
    interface IActionPerformerBehavior
    {
        void Do(ActionPerformerArgs actionPerformerArgs);
        IActionPerformerBehavior GetActionPerformer(ActionPerformerArgs actionPerformerArgs);
    }
}
