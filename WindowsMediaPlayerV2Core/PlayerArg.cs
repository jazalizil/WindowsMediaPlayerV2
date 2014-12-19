using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace WindowsMediaPlayerV2Core
{
    public class PlayerArg : EventArgs
    {
        private MediaState status {get; set; }
        
        public PlayerArg(MediaState st)
        {
            this.status = st;
        }
    }
}
