namespace Venm.Relax.Beatmap
{
    public class TimingPoint
    {
        private int TimingTime;
        private float BPM;

        public TimingPoint(string timingString)
        {
            var tokens = timingString.Split(',');
            TimingTime = (int)float.Parse(tokens[0]);
            BPM = float.Parse(tokens[1]);
        }

        public float getBPM() { return BPM; }

        public int getTime() { return TimingTime; }
    }
}
