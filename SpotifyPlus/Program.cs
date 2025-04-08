namespace SpotifyPlus
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            SpotifyAPI connection = new SpotifyAPI();
            Form1 mainForm = new Form1(connection);


            connection.Update += mainForm.FormUpdate;


            Application.Run(mainForm);
        }
    }
}