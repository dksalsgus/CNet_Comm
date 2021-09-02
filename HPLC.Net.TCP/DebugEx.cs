using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPLC.Net.TCP
{

    internal class DebugEx
    {
        [Conditional("DEBUG")]
        internal static void WriteLine(string format, params object[] args)
        {
            WriteLine(string.Format(format, args));
        }

        [Conditional("DEBUG")]
        public static void WriteLine(string str)
        {
            lock (typeof(DebugEx))
            {
                //Debug.WriteLine(str);
                Console.WriteLine(str);
            }
        }

    }
}
