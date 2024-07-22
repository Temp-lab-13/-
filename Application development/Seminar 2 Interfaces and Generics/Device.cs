using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seminar_2_Interfaces_and_Generics
{
    public class Device : IControllable
    {
        public bool IsOn { get; set; } = false;
        public void On()
        {
            IsOn = true;
        }
        public void Off()
        {
            IsOn = false;
        }
    }

    public class Devices
    {
        public List<IControllable> DevicesList { get; init; }
        
        public Devices()
        {
            DevicesList  = new List<IControllable>();
            DevicesList.Add(new Device());
            DevicesList.Add(new Device());
            DevicesList.Add(new Device());
            DevicesList.Add(new Device());
            DevicesList.Add(new Device());
            DevicesList.Add(new Device());
            DevicesList.Add(new Device());
            DevicesList.Add(new Device());
            DevicesList.Add(new Device());
        }

        public void TurnOnOff(Bits bits) 
        {
            for (byte i = 0; i < 8; i++)
            {
                if (DevicesList[i].IsOn && !bits[i])
                {
                    DevicesList[i].Off();
                }
                else if (!DevicesList[i].IsOn && bits[i])
                {
                    DevicesList[i].On();
                }
                Console.WriteLine($"Device {i}");
            }
        }

        public override string ToString()
        {
            return string.Join("", DevicesList.Select(s=>s.IsOn?"1":"0"));
        }
    }
}
