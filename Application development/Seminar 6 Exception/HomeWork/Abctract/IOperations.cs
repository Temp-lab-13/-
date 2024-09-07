using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork.Abctract
{
    internal interface IOperations
    {
        event EventHandler<EventArgs> GotResalt;
        void Sum(int number);
        void Substruct(int number);
        void Multiply(int number);
        void Divide(int number);
        void CancelLast(int number);

        void Sum(double number);
        void Substruct(double number);
        void Multiply(double number);
        void Divide(double number);
    }
}
