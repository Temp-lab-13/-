using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Seminar_4_Collection_II
{
    internal class HomeWork_Seminar4
    {
        // Способ поиска путём перебора массивов. Доработать. Найти способ без 3-го цикла.
        public static void FindNumber(int[] array, int number)
        {
            int[] arr = { -2, 10, 2, 31, 4, 6, 16, 7, 8, 9 };

            for (int i = 0, j = 1; j < array.Length; j++) 
            {
                for (int x = j + 1; x < array.Length; x++)
                {
                    //Console.WriteLine($"{array[i]} { array[j]} { array[x]} = {array[i] + array[j] + array[x]}");
                    if (number == (array[i] + array[j] + array[x]))
                    {
                        int[] resalt = { array[i], array[j], array[x] };
                        //Print(resalt, number);
                    }
                }
            }
        }

        public static void FindNumberVer2(int[] array, int number)
        {
            Queue<int> que = new Queue<int>(array);
           
            while (que.Count > 0) {
                int temp = que.Dequeue();
                foreach (int x in que)
                {
                    int buff = number - (temp + x);
                    if (que.Contains(buff) && buff != x && buff != temp)
                    {
                        int[] res = {temp, x, (number - (temp + x))};
                        Print(res, number);

                    }

                }   
            }
          


        }

        static void Print(int[] resal, int number)
        {
            Console.WriteLine($"Числа дающие в сумме {number}: { string.Join(" ", resal)};");
        }
    }
}
