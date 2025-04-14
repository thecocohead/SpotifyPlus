using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyPlus
{
    //Encapsulates genres by artist for frontend display
    public class GenreInfo
    {
        //List of genres for a given artist as reported by Spotify. Can be one or more.
        public string GenreName { get; set; }
        //Counter for the number of instances of the genre
        public int GenreCount { get; set; }
    }
}
