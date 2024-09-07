using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork.Exeptions
{
    public class CheckExeption
    {
        public void Execute(Action<int> action, int value)
        {
            try
            {
                action.Invoke(value);
            }
            catch (CalculatorDivibeByZeroExeption ex)
            {
                Console.WriteLine(ex);
            }
            catch (CalculatorOperationCauseOverflowExepction ex)
            {
                Console.WriteLine(ex);
            }
            catch (CalculatorActionCancel ex)
            {
                Console.WriteLine(ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void Execute(Action<double> action, double value)
        {
            try
            {
                action.Invoke(value);
            }
            catch (CalculatorDivibeByZeroExeption ex)
            {
                Console.WriteLine(ex);
            }
            catch (CalculatorOperationCauseOverflowExepction ex)
            {
                Console.WriteLine(ex);
            }
            catch (CalculatorActionCancel ex)
            {
                Console.WriteLine(ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
