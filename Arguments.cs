using System.IO;

namespace VideoConverter
{
    class Arguments
    {

        public static string GetArg(string fileName, string filePath, int width, int height, string ext = "mp4")
        {
            var folderPath = $@"{fileName}\v";
            var videoPath = filePath;
            return string.Format("-hide_banner -y" +
                " -i {0} -preset ultrafast -vf scale=842:-2 -vcodec libx264 -threads 8 -c:a aac -ar 48000 -c:v h264 -profile:v main -crf 20 -sc_threshold 0 -g 48 -keyint_min 48 -hls_time 10 -hls_playlist_type vod -b:v 1400k -maxrate 1498k -bufsize 2100k -b:a 128k -hls_segment_filename " + folderPath + "/480p_%d.ts " + folderPath + "/480p.m3u8" +
                " -i {0} -vf scale=1280:-2 -vcodec libx264 -threads 8 -c:a aac -ar 48000 -c:v h264 -profile:v main -crf 20 -sc_threshold 0 -g 48 -keyint_min 48 -hls_time 10 -hls_playlist_type vod -b:v 2800k -maxrate 2996k -bufsize 4200k -b:a 128k -hls_segment_filename " + folderPath + "/720p_%d.ts " + folderPath + "/720p.m3u8" +
                " -i {0} -vf scale=1920:-2 -vcodec libx264 -threads 8 -c:a aac -ar 48000 -c:v h264 -profile:v main -crf 20 -sc_threshold 0 -g 48 -keyint_min 48 -hls_time 10 -hls_playlist_type vod -b:v 5000k -maxrate 5350k -bufsize 7500k -b:a 192k -hls_segment_filename " + folderPath + "/1080p_%d.ts " + folderPath + "/1080p.m3u8", videoPath);
        }

        public static string GetLongVideoArgs(string fileName, string filePath, int width, int height, string ext = "mp4")
        {
            var folderPath = $@"{fileName}\v";
            var videoPath = filePath;
            return string.Format("-hide_banner -y" +
                " -i {0} -preset ultrafast -vf scale=842:-2 -vcodec libx264 -threads 8 -c:a aac -ar 48000 -c:v h264 -profile:v main -crf 20 -sc_threshold 0 -g 48 -keyint_min 48 -hls_time 60 -hls_playlist_type vod -b:v 1400k -maxrate 1498k -bufsize 2100k -b:a 128k -hls_segment_filename " + folderPath + "/480p_%d.ts " + folderPath + "/480p.m3u8" +
                " -i {0} -preset ultrafast -vf scale=1280:-2 -vcodec libx264 -threads 8 -c:a aac -ar 48000 -c:v h264 -profile:v main -crf 20 -sc_threshold 0 -g 48 -keyint_min 48 -hls_time 60 -hls_playlist_type vod -b:v 2800k -maxrate 2996k -bufsize 4200k -b:a 128k -hls_segment_filename " + folderPath + "/720p_%d.ts " + folderPath + "/720p.m3u8" +
                " -i {0} -preset ultrafast -vf scale=1920:-2 -vcodec libx264 -threads 8 -c:a aac -ar 48000 -c:v h264 -profile:v main -crf 20 -sc_threshold 0 -g 48 -keyint_min 48 -hls_time 60 -hls_playlist_type vod -b:v 5000k -maxrate 5350k -bufsize 7500k -b:a 192k -hls_segment_filename " + folderPath + "/1080p_%d.ts " + folderPath + "/1080p.m3u8", videoPath);
        }

        public static string GetWMVArgs(string fileName, string filePath, int width, int height, string ext, string speed = "fast")
        {
            var folderPath = $@"{fileName}\v";
            var videoPath = filePath;

            return $@"-i ""{ videoPath }"" -preset {speed} -c:v libx264 -f ssegment -vf scale={width}:{height} -hls_flags delete_segments -c:a aac -b:a 128k -ac 2 -segment_list ""{folderPath}\index.m3u8"" -segment_list_type hls -segment_list_size 0 ""{folderPath}\out_%d.ts""";
        }
    }
}
