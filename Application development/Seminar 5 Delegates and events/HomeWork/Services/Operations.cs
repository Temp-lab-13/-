using HomeWork.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork.Services
{
    public class Operations : IOperations
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
                Console.WriteLine(" Нечего отменять.");
            }
        }

        public void Divide(int number)
        {
            if (resalt != 0)
            {
                stack.Push(resalt);
                resalt /= number;
                RaiseEvent();
            }
            else
            {
                Console.WriteLine(" Нельзя делить на 0");
            }
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

        private void RaiseEvent() 
        {
            GotResalt?.Invoke(this, EventArgs.Empty);
        }
    }
}
