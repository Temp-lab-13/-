using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork.Abstract
{
    public interface IOperations
    {
        event EventHandler<EventArgs> GotResalt;
        void Sum(int number);
        void Substruct(int number);
        void Multiply(int number);
        void Divide(int number);
        void CancelLast();
    }
}
