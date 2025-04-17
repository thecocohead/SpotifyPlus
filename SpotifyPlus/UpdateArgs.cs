using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyPlus
{
    /// <summary>
    /// Arguments sent from backend (SpotifyAPI.cs) to frontend (Form1.cs) to update displayed spotify information.
    /// </summary>
    public class UpdateArgs : EventArgs
    {
        //Username reported by Spotify
        public string Username { get; set; }

        //Top artists as reported by Spotify
        public List<ArtistInfo> topArtistsShort {  get; set; }
        public List<ArtistInfo> topArtistsMedium { get; set; }
        public List<ArtistInfo> topArtistsLong { get; set; }

        //Top genres as reported by Spotify
        public List<GenreInfo> topGenresShort { get; set; }
        public List<GenreInfo> topGenresMedium { get; set; }
        public List<GenreInfo> topGenresLong { get; set; }

        //Top tracks as reported by Spotify - see TrackInfo.cs for breakdown
        public List<TrackInfo> topSongsShort { get; set; }
        public List<TrackInfo> topSongsMedium { get; set; }
        public List<TrackInfo> topSongsLong { get; set; }

        
    }
}
