using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seminar_4_Collection_II
{
    /*
     * У вас есть коллекция строк. Необходимо выбрать строки, содержащие определенные слова, и отформатировать результаты.
    */
    internal class Task4
    {

        public static void FindElement(List<string> elements)
        {
            string find = "Лимбо";
            var resalt = elements.Where(elements => elements.ToUpper().Contains(find.ToUpper())).Select(x => x.ToUpper());
            Console.WriteLine(string.Join(" ", resalt));
        }
    }
}
