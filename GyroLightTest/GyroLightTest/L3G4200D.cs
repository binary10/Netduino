using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.NetduinoPlus;

namespace Gyro
{
    class L3G4200D
    {
        SPI.Configuration spi_settings;
        SPI gyro_module;

        public L3G4200D()
        {
                        // write your code here
            spi_settings = new SPI.Configuration(SecretLabs.NETMF.Hardware.NetduinoPlus.Pins.GPIO_PIN_D9
                                          , true
                                          , 100
                                          , 0
                                          , false
                                          , true
                                          , 100
                                          , SPI_Devices.SPI1
                                          );

            gyro_module = new SPI(spi_settings);

        }




    }
}
