using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HomeWork.Services
{
    public class Input
    {
        public string InputOperand() 
        {
            Console.WriteLine(" Выберите одно из доступных действий: + , - , * , / , 'cancel'");
            Console.WriteLine(" Для выхода, ничего не вводите и нажмите 'Enter'");
            Console.Write(" Поле ввода:  ");
            return Console.ReadLine();
        }

        public int InputNumber()
        {
            int number;
            Console.Write(" Введите число: ");
            while (int.TryParse(Console.ReadLine(), out number) != true)
            {
                Console.Write(" Введено не число. Попробуйте ещё раз: ");
            } 
            return number;
        }

    }
}
