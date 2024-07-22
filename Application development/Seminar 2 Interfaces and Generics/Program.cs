namespace Seminar_2_Interfaces_and_Generics
{
    internal class Program
    {
        /*
          * Задача №1: 
          * Спроектируйте интерфейс для класса 
          * способного устанавливать и получать значения отдельных бит в заданном числе.
          * Задача №2:
          * Реализовать интерфейс из прошлой задачи применив его к классу bits из примера предыдущей лекции.
          * Задача №3:
          * Переопределить оператор this и byte.
          * Задача №4:
          * Предположим, у вас есть некоторый набор устройств, 
          * каждое из которых может быть включено или выключено, 
          * и вы хотите иметь возможность выполнять операции над этими устройствами через битовые операторы. 
          * Задача №5:
          * реализовать класс bits альтернативным способ с поддержкой значений от byte до long. 
          * Для реализации задачи преподаватель объясняет как работает оператор sizeof.
        */

        /*
         * Доманяя работа: 
         * Реализуйте операторы неявного приведения из long,int,byte в Bits.
        */
        static void Main(string[] args)
        {
            Console.WriteLine("\n Приведение byte/bits:");

            byte bitBite = new Bits((byte)110);
            var bits = (Bits)bitBite;

            Console.WriteLine(bitBite);
            Console.WriteLine(bits.ToString());

            Console.WriteLine("\n Эксперементы с byte/bits: ");
            byte bitBite2 = bits;
            byte bitBite3 = (byte)bits;
            var bitBite4 = new Bits(bitBite2);
            byte bitBite5 = new Bits(10);
            var bits0 = bitBite5;
            
            Console.WriteLine(bitBite2);
            Console.WriteLine(bitBite3);
            Console.WriteLine(bitBite4);
            Console.WriteLine(bitBite5);
            Console.WriteLine(bits0.ToString());

            
            Console.WriteLine(" Приведение int/bits: ");

            int bitInt = new Bits((int)10);
            var bits2 = (Bits)bitInt;

            Console.WriteLine(bitInt);
            Console.WriteLine(bits2.ToString());

            Console.WriteLine("\n Эксперементы с int/bits: ");
            int bitInt2 = bits2;
            int bitInt3 = (int)bits2;
            int bitInt4 = new Bits(12);
            
            Console.WriteLine(bitInt2);
            Console.WriteLine(bitInt3);
            Console.WriteLine(bitInt4);

            
            Console.WriteLine("\n Приведение long/bits:");

            long bitLong = new Bits((long)130);
            var bits3 = (Bits)bitLong;

            Console.WriteLine(bitLong);
            Console.WriteLine(bits3.ToString());

            Console.WriteLine("\n Эксперементы с long/bits: ");
            long bitLong2 = bits3;
            long bitLong3 = (byte)bits3;
            long bitLong4 = new Bits(13);
            
            Console.WriteLine(bitLong2);
            Console.WriteLine(bitLong3);
            Console.WriteLine(bitLong4);

            // Семинарская практика.

            /*
            Bits bits = new(4);
            Console.WriteLine(bits.GetBitByIndex(2));
            bits.SetBitByIndex(0, true);
            Console.WriteLine(bits.GetBitByIndex(0));
            */

            /*
            bits[1] = true;
            Console.WriteLine(bits[1]);
            Console.WriteLine(bits.Value);
            */

            /*
            Console.WriteLine(bits);
            byte val1 = (byte)bits;
            Console.WriteLine(val1);
            */

            /*
            Devices devices = new Devices();
            Bits bits2 = new(63);
            Console.WriteLine(devices);
            devices.TurnOnOff(bits);
            Console.WriteLine(devices);
            */

            /*
            Bits bit = new Bits(10);
            Bits bitBite = new Bits((byte)11);
            Bits bitInt = new Bits(12);
            Bits bitLong = new Bits((long)13);

            Console.WriteLine(bit);
            Console.WriteLine(bitBite);
            Console.WriteLine(bitInt);
            Console.WriteLine(bitLong);
            */









        }
    }
}
