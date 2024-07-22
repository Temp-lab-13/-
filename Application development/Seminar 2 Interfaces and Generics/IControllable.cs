using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seminar_2_Interfaces_and_Generics
{
    public interface IControllable
    {
        bool IsOn { get; set; }

        void On();
        void Off();
    }
}
