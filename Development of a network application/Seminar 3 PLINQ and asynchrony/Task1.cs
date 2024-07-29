using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seminar_3_PLINQ_and_asynchrony
{
    /*
     * Напише приложение для одновременного выполнения двух задач в потоках созданных с помощью Task. 
     * Нужно подсчитать сумму элементов каждого из массивов,
     * а потом сложить эти суммы полученные после выполнения каждого из потоков и вывести результат на экран.
    */
    public class Task1
    {
        public static int _sum1 = 0;
        public static int _sum2 = 0;

        public static int[] _arr1 = { 1, 5, 8, 8, 7, 1, 7, 6, 4 };
        public static int[] _arr2 = { 1, 9, 2, 3, 1, 4, 6, 4, 4 };

        public static async Task<int> Taska1()
        {
             return await Task.Run(() => _arr1.Sum());
        }

        public static async Task<int> Taska2()
        {
             return await Task.Run(() => _arr2.Sum());
        }
    }   
}
