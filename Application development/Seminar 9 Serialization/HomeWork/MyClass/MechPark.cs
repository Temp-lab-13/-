using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork.MyClass
{
    public class MechPark
    {
        public string Location { get; set; }
        public int UnitsOfEquipment { get; set; }
        public List<Mech> Meches { get; set; } = new List<Mech>();
    }
}
