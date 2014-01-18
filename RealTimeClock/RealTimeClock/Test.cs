using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware.NetduinoPlus;
using System.Threading;

namespace RealTimeClock
{
    class Test
    {
        public static void DS3232()
        {
            RealTimeClock.Hardware.DS3232 d = new RealTimeClock.Hardware.DS3232();
            byte[] b = d.Dump();
        }
        public static void Thermometer()
        {
            DS3232Connection ds3232 = new DS3232Connection();
            Thermometer t = new Thermometer(ds3232);

            while (true)
            {
                Debug.Print(t.Temperature.ToString());
                Thread.Sleep(1000);
                //Thread.Sleep(64000); // Every 64 seconds poll for value
            }
        }
        public static void SquareWave()
        {
            DS3232Connection ds3232 = new DS3232Connection();
            SquareOscillator sqw = new SquareOscillator(ds3232);

            InputPort i = new InputPort(Pins.GPIO_PIN_D0, false, Port.ResistorMode.PullUp);
            OutputPort o = new OutputPort(Pins.ONBOARD_LED, false);

            sqw.EnableOnBattery();
            sqw.SetFrequency((int)SQWFrequencyCode.SQW_1_Hz);
            sqw.Start();

            int times = 0;

            while (times < 500)
            {
                o.Write(i.Read());
                Thread.Sleep(10);
                times++;
            }
            
            sqw.Stop();
            sqw.DisableOnBattery();
            o.Write(false);

        }
        public static void TimeOscillator()
        {
            RealTimeClock.TimeOscillator.Test();
        }
        public static void Memory()
        {
            DS3232Connection ds3232 = new DS3232Connection();
            Memory m = new Memory(ds3232);
            
            // Memory map before
            byte[] dump = m.Dump();
            
            m.DeleteMemory();

            // Memory map after
            dump = m.Dump();
        }


        // Explorations into I2C usage
        public static void Sketch()
        {
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
        }
        public static void Sketch01()
        {
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
        }
    }
}
