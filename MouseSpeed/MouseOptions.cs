using System;
using System.Runtime.InteropServices;

namespace MouseSpeed
{
    public class MouseOptions
    {

        /// <summary>
        /// Use System Parameters Info from User32 to get the current mouse speed.
        /// </summary>
        /// <returns>Current mouse speed.</returns>
        public static int GetMouseSpeed()
        {
            IntPtr Ptr;
            Ptr = Marshal.AllocCoTaskMem(4);
            SPI.SystemParametersInfo(SPI.SPI_GETMOUSESPEED, 0, Ptr, 0);
            int Speed = Marshal.ReadInt32(Ptr);
            Marshal.FreeCoTaskMem(Ptr);

            return Speed;
        }

        /// <summary>
        /// Use System Parameters Info to set the mouse speed to a new value.
        /// </summary>
        /// <param name="speed">Value to set the mouse speed to. Accepts integers from 1 to 20.</param>
        /// <returns>True if speed was changed successfully.</returns>
        public static bool SetMouseSpeed(int speed)
        {
            // Range of values allowed by Windows is 1 to 20.
            if (speed < 1 || speed > 20)
            {                
                throw new ArgumentOutOfRangeException("Valid speeds are integer values from 1 to 20.");
            }

            IntPtr ptr = new IntPtr(speed);
            return SPI.SystemParametersInfo(SPI.SPI_SETMOUSESPEED, 0, ptr, SPI.SPIF_SENDWININICHANGE);            
        }
    }
}

