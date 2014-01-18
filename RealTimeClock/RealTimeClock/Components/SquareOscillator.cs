using System;
using Microsoft.SPOT;

namespace RealTimeClock
{
    class SquareOscillator
    {
        private byte MASK;
        
        private DS3232Connection _ds3232;
        private byte _frequency;

        public SquareOscillator(DS3232Connection d)
        {
            // MASK should be a constant -- how to do this?
            MASK = (byte)(Utility.mask_byte((int)ControlBits.SQW_Freq_Bit_01) | Utility.mask_byte((int)ControlBits.SQW_Freq_Bit_00));

            _ds3232 = d;
            _frequency = GetFrequency();
        }

        // Get the oscillation frequency in Hz
        // This would be useful only for setting Hertz measurement directly
        public int Frequency
        {
            get 
            {
                switch (_frequency)
                {
                    case (byte)SQWFrequencyCode.SQW_1_Hz:
                        return 1;
                    case (byte)SQWFrequencyCode.SQW_1024_Hz:
                        return 1024;
                    case (byte)SQWFrequencyCode.SQW_4096_Hz:
                        return 4096;
                    case (byte)SQWFrequencyCode.SQW_8192_Hz:
                        return 8192;
                    default:
                        return 0;
                }
            }

            set 
            {
                switch (value)
                {
                    case SQWFrequency.SQW_1_Hz:
                        _frequency = (byte)SQWFrequencyCode.SQW_1_Hz;
                        break;
                    case SQWFrequency.SQW_1024_Hz:
                        _frequency = (byte)SQWFrequencyCode.SQW_1024_Hz;
                        break;
                    case SQWFrequency.SQW_4096_Hz:
                        _frequency = (byte)SQWFrequencyCode.SQW_4096_Hz;
                        break;
                    case SQWFrequency.SQW_8192_Hz:
                        _frequency = (byte)SQWFrequencyCode.SQW_8192_Hz;
                        break;
                    default:
                        // No change
                        break;
                }            
            }
        }

        // Frequency settings by code
        public void SetFrequency(int freq_code)
        {
            // Get register into a Byte and update byte with settings
            Byte b = new Byte(_ds3232.Read(Registers.CONTROL));

            byte setting = Utility.setting(freq_code, (int)ControlBits.SQW_Freq_Bit_00);
            b.SetBits(MASK, setting);

            // Update control register with new data
            _ds3232.Write(Registers.CONTROL, b.ToByte());

            // Set frequency when successful
            this._frequency = (byte) freq_code;
        }
        public byte GetFrequency()
        {
            Byte b = new Byte(_ds3232.Read(Registers.CONTROL));
            b.GetBits(MASK);
            return (byte)(b.ToByte() >> (int)ControlBits.SQW_Freq_Bit_00);
        }
        
        // Start the oscillator
        public void Start()
        {
            _ds3232.WriteBit(Registers.CONTROL, (int)ControlBits.SQW, false);
        }

        // Stop the oscillator
        // The next time the oscillator is started, it will run at the last frequency.
        public void Stop()
        {
            _ds3232.WriteBit(Registers.CONTROL, (int)ControlBits.SQW, true);
        }

        // Toggle the oscillator
        public void Toggle()
        {
            _ds3232.ToggleBit(Registers.CONTROL, (int)ControlBits.SQW);
        }

        // Allow oscillator on battery power
        public void EnableOnBattery()
        {
            _ds3232.WriteBit(Registers.CONTROL, (int)ControlBits.SQW_On_Battery_Power, true);
        }

        // Disable oscillator on battery power
        public void DisableOnBattery()
        {
            _ds3232.WriteBit(Registers.CONTROL, (int)ControlBits.SQW_On_Battery_Power, false);
        }

        // Toggle oscillator on battery power
        public void ToggleEnableOnBattery()
        {
            _ds3232.ToggleBit(Registers.CONTROL, (int)ControlBits.SQW_On_Battery_Power);
        }

    }

    static class SQWFrequency
    {
        public const int SQW_1_Hz = 1;
        public const int SQW_1024_Hz = 1024;
        public const int SQW_4096_Hz = 4096;
        public const int SQW_8192_Hz = 8192;
    }

    enum SQWFrequencyCode
    {
          SQW_1_Hz
        , SQW_1024_Hz
        , SQW_4096_Hz
        , SQW_8192_Hz
    }
}
