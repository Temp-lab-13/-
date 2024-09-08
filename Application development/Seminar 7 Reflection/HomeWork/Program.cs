namespace HomeWork
{
    internal class Program
    {
        /*
         * Домашнее задание:
         * Разработайте атрибут позволяющий методу ObjectToString сохранять поля классов с использованием произвольного имени.
         * Метод StringToObject должен также уметь работать с этим атрибутом для записи значение в свойство по имени его атрибута.
         * [CustomName(“CustomFieldName”)]
         * public int I = 0.
         * Если использовать формат строки с данными использованной нами для предыдущего примера то пара ключ значение для свойства I выглядела бы CustomFieldName:0
         * 
         * Подсказка:
         * Если GetProperty(propertyName) вернул null то очевидно свойства с таким именем нет и возможно имя является алиасом заданным с помощью CustomName.
         * Возможно, если перебрать все поля с таким атрибутом то для одного из них propertyName = совпадает с таковым заданным атрибутом.
         */
        static void Main(string[] args)
        {
            char[] sommer = { 'a', 'x', '1', '#' };
            var clas1 = MakeClass.MakeTestClass();
            var clas2 = MakeClass.MakeTestClass(5);
            var clas3 = MakeClass.MakeTestClass(4, "Date", 1, sommer);

            Console.WriteLine(Reflections.ObjectToString(clas3));
            string test = Reflections.ObjectToString(clas3);
            var obj = Reflections.StringToObject(test);
            Console.WriteLine(Reflections.ObjectToString(obj));
        }

    }
}
