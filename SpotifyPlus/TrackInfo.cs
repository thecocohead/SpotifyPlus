using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyPlus
{
    /// <summary>
    /// Encapsulates information about a Track for frontend display. 
    /// </summary>
    public class TrackInfo
    {
        //Title of track, as reported by Spotify
        public string Title {  get; set; }
        //List of artists as reported by Spotify. Most songs only have one artist, but some may have multiple. 
        public List<string> Artists { get; set; }

        //URL of image hosted by spotify representing the album cover of the track. 
        public string CoverImage {  get; set; }
    }
}
