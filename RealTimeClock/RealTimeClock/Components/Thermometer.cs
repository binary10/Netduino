using System;
using Microsoft.SPOT;

namespace RealTimeClock
{
    class Thermometer
    {
        private DS3232Connection _ds3232;

        private byte _tempMSB;
        private byte _tempLSB;
        private double _temperature;

        public Thermometer(DS3232Connection d)
        {
            this._ds3232 = d;

            // initialize temp to 0x00
            this._tempMSB = 0x00;
            this._tempLSB = 0x00;
            this._temperature = 0x00;
        }

        public double Temperature
        {
            get
            {
                // Extract values from device
                this._tempMSB = _ds3232.Read((int)Registers.TEMPERATURE_MSB);
                this._tempLSB = _ds3232.Read((int)Registers.TEMPERATURE_LSB);

                // Convert bytes to decimal and store last value
                this._temperature = convert_temperature(this._tempMSB, this._tempLSB);

                // Provide temperature
                return this._temperature;
            }
        }

        // Methods

        // TODO
        // Calibrate temperature... 
        // This might be best at DS3232 level as a system function
        // Pass in the oscillator status via oscillator object

        // Static-style functions
        private double convert_temperature(byte tempMSB, byte tempLSB)
        {
            // Convert each value according to datasheet
            double dec_tempMSB = (double) Utility.twos_complement(tempMSB);    // Two's complement
            bool is_negative =  (dec_tempMSB < 0);
            double dec_tempLSB = Utility.fractional_twos_complement(tempLSB, 2, is_negative);

            return dec_tempMSB + dec_tempLSB;
        }

        public double Main()
        {
            // Stub values
            byte tempMSB = 0xef;
            //tempMSB = 0x6b;
            //tempMSB = 0x19;
            byte tempLSB = 0xC0;
            tempLSB = 0x40;

            return convert_temperature(tempMSB, tempLSB);
        }
    }
}
