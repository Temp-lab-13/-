using HomeWork.Services.CalculatorLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork.Exeptions
{
    public class CalculatorExeptions : Exception
    {
        public Stack<CalculatorActionLog> ActionLog { get; private set; }
        public CalculatorExeptions(string? message, Stack<CalculatorActionLog> actionLogs) : base(message)
        {
            ActionLog = actionLogs;
        }

        public CalculatorExeptions(string message, Exception exception) : base(message, exception)
        {
        }

        public override string ToString()
        {
            return Message + ": " + string.Join("\n", ActionLog.Select(x => $"{x.Action} {x.Argument}"));
        }
    }

    public class CalculatorDivibeByZeroExeption : CalculatorExeptions
    {
        public CalculatorDivibeByZeroExeption(string? massage, Stack<CalculatorActionLog> actionLogs) : base(massage, actionLogs)
        {
        }

        public CalculatorDivibeByZeroExeption(string? massage, Exception exception) : base(massage, exception)
        {
        }
    }

    public class CalculatorOperationCauseOverflowExepction : CalculatorExeptions
    {
        public CalculatorOperationCauseOverflowExepction(string? message, Stack<CalculatorActionLog> actionLogs) : base(message, actionLogs)
        {
        }

        public CalculatorOperationCauseOverflowExepction(string? message, Exception exception) : base(message, exception)
        {
        }
    }

    public class CalculatorActionCancel : CalculatorExeptions
    {
        public CalculatorActionCancel(string? massage, Stack<CalculatorActionLog> actionLogs) : base(massage, actionLogs)
        {
        }

        public CalculatorActionCancel(string? massage, Exception exception) : base(massage, exception)
        {
        }
    }
}
