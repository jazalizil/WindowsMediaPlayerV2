using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace WindowsMediaPlayerV2Core
{
    public abstract class Media : MediaElement
    {
        public String Title { get; set; }
        public int Duration { get; set; }
        public String Extension { get; set; }
    }
}
