namespace Seminar_4_Collection_II
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Домашняя работа:
            int[] arr = { -2, 10, 2, 31, 4, 6, 16, 7, 8, 9 };
            int number = 18;
            //HomeWork_Seminar4.FindNumber(arr, number);
            //Console.WriteLine("\n Иной вариант: ");
            HomeWork_Seminar4.FindNumberVer2(arr, number);


            // Задача №1:
            /*
            List<int> ints = new List<int> { 0, 1, 1, -1, 101, 102, 101, 11, 1111, 11 };
            Task1.PrintList(ints);
            */

            // Задача №2:
            /*
            List<int> ints = new List<int> { 1, 2, 2, 2, 3, 4, 4, 5, 5, 5, 5, 6, 7, 0 };
            Task2.SortInts(ints);
            */

            // Задавча №3:
            /*
            List<User> users = new List<User>();
            users.Add(new User {FirstName = "One", LastName = "L", Age = 12});
            users.Add(new User { FirstName = "Two", LastName = "L", Age = 13 });
            users.Add(new User { FirstName = "One", LastName = "D", Age = 14 });
            users.Add(new User { FirstName = "Three", LastName = "F", Age = 12 });
            users.Add(new User { FirstName = "Five", LastName = "X", Age = 199 });

            var ferstName = users.GroupBy(y => y.FirstName).OrderByDescending(g => g.Count()).First().Key; // Сортируем список юзеров по имени,
                                                                                                           // после сортируем отсортированные списки сортируем по колличеству их элементов
                                                                                                           // (то есть по колличеству людей с одинаковым именем.
                                                                                                           // и сохраняем в переменную, первый элемент (самый частовстречаемое имя).
            Console.WriteLine(ferstName); // One
            var ferstAge = users.GroupBy(y => y.Age).OrderBy(g => g.Count()).First().Key;                  // Всё тоже самое, но теперь отсортированный список по возростам, сортируем по наименьшему признаку
            Console.WriteLine(ferstAge);                                                                   // т.е. по наименее встречаемму возрасту среди юзеров (в нашем случаи, первого юзера в списке, не имеющего одногодки.
            var lastName = users.GroupBy(y => y.LastName).OrderBy(g => g.Count()).Last().Key;              // Аналогично предыдущему запросу, только Last возращает последний элемент получвшегся отсортированного списка. Т.е. Наиболее встречающиеся фамилия с конца.
            Console.WriteLine(lastName);
            */

            // Задача №4:
            /*
            List<string> list = new List<string>();
            list.Add("Ac");
            list.Add("Argentum");
            list.Add("Aя");
            list.Add("Лимбо");
            list.Add("Локи");
            list.Add("Синт");
            list.Add("4ат");
            Task4.FindElement(list);
            */
        }
    }
}
