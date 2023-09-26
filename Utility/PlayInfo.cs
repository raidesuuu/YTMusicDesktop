using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YTMusicDesktop.Utility
{
    internal class PlayInfo
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string URL { get; set; }
        public string Image { get; set; }
        public bool IsPlaying { get; set; }
        public bool Ad { get; set; }
        public int Progress { get; set; }
        public int EndTime { get; set; }    
    }
}
