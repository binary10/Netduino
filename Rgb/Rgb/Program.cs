using System;
using System.Threading;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware.NetduinoPlus;
using Toolbox.NETMF.Hardware;

/*
    1    ---O   --5--   O---    5
    2    ---O |1     6| O---    6
            | |       | G       GND
    GND     G   --2--   |
            | |3     8| |
    3    ---O |       | O---    7   (Decimal Point)
    4    ---O   --4--   O---    8

*/
namespace Rgb
{
    public class Program
    {
       

        public static void Main()
        {
            OutputPort o = new OutputPort(Pins.ONBOARD_LED, false);
            var timer_onboard = new ExtendedTimer(new TimerCallback(test), o, 10, 100);
            var light = new RgbLight();
            Thread.Sleep(Timeout.Infinite);
        }

        public static void test(object state)
        {
            var light = (OutputPort)state;
        }

    }

    public class ToggleOutputPort : OutputPort
    {
        
    }
}
