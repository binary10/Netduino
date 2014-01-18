using System;
using Microsoft.SPOT;

namespace RealTimeClock
{
    class Memory
    {
        private DS3232Connection _ds3232;

        public int BytesTotal
        {
            get { return Registers.SRAM_LAST - Registers.SRAM_FIRST + 1; }
        }

        public Memory(DS3232Connection d)
        {
            this._ds3232 = d;
        }

        // Get a byte at an address
        public byte Read(byte address)
        {
            if (ValidateAddress(address))
            {
                return _ds3232.Read(address);
            }
            else
            {
                return 0;
            }
        }

        // Set a byte at an address
        public void Write(byte address, byte data)
        {
            if (ValidateAddress(address))
            {
                _ds3232.Write(address, data);
            }
        }

        // Delete byte
        public void Delete(byte address)
        {
            if (ValidateAddress(address))
            {
                _ds3232.Write(address, 0);
            }
        }

        // Clears memory to all zero
        public void DeleteMemory()
        {
            // Create an empty memory array
            byte[] m = new byte[BytesTotal];
            _ds3232.Write(Registers.SRAM_FIRST, m);
        }

        // Dump memory map
        public byte[] Dump()
        {
            return _ds3232.Read(Registers.SRAM_FIRST, BytesTotal);
        }

        // Verify that the address provided exists in the Memory range
        private bool ValidateAddress(byte address)
        {
            return (Registers.SRAM_FIRST <= address) && (address <= Registers.SRAM_LAST);
        }

        /* TODO:
         * Create an implementation for these functions assuming zero byte is null
        
        public int BytesAvailable
        {
            get { return 3; }
        }
        public int BytesRemaining
        {
            get { return 4; }
        }
        public double PercentAvailable
        {
            get { return (double)BytesAvailable / BytesTotal; }
        }
        */
    }
}
