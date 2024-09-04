using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskOne.Abstract
{
    internal interface ICalc
    {
        event EventHandler <EventArgs> GotResalt;
        void Sum(int number);
        void Substruct(int number);
        void Multiply(int number);
        void Divide(int number);
        void CancelLast();
    }
}
