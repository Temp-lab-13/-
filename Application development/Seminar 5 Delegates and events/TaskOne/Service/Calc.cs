using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskOne.Abstract;

namespace TaskOne.Service
{
    internal class Calc : ICalc
    {
        public int resalt = 0;
        private Stack<int> stack = new Stack<int>();

        public event EventHandler<EventArgs> GotResalt;

        public void CancelLast()
        {
            if (stack.Count > 0)
            {
                resalt = stack.Pop();
                RaiseEvent();
            }
            else
            {
                Console.WriteLine("Нечего отменять.");
            }
        }

        public void Divide(int number)
        {
            stack.Push(resalt);
            resalt /= number;
            RaiseEvent();
        }

        public void Multiply(int number)
        {
            stack.Push(resalt);
            resalt *= number;
            RaiseEvent();
        }

        public void Substruct(int number)
        {
            stack.Push(resalt);
            resalt -= number;
            RaiseEvent();
        }

        public void Sum(int number)
        {
            stack.Push(resalt);
            resalt += number;
            RaiseEvent();
        }

        private void RaiseEvent()   // Метод сигнализирует о произошебшем событии, который вызывает пописанные на Calc методы. В нашем случаи вызывающем метод который распечатывает результат в консоль.
        {
            GotResalt?.Invoke(this, EventArgs.Empty);   
        }
    }
}
