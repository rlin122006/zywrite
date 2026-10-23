using System.Runtime.InteropServices;

namespace zywrite.Core
{
    public static partial class Typer
    {
        #region LibraryImports
        [LibraryImport("user32.dll")]
        private static partial void keybd_event(
            byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);
        #endregion
        private const uint KeyEventKeyUp = 0x0002;

        public static void SimulateKeyPress(int vkCode)
        {
            keybd_event((byte)vkCode, 0, 0, UIntPtr.Zero);
            keybd_event((byte)vkCode, 0, KeyEventKeyUp, UIntPtr.Zero);
        }
    }
}
