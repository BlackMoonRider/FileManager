﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.ActionPerformers
{
    class ChoosePreviousPanel : AbstractActionPerformerBehavior
    {
        public override void Do(ActionPerformerArgs actionPerformerArgs)
        {
            PanelSet panelSet = (PanelSet)actionPerformerArgs.Sender;

            var panels = panelSet.Panels;

            for (int i = 0; i < panels.Count; i++)
            {
                if (panels[i].Focused)
                {
                    panels[i].Focused = false;
                    if (i == 0)
                        panels[panels.Count - 1].Focused = true;
                    else
                        panels[i - 1].Focused = true;
                    return;
                }
            }
        }
    }
}
