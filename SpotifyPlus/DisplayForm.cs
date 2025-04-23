using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace SpotifyPlus
{
    public partial class DisplayForm : Form
    {
        public DisplayForm(int timeFrame, string username, List<ArtistInfo> artists, List<GenreInfo> genres, List<TrackInfo> tracks, int numBandsGenres)
        {
            //TimeFrame:
            // 0 = short
            // 1 = medium
            // 2 = long
            InitializeComponent();
            this.BackColor = ColorTranslator.FromHtml("#121212");

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

            //Round picture boxes
            RoundPictureBox(imgArtist1, 4);
            RoundPictureBox(imgArtist2, 4);
            RoundPictureBox(imgArtist3, 4);
            RoundPictureBox(imgArtist4, 4);
            RoundPictureBox(imgArtist5, 4);

            RoundPictureBox(imgSong1, 4);
            RoundPictureBox(imgSong2, 4);
            RoundPictureBox(imgSong3, 4);
            RoundPictureBox(imgSong4, 4);
            RoundPictureBox(imgSong5, 4);
            RoundPictureBox(imgSong6, 4);
            RoundPictureBox(imgSong7, 4);
            RoundPictureBox(imgSong8, 4);
            RoundPictureBox(imgSong9, 4);
            RoundPictureBox(imgSong10, 4);

            userHeader.Text = username + "'s";
            //place Spotify logo directly after username string
            SpotifyLogo.Location = new System.Drawing.Point(
                userHeader.Location.X + userHeader.Size.Width + 3,
                userHeader.Location.Y + (userHeader.Height - SpotifyLogo.Height) / 2
                );
            header.Text = "Stats";
            //place 'Stats' directly after spotify logo
            header.Location = new System.Drawing.Point(
                SpotifyLogo.Location.X + SpotifyLogo.Size.Width + 3,
                SpotifyLogo.Location.Y + (SpotifyLogo.Height - header.Height) / 2
                );
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
            topGenresHeader.Text = "Top Genres of " + numBandsGenres + " artists";
            

            //List to align genres listings to artists listings
            Label[] artistLabels = new Label[]
            {
                txtArtist1, txtArtist2, txtArtist3, txtArtist4, txtArtist5
            };

            int[] genrePercentages = new int[genres.Count];
            int totalGenreCount = genres.Sum(g => g.GenreCount);
           
            int otherBar = 0;
            int maxBars = 5;
            int stupidcount = 0;
            int x = topGenresHeader.Location.X;
            foreach (GenreInfo genre in genres.OrderByDescending(g => g.GenreCount))
            {
                if (numBandsGenres == 0 || genres.All(g => g.GenreCount == 0))
                {
                    CreateGenreBar("No Genre Data", 100, new Point(334, txtArtist1.Location.Y));
                    return;
                }
                if (genre.GenreCount > 0 && stupidcount < maxBars)
                {
                    int percentage = (int)Math.Round((genre.GenreCount * 100.0) / totalGenreCount);
                    int y = artistLabels[stupidcount].Location.Y;

                    CreateGenreBar($"{genre.GenreName} ({percentage}%)", percentage, new Point(x, y));
                    otherBar += percentage;
                    stupidcount++;
                }
               
            }
            if (otherBar < 100 && stupidcount == maxBars)
            {
                if (stupidcount == maxBars)
                {
                    int remainder = 100 - otherBar;
                    int y = txtSongName6.Location.Y;

                    CreateGenreBar($"Other ({remainder}%)", remainder, new Point(x, y));
                }
                else
                {
                    int remainder = 100 - otherBar;
                    int y = artistLabels[stupidcount].Location.Y;

                    CreateGenreBar($"Other ({remainder}%)", remainder, new Point(x, y));
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
        private void RoundPictureBox(PictureBox pic, int radius)
        {
            Rectangle bounds = new Rectangle(0, 0, pic.Width, pic.Height);
            int diameter = radius * 2;
            GraphicsPath path = new GraphicsPath();

            //top left corner
            path.AddArc(bounds.X, bounds.Y, diameter, diameter, 180, 90);
            //top right corner
            path.AddArc(bounds.Right - diameter, bounds.Y, diameter, diameter, 270, 90);
            //bottom right corner
            path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);
            //bottom left corner
            path.AddArc(bounds.X, bounds.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();

            pic.Region = new Region(path);

        }

        private void CreateGenreBar(string genreName, int percentage, Point location)
        {
            Label genreLabel = new Label();
            genreLabel.Text = genreName;
            genreLabel.Location = location;
            genreLabel.ForeColor = Color.White;
            genreLabel.Font = new Font("Segoe UI", 16F);
            genreLabel.AutoSize = true;

            this.Controls.Add(genreLabel);

            int barY = genreLabel.Location.Y + genreLabel.Height + 2;

            ProgressBar genreBar = new ProgressBar();
            genreBar.Location = new Point(location.X + 10, barY);
            genreBar.Size = new Size(200, 15);
            genreBar.Value = percentage;
            genreBar.ForeColor = ColorTranslator.FromHtml("#1DB954"); 
            genreBar.Style = ProgressBarStyle.Continuous;

            
            this.Controls.Add(genreBar);
        }


    }

}
