using TaskOne.Abstract;
using TaskOne.Exeptions;
using TaskOne.Services;

namespace TaskOne
{
    internal class Program
    {
        /*
         * Задача №1:
         * Доработайте реализацию калькулятора, разработав собственные типы ошибок:
         * - CalcDivibeByExeption;
         * - CalcOperationCauseOverflowExepction.
         * 
         * Задача №2:
         * Реализуйте класс - список, описывающий последовательность действий приведших к исключению.
         * Т.е. хранящий не только информацию о прошлых резальтатах, но и операторах.
         */
        static void Main(string[] args)
        {
            ICalc calc = new Calc();
            calc.GotResalt += CalcGotResaltUP;
            calc.GotResalt += CalcGotResalt;
            //Execute(calc.Sum, 5);
            Execute(calc.Sum, 5);
            Execute(calc.Sum, int.MaxValue);
            
            //Execute(calc.Substruct, 2);
            //Execute(calc.Multiply, 3);
            //Execute(calc.Divide, 0);

        }

        static void CalcGotResalt(object sendler, EventArgs eventArgs) 
        {
            Console.WriteLine($"Resalt: {((Calc)sendler).resalt}"); 
        }

        static void CalcGotResaltUP(object sendler, EventArgs eventArgs)
        {
            Console.Write($"Resalt: ");
        }

        static void Execute(Action<int> action, int value)
        {
            try
            {
                action.Invoke(value);
            }
            catch (CalcDivibeByExeption ex)
            {
                Console.WriteLine(ex);
            }
            catch (CalcOperationCauseOverflowExepction ex)
            {
                Console.WriteLine(ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
