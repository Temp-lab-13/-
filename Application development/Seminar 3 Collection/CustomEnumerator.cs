using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seminar_3_Collection
{
    internal class CustomEnumerator : IEnumerator<int> //Тоже Task2
    {
        public int Current { get; private set; } = -1;

        object IEnumerator.Current => 0;
        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            if (Current < 10)
            {
                Current++;
                return true;
            }
            return false;
        }

        public void Reset()
        {
            Current = -1;
        }
    }
}
