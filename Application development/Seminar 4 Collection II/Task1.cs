using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seminar_4_Collection_II
{
    /*
     * Задача №1:
     * Дан список целых чисел (числа не последовательны), в котором некоторые числа повторяются. 
     * Выведите список чисел на экран, исключив из него повторы. Постарайтесь сделать задачу за O(N) 
     * List<int> ints = new List<int> { 0, 1, 1, -1, 101, 102, 101, 11, 1111, 11 };
    */
    internal class Task1
    {
        public static void PrintList(List<int> list)
        {
            // Вариант 1.
            var set1 = new HashSet<int>(list); // В коллекцию записываются исключительно уникальные значения. И повторяющиеся цифры добавлены не будут.
            foreach (var item in set1)
            {
                Console.WriteLine(item);
            }

            // Вариант 2.
            List<int> list2 = new List<int>();
            foreach (var item in list)
            {
                if (!list2.Any(i => i == item)) // Ани с люмбдой возращает тру, если в листе есть хотя бы один элемент равный item
                {
                    list2.Add(item);
                }
            }

            foreach (var item in list2)
            {
                Console.WriteLine(item);
            }
        }
    }
}
