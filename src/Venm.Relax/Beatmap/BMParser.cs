using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Venm.Relax.Beatmap
{
    public class BMParser
    {
        public List<TimingPoint> TimingPoints = new List<TimingPoint>();
        public List<HitObject> HitObjects = new List<HitObject>();
        public string SongTitle;
        public string SongVersion;
        private float SliderMultiplier;

        public static BMParser Instance = new BMParser();

        public void ParseBeatmap(string beatmap)
        {
            using (StreamReader sR = new StreamReader(beatmap))
            {
                bool General = false;
                bool Metadata = false;
                bool Timing = false;
                bool Hits = false;
                bool Difficulty = false;

                while (sR.Peek() != -1)
                {
                    string str = sR.ReadLine();

                    if (str.Contains("[General]"))
                    {
                        General = true;
                    }
                    else if (General)
                    {
                        // Get useful information from the general section here...

                        if (!str.Contains(':'))
                        {
                            General = false;
                        }
                    }
                    else if (str.Contains("[Metadata]"))
                    {
                        Metadata = true;
                    }
                    else if (Metadata)
                    {
                        if (str.Contains("Title:"))
                        {
                            SongTitle = str.Split(':')[1];
                        }
                        else if (str.Contains("Version:"))
                        {
                            SongVersion = str.Split(':')[1];
                        }
                        else if (!str.Contains(':'))
                        {
                            Metadata = false;
                        }
                    }
                    else if (str.Contains("[Difficulty]"))
                    {
                        Difficulty = true;
                    }
                    else if (Difficulty)
                    {
                        if (str.Contains("SliderMultiplier"))
                        {
                            SliderMultiplier = float.Parse(str.Split(':')[1]);
                        }
                        else if (!str.Contains(':'))
                        {
                            Difficulty = false;
                        }
                    }
                    else if (str.Contains("[TimingPoints]"))
                    {
                        Timing = true;
                    }
                    else if (Timing)
                    {
                        if (!str.Contains(','))
                        {
                            Timing = false;
                        }
                        else
                        {
                            TimingPoint timingPoint = new TimingPoint(str);
                            TimingPoints.Add(timingPoint);
                        }
                    }
                    else if (str.Contains("[HitObjects]"))
                    {
                        Hits = true;
                    }
                    else if (Hits)
                    {
                        if (!str.Contains(','))
                        {
                            Hits = false;
                        }
                        else
                        {
                            HitObject hitObject = new HitObject(str, TimingPoints, SliderMultiplier);
                            HitObjects.Add(hitObject);
                        }
                    }
                }
            }
        }
    }
}
