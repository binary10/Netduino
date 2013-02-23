using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.NetduinoPlus;

namespace ServoPotControl
{
    public class Program
    {
        public static void Main()
        {
            // Create servo object
            PWM servo = new PWM(Pins.GPIO_PIN_D9);
            
            // Create an input variable that represents the potentiometer
            AnalogInput pot = new AnalogInput(Pins.GPIO_PIN_A0);
            // Set range to fit the pulse width range for position
            pot.SetRange(750, 2250);
            
            while (true)
            {
                // Update position reading
                int position = pot.Read();
                // Set position based on reading
                servo.SetPulse(20000, (uint)position);

                // Wait to get to the position
                Thread.Sleep(25);
            }
        }

    }
}
