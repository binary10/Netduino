using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.NetduinoPlus;
using Toolbox.NETMF;
using Toolbox.NETMF.Hardware;

namespace RealTimeClock
{

    class DS3232Connection
    {
        private MultiI2C _DS3232;
        private const ushort I2C_ADDRESS = 0x68;

        public DS3232Connection()
        {
            _DS3232 = new MultiI2C(I2C_ADDRESS, 100);
        }

        // Read Operations
        public byte[] Read(byte address, int bytes)
        {
            byte[] readBytes  = new byte[bytes];

            int write_bytes = WriteAddress(address);
            int read_bytes = _DS3232.Read(readBytes);

            return readBytes; 
        }
        public byte Read(byte address)
        {
            return Read(address, 1)[0];
        }

        // Write Operations
        public int WriteAddress(byte address)
        {
            return _DS3232.Write(new byte[1] { address });
        }
        public int Write(byte address, byte data)
        {
            byte[] write_bytes = new byte[2] { address, data };
            return _DS3232.Write(write_bytes);
        }
        // Input:  Byte array with address at position zero and data following
        // Output: Writes byte to address and increments for subsequent data
        // Returns: An integer representing bytes written to the connection
        public int Write(byte[] address_data)
        {
            return _DS3232.Write(address_data);
        }
        public int Write(byte address, byte[] data)
        {
            // Create a new array
            byte[] write_bytes = new byte[data.Length + 1];

            // Populate new array
            write_bytes[0] = address;
            for (int i = 1; i < write_bytes.Length; i++)
            {

                write_bytes[i] = data[i - 1];
            }

            return _DS3232.Write(write_bytes);
        }

        // Bit level operation
        public void WriteBit(byte register, int bit_position, bool bit)
        {
            Byte b = new Byte(Read(register));     // Read control register
            b.SetBit(bit_position, bit);           // Modify oscillator bit

            // Write new byte value
            Write(register, b.ToByte());

        }
        public void ToggleBit(byte register, int bit_position)
        {
            Byte b = new Byte(Read(register));     // Read control register
            b.ToggleBit(bit_position);                // Modify oscillator bit

            // Write new byte value
            Write(register, b.ToByte());

        }
        
    }
}
