using System;
using Microsoft.SPOT;

namespace RealTimeClock.Hardware
{
    class DS3232
    {
        private DS3232Connection _ds3232;

        public Clock Clock;
        public Alarm Alarm;
        public Alarm SecondaryAlarm;
        public Thermometer Thermometer;
        public Oscillator32KHz Oscillator32KHz;
        public SquareOscillator SQW;
        public TimeOscillator Oscillator;
        public Memory Memory;


        // Constructor
        public DS3232()
        {
            _ds3232 = new DS3232Connection();

            //Clock = new Clock(_ds3232);
            //Alarm
            //SecondaryAlarm
            Thermometer = new Thermometer(_ds3232);
            Oscillator32KHz = new Oscillator32KHz(_ds3232);
            SQW = new SquareOscillator(_ds3232);
            Oscillator = new TimeOscillator(_ds3232);
            Memory = new Memory(_ds3232);
        }

        public int BytesTotal
        {
            get { return Registers.SRAM_LAST + 1; }
        }

        public byte[] Dump()
        {
            return _ds3232.Read(0x00, BytesTotal);
        }

        // Calibrate temperature
    }
}