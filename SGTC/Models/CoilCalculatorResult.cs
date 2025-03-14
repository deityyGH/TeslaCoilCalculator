using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGTC.Models
{
    public class CoilCalculatorResult
    {
        private static CoilCalculatorResult _instance;
        public static CoilCalculatorResult Instance => _instance ??= new CoilCalculatorResult();


        // Primary
        public string PrimaryResonance { get; set; }
        public string PrimaryInductance { get; set; }
        public string PrimaryXc { get; set; }
        public string PrimaryXl { get; set; }
        public string PrimaryWireLength { get; set; }
        public string PrimaryWireWeight { get; set; }
        public string PrimaryCoilHeight { get; set; }
        public string PrimaryCapacitance { get; set; }

        // Secondary + topload
        public string SecondaryResonance { get; set; }
        public string SecondaryInductance { get; set; }
        public string SecondaryXc { get; set; }
        public string SecondaryXl { get; set; }
        public string SecondaryWireLength { get; set; }
        public string SecondaryWireWeight { get; set; }
        public string SecondaryCoilHeight { get; set; }
        public string TopLoadCapacitance { get; set; }
        public string SecondaryResonanceNoTopLoad { get; set; }

        private CoilCalculatorResult() { }
    }
}
