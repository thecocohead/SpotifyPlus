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

            DisplayForm displayForm = new DisplayForm(0, e.Username, e.topArtistsMedium, e.topGenresMedium, e.topSongsMedium);
            displayForm.ShowDialog();

            /*
            //check if async call is required
            if (label3.InvokeRequired)
            {
                //async call
                label3.Invoke((MethodInvoker)delegate { label3.Text = $"Username: {e.Username}"; });
            } else
            {
                //sync
                label3.Text = $"Username: {e.Username}";
            }

            string artistlist = "Top Artists:\n";
            int count = 1;
            //foreach (string artist in e.topArtistsShort)
            //{
            //    artistlist += count + ". " + artist + "\n";
            //    count++;
            //}

            if (label4.InvokeRequired) 
                {
                    //Another async call
                    label4.Invoke((MethodInvoker)delegate { label4.Text = artistlist; });
                } else
                {
                    //sync call
                    label4.Text = artistlist;
                }
            */
        } 
    }
} 
