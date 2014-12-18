using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsMediaPlayerV2Core
{
    public class PlayerArg : EventArgs
    {
        private Player.STATUS status {get; set; }
        
        public PlayerArg(Player.STATUS st)
        {
            this.status = st;
            Console.WriteLine(status.ToString());
        }
    }
}
