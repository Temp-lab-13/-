namespace TaskTwo
{
    internal class Program
    {
        /*
         * Задача №3:
         * Создать метод, который принимает список чисел и функцию (делает Func)б
         * выполняющию какую-либо операцию над числами и возращающую результат. 
         */
        static void Main(string[] args)
        {
            List<int> list = new List<int>() { 2, 3, 6, 8, 1, 5 };
            int resalt = CalcSumm(list, (x, y) => x + y);
            Console.WriteLine(resalt);

            int resaltchet = CalcSummChet(list, x => x % 2 == 0, (x, y) => x + y, Console.WriteLine);
            Console.WriteLine(resaltchet);
        }

        static int CalcSumm(List<int> list, Func<int, int, int> operation)
        {
            int summ = 0;
            foreach (var item in list)
            {
                summ = operation(summ, item);
            }
            return summ;
        }

        static int CalcSummChet(List<int> list, Predicate<int> isevan, Func<int, int, int> operation, Action<int> action)
        {
            int summ = 0;
            foreach (var item in list)
            {
                if(isevan(item))
                {
                    summ = operation(summ, item);
                    action(summ);
                }
            }
            return summ;
        }
    }
}
