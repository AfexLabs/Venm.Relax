using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venm.Relax.Beatmap
{
    public class HitObject
    {
        private int startTime;
        private int endTime;
        private int sliderRepeatPoints;
        public float PixelLength;
        private int sliderTime;
        private char sliderType;
        private int hitType;
        private int stackId = 0;

        public HitObject(string hitString, List<TimingPoint> timingPoints, float mapSliderMultiplier)
        {
            endTime = 0;
            var tokens = hitString.Split(',');

            startTime = int.Parse(tokens[2].ToString());
            hitType = int.Parse(tokens[3].ToString());

            if (isSlider())
            {
                var repeatCount = int.Parse(tokens[6].ToString());
                PixelLength = float.Parse(tokens[7]);
                sliderRepeatPoints = repeatCount;
                var beatLengthBase = timingPoints[0].getBPM();
                var BPM = timingPoints[0].getBPM();

                foreach (var p in timingPoints)
                {
                    if (p.getTime() <= startTime)
                    {
                        if (p.getBPM() >= 0.0f)
                        {
                            beatLengthBase = p.getBPM();
                        }
                        BPM = p.getBPM();
                    }
                }

                if (BPM < 0.0f)
                {
                    var newMulti = BPM / -100.0f;
                    BPM = beatLengthBase * newMulti;
                }

                sliderTime = (int)(BPM * (PixelLength / mapSliderMultiplier) / 100.0f);
                endTime = sliderTime * repeatCount + startTime;
            }
            else if (isSpinner())
            {
                endTime = int.Parse(tokens[5].ToString());
            }
            else
            {
                endTime = startTime;
            }
        }

        public bool isSlider()
        {
            return (hitType & 2) > 0;
        }

        public bool isSpinner()
        {
            return (hitType & 8) > 0;
        }

        public int getStartTime()
        {
            return startTime;
        }

        public int getEndTime()
        {
            return endTime;
        }
    }
}
