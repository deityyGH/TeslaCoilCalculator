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
        public double SecondaryResonance { get; set; } = 0;
        public double SecondaryInductance { get; set; } = 0;
        public double SecondaryXc { get; set; } = 0;
        public double SecondaryXl { get; set; } = 0;
        public double SecondaryWireLength { get; set; } = 0;
        public double SecondaryWireWeight { get; set; } = 0;
        public double SecondaryCoilHeight { get; set; } = 0;
        public double TopLoadCapacitance { get; set; } = 0;
        public double SecondaryResonanceNoTopLoad { get; set; } = 0;


        // Units

        public string PrimaryResonanceUnit { get; set; } = "";
        public string PrimaryInductanceUnit { get; set; } = "";
        public string PrimaryXcUnit { get; set; } = "";
        public string PrimaryXlUnit { get; set; } = "";
        public string PrimaryWireLengthUnit { get; set; } = "";
        public string PrimaryWireWeightUnit { get; set; } = "";
        public string PrimaryCoilHeightUnit { get; set; } = "";
        public string PrimaryCapacitanceUnit { get; set; } = "";

        // Secondary + topload
        public string SecondaryResonanceUnit { get; set; } = "";
        public string SecondaryInductanceUnit { get; set; } = "";
        public string SecondaryXcUnit { get; set; } = "";
        public string SecondaryXlUnit { get; set; } = "";
        public string SecondaryWireLengthUnit { get; set; } = "";
        public string SecondaryWireWeightUnit { get; set; } = "";
        public string SecondaryCoilHeightUnit { get; set; } = "";
        public string TopLoadCapacitanceUnit { get; set; } = "";
        public string SecondaryResonanceNoTopLoadUnit { get; set; } = "";

        private CoilCalculatorResult() { }
    }
}
