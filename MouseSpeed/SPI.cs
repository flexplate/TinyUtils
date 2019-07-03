using System;
using System.Runtime.InteropServices;

namespace MouseSpeed
{
    /// <summary>
    /// References the SystemParametersInfo method from WinUser.
    /// More details at https://docs.microsoft.com/en-us/windows/desktop/api/winuser/nf-winuser-systemparametersinfoa
    /// </summary>
    internal class SPI
    {
        public const uint SPI_GETMOUSESPEED = 0x0070;
        public const uint SPI_SETMOUSESPEED = 0x0071;
        public const uint SPIF_UPDATEINIFILE = 0x01;
        public const uint SPIF_SENDWININICHANGE = 0x02;

        /// <summary>
        /// Retrieves or sets the value of one of the system-wide parameters. This function can also update the user profile while setting a parameter.
        /// </summary>
        /// <param name="uiAction">The system-wide parameter to be retrieved or set. The possible values are organized in a table at the above URI</param>
        /// <param name="uiParam">Usage depends on which parameter value is set in <paramref name="uiAction"/>. For this app, we leave it at 0.</param>
        /// <param name="pvParam">Usage depends on which parameter value is set in <paramref name="uiAction"/>. For this app, we reference it to an IntPtr to get/set the mouse speed.</param>
        /// <param name="fWinIni">If a system parameter is being set, specifies whether the user profile is to be updated,
        /// and if so, whether the WM_SETTINGCHANGE message is to be broadcast to all top-level windows to notify them of the change. For this app we leave it at </param>
        /// <returns>If the function succeeds, the return value is a nonzero value. If the function fails, the return value is zero.To get extended error information, call GetLastError.</returns>
        [DllImport("User32.dll")]
        public static extern bool SystemParametersInfo(UInt32 uiAction, UInt32 uiParam, IntPtr pvParam, UInt32 fWinIni);
    }
}
