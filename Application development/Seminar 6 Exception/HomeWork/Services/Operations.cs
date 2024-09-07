using HomeWork.Abctract;
using HomeWork.Exeptions;
using HomeWork.Services.CalculatorLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork.Services
{
    public class Operations : IOperations
    {
        public double resalt = 0;
        private Stack<double> stack = new Stack<double>();
        private Stack<CalculatorActionLog> actions = new Stack<CalculatorActionLog>();

        public event EventHandler<EventArgs> GotResalt;

        public void CancelLast(int number)
        {
            if (stack.Count > 0)
            {
                resalt = stack.Pop();
                RaiseEvent();
            }
            else
            {
                throw new CalculatorActionCancel("Нет дествий для отмены", actions);
            }
        }

        public void Divide(int number)
        {
            Divide((double)number);
        }

        public void Divide(double number)
        {
            if (resalt != 0 && number != 0)
            {
                stack.Push(resalt);
                resalt /= number;
                RaiseEvent();
            }
            else
            {
                actions.Push(new CalculatorActionLog(CalculatorAction.Divide, number));
                throw new CalculatorDivibeByZeroExeption("Деление на 0 не допустимо", actions);
            }
        }

        public void Multiply(int number)
        {
            Multiply((double)number);
        }

        public void Multiply(double number)
        {
            ulong temp = (ulong)(resalt * number);
            if (temp < double.MaxValue)
            {
                stack.Push(resalt);
                resalt *= number;
                RaiseEvent();

            }
            else
            {
                actions.Push(new CalculatorActionLog(CalculatorAction.Multiply, number));
                throw new CalculatorOperationCauseOverflowExepction("Результат превысил допустимые значения", actions);
            }
        }

        public void Substruct(int number)
        {
            Substruct((double)number);
        }

        public void Substruct(double number)
        {
            long temp = (long)(resalt - number);
            if (temp > double.MinValue)
            {
                stack.Push(resalt);
                resalt -= number;
                RaiseEvent();
            }
            else
            {
                actions.Push(new CalculatorActionLog(CalculatorAction.Sum, number));
                throw new CalculatorOperationCauseOverflowExepction("Результат превысил допустимые значения", actions);
            }
        }

        public void Sum(int number)
        {
            Sum((double)number);
        }

        public void Sum(double number)
        {
            ulong temp = (ulong)(resalt + number);
            if (temp < double.MaxValue)
            {
                stack.Push(resalt);
                resalt += number;
                RaiseEvent();
            }
            else
            {
                actions.Push(new CalculatorActionLog(CalculatorAction.Sum, number));
                throw new CalculatorOperationCauseOverflowExepction("Результат превысил допустимые значения", actions);
            }
        }

        private void RaiseEvent()
        {
            GotResalt?.Invoke(this, EventArgs.Empty);
        }
    }
}

