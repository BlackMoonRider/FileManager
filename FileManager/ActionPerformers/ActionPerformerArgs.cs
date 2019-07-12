using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    class ActionPerformerArgs
    {
        public ConsoleKeyInfo Key { get; private set; }
        public object Sender { get; private set; }

        public ActionPerformerArgs(ConsoleKeyInfo key, object sender)
        {
            Key = key;
            Sender = sender;
        }
    }
}