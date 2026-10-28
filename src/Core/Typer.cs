using System.Runtime.InteropServices;

namespace zywrite.Core
{
    public static class Typer
    {
        private const uint INPUT_KEYBOARD = 1;
        private const uint KEYEVENTF_KEYUP = 0x0002;

        [StructLayout(LayoutKind.Sequential)]
        private struct INPUT
        {
            public uint type;
            public KEYBDINPUT ki;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct KEYBDINPUT
        {
            public ushort wVk;
            public ushort wScan;
            public uint dwFlags;
            public uint time;
            public UIntPtr dwExtraInfo;
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

        private static INPUT CreateKeyInput(int vkCode, bool keyUp = false)
        {
            return new INPUT
            {
                type = INPUT_KEYBOARD,
                ki = new KEYBDINPUT
                {
                    wVk = (ushort)vkCode,
                    wScan = 0,
                    dwFlags = keyUp ? KEYEVENTF_KEYUP : 0,
                    time = 0,
                    dwExtraInfo = UIntPtr.Zero
                }
            };
        }

        public static void PressKey(int vkCode)
        {
            INPUT[] inputs =
            [
                CreateKeyInput(vkCode),
                CreateKeyInput(vkCode, keyUp: true)
            ];

            uint sent = SendInput((uint)inputs.Length, inputs, Marshal.SizeOf<INPUT>());

            if (sent != inputs.Length)
            {
                throw new InvalidOperationException($"SendInput inserted {sent} of " + $"{inputs.Length} events.");
            }
        }

        public static void PressShiftedKey(int vkCode)
        {
            INPUT[] inputs =
            [
                CreateKeyInput((byte)Keycodes.vkLeftShift),
                CreateKeyInput(vkCode),
                CreateKeyInput(vkCode, keyUp: true),
                CreateKeyInput((byte)Keycodes.vkLeftShift, keyUp: true)
            ];

            uint sent = SendInput((uint)inputs.Length, inputs, Marshal.SizeOf<INPUT>());

            if (sent != inputs.Length)
            {
                throw new InvalidOperationException($"SendInput inserted {sent} of " + $"{inputs.Length} events.");
            }
        }
    }
}
