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

    class DS3232
    {
        private MultiI2C _DS3232;
        private const ushort I2C_ADDRESS = 0x68;


        
        public DS3232()
        {
            _DS3232 = new MultiI2C(I2C_ADDRESS, 100);
        }

        public byte[] Read(byte address, int bytes)
        {
            byte[] readBytes  = new byte[bytes];

            int write_bytes = WriteAddress(address);
            int read_bytes = _DS3232.Read(readBytes);

            return readBytes; 
        }

        public byte ReadByte(byte address)
        {
            return Read(address, 1)[0];
        }


        private int WriteAddress(byte address)
        {
            return _DS3232.Write(new byte[1] { address });
        }


        private int WriteByte(byte address, byte data)
        {
            byte[] write_bytes = new byte[2] { address, data };
            return _DS3232.Write(write_bytes);
        }


        private int Write(byte address, byte[] data)
        {
            byte[] write_bytes = new byte[data.Length + 1];

            write_bytes[0] = address;
            
            for (int i = 1; i <= write_bytes.Length; i++)
            {
                write_bytes[i] = data[i--];
            }

            return _DS3232.Write(write_bytes);
        }


        public static void Main()
        {
            DS3232 clock = new DS3232();
            Byte b = new Byte(clock.ReadByte(Registers.SRAM_FIRST));
            Debug.Print(b.ToString());

            byte test_value = 0x00;
            while (test_value <= Registers.SRAM_LAST)
            {
                clock.WriteByte(Registers.SRAM_FIRST, test_value);

                b.b = clock.ReadByte(Registers.SRAM_FIRST);
                Debug.Print(b.ToString());
                Debug.Print(b.get_bit((int)ControlBits.Alarm_Interrupt).ToString());
                b.set_bit((int)ControlBits.Alarm_Interrupt, false);
                Debug.Print(b.ToString());
                Thread.Sleep(3000);
                test_value++;
            }
        }
    }
}
