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

        public string InputNumber()
        {
            Console.Write(" Введите число: ");  
            return Console.ReadLine();
        }


    }
}
