using System;
using Microsoft.SPOT;

namespace RealTimeClock
{
    class Utility
    {
        public static byte mask_byte(int bit_position)
        {
            return (byte)System.Math.Pow(2, bit_position);
        }

        public static byte decToBcd(byte val)
        {
            return 0x00; //((val / 10 * 16) + (val % 10));
        }

        public static byte bcdToDec(byte val)
        {
            return 0x00;
        }
    }
}
