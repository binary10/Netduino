using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.NetduinoPlus;
using Toolbox.NETMF;

namespace RealTimeClock
{
    public class Program
    {
       

            /*
            // Create the config object with device address and clock speed
            I2CDevice.Configuration c = new I2CDevice.Configuration(0x68, 100);
            I2CDevice d = new I2CDevice(c);

            byte[] read_byte = new byte[1];

            byte test_value = 0x00;

            while (test_value < 0xff)
            {
                // Read the register
                I2CDevice.I2CWriteTransaction w = I2CDevice.CreateWriteTransaction(new byte[] { 0x14 });
                I2CDevice.I2CReadTransaction r = I2CDevice.CreateReadTransaction(read_byte);
                int bytes_exchanged = d.Execute(new I2CDevice.I2CTransaction[] { w, r }, 100);

                // Write to the register
                w = I2CDevice.CreateWriteTransaction(new byte[] { 0x14, test_value });
                bytes_exchanged = d.Execute(new I2CDevice.I2CTransaction[] { w }, 100);

                foreach (byte b in read_byte)
                {
                    Debug.Print(b.ToString());
                }
                test_value++;

                Thread.Sleep(3000);
            }
             */



            /*
            //create I2C object
            //note that the netmf i2cdevice configuration requires a 7-bit address! It set the 8th R/W bit automatically.
            I2CDevice.Configuration con =
               new I2CDevice.Configuration(0x68, 100);
            I2CDevice MyI2C = new I2CDevice(con);


            // Create transactions
            // We need 2 in this example, we are reading from the device
            // First transaction is writing the "read command"
            // Second transaction is reading the data
            I2CDevice.I2CTransaction[] xActions =
                new I2CDevice.I2CTransaction[2];

            // create write buffer (we need one byte)
            byte[] RegisterNum = new byte[1] { 0x14 };
            xActions[0] = I2CDevice.CreateWriteTransaction(RegisterNum);
            // create read buffer to read the register
            byte[] RegisterValue = new byte[1];
            xActions[1] = I2CDevice.CreateReadTransaction(RegisterValue);

            // Now we access the I2C bus using a timeout of one second 
            // if the execute command returns zero, the transaction failed (this 
            // is a good check to make sure that you are communicating with the device correctly
            // and don’t have a wiring issue or other problem with the I2C device)
            if (MyI2C.Execute(xActions, 1000) == 0)
            {
                Debug.Print("Failed to perform I2C transaction");
            }
            else
            {
                Debug.Print("Register value: " + RegisterValue[0].ToString());
            }
            */

    }
}