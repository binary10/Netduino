using System;
using Microsoft.SPOT;

namespace RealTimeClock
{
    class Clock
    {
        private const int TWELVE_HOUR_BIT_POSITION = 6;
        private const int CENTURY = 0;
        private const int MILLENIUM = 2000;

        private DS3232Connection _ds3232;
        private Byte _seconds;
        private bool _seconds_sign_bit;

        private Byte _minutes;
        private bool _minutes_sign_bit;

        private Byte _hours;
        private bool _hours_sign_bit;

        private Byte _day;
        private Byte _date;
        private bool _date_sign_bit;

        private Byte _month;
        private Byte _year;

        private bool _century;
        private bool _twelve_hour;

        private DateTime _timestamp; // Keeps last timestamp polled
        
        public Clock(DS3232Connection d)
        {
            this._ds3232 = d;
        }

        // Get mask bit (zero for clock, but alarms contain information)
        // Precision adjustment for clock and alarms to allow encapsulation
        public DateTime Now 
        {
            get
            {
                GetDateRegisters();
                // Test for 12 hour
                _century = DecodeCentury();
                _twelve_hour = DecodeTwelveHour();

                
                _timestamp = new DateTime(  DecodeYear(), DecodeMonth()
                                            , DecodeDate(), DecodeHour()
                                            , DecodeMinute(), DecodeSecond());

                return _timestamp;
            }
        }

        public int Second
        {
            get
            {
                GetDateRegisters();
                return DecodeSecond();
            }
        }
        private int DecodeSecond()
        {
            return _seconds.BCDToDecimal();
        }

        public int Minute
        {
            get
            {
                GetDateRegisters();
                return DecodeMinute();
            }
        }
        private int DecodeMinute()
        {
            return _minutes.BCDToDecimal();
        }

        public int Hour
        {
            get
            {
                GetDateRegisters();
                if(DecodeTwelveHour())
                {

                }
                else
                {
                    return _hours.BCDToDecimal();
                }
            }
        }
        private int DecodeHour()
        {
            return 1;
        }

        public int Day
        {
            get
            {
                GetDateRegisters();
                return DecodeDay();
            }
        }
        private int DecodeDay()
        {
            return _day.BCDToDecimal();
        }

        public int Date
        {
            get
            {
                GetDateRegisters();
                return DecodeDate();
            }
        }
        private int DecodeDate()
        {
            return _date.BCDToDecimal();
        }

        public int Month
        {
            get
            {
                GetDateRegisters();
                return DecodeMonth();
            }
        }
        private int DecodeMonth()
        {
            return _month.BCDToDecimal(); 
        }

        public int Year
        {
            get
            {
                GetDateRegisters();
                return DecodeYear();
            }
        }
        private int DecodeYear()
        {
            return _year.BCDToDecimal() + CENTURY + MILLENIUM;
        }

        private void GetDateRegisters()
        {
            byte[] date_registers = _ds3232.Read(Registers.SECONDS, Registers.YEAR - Registers.SECONDS + 1);
            _seconds = new Byte(date_registers[Registers.SECONDS]);
            _minutes = new Byte(date_registers[Registers.MINUTES]);
            _hours   = new Byte(date_registers[Registers.HOURS]);
            _day     = new Byte(date_registers[Registers.DAY]);
            _date    = new Byte(date_registers[Registers.DATE]);
            _month   = new Byte(date_registers[Registers.MONTH]);
            _year    = new Byte(date_registers[Registers.YEAR]);
        }

        private bool DecodeTwelveHour()
        {
            return _hours.GetBit(TWELVE_HOUR_BIT_POSITION);
        }

        private bool DecodeCentury()
        {
            // The most significant bit
            return _month.Sign;
        }

        public static void Test()
        {
        }

        
    }
}