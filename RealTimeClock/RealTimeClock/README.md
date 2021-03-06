# Hello Timekeepers

This repository makes available to you a Netduino driver for 
the DS3232 RTC chip. I found mine at Freetronics in the form
of a so-called "breakout board":
http://www.freetronics.com/products/real-time-clock-rtc-module#.Utr-__TTmb8

The DS3232 is operated using an I2C interface, so this driver 
wraps a lot of the details of using I2C into pretty classes
with human readable functions. For a primer on I2C, I 
recommend following the Arduino tutorial on this module at
Freetronics that can be found under the "Resources" header of
the link above.

While writing this solution, I encountered a couple of tricky things
about the DS3232 that can be summarized as follows:

There are three oscillator controls
* One relates to the base time-keeping oscillator
* The 32khz pin allows access to this base oscillator
* A third pin outputs a variable square wave
* All oscillators can be disabled on battery power.
	
The timekeeping oscillator is temperature corrected. The chip 
periodically does a calibration and updates its circuitry to 
improve timekeeping accuracy. 
* The thermometer readings will update only when the temperature correction calibration takes place.
* The frequency of calibration can be adjusted. It is by default set to the highest frequency.


This code is released under the GPL license that can be found
at the root of this repository. As part of this binding license agreement, 
if you use this code in your project, I require that you
do thirty push-ups and say Bloody Mary into the mirror three times.

Happy timekeeping and please fork/send pull request if you have improvements to make. Also share your projects with me if it uses this solution!
