using System;
using System.Diagnostics;

namespace OverlayKit.Window
{
    using System.Runtime.InteropServices;
    using System;

    public class WinHandler
    {
        const int GWL_EXSTYLE = -20;
        const int WS_EX_LAYERED = 0x80000;
        const int LWA_COLORKEY = 0x1;

        #region Imports

        [DllImport("user32.dll", SetLastError = true)]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        static extern int SetLayeredWindowAttributes(IntPtr hwnd, int crKey, byte bAlpha, int dwFlags);

        #endregion

        /// <summary>
        /// Make the window layered
        /// </summary>
        public static int SetLayered(IntPtr hwnd) =>
            SetWindowLong(hwnd, GWL_EXSTYLE, GetWindowLong(hwnd, GWL_EXSTYLE) | WS_EX_LAYERED);

        /// <summary>
        /// WARNING: Call SetLayered before using this method
        /// <br />
        /// used to set the current layered windows transparency key
        /// </summary>
        /// <param name="hwnd"></param>
        /// <returns></returns>
        public static int LayerTransparency(IntPtr hwnd) =>
            SetLayeredWindowAttributes(hwnd, 0, 0, LWA_COLORKEY);

        /// <summary>
        /// sets the window to use layered (SetLayered) then calls LayerTransparency
        /// </summary>
        /// <param name="hwnd"></param>
        public static void AddTransparencyLayer(IntPtr hwnd)
        {
            SetLayered(hwnd);
            LayerTransparency(hwnd);
        }

        /// <summary>
        /// Process.GetCurrentProcess().MainWindowHandle;
        /// </summary>
        public static IntPtr GetCurrentProcessWindowPtr() => Process.GetCurrentProcess().MainWindowHandle;
    }
}
