using Calculis.Core.Calculation;
using Calculis.Core.Entities.Items.Abstractions;

namespace Calculis.Console
{

    class Program
    {
        static void Main()
        {
            var items = new List<IItem>();

            items.Add(new DataItem("i1", 3));
            items.Add(new DataItem("i2", 2));

            var calculis = new Engine(items);

            var calc1 = calculis.Add("calc1", "i1 + i2 * 5");
            var calc2 = calculis.Add("calc2", "POW(calc1;2)");

            System.Console.WriteLine($"calc1: {calc1.Value}"); //'calc1: 28'
            System.Console.WriteLine($"calc2: {calc2.Value}"); //'calc2: 169'

            System.Console.ReadKey();
        }
    }
}
