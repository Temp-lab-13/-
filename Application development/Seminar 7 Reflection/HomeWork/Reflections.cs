using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork
{
    internal class Reflections
    {
        public static object StringToObject(string s)
        {

            // "NameSpaice.TestClass, test2, Version=1.0.0.0, Cultura=neutral, PublicKeyToken=null:TestClass|I:1|S:STR|D:2.0|"
            string[] arr = s.Split("|");
            string[] arr2 = arr[0].Split(",");
            object obj = Activator.CreateInstance(null, arr2[0])?.Unwrap();

            if (arr.Length > 1 && obj != null)
            {
                var type = obj.GetType();
                for (int i = 1; i < arr.Length; i++)
                {
                    {
                        string[] nameAndValue = arr[i].Split(":");
                        var p = type.GetProperty(nameAndValue[0]);
                        if (p == null)  // Если у нас выпал null, то вероятно поля в классе либо не существует, либо оно было изменено кастовым атрибутом, что требуется проверить отдельно.
                        {
                            var t = type.GetProperties();   
                            foreach (var item in t) // Берём все поля.
                            {
                                var attribut = item.GetCustomAttribute<CustomNameAttribute>(); // Берём их кастомные названия.
                                if (attribut != null && attribut.CustomName.Equals(nameAndValue[0]))    // проверяем на соотвествие полученному полю. Если оно есть, то записываем.
                                {
                                    /*if (item.PropertyType == typeof(int)) 
                                    {
                                        item.SetValue(obj, int.Parse(nameAndValue[1]));
                                    }
                                    else if (item.PropertyType == typeof(string))
                                    {
                                        item.SetValue(obj, nameAndValue[1]);
                                    }*/
                                    p = item; break;    // Не будем копировать кусок кода для проверки типов записываемых значений,
                                                        // и просто перепишем значение р названием нашего атрибута. И спустим его отрабатывать ниже.
                                }
                            }
                        }

                        if (p == null) continue; //  И если у нас снова выподит null даже после проверки полей кастомных артибутов (а он выпадит), то мы просто переходим к следующей итерации.

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
            // "NameSpaice.TestClass, test2, Version=1.0.0.0, Cultura=neutral, PublicKeyToken=null:TestClass|I:1|S:STR|D:2.0|"
            Type type = o.GetType();
            StringBuilder sB = new StringBuilder();

            sB.Append(type.AssemblyQualifiedName);
            sB.Append(':');
            sB.Append(type.Name);
            sB.Append('|');

            var prop = type.GetProperties();

            foreach (var item in prop)
            {
                var temp = item.GetValue(o);
                var attribut = item.GetCustomAttribute<CustomNameAttribute>(); // Получаем атрибут поля.
                if (attribut != null)   // Проверяем что он не null и записываем его имя. 
                {
                    sB.Append(attribut.CustomName);
                    sB.Append(":");  
                }
                else // Если null, то присваеваем имена полей по умолчанию.
                {
                    sB.Append(item.Name);
                    sB.Append(':');
                }

                if (item.PropertyType == typeof(char[]))    // А после, уже присваиваем значения.
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
