using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace WindowsMediaPlayerV2Core
{
    public class Sound : Media
    {
        public Sound()
        {
            this.Height = 250;
            this.Width = 347;
        }
        public String Artist {get;set;}
        public String Album { get; set; }
    }
}
