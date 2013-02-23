using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.NetduinoPlus;


// This client code triggers the onboard LED of Netduino to turn on
// indefinitely after its onboard switch is pushed a number of times
// A reset capability allows the trigger to be started over again.

// I should be creating classes that abstract the idea of Triggering
// an electrical component to turn on. By having a Trigger class,
// I could pass a light, or any other electrical component port, to
// the constructor and make it a context specific trigger.

namespace LightTrigger
{
    public class Program
    {
        public static void Main()
        {
            // Create the object that manages the application
            // Light should remain on after 'n' button clicks
            // Should ultimately have a Trigger class that I can
            // Pass a light and button to.
            LightTrigger li = new LightTrigger(5);

            // Continue to run the application indefinitely
            Thread.Sleep(Timeout.Infinite);
        }
    }
}
