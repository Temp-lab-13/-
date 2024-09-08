using System.Text;

namespace TaskOne
{
    internal class Program
    {
        /*
         * Задача №1:
         * Дан класс (ниже)б создать методы создающий этот класс вызывая один из его конструкторов (по одному конструктору на метод)
         * class TestClass
         * {
         *      public int I { get; set; }
         *      public string? S { get; set; }
         *      public decimal D { get; set; }
         *      public char[]? C { get; set; }
         *      
         *      public TestClass()
         *      {}
         *      
         *      private TestClass(int i)
         *      {
         *          this.I = i;
         *      }
         *      
         *      public TestClass(int i, string s, decimal d, char[] c):this(i)
         *      {
         *          this.S = s;
         *          this.D = d;
         *          this.C = c;
         *      }
         * }
         * 
         * Задача №2:
         * Напишите два метода использующих рефлексию:
         * 1 - сохраняет информацию о классе в строку.
         * 2 - позволяет восстановить информацию о классе из этой строки.
         * В качестве примера используйте класс TestClass.
         * Шаблоны методов для реализации:
         * static object StringToObject(string s) {}
         * static string ObjectToString(object o) {}
         * 
         * Подсказка: 
         * Строка должна содержать название класса, полей и значений.
         * Ограничтесь диапозоном значений представленном в классе.
         * Если класс находится в сборке, то можно не указывать имя сборки в параметрах активатора.
         * Activator.CreateInstance(null, "TestClass") - тоже сработает.
         * 
         * Пример того, как мог бы выглядеть сохранённый в строку объект: 
         * "TestClass, test2, Version=1.0.0.0, Cultura=neutral, PublicKeyToken=null:TestClass|I:1|S:STR|D:2.0|"
         * Ключ-значения разделяются двоеточие, а сами пары - вертикальной чертой.
         */
        static void Main(string[] args)
        {
            char[] sommer = { 'a', 'x', '1', '#'};
            var clas1 = MakeTestClass();
            var clas2 = MakeTestClass(5);
            var clas3 = MakeTestClass(4, "строка", 1, sommer);

            Console.WriteLine(ObjectToString(clas3));
            string test = ObjectToString(clas3);
            var obj = StringToObject(test);
            Console.WriteLine(ObjectToString(obj));
        }

        public static TestClass MakeTestClass() 
        {
            Type testClass = typeof(TestClass);
            var construct = Activator.CreateInstance(testClass) as TestClass; // Создание класс через его первый пустой конструктор.
            return construct;
        }

        public static TestClass MakeTestClass(int i)
        {
            Type testClass = typeof(TestClass);
            var construct = Activator.CreateInstance(testClass, new object[] {i}) as TestClass; // Создание класс через его второй конструктор, принимающий int.
            return construct;
        }

        public static TestClass MakeTestClass(int i, string s, decimal d, char[] c)
        {
            Type testClass = typeof(TestClass);
            var construct = Activator.CreateInstance(testClass, new object[] { i, s, d, c }) as TestClass; // Создание класс через его третий конструктор, принимающий int, string, decimal, char[].
            return construct;
        }


        public static object StringToObject(string s) 
        {
            // "NameSpaice.TestClass, test2, Version=1.0.0.0, Cultura=neutral, PublicKeyToken=null:TestClass|I:1|S:STR|D:2.0|"
            string[] arr = s.Split("|");
            string[] arr2 = arr[0].Split(","); // Здесь мы получаем ссылку на файл в нашем проекте: "TaskOne.TestClass".
            object obj = Activator.CreateInstance(null, arr2[0])?.Unwrap();
 
            if (arr.Length > 1 && obj != null)
            {
                var type = obj.GetType();
                for (int i = 1; i < arr.Length; i++)
                {
                    {
                        string[] nameAndValue = arr[i].Split(":");
                        var p = type.GetProperty(nameAndValue[0]);
                        if (p == null) { continue; }
                        if (p.PropertyType == typeof(int)) 
                        {
                            p.SetValue(obj, int.Parse(nameAndValue[1]));
                        }
                        else if (p.PropertyType == typeof(string)) 
                        {
                            p.SetValue(obj, nameAndValue[1]);
                        }
                        else if (p.PropertyType == typeof(decimal)) 
                        {
                            p.SetValue(obj, decimal.Parse(nameAndValue[1]));
                        }
                        else if (p.PropertyType == typeof(char[])) 
                        {
                            p.SetValue(obj, nameAndValue[1].ToCharArray());
                        }
                    }
                }
            }
            return obj;
        }
        public static string ObjectToString(object o) 
        {
            Type type = o.GetType();
            StringBuilder sB = new StringBuilder();

            var prop = type.GetProperties();
           
            sB.Append(type.AssemblyQualifiedName);
            sB.Append(':');
            sB.Append(type.Name);
            sB.Append('|');
            foreach (var item in prop)
            {
                var temp = item.GetValue(o);
                sB.Append(item.Name);
                sB.Append(':');
                if (item.PropertyType == typeof(char[]))
                {
                    sB.Append(new string(temp as char[]));
                }
                else { sB.Append(temp); }
                sB.Append('|');
            }
            return sB.ToString();
        }

    }
}
