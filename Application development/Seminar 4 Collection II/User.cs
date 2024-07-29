using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seminar_4_Collection_II
{
    /*
     * Задача №3:
     * Поиск наиболее часто встречающихся элементов:
     * В списке объектов определенного типа найдите элементы, которые встречаются наибольшее количество раз.
    */
    internal class User //: IEquatable<User>
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public int Age { get; set; }

        /*public bool Equals(User? other)
        {
            if(other is null) return false;
            return FirstName == other.FirstName && Age == other.Age && LastName == other.LastName;
        }*/
    }
}
