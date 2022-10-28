using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleRemoteControl.Commands;
using SimpleRemoteControl.Receivers;

namespace SimpleRemoteControl
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var remote = new Invokers.SimpleRemoteControl();
    
            var light = new Light();
            var garageDoor = new GarageDoor();

            var lightOn = new LightOnCommand(light);
            var garageOpen = new GarageDoorOpenCommand(garageDoor);

            remote.SetCommand(lightOn);
            remote.ButtonWasPressed();

            remote.SetCommand(garageOpen);
            remote.ButtonWasPressed();
        }
    }
}
