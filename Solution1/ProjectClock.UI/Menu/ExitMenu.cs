using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace ProjectClock.UI.Menu
{
    internal class ExitMenu
    {
        internal void Run()
        {
            WriteLine("\nPress any key to exit");
            ReadKey(true);
            Environment.Exit(0);
        }

    }
}
