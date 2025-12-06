using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkServer.Input
{
    internal class InputMgr
    {
        public static void Exec()
        {
            while (true)
            {
                var input = Console.ReadLine();
                if (input != null)
                {
                    Command.Exec(input);
                }
            }
        }
    }
}
