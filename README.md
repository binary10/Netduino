Netduino
========

In the course of learning the .NET Micro Framework -- and embedded programming in general -- I have amassed a humble numble... ahem... number of examples that might be useful for others. So, I'm doing the right thing and sharing them on GitHub. All of these projects are written for the `Netduino Plus 1` board, however it should be straightforward to convert to any current board revisions.

#### Libraries
These are modules that can be used in your projects.

* [RealTimeClock]: A library for controlling the DS3232 clock chip over I2C.


#### Applications/Sketches
Pretty much all of these are beginner applications that I wrote while starting to learn embedded programming. They are classic examples of what types of things you can do with microcontrollers. I consider all of these sketches -- not full-scale applications -- so I haven't put a lot of work into making them very robust.

* [AlternateLightButton]: Toggle two LEDs with a button.
* [LightInterruptPort]: Demonstrates the use of the InterruptPort class for event signaling.
* [Servo]: Slow, automatic oscillation of a servo.
* [ServoPotControl]: Control a servo's angle by using a potentiometer.


#### Deploying to Your Netduino
It's all text, so open up your command line and Emacs editor and...

Just kidding! Get yourself a modern IDE like Xamarin Studio or Visual Studio and open the solution (`.sln`) file. Check the connections to your board and click the debug button. This will deploy the application to the device and allow you to user the debugger as usual. Once deployed, you can take the board with you anywhere and supply power for it to run the application.

You can find instructions for downloading an IDE, .NETMF, and the Netduino SDK at the [Netduino] download page. I created, compiled, and deployed these projects on Windows with Visual Studio, but you should be able to deploy through Xamarin Studio on Windows, Mac or Linux.

#### Enjoy 
Please feel free to fork, send me a merge request, or contact me with questions on the Netduino forum. 

[RealTimeClock]: https://github.com/binary10/Netduino/tree/master/RealTimeClock
[AlternateLightButton]: https://github.com/binary10/Netduino/tree/master/AlternateLightButton
[LightInterruptPort]: https://github.com/binary10/Netduino/tree/master/LightInterruptPort
[Servo]: https://github.com/binary10/Netduino/tree/master/Servo
[ServoPotControl]: https://github.com/binary10/Netduino/tree/master/ServoPotControl

[Netduino]: http://www.netduino.com/downloads/
