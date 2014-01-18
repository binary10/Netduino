using System;
using Microsoft.SPOT;

namespace RealTimeClock
{
    class Registers
    {
        // Clock registers
        public const byte SECONDS = 0x00;
        public const byte MINUTES = 0x01;
        public const byte HOURS = 0x02;
        public const byte DAY = 0x03;
        public const byte DATE = 0x04;
        public const byte MONTH = 0x05;
        public const byte YEAR = 0x06;


        // Alarm registers
        // Most sig. bit is a mask bit for each register that changes behavior of alarm
        public const byte ALARM_SECONDS = 0x07;
        public const byte ALARM_MINUTES = 0x08;
        public const byte ALARM_HOURS = 0x09;
        public const byte ALARM_DAY = 0x0A;
        public const byte SECONDARY_ALARM_MINUTES = 0x0B;
        public const byte SECONDARY_ALARM_HOURS = 0x0C;
        public const byte SECONDARY_ALARM_DAY = 0x0D;


        // Control registers
        public const byte CONTROL = 0x0E;
        public const byte STATUS = 0x0F;

        // Aging offset
        public const byte AGING = 0x10;

        // Temperature registers
        public const byte TEMPERATURE_MSB = 0x11;   // Most sig. byte
        public const byte TEMPERATURE_LSB = 0x12;   // Least sig. byte


        // Storage registers
        public const byte SRAM_FIRST = 0x14;
        public const byte SRAM_LAST = 0xFF;

    }

    enum ControlBits
    {
        Alarm_Interrupt,
        Secondary_Alarm_Interrupt,
        SQW,
        SQW_Freq_Bit_00,
        SQW_Freq_Bit_01,
        Temp_Correction,        // Check if 'Busy' before triggering
        SQW_On_Battery_Power,
        Oscillator
    }

    enum StatusBits
    {
        Alarm,
        Secondary_Alarm,
        Busy,               // READ ONLY. TCXO correction in progress
        Oscillator,
        Correction_Sample_Rate_Bit_00,
        Correction_Sample_Rate_Bit_01,
        Oscillator_On_Battery_Power,
        Oscillator_Stopped  // Write to Zero.
    }
}
