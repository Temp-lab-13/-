using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Seminar_2_Interfaces_and_Generics
{
    // Применение интерфейса IBitGettable к классу Bits;
    public class Bits : IBitGetable
    {
        public long Value { get; private set; }

        public int Size { get; private set; }

        public Bits(byte value) // Конструктор класса, позволяет сразу задать значение.
        {
            Value = value;
            Size = sizeof(byte);
        }

        public Bits(int value) // 
        {
            Value = value;
            Size = sizeof(int);
        }

        public Bits(long value) // 
        {
            Value = value;
            Size = sizeof(byte);
        }


        public bool GetBitByIndex(byte index) // Реализованный метод интерфейса, возращающий значение
        {
            return (Value & (1 << index)) != 0;
        }

        public void SetBitByIndex(byte index, bool value) // Реализованный метод интрефейса, устанавливающий
        {                                                 // новое значение байта (с указанием индекса 0/1);
            if (value)
            {
                Value |= (byte)(1 << index);
            }
            else
            {
                Value &= (byte)~(1 << index);
            }
        }

        public bool this[byte index] // Переопределение оператора this;
        {
            get => GetBitByIndex(index);
            set => SetBitByIndex(index, value);
        }
        // оператор приведения из byte в bits;
        public static implicit operator byte(Bits bits) => (byte)bits.Value; 
        public static explicit operator Bits(byte value) => new (value); 
        // оператор пирведения из int в bits;
        public static implicit operator int(Bits bits) => (int)bits.Value; 
        public static explicit operator Bits(int value) => new(value); 
        // оператор пирведения из long в bits;
        public static implicit operator long(Bits bits) => (long)bits.Value; 
        public static explicit operator Bits(long value) => new(value);

        public override string ToString()
        {
            return $"{Value} -> {Convert.ToString(Value, 2).PadLeft(Size + 1, '0')}";
        }
    }
}
