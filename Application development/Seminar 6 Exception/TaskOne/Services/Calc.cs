using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskOne.Abstract;
using TaskOne.Exeptions;

namespace TaskOne.Services
{
    internal class Calc : ICalc
    {
        public int resalt = 0;
        private Stack<int> stack = new Stack<int>();
        private Stack<CalcActionLog> actions = new Stack<CalcActionLog>();

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
            if (number == 0 || resalt == 0)
            {
                actions.Push(new CalcActionLog(CalcAction.Divide, number));
                throw new CalcDivibeByExeption("Нельзя делить на ноль.", actions);
            }
            stack.Push(resalt);
            resalt /= number;
            RaiseEvent();
        }

        public void Multiply(int number)
        {
            ulong temp = (ulong) (resalt * number);
            if (temp > int.MaxValue)
            {
                actions.Push(new CalcActionLog(CalcAction.Multiply, number));
                throw new CalcOperationCauseOverflowExepction("Результат слишком большой.", actions);
            }
            stack.Push(resalt);
            resalt *= number;
            RaiseEvent();
        }

        public void Substruct(int number)   // Тут не работает ошибка.
        {
            long temp = resalt - number;
            if (temp < int.MinValue || (temp == int.MinValue && number == int.MaxValue))
            {
                actions.Push(new CalcActionLog(CalcAction.Substruct, number));
                throw new CalcOperationCauseOverflowExepction("Результат слишком отрицательный.", actions);
            }
            stack.Push(resalt);
            resalt -= number;
            RaiseEvent();
        }

        public void Sum(int number) 
        {
            ulong temp = (ulong) (resalt + number);
            if (temp > int.MaxValue)
            {
                actions.Push(new CalcActionLog(CalcAction.Sum, number));
                throw new CalcOperationCauseOverflowExepction("Результат слишком большой.", actions);
            }
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
