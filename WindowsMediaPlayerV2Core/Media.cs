using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsMediaPlayerV2Core
{
    public abstract class Media
    {
        public String FileName { get; set; }
        public int Duration { get; set; }
        public String Extension { get; set; }
        public String Path { get; set; }
    }
}
