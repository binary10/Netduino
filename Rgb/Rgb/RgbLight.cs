using System;
using System.Threading;
using Microsoft.SPOT;
using SecretLabs.NETMF.Hardware.NetduinoPlus;
using Toolbox.NETMF.Hardware;

namespace Rgb
{
    class RgbLight
    {
        RgbLed light = new RgbLed(PWMChannels.PWM_PIN_D5
            , PWMChannels.PWM_PIN_D6
            , PWMChannels.PWM_PIN_D9
            , true);

        Random r = new Random();
        byte[] red = new byte[1];
        byte[] green = new byte[1];
        byte[] blue = new byte[1];

        ExtendedTimer t;

        public void Rgb()
        {
            t = new Microsoft.SPOT.ExtendedTimer(new TimerCallback(OnChangeLightColor)
                                                        , null
                                                        , 0
                                                        , 100);
        }

        private void OnChangeLightColor(object state)
        {
            r.NextBytes(red);
            r.NextBytes(green);
            r.NextBytes(blue);
            light.Write(red[0], green[0], blue[0]);
        }


    }
}
