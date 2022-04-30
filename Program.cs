using System;
using System.IO;
using System.Text;

namespace VideoConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            Logging log = new Logging();
            try
            {
                Console.OutputEncoding = Encoding.UTF8;
                Console.InputEncoding = Encoding.UTF8;
                Logging.WriteLog("Starting..");
                Console.WriteLine("Source Folder: ");
                string sourceDir = Console.ReadLine(); // ex: C:\Users\alayedm\Videos

                if (Directory.Exists(sourceDir))
                {
                    Logging.WriteLog($"Source folder is: {sourceDir}");
                    Console.WriteLine("Distination Folder: ");
                    string distDir = Console.ReadLine(); // ex: C:\inetpub\wwwroot\videos
                    if (Directory.Exists(distDir))
                    {
                        Logging.WriteLog($"Distination folder is: {distDir}");
                        DirectoriesHandler dh = new DirectoriesHandler(sourceDir, distDir);
                        dh.CreateDirectories(); // Create all folders in distination lib. (can add a check if they're actually created)

                        VideosHandler vh = new VideosHandler(sourceDir, distDir);
                        vh.HandleVideoFiles(); // Transcode videos then place them in newly created folders.
                    }
                    else
                    {
                        throw new DirectoryNotFoundException();
                    }
                }
                else
                {
                    throw new DirectoryNotFoundException();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Logging.WriteLog(ex.Message);
            }
        }
    }
}