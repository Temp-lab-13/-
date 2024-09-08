using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork
{
    internal class MakeClass
    {
        public static TestClass MakeTestClass()
        {
            Type testClass = typeof(TestClass);
            var construct = Activator.CreateInstance(testClass) as TestClass; // Создание класс через его первый пустой конструктор.
            return construct;
        }

        public static TestClass MakeTestClass(int i)
        {
            Type testClass = typeof(TestClass);
            var construct = Activator.CreateInstance(testClass, new object[] { i }) as TestClass; // Создание класс через его второй конструктор, принимающий int.
            return construct;
        }

        public static TestClass MakeTestClass(int i, string s, decimal d, char[] c)
        {
            Type testClass = typeof(TestClass);
            var construct = Activator.CreateInstance(testClass, new object[] { i, s, d, c }) as TestClass; // Создание класс через его третий конструктор, принимающий int, string, decimal, char[].
            return construct;
        }
    }
}
