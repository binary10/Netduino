using System;
using Microsoft.SPOT;

namespace RealTimeClock
{
    class TimeOscillator
    {
        // Abbreviations
        // TCXO     Temperature-Compensated Crystal Oscillator

        private DS3232Connection _ds3232;

        public TimeOscillator(DS3232Connection d)
        {
            this._ds3232 = d;
        }
        
        // Properties
        // Check busy signal
        public bool Busy
        {
            // Time oscillator busy doing temperature correction on capacitor array
            get 
            { 
                Byte b = new Byte(_ds3232.Read(Registers.STATUS)); 
                return b.GetBit((int)StatusBits.Busy) ; 
            }
        }
        // Check if oscillator stopped
        public bool Stopped
        {
            get
            {
                Byte b = new Byte(_ds3232.Read(Registers.STATUS));
                return b.GetBit((int)StatusBits.Oscillator_Stopped);
            }
        }


        // Calibrate to temperature manually
        // Set sampling rate for calibration

        // Allow oscillator on battery power
        // Allow oscillator on battery power
        public void EnableOnBattery()
        {
            _ds3232.WriteBit(Registers.CONTROL, (int)StatusBits.Oscillator_On_Battery_Power, true);
        }
        // Disable oscillator on battery power
        public void DisableOnBattery()
        {
            _ds3232.WriteBit(Registers.CONTROL, (int)StatusBits.Oscillator_On_Battery_Power, false);
        }
        // Toggle oscillator on battery power
        public void ToggleEnableOnBattery()
        {
            _ds3232.ToggleBit(Registers.CONTROL, (int)StatusBits.Oscillator_On_Battery_Power);
        }

        public static void Test()
        {
            DS3232Connection d = new DS3232Connection();
            TimeOscillator t = new TimeOscillator(d);

            bool b = t.Stopped;
        }
    }

    enum TCXOSampleRate
    {
          Period_64_Seconds
        , Period_128_Seconds
        , Period_256_Seconds
        , Period_512_Seconds
    }
}
