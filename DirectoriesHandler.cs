using System;
using System.IO;
using System.Threading;

namespace VideoConverter
{
    class DirectoriesHandler
    {
        private readonly string sourceDir;
        private readonly string distDir;

        public DirectoriesHandler(string sourceDir, string distDir)
        {
            this.distDir = distDir;
            this.sourceDir = sourceDir;
        }

        public void CreateDirectories()
        {
            foreach (string dirPath in Directory.GetDirectories(sourceDir, "*", SearchOption.AllDirectories))
                Directory.CreateDirectory(dirPath.Replace(sourceDir, distDir));

            Console.WriteLine("\nFolders created successfully.");
            Logging.WriteLog("Folders created successfully.");
        }


        public static void CreateVideoFolders(string path)
        {
            if (!Directory.Exists($@"{path}\v"))
                Directory.CreateDirectory($@"{path}\v");
            if (!Directory.Exists($@"{path}\subtitles"))
                Directory.CreateDirectory($@"{path}\subtitles");
            if (!Directory.Exists($@"{path}\attachments"))
                Directory.CreateDirectory($@"{path}\attachments");
        }
    }
}