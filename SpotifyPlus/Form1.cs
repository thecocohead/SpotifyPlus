using System;
using System.Diagnostics;
using System.Net;
using System.Security.Policy;
using System.Threading.Tasks;
using SpotifyAPI.Web;

namespace SpotifyPlus
{
    public partial class Form1 : Form
    {

        public delegate void Update(object sender, UpdateArgs e);
        private SpotifyAPI connection;
        private UpdateArgs userInformation;
        public Form1(SpotifyAPI connection)
        {
            //WinForms Initialization
            InitializeComponent();
            this.connection = connection;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //initialize connection
            Task.Run(connection.Connect);

        }

        public void FormUpdate(object? sender, UpdateArgs e)
        {

            userInformation = e;

            //enable buttons
            if (shortTermButton.InvokeRequired)
            {
                //async call
                shortTermButton.Invoke((MethodInvoker)delegate { shortTermButton.Enabled = true; });
            }
            else
            {
                shortTermButton.Enabled = true;
            }
            if (mediumTermButton.InvokeRequired)
            {
                //async call
                mediumTermButton.Invoke((MethodInvoker)delegate { mediumTermButton.Enabled = true; });
            }
            else
            {
                mediumTermButton.Enabled = true;
            }
            if (longTermButton.InvokeRequired)
            {
                //async call
                longTermButton.Invoke((MethodInvoker)delegate { longTermButton.Enabled = true; });
            }
            else
            {
                longTermButton.Enabled = true;
            }
            if (connectButton.InvokeRequired)
            {
                //async call
                connectButton.Invoke((MethodInvoker)delegate { connectButton.Enabled = false; });
            }
            else
            {
                connectButton.Enabled = false;

                //DisplayForm displayForm = new DisplayForm(0, e.Username, e.topArtistsLong, e.topGenresLong, e.topSongsLong);
                //displayForm.ShowDialog();
            }
        }

        private void shortTermButton_Click(object sender, EventArgs e)
        {
            DisplayForm displayForm = new DisplayForm(0, userInformation.Username, userInformation.topArtistsShort, userInformation.topGenresShort, userInformation.topSongsShort);
            displayForm.ShowDialog();
        }

        private void mediumTermButton_Click(object sender, EventArgs e)
        {
            DisplayForm displayForm = new DisplayForm(1, userInformation.Username, userInformation.topArtistsMedium, userInformation.topGenresMedium, userInformation.topSongsMedium);
            displayForm.ShowDialog();
        }

        private void longTermButton_Click(object sender, EventArgs e)
        {
            DisplayForm displayForm = new DisplayForm(2, userInformation.Username, userInformation.topArtistsLong, userInformation.topGenresLong, userInformation.topSongsLong);
            displayForm.ShowDialog();
        }
    }
} 
