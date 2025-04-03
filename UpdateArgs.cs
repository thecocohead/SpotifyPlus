using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyPlus
{
    public class UpdateArgs : EventArgs
    {
        public string Username { get; set; }
        public List<string> topArtists {  get; set; }

        public List<TrackInfo> topSongs { get; set; }
    }
}
