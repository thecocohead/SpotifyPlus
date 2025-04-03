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
    public class SpotifyAPI
    {
        private string verifier;
        private string challenge;
        private static EmbedIOAuthServer _server;

        public event EventHandler<UpdateArgs> Update;
        public SpotifyAPI()
        {
            (verifier, challenge) = PKCEUtil.GenerateCodes();
        }

        public async Task Connect()
        {
            // Make sure "http://localhost:5543/callback" is in your spotify application as redirect uri!
            _server = new EmbedIOAuthServer(new Uri("http://localhost:5543/callback"), 5543);
            await _server.Start();

            _server.ImplictGrantReceived += OnImplicitGrantReceived;
            _server.ErrorReceived += OnErrorReceived;

            var request = new LoginRequest(_server.BaseUri, "b13ac5dc0324440aa6f6be27785968f1", LoginRequest.ResponseType.Token)
            {
                Scope = new List<string> { Scopes.UserReadEmail, Scopes.UserTopRead }
            };
            BrowserUtil.Open(request.ToUri());
        }

        private async Task OnImplicitGrantReceived(object sender, ImplictGrantResponse response)
        {
            await _server.Stop();
            var spotify = new SpotifyClient(response.AccessToken);
            // do calls with Spotify
            var profile = await spotify.UserProfile.Current();
            //var tracks = await spotify.UserProfile.ToJson();

            UsersTopItemsRequest utir = new UsersTopItemsRequest(TimeRange.ShortTerm);
            utir.Limit = 10;
            var utar = await spotify.UserProfile.GetTopArtists(utir);

            //Request for top tracks
            UsersTopItemsRequest tracksUtir = new UsersTopItemsRequest(TimeRange.ShortTerm);
            tracksUtir.Limit = 5;
            var tracksUtar = await spotify.UserProfile.GetTopTracks(tracksUtir);

            //package
            UpdateArgs args = new UpdateArgs();
            args.Username = profile.DisplayName;

            List<string> topArtists = new List<string>();

            List<string> topTracks = new List<string>();

            //top artists
            foreach (var item in utar.Items)
            {
                topArtists.Add(item.Name);
            }
            args.topArtists = topArtists;

            //top tracks
            foreach (var item in tracksUtar.Items)
            {
                topTracks.Add(item.Name);
            }
            args.topSongs = topTracks;

            //send

            Update?.Invoke(this, args);
        }

        private async Task OnErrorReceived(object sender, string error, string state)
        {
            Console.WriteLine($"Aborting authorization, error received: {error}");
            await _server.Stop();
        }
    }
}
