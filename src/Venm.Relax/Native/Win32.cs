using System;
using System.Runtime.InteropServices;

namespace Venm.Relax.Native
{
    public class Win32
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

        public const int MOUSEEVENTF_LEFTDOWN = 0x02;
        public const int MOUSEEVENTF_LEFTUP = 0x04;
        public const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        public const int MOUSEEVENTF_RIGHTUP = 0x10;

        [DllImport("user32.dll", EntryPoint = "SendInput", SetLastError = true)]
        public extern static uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

        [StructLayout(LayoutKind.Sequential)]
        public struct MOUSEINPUT
        {
            public int dx;
            public int dy;
            public int mouseData;
            public int dwFlags;
            public int time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct KEYBDINPUT
        {
            public short wVk;
            public short wScan;
            public int dwFlags;
            public int time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct HARDWAREINPUT
        {
            public int uMsg;
            public short wParamL;
            public short wParamH;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct INPUT
        {
            [FieldOffset(0)]
            public int type;
            [FieldOffset(4)]
            public MOUSEINPUT mi;
            [FieldOffset(4)]
            public KEYBDINPUT ki;
            [FieldOffset(4)]
            public HARDWAREINPUT hi;
        }

        public static uint PressKey(int key)
        {
            INPUT input_down = new INPUT();
            input_down.ki.wVk = (short)key;
            input_down.ki.dwFlags = 1;
            input_down.type = 1;

            INPUT[] input = { input_down };

            return SendInput(1, input, Marshal.SizeOf(input_down));
        }
        public static uint ReleaseKey(int key)
        {
            INPUT input_down = new INPUT();
            input_down.ki.wVk = (short)key;
            input_down.ki.dwFlags = 2;
            input_down.type = 1;

            INPUT[] input = { input_down };

            return SendInput(1, input, Marshal.SizeOf(input_down));
        }
    }
}
