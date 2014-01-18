using System;
using Microsoft.SPOT;

namespace RealTimeClock
{

    class Byte
    {
        private byte _b;

        public Byte(byte b_init)
        {
            b = b_init;
        }

        // Byte property
        public byte b
        {
            get { return _b; }
            set { _b = value; }
        }

        public void set_bit(int bit_position, bool bit)
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


        public void toggle_bit(int bit_position)
        {
            byte mask_byte = Utility.mask_byte(bit_position);
            this._b = (byte)(this._b ^ mask_byte);
        }


        public bool get_bit(int bit_position, bool logic = true)
        {
            // use logic to choose between returning positive or negative logic
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

        public string ToString()
        {
            return b.ToString();
        }
    }

}
