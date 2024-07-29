using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seminar_4_Collection_II
{
    /*
     * Задача №2:
     * Дан список целых чисел (числа не последовательны), в котором некоторые числа повторяются. 
     * Выведите список чисел на экран, расположив их в порядке возрастания частоты повторения
     * List<int> ints = new List<int> { 1, 2, 2, 2, 3, 4, 4, 5, 5, 5, 5, 6, 7, 0 };
     * Задача №2.1:
     * Модифицируйте код предыдущей задачи таким образом чтобы вывод элементов был в порядке убывания частоты их повторения.
    */
    internal class Task2
    {
        public static void SortInts(List<int> list)
        {
            Dictionary<int, int> dic = new Dictionary<int, int>(); // Записывем в словарь, где key будет наше значение, а value - счётчик повтора значений в листе.

            foreach (int i in list)
            {
                if (dic.ContainsKey(i))
                {
                    dic[i]++;
                }
                else
                {
                    dic.Add(i, 0);
                }
            }

            PriorityQueue<int, int> queue = new PriorityQueue<int, int>(); // Приоритетная колекция сортирует по приоритету от 1 до ~

            foreach (var i in dic)
            {
                //queue.Enqueue(i.Key, i.Value );
                queue.Enqueue(i.Key, i.Value * -1);//Задача №2.1:
            }

            while (queue.Count > 0) // Вывод.
            {
                Console.WriteLine(queue.Dequeue());
            }

        }
    }
}
