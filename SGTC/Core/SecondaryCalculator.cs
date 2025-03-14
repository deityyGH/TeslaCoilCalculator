using SGTC.Models;
using SGTC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGTC.Core
{
    public class SecondaryCalculator
    {
        private readonly CoilCalculatorData _data = CoilCalculatorData.Instance;

        public double SecondaryResonance { get; set; } = 0;
        public double SecondaryInductance { get; set; } = 0;
        public double SecondaryXc { get; set; } = 0;
        public double SecondaryXl { get; set; } = 0;
        public double SecondaryWireLength { get; set; } = 0;
        public double SecondaryWireWeight { get; set; } = 0;
        public double SecondaryCoilHeight { get; set; } = 0;
        public double TopLoadCapacitance { get; set; } = 0;
        public double SecondaryResonanceNoTopLoad { get; set; } = 0;

        public static void Run(ResultViewModel resultViewModel)
        {
            //GetInductance(resultViewModel);

        }
    }
}
