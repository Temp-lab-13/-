using TaskOne.Abstract;
using TaskOne.Service;

namespace TaskOne
{
    internal class Program
    {
        /*
         * Задача №1:
         * Спроектируйте интерфейс калькулятора, поддерживающего простые арифметические действия, 
         * хранящего результат и так же способного выводить информацию о результате при помощи события.
         * Задача №2:
         * Модифицируйте код калькулятора, реализовав инрт6ерфейс ниже:
         * interface ICalc
         * {
         *  event EventHandler <EventArgs> GotResalt;
         *  int Sum(int number);
         *  int Substruct(int number);
         *  int Multiply(int number);
         *  int Divide(int number);
         *  void CancelLast();
         *  }
         *  Арифметические методы должны выполняться как обычно,
         *  а CancelLast должен отменять последнее действие.
         *  При этом, метод долже отменять последовательно все предыдущие действия, вплодь до первого, включительно.
         */
        static void Main(string[] args)
        {
            ICalc calc = new Calc();
            calc.GotResalt += CalcGotResaltUP;  //  Подписывать можно много методов.
            calc.GotResalt += CalcGotResalt;    //  Подписываем метод.
            calc.Sum(5);
            calc.Substruct(2);
            calc.Multiply(3);
            calc.Divide(2);
        }

        static void CalcGotResalt(object sendler, EventArgs eventArgs) //   Метод который пописывается на события GotResalt из метода Calc.
        {
            Console.WriteLine($"Resalt: {((Calc)sendler).resalt}"); // При активации события, он будет распечатывать результат (содержание переменной resalt метода Calc), в консоль.
        }

        static void CalcGotResaltUP(object sendler, EventArgs eventArgs) 
        {
            Console.Write($"Resalt: "); 
        }
    }
}
