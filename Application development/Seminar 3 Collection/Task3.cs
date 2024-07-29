using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seminar_3_Collection
{
    /*
     * Есть лабиринт описанный в виде двумерного массива, где 1 - это стены, 0 - проход, 2 - искомая цель.
     * Лабиринт:
     * 1 1 1 1 1 1 1
     * 1 0 0 0 0 0 1
     * 1 0 1 1 1 0 1
     * 0 0 0 0 1 0 2
     * 1 1 0 0 1 1 1
     * 1 1 1 1 1 1 1 
     * 1 1 1 1 1 1 1
     * Напишите алгоритм, определяющий наличие выхода и выводящий на экран кооринаты точки выхода.
     */
    internal class Task3
    {
        public static void Labirint(int y, int x)
        {
            int[,] lab13 = new int[,]
            {
                { 1, 1, 1, 1, 1, 1, 1 },
                { 1, 0, 0, 0, 0, 0, 1 },
                { 1, 0, 1, 1, 1, 0, 1 },
                { 0, 0, 0, 0, 1, 0, 2 },
                { 1, 1, 0, 0, 1, 1, 1 },
                { 1, 1, 1, 1, 1, 1, 1 },
                { 1, 1, 1, 1, 1, 1, 1 }
            };
            FindPath(y, x, lab13);
        }

        static bool FindPath(int i, int j, int[,] lab13)
        {
            Stack<Tuple<int, int>> path = new Stack<Tuple<int, int>>();

            if (lab13[i, j] == 1)
            {
                Console.WriteLine($"Вы заспавнились в стене и умерли.");
                return false;
            }
            path.Push(new(i,j));

            while (path.Count > 0)
            {
                var current = path.Pop();
                if (lab13[current.Item1, current.Item2] == 2)
                {
                    Console.WriteLine($"Выход найден по координатам:\n x = {current.Item2 + 1} \n y = {current.Item1 + 1} ");
                    return true;
                }

                lab13[current.Item1, current.Item2] = 1;

                if (current.Item1 + 1 < lab13.GetLength(0) && lab13[current.Item1 + 1, current.Item2] != 1)
                {
                    path.Push(new(current.Item1 + 1, current.Item2));
                }

                if (current.Item2 + 1 < lab13.GetLength(1) && lab13[current.Item1, current.Item2 + 1] != 1)
                {
                    path.Push(new(current.Item1, current.Item2 + 1));
                }

                if (current.Item1 > 0 && lab13[current.Item1 - 1, current.Item2] != 1)
                {
                    path.Push(new(current.Item1 - 1, current.Item2));
                }

                if (current.Item2 > 0 && lab13[current.Item1, current.Item2 - 1] != 1)
                {
                    path.Push(new(current.Item1, current.Item2 - 1));
                }
            }

            Console.WriteLine("Выход не найден, вы обречены на медлунню и мучительную смерть в полном одиночестве.");
            return false;
        }


    }
}
