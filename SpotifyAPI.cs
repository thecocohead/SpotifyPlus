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

            //Get user's 10 top artists
            UsersTopItemsRequest topArtistRequest = new UsersTopItemsRequest(TimeRange.ShortTerm);
            topArtistRequest.Limit = 10;
            var topArtistResponse = await spotify.UserProfile.GetTopArtists(topArtistRequest);

            //Get user's 5 top tracks
            UsersTopItemsRequest topTracksRequest = new UsersTopItemsRequest(TimeRange.ShortTerm);
            topTracksRequest.Limit = 5;
            var topTracksResponse = await spotify.UserProfile.GetTopTracks(topTracksRequest);


            //Package information for sending to front end

            //Create package
            UpdateArgs args = new UpdateArgs();

            //Package user's Spotify username
            args.Username = profile.DisplayName;

            //Package user's Top Artists
            List<string> topArtists = new List<string>();
            foreach (var item in topArtistResponse.Items)
            {
                topArtists.Add(item.Name);
            }
            args.topArtists = topArtists;

            //Package user's Top Tracks
            List<TrackInfo> topTracks = new List<TrackInfo>();
            foreach (var item in topTracksResponse.Items)
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
                topTracks.Add(newTrack);
            }
            args.topSongs = topTracks;

            //Send to front end via method invocation

            Update?.Invoke(this, args);
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
