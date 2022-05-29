using System.Collections.Generic;
using Calculis.Core;

namespace Calculis.Functions
{
    //Parameters:
    //1 - interval id: 0 - munute; 1 - hour;
    //
    public class MaxofFunction : TemporalFunction
    {
        public MaxofFunction(IList<IValueItem> args) : base(args)
        {
            Name = "MAXOF";
            Function = () =>
            {
                //Select interval
                int selectedCount = 0;
                double[] selectedCash;

                if (args[0].Value == 0)
                    selectedCount = 60;
                else if (args[0].Value == 1)
                    selectedCount = 3600;

                selectedCash = new double[selectedCount];

                
                
                

                return 0;
            };
        }
    }
}
