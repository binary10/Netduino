using System;
using Microsoft.SPOT;

namespace RealTimeClock
{
    static class Utility
    {
        // Returns a byte mask for a given bit (zero index)
        // e.g. mask_byte(3) --> 00001000
        public static byte mask_byte(int bit_position)
        {
            return  (byte)System.Math.Pow(2, bit_position);
        }

        public static byte setting(int setting, int index)
        {
            // TODO:
            // Create logic to validate shift so it stays within a byte
            return (byte)(setting << index);
        }

        public static int twos_complement(byte b)
        {
            // Check if initial bit is negative
            Byte b_obj = new Byte(b);
            bool is_negative = b_obj.Sign;

            if( is_negative )
            {
                return b - (1 << 8);
            }

            return (int)b;
        }
        public static double fractional_twos_complement(byte b, int bits, bool is_negative)
        {
            // Get decimal
            double d = (double)(b >> (8 - bits));

            // Change if sign is negative
            if (is_negative)
            {
                d -= (1 << bits);
            }

            // Create fraction
            return d / (1 << bits);
        }
    }
}
