using HomeWork.Services;

namespace HomeWork
{
    internal class Program
    {
        /*
         * Доработайте класс калькулятора способным работать как с целочисленными так и с дробными числами. 
         * (возможно стоит задействовать перегрузку методов)
         */

        static void Main(string[] args)
        {
            Calculator menu = new Calculator();
            menu.CalculatorMenu();
        }
    }
}
