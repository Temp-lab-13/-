using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork.Services.CalculatorLog
{
    public class CalculatorActionLog
    {
        public CalculatorAction Action { get; private set; }
        public int Argument { get; private set; }
        public double ArgumentDoubl { get; private set; }
        public CalculatorActionLog(CalculatorAction action, int argument)
        {
            Action = action;
            Argument = argument;
        }

        public CalculatorActionLog(CalculatorAction action, double argument)
        {
            Action = action;
            ArgumentDoubl = argument;
        }
    }
}
