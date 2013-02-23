using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware.NetduinoPlus;
using System.Threading;

// Darek, what if I want to extend this behavior to anything that is 
// able to turn on or off? Would I create an interface ISwitchable to 
// implement this so I can pass my components from Program.cs?

namespace LightTrigger
{
    public class LightTrigger
    {
        // Fields are private by default

        // For an interface, create a transducer field
        OutputPort Led;

        // For an interface, create a "trippedBy" field
        InterruptPort Button;

        // In an interface, create a "resetBy" field
        InterruptPort Reset;

        // In an interface, create a "tripsToTrigger"
        int TriggerAfter;

        // Acceptable to initialize integers here
        int buttonCounter = 0;
        

        public LightTrigger(int clicks) // Use IButton (interface of button) as the type of the passed button in constructor
        {
            this.TriggerAfter = clicks;

            Led = new OutputPort(Pins.ONBOARD_LED, false);
            
            // In larger applications, create a class that encapsulates the hardware components. It should
            // configure the hardware once and behind the scenes so that the rest of the program doesn't have those details. 
            // (E.g. set what pins are being used). Also, it makes for sense for the rest of the program to 
            // refer to a "button" object instead of an InterruptPort object
            // Define a button interface, create a class for a particular button, instantiate button in main and pass it to 
            // the constructor of the managing class (Dependency injection)
            // Configuration is done up top, passed into the application
            Button = new InterruptPort(Pins.ONBOARD_SW1, false, Port.ResistorMode.Disabled, Port.InterruptMode.InterruptEdgeBoth);
            
            // PullUp resistor setting causes netduino to create a voltage at the pin
            // PullDown resistor setting is used when you supply a voltage to the pin
            Reset = new InterruptPort(Pins.GPIO_PIN_D13, false, Port.ResistorMode.PullUp, Port.InterruptMode.InterruptEdgeLow);
            
            //Type typeOfOutputPort = led.GetType();

            // Hold an instance of this delegate in a name so you can subscribre or unsubscribe easily and neatly
            // Set handler for trigger button
            var handlerDelegate = new NativeEventHandler(button_onInterrupt);
            Button.OnInterrupt += handlerDelegate;
            
            // Set handler for reset button
            var del = new NativeEventHandler(reset_onInterrupt);
            Reset.OnInterrupt += del;
        }

        // The event handler to reset the trigger
        void reset_onInterrupt(uint data1, uint data2, DateTime time)
        {
            // Reset the interrupt flag on the Reset button object
            Reset.ClearInterrupt();
            resetTrigger();
        }

        // A method to reset the trigger
        void resetTrigger()
        {
            // Turn the trigger indicator light off
            Led.Write(false);
            // Reset the counter
            buttonCounter = 0;
        }

        // How do I change the blink effect when the button is pressed?
        void button_onInterrupt(uint data1, uint data2, DateTime time)
        {
            Debug.Print(time.ToString());

            if (data2 == 0)
            {
                buttonCounter++;
                if (buttonCounter > TriggerAfter)
                    Led.Write(true);
                    // Write an HTTP request method to send trigger information
                    // Send DateTime, TriggerAfter, external sensor value, and type of sensor
                }
            }
        }

    }
}
