using System;
using System.Collections.Generic;
using System.Text;

namespace HPLC.Net.TCP
{
    public delegate void CommandReceivedEventHandler(object sender, CommandEventArgs e);

    public class CommandEventArgs : EventArgs
    {
        public virtual byte Cmd { get { return 0; } }

        //public byte[] Data { get { return data; } }

        //public CommandEventArgs()
        //{
        //}

        //public CommandEventArgs(byte commandID, int length)
        //    : this(commandID)
        //{
        //    this.data = new byte[length];
        //}
    }
}
