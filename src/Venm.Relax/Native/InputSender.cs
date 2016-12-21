using System.Threading;

namespace Venm.Relax.Native
{
    class InputSender
    {
        private static string _curKey = "X";
        private static int curIndex = 0;

        public static void Click()
        {
            Press();
            Thread.Sleep(2);
            ReleaseKeys();
        }

        public static void FourKeyClick()
        {
            switch (curIndex)
            {
                case 0:
                    Win32.PressKey('Z');
                    Thread.Sleep(2);
                    Win32.ReleaseKey('Z');
                    curIndex++;
                    break;
                case 1:
                    Win32.PressKey('X');
                    Thread.Sleep(2);
                    Win32.ReleaseKey('X');
                    curIndex++;
                    break;
                case 2:
                    Win32.mouse_event(Win32.MOUSEEVENTF_LEFTDOWN | Win32.MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                    curIndex++;
                    break;
                case 3:
                    Win32.mouse_event(Win32.MOUSEEVENTF_RIGHTDOWN | Win32.MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
                    curIndex = 0;
                    break;
            }
        }

        public static void Press()
        {
            switch (curIndex)
            {
                case 0:
                    Win32.PressKey('Z');
                    Thread.Sleep(2);
                    curIndex++;
                    break;
                case 1:
                    Win32.PressKey('X');
                    Thread.Sleep(2);
                    curIndex++;
                    break;
                case 2:
                    Win32.mouse_event(Win32.MOUSEEVENTF_LEFTDOWN, (uint)0, (uint)0, 0, 0);
                    curIndex++;
                    break;
                case 3:
                    Win32.mouse_event(Win32.MOUSEEVENTF_RIGHTDOWN, (uint)0, (uint)0, 0, 0);
                    curIndex = 0;
                    break;
            }
        }

        public static void ReleaseKeys()
        {
            Win32.ReleaseKey('X');
            Thread.Sleep(2);
            Win32.ReleaseKey('Z');
            Thread.Sleep(2);
            Win32.mouse_event(Win32.MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
            Win32.mouse_event(Win32.MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
        }
    }
}
