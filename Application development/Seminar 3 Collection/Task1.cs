using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seminar_3_Collection
{
    // Используя стэк инвертируйте порядок слудования элементов в списке.
    internal class Task1
    {
        public static void Reverse (List<int> list)
        {
            Stack<int> stack = new Stack<int>();
            for (int i = 0; i < list.Count; i++)
            {
                stack.Push(list[i]);
            }

            list.Clear();

            for (; 0 < stack.Count;)
            {
                list.Add(stack.Pop());
            }
            //Console.WriteLine(string.Join(", ", list));
        }
    }
}
