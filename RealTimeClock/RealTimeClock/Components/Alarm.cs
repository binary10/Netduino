using System;
using Microsoft.SPOT;

namespace RealTimeClock
{
    class Alarm
    {
        DS3232Connection _ds3232;

        // Clock instance to set alarm time?

        bool _day_date;

        // Alarm conditions
        bool _each_second;
        bool _on_the_second;
        bool _on_the_minute;
        bool _on_the_hour;
        bool _day_flag;

        // Alarm action
        bool _interrupt;
        bool _triggered;
        

        public void Every_Second()
        {
        }

        public void Every_Minute()
        {
        }

        public void Every_Hour()
        {
        }

        public void Every_Day()
        {
        }

        private bool DecodeDayDate()
        {
            return true;
        }

        // Setting mask bits
        // Get all registers
        // For each register, get bit and shift 

        // Get current alarm setting
    }

    enum AlarmBits
    {
          Every_Month
        , Every_Second
        , Every_Minute
        , Every_Hour
        , Every_Day
        , Every_Week
    }
}
