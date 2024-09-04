using HomeWork.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace HomeWork.Services
{
    public class Calculator
    {
        private IOperations Operation;
        private Input Input;

        public Calculator()
        {
            Operation = new Operations();
            Input = new Input();
            Operation.GotResalt += OperationGotResalt;
        }
        public void CalculatorMenu() 
        {
            Console.WriteLine(" Welcom!\n");
            bool flag = true;
            string? operand = string.Empty;
            int number = 0;

            do
            {
                operand = Input.InputOperand();

                if (String.IsNullOrEmpty(operand))
                {
                    flag = false;
                    Console.WriteLine(" Завершение работы приложения.");
                } 
                else
                {
                    if (CheckOperations(operand))
                    {
                        if(!operand.Equals("cancel")) number = Input.InputNumber();
                        Operations(operand, number); 
                    }
                }

            } while (flag);
        }

        private bool CheckOperations(string operand)
        {
            switch (operand)
            {
                case "+": return true;
                case "-": return true;
                case "*": return true;
                case "/": return true;
                case "cancel": return true;
                default:
                    Console.WriteLine(" Выбрано недопустимое действие, попробуйте снова.");
                    return false;
            }
        }

        private void Operations(string operand, int number)
        {
            switch (operand)
            {
                case "+":
                    Operation.Sum(number);
                    break;
                case "-":
                    Operation.Substruct(number);
                    break;
                case "*":
                    Operation.Multiply(number);
                    break;
                case "/":
                    Operation.Divide(number);
                    break;
                case "cancel":
                    Operation.CancelLast();
                    break;
            }
        }

        static void OperationGotResalt(object sendler, EventArgs eventArgs) 
        {
            Console.WriteLine($"Resalt: {((Operations)sendler).resalt}");
        }

    }
}
