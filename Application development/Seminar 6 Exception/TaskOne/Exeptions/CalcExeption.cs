using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskOne.Services;

namespace TaskOne.Exeptions
{
    internal class CalcExeption : Exception
    {
        public Stack<CalcActionLog> ActionLog { get; private set; }
        public CalcExeption(string? message, Stack<CalcActionLog> actionLogs) : base(message)
        {
            ActionLog = actionLogs;
        }

        public CalcExeption(string message, Exception exception) : base(message, exception)
        {
        }

        public override string ToString()
        {
            return Message + ": " + string.Join("\n", ActionLog.Select(x => $"{x.CalcAction} {x.CalcArgument}"));
        }

    }

    internal class CalcDivibeByExeption : CalcExeption
    {
        public CalcDivibeByExeption(string? massage, Stack<CalcActionLog> actionLogs) : base(massage, actionLogs)
        {
        }

        public CalcDivibeByExeption(string? massage, Exception exception) : base(massage, exception)
        {
        }
    }

    internal class CalcOperationCauseOverflowExepction : CalcExeption
    {
        public CalcOperationCauseOverflowExepction(string? message, Stack<CalcActionLog> actionLogs) : base(message, actionLogs)
        {
        }

        public CalcOperationCauseOverflowExepction(string? message, Exception exception) : base(message, exception)
        {
        }
    }
}
