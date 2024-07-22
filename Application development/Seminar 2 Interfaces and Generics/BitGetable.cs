using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seminar_2_Interfaces_and_Generics
{
    // интерфейс способнsq устанавливать и получать значения отдельных бит в заданном числе.
    public interface IBitGetable
    {
        bool GetBitByIndex(byte index); // Получаем;
        void SetBitByIndex(byte index, bool value); // Устанавливаем;
    }
}
