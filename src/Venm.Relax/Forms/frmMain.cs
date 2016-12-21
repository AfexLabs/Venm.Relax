using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Venm.Relax.Beatmap;
using Venm.Relax.Core;
using Venm.Relax.Native;

namespace Venm.Relax.Forms
{
    public partial class frmMain : Form
    {
        private readonly KeyboardHook _keyboardHook = new KeyboardHook();
        private Thread _relaxThread;

        private bool _playing = false;

        public frmMain()
        {
            InitializeComponent();
            _keyboardHook.KeyPressed += new EventHandler<KeyPressedEventArgs>(hook_KeyPressed);
            _keyboardHook.RegisterHotKey(Native.ModifierKeys.Control, Keys.E);

        }

        void hook_KeyPressed(object sender, KeyPressedEventArgs e)
        {
            _playing = !_playing;

            if (_playing)
            {
                _relaxThread = new Thread(new ThreadStart(RelaxThread))
                {
                    IsBackground = true
                };
                _relaxThread.Start();
            }
            else
            {
                _relaxThread.Abort();
                _relaxThread = null;
            }
        }

        private void RelaxThread()
        {
            while (_playing)
            {
                if (_playing)
                {
                    for (int i = 0; i < BMParser.Instance.HitObjects.Count; i++)
                    {
                        InputSender.ReleaseKeys();

                        while (OsuManager.Instance.CurrentAudioTime < BMParser.Instance.HitObjects[i].getStartTime())
                        {
                            Thread.Sleep(1);
                        }

                        if (BMParser.Instance.HitObjects[i].isSlider())
                        {
                            InputSender.Press();

                            while (OsuManager.Instance.CurrentAudioTime < BMParser.Instance.HitObjects[i].getEndTime())
                            {
                                Thread.Sleep(1);
                            }
                        }
                        else if (BMParser.Instance.HitObjects[i].isSpinner())
                        {
                            InputSender.Press();

                            while (OsuManager.Instance.CurrentAudioTime < BMParser.Instance.HitObjects[i].getEndTime())
                            {
                                Thread.Sleep(1);
                            }
                        }
                        else
                        {
                            InputSender.FourKeyClick();
                        }
                    }
                }
                else
                {
                    break;
                }
            }
        }

        #region GUI Updating

        private void guiTimer_Tick(object sender, EventArgs e)
        {
            lblCurrentTime.Text = @"Current time: " + OsuManager.Instance.CurrentAudioTime;
        }

        #endregion

        private void btnLoad_Click(object sender, EventArgs e)
        {
            BMParser.Instance.HitObjects = new List<HitObject>();
            BMParser.Instance.TimingPoints = new List<TimingPoint>();

            OpenFileDialog ofn = new OpenFileDialog();
            ofn.Filter = @"Osu Beatmap (*.osu)|*.osu";
            DialogResult result = ofn.ShowDialog();
            if (result == DialogResult.OK)
            {
                BMParser.Instance.ParseBeatmap(ofn.FileName);
            }

            Console.WriteLine(@"HitObjects: " + BMParser.Instance.HitObjects.Count);
        }
    }
}
