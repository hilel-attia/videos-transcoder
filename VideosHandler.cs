using NReco.VideoInfo;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace VideoConverter
{
    class VideosHandler
    {
        private readonly string sourceDir;
        private readonly string distDir;
        public VideosHandler(string sourceDir, string distDir)
        {
            this.sourceDir = sourceDir;
            this.distDir = distDir;
        }

        public void HandleVideoFiles()
        {
            var filesPaths = Directory.GetFiles(sourceDir, "*.*", SearchOption.AllDirectories);
            int filesConvertedCount = 0;
            Console.WriteLine("Speed: (ex: ultrafast/superfast(recommended)/fast");
            string speed = string.Empty;
            while (string.IsNullOrEmpty(speed))
            {
                speed = Console.ReadLine();
                speed = Utilities.Presets(speed);
                if (string.IsNullOrEmpty(speed))
                    Console.WriteLine("Not a valid speed, enter again:");
            }

            foreach (string filePath in filesPaths)
            {
                Logging.WriteLog($"Starting with {filePath}");
                FileInfo fi = new FileInfo(filePath);
                if (Utilities.IsMedia(fi.Extension.ToUpperInvariant())) // Checking if file is a video.
                {
                    var newPath = fi.DirectoryName.Replace(sourceDir, distDir);
                    Logging.WriteLog($@"New folder path for {fi.Name} is: {newPath}");
                    if (!Directory.Exists($@"{newPath}\{Path.GetFileNameWithoutExtension(fi.Name)}\v"))
                    {
                        PreTranscodeOps(filePath, newPath, speed);
                        Logging.WriteLog($"Done with {filePath}");
                        filesConvertedCount++;
                    }
                    else if (Directory.GetFiles($@"{newPath}\{Path.GetFileNameWithoutExtension(fi.Name)}\v").Length < 1)
                    {
                        Logging.WriteLog("Found the folder but it's empty. So transcoding Now..");
                        PreTranscodeOps(filePath, newPath, speed);
                        Logging.WriteLog($"Done with {filePath}");
                        filesConvertedCount++;
                    }
                    else
                    {
                        Console.WriteLine("File already transcoded.");
                        Logging.WriteLog("File already transcoded.");
                    }
                } else
                {
                    Console.WriteLine("File is not listed as Media File.");
                    Logging.WriteLog("File is not listed as Media File.");
                }
            }
            Console.WriteLine($"Done with {filesConvertedCount} videos found in {sourceDir}");
            Logging.WriteLog($"Done with {filesConvertedCount} videos found in {sourceDir}");
        }

        public void PreTranscodeOps(string filePath, string newPath, string speed)
        {
            var ffProbe = new FFProbe();
            var videoInfo = ffProbe.GetMediaInfo(filePath);
            var fileName = Path.GetFileNameWithoutExtension(filePath);
            var fileExt = Path.GetExtension(filePath);
            int width = videoInfo.Streams[0].Width > -1 ? videoInfo.Streams[0].Width : videoInfo.Streams[1].Width;
            int height = videoInfo.Streams[0].Height > -1 ? videoInfo.Streams[0].Height : videoInfo.Streams[1].Height;
            Logging.WriteLog($"Current video: {filePath}\n- New path is: {newPath} ");
            if (videoInfo.Duration > TimeSpan.FromMinutes(30)) // if vid is longer than 30mins return isLong = true. (right now this is not doing anything).
                Transcode(fileName, filePath, newPath, fileExt, width, height, speed, true);
            else
                Transcode(fileName, filePath, newPath, fileExt, width, height, speed, false);
        }

        static Task<int> Transcode(string fileName, string filePath, string newPath, string ext, int width, int height, string speed, bool isLong)
        {
            try
            {
                var tcs = new TaskCompletionSource<int>();
                newPath += $@"\{ fileName }";
                var process = new Process
                {
                    StartInfo = { FileName = "ffmpeg.exe", Arguments = Arguments.GetWMVArgs(newPath, filePath, width, height, ext, speed) },
                    EnableRaisingEvents = true
                };

                process.Exited += (sender, args) =>
                {
                    tcs.SetResult(process.ExitCode);
                    process.Dispose();
                };
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                    Logging.WriteLog($"Folder: {newPath} created.");
                }
                else
                {
                    Logging.WriteLog($"Folder: {newPath} already exists. Moving on.");
                }

                DirectoriesHandler.CreateVideoFolders(newPath);
                Logging.WriteLog($"Created video folders for {newPath}");
                process.Start();
                string processName = process.ProcessName;
                Logging.WriteLog($"Started proccess: {processName}");
                process.WaitForExit();
                Logging.WriteLog($"Exited proccess: {processName}");
                return tcs.Task;
            }
            catch (Exception ex)
            {
                Logging.WriteLog($"Exception occured: {ex.Message}");
                throw ex;
            }
        }
    }
}
