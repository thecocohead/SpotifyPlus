using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpotifyPlus
{
    public partial class DisplayForm : Form
    {
        public DisplayForm(int timeFrame, string username, List<ArtistInfo> artists, List<GenreInfo> genres, List<TrackInfo> tracks)
        {
            //TimeFrame:
            // 0 = short
            // 1 = medium
            // 2 = long
            InitializeComponent();

            //set all pictureboxes to stretch images given
            imgArtist1.SizeMode = PictureBoxSizeMode.StretchImage;
            imgArtist2.SizeMode = PictureBoxSizeMode.StretchImage;
            imgArtist3.SizeMode = PictureBoxSizeMode.StretchImage;
            imgArtist4.SizeMode = PictureBoxSizeMode.StretchImage;
            imgArtist5.SizeMode = PictureBoxSizeMode.StretchImage;
            imgSong1.SizeMode = PictureBoxSizeMode.StretchImage;
            imgSong2.SizeMode = PictureBoxSizeMode.StretchImage;
            imgSong3.SizeMode = PictureBoxSizeMode.StretchImage;
            imgSong4.SizeMode = PictureBoxSizeMode.StretchImage;
            imgSong5.SizeMode = PictureBoxSizeMode.StretchImage;
            imgSong6.SizeMode = PictureBoxSizeMode.StretchImage;
            imgSong7.SizeMode = PictureBoxSizeMode.StretchImage;
            imgSong8.SizeMode = PictureBoxSizeMode.StretchImage;
            imgSong9.SizeMode = PictureBoxSizeMode.StretchImage;
            imgSong10.SizeMode = PictureBoxSizeMode.StretchImage;


            header.Text = username + "'s Spotify Stats";
            //artist images
            imgArtist1.ImageLocation = artists[0].Image;
            imgArtist2.ImageLocation = artists[1].Image;
            imgArtist3.ImageLocation = artists[2].Image;
            imgArtist4.ImageLocation = artists[3].Image;
            imgArtist5.ImageLocation = artists[4].Image;
            //artist names
            txtArtist1.Text = artists[0].Name;
            txtArtist2.Text = artists[1].Name;
            txtArtist3.Text = artists[2].Name;
            txtArtist4.Text = artists[3].Name;
            txtArtist5.Text = artists[4].Name;
            //track images
            imgSong1.ImageLocation = tracks[0].CoverImage;
            imgSong2.ImageLocation = tracks[1].CoverImage;
            imgSong3.ImageLocation = tracks[2].CoverImage;
            imgSong4.ImageLocation = tracks[3].CoverImage;
            imgSong5.ImageLocation = tracks[4].CoverImage;
            imgSong6.ImageLocation = tracks[5].CoverImage;
            imgSong7.ImageLocation = tracks[6].CoverImage;
            imgSong8.ImageLocation = tracks[7].CoverImage;
            imgSong9.ImageLocation = tracks[8].CoverImage;
            imgSong10.ImageLocation = tracks[9].CoverImage;
            //track names
            txtSongName1.Text = tracks[0].Title;
            txtSongName2.Text = tracks[1].Title;
            txtSongName3.Text = tracks[2].Title;
            txtSongName4.Text = tracks[3].Title;
            txtSongName5.Text = tracks[4].Title;
            txtSongName6.Text = tracks[5].Title;
            txtSongName7.Text = tracks[6].Title;
            txtSongName8.Text = tracks[7].Title;
            txtSongName9.Text = tracks[8].Title;
            txtSongName10.Text = tracks[9].Title;

            //artists
            txtSongArtist1.Text = formatArtist(tracks[0]);
            txtSongArtist2.Text = formatArtist(tracks[1]);
            txtSongArtist3.Text = formatArtist(tracks[2]);
            txtSongArtist4.Text = formatArtist(tracks[3]);
            txtSongArtist5.Text = formatArtist(tracks[4]);
            txtSongArtist6.Text = formatArtist(tracks[5]);
            txtSongArtist7.Text = formatArtist(tracks[6]);
            txtSongArtist8.Text = formatArtist(tracks[7]);
            txtSongArtist9.Text = formatArtist(tracks[8]);
            txtSongArtist10.Text = formatArtist(tracks[9]);

            //genres
            topGenres.Text = "";
            foreach (GenreInfo genre in genres)
            {
                if (genre.GenreCount > 0)
                {
                    topGenres.Text += genre.GenreName + " (" + genre.GenreCount + ")\n";
                }
            }


        }

        public string formatArtist(TrackInfo track)
        {
            if(track.Artists.Count == 1)
            {
                return track.Artists[0];
            }
            else
            {
                string artistList = "";
                foreach (string artist in track.Artists)
                {
                    artistList += artist + ", ";
                }
                //remove last comma and space
                artistList = artistList.Remove(artistList.Length - 2);
                return artistList;
            }
        }
    }

}
