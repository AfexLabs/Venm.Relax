using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Magic;
using Venm.Relax.Patchables;

namespace Venm.Relax.Core
{
    public class OsuManager
    {
        private static BlackMagic _osu;
        private static Thread _checker;

        public static OsuManager Instance = new OsuManager();

        public OsuManager()
        {
            try
            {
                var osuProc = Process.GetProcessesByName("osu!").FirstOrDefault();
                _osu = new BlackMagic(osuProc.Id);
                Init();
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"Exception: " + ex);
            }
        }

        private void Init()
        {

        }

        public int CurrentAudioTime
        {
            get { return _osu.ReadInt((uint)Offsets.Osu.TimingAddress); }
        }
    }
}
