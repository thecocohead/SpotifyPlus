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
            //Background Color of window
            this.BackColor = System.Drawing.ColorTranslator.FromHtml("#121212");
            //Spotify Plus Header
            headerLabel.ForeColor = ColorTranslator.FromHtml("#FFFFFF");

            //Standard Button Color Settings
            connectButton.FlatStyle = FlatStyle.Flat;
            connectButton.BackColor = ColorTranslator.FromHtml("#121212");
            connectButton.ForeColor = ColorTranslator.FromHtml("#FFFFFF");
            connectButton.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#1ED760");
            //Short Term Button
            shortTermButton.FlatStyle = FlatStyle.Flat;
            shortTermButton.BackColor = ColorTranslator.FromHtml("#121212");
            shortTermButton.ForeColor = ColorTranslator.FromHtml("#FFFFFF");
            shortTermButton.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#1ED760");
            //Medium Term Button
            mediumTermButton.FlatStyle = FlatStyle.Flat;
            mediumTermButton.BackColor = ColorTranslator.FromHtml("#121212");
            mediumTermButton.ForeColor = ColorTranslator.FromHtml("#FFFFFF");
            mediumTermButton.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#1ED760");
            //Long Term Button
            longTermButton.FlatStyle = FlatStyle.Flat;
            longTermButton.BackColor = ColorTranslator.FromHtml("#121212");
            longTermButton.ForeColor = ColorTranslator.FromHtml("#FFFFFF");
            longTermButton.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#1ED760");
           
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
            }

            if(this.InvokeRequired)
            {
                //async call
                this.Invoke((MethodInvoker)delegate { this.WindowState = FormWindowState.Minimized; this.Show(); this.WindowState = FormWindowState.Normal; });
            }
            else
            {
                this.WindowState = FormWindowState.Minimized;
                this.Show();
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void shortTermButton_Click(object sender, EventArgs e)
        {
            DisplayForm displayForm = new DisplayForm(0, userInformation.Username, userInformation.topArtistsShort, userInformation.topGenresShort, userInformation.topSongsShort, userInformation.numArtistsShort);
            displayForm.ShowDialog();
        }

        private void mediumTermButton_Click(object sender, EventArgs e)
        {
            DisplayForm displayForm = new DisplayForm(1, userInformation.Username, userInformation.topArtistsMedium, userInformation.topGenresMedium, userInformation.topSongsMedium, userInformation.numArtistsMedium);
            displayForm.ShowDialog();
        }

        private void longTermButton_Click(object sender, EventArgs e)
        {
            DisplayForm displayForm = new DisplayForm(2, userInformation.Username, userInformation.topArtistsLong, userInformation.topGenresLong, userInformation.topSongsLong, userInformation.numArtistsLong);
            displayForm.ShowDialog();
        }
    }
} 
