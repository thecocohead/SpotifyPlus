using System;
using System.Diagnostics;
using System.Drawing.Drawing2D;
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

            //Initialize event handlers for button
            button1.MouseDown += button1_MouseDown;
            button1.MouseUp += button1_MouseUp;
            button1.MouseLeave += button1_MouseLeave;
            //Initial color settings
            button1.FlatStyle = FlatStyle.Flat;
            button1.BackColor = ColorTranslator.FromHtml("#121212");
            button1.ForeColor = ColorTranslator.FromHtml("#FFFFFF");
            button1.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#1ED760");

            //UGH
            tabPage1.BackColor = ColorTranslator.FromHtml("#121212");
            tabPage2.BackColor = ColorTranslator.FromHtml("#121212");
            tabPage3.BackColor = ColorTranslator.FromHtml("#121212");

            //RAGH
            tabControl1.Appearance = TabAppearance.Normal;
            tabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabControl1.DrawItem += new DrawItemEventHandler(tabControl1_DrawItem);

            label1.ForeColor = ColorTranslator.FromHtml("#FFFFFF");
            label4.ForeColor = ColorTranslator.FromHtml("#FFFFFF");
            label3.ForeColor = ColorTranslator.FromHtml("#1ED760");
            label5.ForeColor = ColorTranslator.FromHtml("#FFFFFF");
        }


        private void button1_MouseLeave(object? sender, EventArgs e)
        {
            button1.BackColor = ColorTranslator.FromHtml("#121212"); ;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //initialize connection
            Task.Run(connection.Connect);

        }

        //Use variable defined by short-med-long to change time frame listing? Depending on how we display time frames
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

            //Section for controlling Top artists listing
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

            //Section for controlling Top tracks listing
            string tracklist = "Top Tracks:\n";
            count = 1;
            foreach (var track in e.topSongsShort)
            {
                tracklist += count + ". " + track.Title + "\n";
                count++;
            }

            if (label5.InvokeRequired)
            {
                //Another another async call
                label5.Invoke((MethodInvoker)delegate { label5.Text = tracklist; });
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //Function for controlling tab parameters
        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            //Get the tab page
            TabPage tabPage = tabControl1.TabPages[e.Index];

            //Define the trapezoid tabs
            int height = e.Bounds.Height;
            int width = e.Bounds.Width;

            //Defines Points for trapezoidal shape
            Point[] trapezoidPoints = new Point[]
            {
        new Point(e.Bounds.Left + 7, e.Bounds.Top),  //Top-left
        new Point(e.Bounds.Right - 7, e.Bounds.Top), //Top-right
        new Point(e.Bounds.Right, e.Bounds.Bottom),   //Bottom-right
        new Point(e.Bounds.Left, e.Bounds.Bottom)     //Bottom-left
            };

            //Create a GraphicsPath for the trapezoid
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddPolygon(trapezoidPoints);

                //Fill color
                Brush tabBrush = new SolidBrush(ColorTranslator.FromHtml("#121212")); 
                e.Graphics.FillPath(tabBrush, path);

                //Draw the tab text in the center of the tab
                Brush textBrush = new SolidBrush(ColorTranslator.FromHtml("#FFFFFF"));
                StringFormat stringFormat = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };

                //Draw the tab text within the trapezoid
                e.Graphics.DrawString(tabPage.Text, e.Font, textBrush, e.Bounds, stringFormat);
            }
        }

        //Function for when the connect button is pressed
        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            button1.BackColor = ColorTranslator.FromHtml("#1f1f1f");
        }

        //Function for when the connect button is released
        private void button1_MouseUp(object sender, MouseEventArgs e)
        {
            button1.BackColor = ColorTranslator.FromHtml("#121212");
        }
        //Function to handle the mouse leaving the connect button mid press


    }
}
