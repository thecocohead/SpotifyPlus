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
            //Background Color of window
            this.BackColor = System.Drawing.ColorTranslator.FromHtml("#121212");

            //Color settings for main connect button
            button1.FlatStyle = FlatStyle.Flat;
            button1.BackColor = ColorTranslator.FromHtml("#121212");
            button1.ForeColor = ColorTranslator.FromHtml("#FFFFFF");
            button1.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#1ED760");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //initialize connection
            Task.Run(connection.Connect);

        }

        public void FormUpdate(object? sender, UpdateArgs e)
        {

            //check if async call is required
            if (label3.InvokeRequired)
            {
                //async call
                label3.Invoke((MethodInvoker)delegate { label3.Text = $"Username: {e.Username}"; });
            }
            else
            {
                //sync
                label3.Text = $"Username: {e.Username}";
            }

            string artistlist = "Top Artists:\n";
            int count = 1;
            foreach (string artist in e.topArtistsShort)
            {
                artistlist += count + ". " + artist + "\n";
                count++;
            }

            if (label4.InvokeRequired)
            {
                //Another async call
                label4.Invoke((MethodInvoker)delegate { label4.Text = artistlist; });
            }
            else
            {
                //sync call
                label4.Text = artistlist;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
