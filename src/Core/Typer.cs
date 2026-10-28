using System.Runtime.InteropServices;
using static zywrite.Core.CharacterMapper;

namespace zywrite.Core
{
    public static partial class Typer
    {
        # region SendInput Function (copy and pasted from Microsoft don't touch)
        [StructLayout(LayoutKind.Sequential)]
        private struct INPUT
        {
            public uint type;
            public INPUTUNION U;
        }

        [StructLayout(LayoutKind.Explicit)]
        private struct INPUTUNION
        {
            [FieldOffset(0)]
            public MOUSEINPUT mi;

            [FieldOffset(0)]
            public KEYBDINPUT ki;

            [FieldOffset(0)]
            public HARDWAREINPUT hi;
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

        [StructLayout(LayoutKind.Sequential)]
        private struct MOUSEINPUT
        {
            public int dx;
            public int dy;
            public uint mouseData;
            public uint dwFlags;
            public uint time;
            public UIntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct HARDWAREINPUT
        {
            public uint uMsg;
            public ushort wParamL;
            public ushort wParamH;
        }

        [LibraryImport("user32.dll", SetLastError = true)]
        private static partial uint SendInput(uint nInputs, [In] INPUT[] pInputs, int cbSize);

        private static INPUT CreateKeyInput(int vkCode, bool keyUp = false)
        {
            return new INPUT
            {
                type = 1, // INPUT_KEYBOARD
                U = new INPUTUNION
                {
                    ki = new KEYBDINPUT
                    {
                        wVk = (ushort)vkCode,
                        wScan = 0,
                        dwFlags = keyUp ? 0x0002u : 0u,
                        time = 0,
                        dwExtraInfo = UIntPtr.Zero
                    }
                }
            };
        }
        # endregion

        // ingests string
        public static async Task TypeString(string text)
        {
            await Task.Delay(1000);

            foreach (char character in text)
            {
                // processes every character one at a time
                await TypeCharacter(character);
            }
        }

        private static async Task TypeCharacter(char character)
        {
            // calls mapper to get action for character
            KeyAction action = GetAction(character);

            // executes tpying action
            if (action.ShiftRequired)
            {
                await PressShiftedKey(action.VkCode);
            }
            else
            {
                await PressKey(action.VkCode);
            }
        }

        // normal key
        private async static Task PressKey(int vkCode)
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

        // shifted key
        private async static Task PressShiftedKey(int vkCode)
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
