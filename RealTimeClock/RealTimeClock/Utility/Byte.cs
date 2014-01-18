using System;
using Microsoft.SPOT;

namespace RealTimeClock
{

    class Byte
    {
        // TODO
        // Refactor method names
        // Refactor property

        // Constants
        private const int SIGN_BIT_POSITION = 7;
        
        // Private fields
        private byte _b;

        // Constructor
        public Byte(byte b_init)
        {
            _b = b_init;
        }

        // Properties
        public bool Sign
        {
            // Return true if is negative
            get
            {
                return GetBit(SIGN_BIT_POSITION);
            }
        }

        // Methods
        public void SetBit(int bit_position, bool bit)
        {
            byte mask_byte = Utility.mask_byte(bit_position);

            if (bit)
            {
                // Keep all bits the same and set mask bit to 1
                this._b = (byte)(this._b | mask_byte);

            }
            else
            {
                // Keep all bits the same but set mask bit to 0
                this._b = (byte)(this._b & ~mask_byte);
            }
        }
        public void SetBits(byte bit_mask, byte bit_settings)
        {
            // Takes byte mask contains '1' in bit positions to modify
            // and a byte with values to input in those positions.

            // Set values in original byte to zero in mask positions.
            // Taking OR with new settings will only alter mask positions.
            this._b = (byte)((this._b & ~bit_mask) | bit_settings);
        }

        public void ToggleBit(int bit_position)
        {
            byte mask_byte = Utility.mask_byte(bit_position);
            this._b = (byte)(this._b ^ mask_byte);
        }
        public void ToggleBits(byte bit_mask)
        {
            this._b = (byte)(this._b ^ bit_mask);
        }

        public bool GetBit(int bit_position, bool logic = true)
        {
            byte mask_byte = Utility.mask_byte(bit_position);
            bool bit = (byte)(this._b & mask_byte) == mask_byte;

            if (logic)
            {
                return bit;
            }
            else
            {
                return !bit;
            }
        }
        public byte GetBits(byte bit_mask)
        {
            return (byte)(this._b & bit_mask);
        }

        // Binary coded decimal
        public int BCDToDecimal()
        {
            int ones = (_b & 0x0f);
            int tens = (_b & 0xf0) >> 4;
            return (tens * 10) + ones;
        }
        public void DecimalToBCD(int dec)
        {
            byte lowOrder = (byte)(dec % 10);
            byte highOrder = (byte)((dec / 10) << 4);
            _b = (byte)(highOrder | lowOrder);
        }

        // Overrides
        public string ToString()
        {
            return _b.ToString();
        }
        public byte ToByte()
        {
            return this._b;
        }
    }

}
