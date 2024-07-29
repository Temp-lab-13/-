using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seminar_3_Collection
{
    /*
     * Реализуйте класс с поддержкой IEnumerable<int> - CustomEnurable
     * который в случаи использования его в следующем коде:
     * foreach (var x in new CustomEnurable())
     * {
     *      Console.WriteLine(x);
     * }
     * 
     * Выведет на экран значения от 0 до 10.
     * Возможно придётся реализовать ещё и IEnumerator
     * */
    internal class CustomEnurable : IEnumerable<int> // Task2
    {
        public IEnumerator<int> GetEnumerator() => new CustomEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
