using System;
using Microsoft.SPOT;

namespace RealTimeClock
{
    class Oscillator32KHz
    {
        private DS3232Connection _ds3232;

        public Oscillator32KHz(DS3232Connection d)
        {
            this._ds3232 = d;
        }
        
        // Start the oscillator
        public void Start()
        {
            // Should rather be:
            // _ds3232.enable_bit(...);
            _ds3232.WriteBit(Registers.CONTROL, (int)StatusBits.Oscillator, true);
        }
        // Stop the oscillator
        public void Stop()
        {
            _ds3232.WriteBit(Registers.CONTROL, (int)StatusBits.Oscillator, false);
        }
        // Toggle the oscillator
        public void Toggle()
        {
            _ds3232.ToggleBit(Registers.CONTROL, (int)StatusBits.Oscillator);
        }
    }
}
