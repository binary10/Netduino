using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.NetduinoPlus;

namespace AlternateLightButton
{
    public class Program
    {
        public static void Main()
        {
            // Initialize Outputs
            OutputPort ledBuiltIn = new OutputPort(Pins.ONBOARD_LED, false);
            OutputPort ledGreen = new OutputPort(Pins.GPIO_PIN_D11, true);

            // Initialize Inputs
            InputPort button = new InputPort(Pins.ONBOARD_SW1, false, Port.ResistorMode.Disabled);

            // Declare variables
            bool buttonState = false;
            int buttonPress = 0;

            // Loop until button has been pressed 5 times
            while (buttonPress < 6)
            {
                // Read the button bool
                // TRUE when open, FALSE when closed
                buttonState = button.Read();

                // Turn on blue light when button open
                ledBuiltIn.Write(buttonState);

                // Turn green light on when button closed
                ledGreen.Write(!buttonState);
            }

        }

    }

}
