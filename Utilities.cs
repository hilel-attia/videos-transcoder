using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace VideoConverter
{
    public class Utilities
    {
        public static bool IsMedia(string ext)
        {
            string [] allowedExtenstions =
            {
                ".WAV",
                ".MID",
                ".MIDI",
                ".WMA",
                ".MP3",
                ".OGG",
                ".RMA",
                ".AVI",
                ".MP4",
                ".DIVX",
                ".WMV"
            };

            if (Array.IndexOf(allowedExtenstions, ext) != -1)
                return true;
            return false;
        }

        public static void Countdown(int seconds = 3)
        {
            for (int i = seconds; i >= 0; i--)
            {
                Console.Write("\rStarting in {0:00}", i);
                Thread.Sleep(1000);
            }
        }

        public static string Presets(string speed)
        {
            // A preset is a collection of options that will provide a certain encoding speed to compression ratio. A slower preset will provide better compression (compression is quality per filesize). This means that, for example, if you target a certain file size or constant bit rate, you will achieve better quality with a slower preset. Similarly, for constant quality encoding, you will simply save bitrate by choosing a slower preset.
            speed = speed.ToLower();
            string[] presets =
            {
                "ultrafast",
                "superfast",
                "veryfast",
                "faster",
                "fast",
                "medium", // default preset 
                "slow",
                "slower",
                "veryslow"
            };
            if (Array.IndexOf(presets, speed) != -1)
                return speed;
            return string.Empty;
        }

        public static double ToMb(long bytes)
        {
            return (bytes / 1024f) / 1024f;
        }
    }
}
