using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;
using Swan;

namespace SpotifyPlus
{
    /// <summary>
    /// Handles connecting and interfacing with the Spotify API via Spotify API-.NET
    /// </summary>
    public class SpotifyAPI
    {
        //Spotify Client ID
        private const string ClientId = "b13ac5dc0324440aa6f6be27785968f1";
        
        //PKCE Verifier & Challenge - used for connection and API calls
        private string verifier;
        private string challenge;

        //Embedded Web Server used for Callback
        private static EmbedIOAuthServer _server;

        public event EventHandler<UpdateArgs> Update;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public SpotifyAPI()
        {
            //Generate PKCE Verifier & Challenge Code
            (verifier, challenge) = PKCEUtil.GenerateCodes();
        }

        /// <summary>
        /// Connects to Spotify's API via Implicit Grant
        /// </summary>
        /// <returns>No return value</returns>
        public async Task Connect()
        {
            //Implicit Grant requires a http callback once the user authenticates with Spotify. 
            //SpotifyAPI-net has a built in web server - and that is used for this method.
            _server = new EmbedIOAuthServer(new Uri("http://localhost:5543/callback"), 5543);
            //Start SpotifyAPI-net web server
            await _server.Start();

            //Once a successful grant is recieved, an event is invoked and OnImplicitGrantRecieved is ran. 
            _server.ImplictGrantReceived += OnImplicitGrantReceived;
            //Otherwise, on an error, run OnErrorRecieved. 
            _server.ErrorReceived += OnErrorReceived;

            //Generate request token
            var request = new LoginRequest(_server.BaseUri, ClientId, LoginRequest.ResponseType.Token)
            {
                //API Scopes requested from Spotify
                Scope = new List<string> { Scopes.UserReadEmail, Scopes.UserTopRead }
            };

            //Open user's web browser to Spotify API authentication website
            BrowserUtil.Open(request.ToUri());
        }


        /// <summary>
        /// Method runs when implicit grant is recieved from Spotify. 
        /// Handles calls required to Spotify's API and invokes a method to notify front end. 
        /// </summary>
        /// <param name="sender">Unused, typically null or self</param>
        /// <param name="response">Response token from spotify API</param>
        /// <returns>Null</returns>
        private async Task OnImplicitGrantReceived(object sender, ImplictGrantResponse response)
        {
            //Stop web server - it is no longer needed. 
            await _server.Stop();

            //Setup SpotifyClient for calls. This is an object defined by SpotifyAPI-net
            var spotify = new SpotifyClient(response.AccessToken);


            //Spotify Calls

            //User profile - contains information about the user, such as username and email
            var profile = await spotify.UserProfile.Current();

            /* NOTE ABOUT TIME RANGES
             * Spotify's API defines three time ranges:
             * TimeRange.ShortTerm - for the last approx. 4 weeks
             * TimeRange.MediumTerm - for the last approx. 6 months
             * TimeRange.LongTerm - for the last approx. year
             */

            //Get user's 5 top artists for each timeframe
            UsersTopItemsRequest topArtistRequestShort = new UsersTopItemsRequest(TimeRange.ShortTerm);
            UsersTopItemsRequest topArtistRequestMedium = new UsersTopItemsRequest(TimeRange.MediumTerm);
            UsersTopItemsRequest topArtistRequestLong = new UsersTopItemsRequest(TimeRange.LongTerm);
            topArtistRequestShort.Limit = 5;
            topArtistRequestMedium.Limit = 5;
            topArtistRequestLong.Limit = 5;
            var topArtistResponseShort = await spotify.UserProfile.GetTopArtists(topArtistRequestShort);
            var topArtistResponseMedium = await spotify.UserProfile.GetTopArtists(topArtistRequestMedium);
            var topArtistResponseLong = await spotify.UserProfile.GetTopArtists(topArtistRequestLong);

            //Get user's 10 top tracks for each timeframe
            UsersTopItemsRequest topTracksRequestShort = new UsersTopItemsRequest(TimeRange.ShortTerm);
            UsersTopItemsRequest topTracksRequestMedium = new UsersTopItemsRequest(TimeRange.MediumTerm);
            UsersTopItemsRequest topTracksRequestLong = new UsersTopItemsRequest(TimeRange.LongTerm);
            topTracksRequestShort.Limit = 10;
            topTracksRequestMedium.Limit = 10;
            topTracksRequestLong.Limit = 10;
            var topTracksResponseShort = await spotify.UserProfile.GetTopTracks(topTracksRequestShort);
            var topTracksResponseMedium = await spotify.UserProfile.GetTopTracks(topTracksRequestMedium);
            var topTracksResponseLong = await spotify.UserProfile.GetTopTracks(topTracksRequestLong);

            //Re-request top artist from top 5 to top 20. Seems to present a more accurate mix of genres for the end user. 
            topArtistRequestShort.Limit = 20;
            topArtistRequestMedium.Limit = 20;
            topArtistRequestLong.Limit = 20;
            var topGenreResponseShort = await spotify.UserProfile.GetTopArtists(topArtistRequestShort);
            var topGenreResponseMedium = await spotify.UserProfile.GetTopArtists(topArtistRequestMedium);
            var topGenreResponseLong = await spotify.UserProfile.GetTopArtists(topArtistRequestLong);

            //Get the number of artists used. 
            int artistCountShort = CountBands(topGenreResponseShort);
            int artistCountMedium = CountBands(topGenreResponseMedium);
            int artistCountLong = CountBands(topGenreResponseLong);



            //Package information for sending to front end

            //Create package
            UpdateArgs args = new UpdateArgs();

            //Package user's Spotify username
            args.Username = profile.DisplayName;

            //Package user's Top Artists

            args.topArtistsShort = PackageTopArtists(topArtistResponseShort);
            args.topArtistsMedium = PackageTopArtists(topArtistResponseMedium);
            args.topArtistsLong = PackageTopArtists(topArtistResponseLong);

            //Package user's Top Genres (Found within top artists)

            args.topGenresShort = PackageTopGenres(topGenreResponseShort);
            args.topGenresMedium = PackageTopGenres(topGenreResponseMedium);
            args.topGenresLong = PackageTopGenres(topGenreResponseLong);

            //Package user's Top Tracks

            args.topSongsShort = PackageTopTracks(topTracksResponseShort);
            args.topSongsMedium = PackageTopTracks(topTracksResponseMedium);
            args.topSongsLong = PackageTopTracks(topTracksResponseLong);

            //Package number of artists used to calculate top genres
            args.numArtistsShort = artistCountShort;
            args.numArtistsMedium = artistCountMedium;
            args.numArtistsLong = artistCountLong;

            //Send to front end via method invocation

            Update?.Invoke(this, args);
        }

        public List<TrackInfo> PackageTopTracks(UsersTopTracksResponse response)
        {
            List<TrackInfo> output = new List<TrackInfo>();
            if (response.Items.Count == 0)
            {
                return output;
            }
            foreach (var item in response.Items)
            {
                //create object
                TrackInfo newTrack = new TrackInfo();

                //find artists
                List<string> artistList = new List<string>();
                foreach (var artist in item.Artists)
                {
                    artistList.Add(artist.Name);
                }

                //items
                newTrack.Title = item.Name;
                newTrack.Artists = artistList;
                newTrack.CoverImage = item.Album.Images[0].Url;

                //add to list
                output.Add(newTrack);
            }
            return output;
        }

        public List<ArtistInfo> PackageTopArtists(UsersTopArtistsResponse response)
        {
            List<ArtistInfo> output = new List<ArtistInfo>();
            if(response.Items.Count == 0)
            {
                return output;
            }
            foreach (var item in response.Items)
            {
                //building the ArtistInfo object
                ArtistInfo newArtist = new ArtistInfo();
                newArtist.Name = item.Name;
                newArtist.Image = item.Images[0].Url;
                output.Add(newArtist);
            }
            return output;
        }

        //Gets all genres from their associated artist and lists them by artist
        public List<GenreInfo> PackageTopGenres(UsersTopArtistsResponse response)
        {
            List<GenreInfo> unsortedOutput = new List<GenreInfo>();
            List<GenreInfo> output = new List<GenreInfo>();
            if (response.Items.Count == 0)
            {
                return output;
            }
            foreach (var item in response.Items)
            {
                //Get all genres from the artist response
                foreach (var genre in item.Genres)
                {
                    //Check if the genre already is in the output
                    bool genreExists = false;

                    foreach (var genreInOutput in unsortedOutput)
                    {
                        //Increment the genre count if the genre exists
                        if (genreInOutput.GenreName.Equals(genre))
                        {
                            genreInOutput.GenreCount++;
                            genreExists = true;
                            break;
                         }
                    }

                    //Add the genre if it doesn't exist
                    if(!genreExists)
                    {
                        GenreInfo newGenre = new GenreInfo();
                        newGenre.GenreName = genre;
                        newGenre.GenreCount = 1;
                        unsortedOutput.Add(newGenre);
                    }
                }
            }

            //Sort the list
            int unsortedOutputCount = 5;
            while(output.Count < unsortedOutputCount)
            { 
                //Find the top genre in the unsorted list
                GenreInfo topGenre = new GenreInfo();
                topGenre.GenreCount = 0;
                foreach (var unsortedGenre in unsortedOutput)
                {
                    if (unsortedGenre.GenreCount > topGenre.GenreCount)
                    {
                        topGenre.GenreName = unsortedGenre.GenreName;
                        topGenre.GenreCount = unsortedGenre.GenreCount;
                    }
                }

                //Add the top genre to output
                output.Add(topGenre);

                //Remove the top genre from the unsorted list
                for (int i = 0; i < unsortedOutput.Count; i++)
                {
                    if (topGenre.GenreName.Equals(unsortedOutput[i].GenreName))
                    {
                        unsortedOutput.RemoveAt(i);
                    }
                }
            }

            return output;
        }//Package Top Genres

        public int CountBands(UsersTopArtistsResponse response)
        {
            return response.Items.Count;
        }

        /// <summary>
        /// Error recieved from API
        /// </summary>
        /// <param name="sender">Unused</param>
        /// <param name="error">Error recieved from API</param>
        /// <param name="state">Unused</param>
        /// <returns>Null</returns>
        private async Task OnErrorReceived(object sender, string error, string state)
        {
            Console.WriteLine($"Aborting authorization, error received: {error}");
            await _server.Stop();
        }
    }
}
