using System;
using System.Collections.Generic;
using System.Text;
using System.Media;
using System.Threading;
using System.Threading.Tasks;

namespace projektGra
{
    public class MusicPlay
    {
        public static SoundPlayer MusicPlayer = new SoundPlayer();

        public static void LoadMusic(string path)
        {
            try
            {
                MusicPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + path;
            }
            catch { }
        }
        public static void PlayMusic()
        {
            try 
            { 
                MusicPlayer.PlayLooping(); 
            }
            catch { }
        }
        public static void Stop()
        {
            try
            {
                MusicPlayer.Stop();
            }
            catch { }
        }
        public static void CollectFood()
        {
            Beep(3000, 50);
            Beep(2500, 50);
        }
        public static void Eat()
        {
            Beep(3000, 50);
        }
        public static void DMG()
        {
            Beep(300, 100);
        }
        public static void Transition()
        {
            try
            {
                MusicPlayer.Play();
            }
            catch { }
        }
        async private static void Beep(int f, int d)
        {
            await Task.Run(() => {
                Console.Beep(f, d);
            });
        }

    }
}
