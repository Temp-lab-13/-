using HomeWork.Abctract;
using HomeWork.Exeptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HomeWork.Services
{
    public class Calculator
    {
        private IOperations Operation;
        private Input Input;
        private CheckExeption CheckExeption;

        public Calculator()
        {
            Operation = new Operations();
            Input = new Input();
            CheckExeption = new CheckExeption();
            Operation.GotResalt += OperationGotResalt;
        }
        public void CalculatorMenu()
        {
            Console.WriteLine(" Welcom!\n");
            bool flag = true;
            string? operand = string.Empty;

            do
            {
                operand = Input.InputOperand();

                if (System.String.IsNullOrEmpty(operand))
                {
                    flag = false;
                    Console.WriteLine(" Завершение работы приложения.");
                }
                else
                {
                    if (CheckOperations(operand))
                    {
                        if (!operand.Equals("cancel"))
                        {
                            var buffer = Input.InputNumber();

                            if(int.TryParse(buffer, out int numberInt))
                            {
                                Operations(operand, numberInt);
                            } 
                            else if(double.TryParse(buffer, out double numberDoubl))
                            {
                                Operations(operand, numberDoubl);
                            }
                            else
                            {
                                Console.WriteLine("Введено не допустимое число, или не число.");
                            }
                        }
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
            Operations(operand, (double)number);
        }

        private void Operations(string operand, double number)
        {
            switch (operand)
            {
                case "+":
                    CheckExeption.Execute(Operation.Sum, number);
                    break;
                case "-":
                    CheckExeption.Execute(Operation.Substruct, number);
                    break;
                case "*":
                    CheckExeption.Execute(Operation.Multiply, number);
                    break;
                case "/":
                    CheckExeption.Execute(Operation.Divide, number);
                    break;
                case "cancel":
                    CheckExeption.Execute(Operation.CancelLast, 0);
                    break;
            }
        }



        static void OperationGotResalt(object sendler, EventArgs eventArgs)
        {
            Console.WriteLine($"Resalt: {((Operations)sendler).resalt}");
        }
    }
}
