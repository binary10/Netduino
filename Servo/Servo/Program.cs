using System;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.NetduinoPlus;

namespace Servo
{

    public class Program
    {
        public static void Main()
        {
            // write your code here
            PWM myservo = new PWM(Pins.GPIO_PIN_D9);

            // All PWM SetPulse parameters are in units of MICROseconds
            // (For your reference: push decimal point over 3 digits for milliseconds)
            // Bottom is the counterclockwisemost position
            // Top is clockwisemost position
            uint angleBottom = 700;
            uint angleTop = 2100;
            uint period = 20000;

            backAndForth(myservo, period, angleTop, angleBottom);

            //InterruptPort button = new InterruptPort(Pins.ONBOARD_SW1, false, 
            //    Port.ResistorMode.Disabled, Port.InterruptMode.InterruptEdgeBoth);

            //button.OnInterrupt += new NativeEventHandler(button_OnInterrupt);
            
        }


        //static void button_OnInterrupt(uint data1, uint data2, DateTime time)
        //{
        //    if (data2 == 0)
        //    {
        //        myservo.SetPulse(period, angleBottom);
        //    }
        //}

        static void backAndForth(PWM myservo, uint period, uint angleTop, uint angleBottom)
        {
            while (true)
            {
                myservo.SetPulse(period, angleBottom);
                Thread.Sleep(1000);
                myservo.SetPulse(period, angleTop);
                Thread.Sleep(1000);
                myservo.SetPulse(period, angleBottom);
            }
        }

        //static void typewriter() 
        //{

        //    // move through the full range of positions
        //    for (uint currentPosition = angleBottom;
        //         currentPosition <= angleTop;
        //         currentPosition += 10)
        //    {
        //        // move the servo to the new position.
        //        myservo.SetPulse(20000, currentPosition);
        //        Thread.Sleep(10);
        //    }

        //    // return to first position and wait a half second.
        //    myservo.SetPulse(20000, angleBottom);
        //}
    }
}
