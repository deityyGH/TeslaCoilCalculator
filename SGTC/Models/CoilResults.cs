using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGTC.Models
{
    public class CoilResults
    {
        public double PrimaryResonance { get; set; }
        public double PrimaryInductance { get; set; }
        public double PrimaryXc { get; set; }
        public double PrimaryXl { get; set; }
        public double PrimaryWireLength { get; set; }
        public double PrimaryWireWeight { get; set; }
        public double PrimaryCoilHeight { get; set; }
        public double PrimaryCapacitance { get; set; }
        public double PrimaryTurns { get; set; }

        // Secondary + topload
        public double SecondaryResonance { get; set; }
        public double SecondaryInductance { get; set; }
        public double SecondaryXc { get; set; }
        public double SecondaryXl { get; set; }
        public double SecondaryWireLength { get; set; }
        public double SecondaryWireWeight { get; set; }
        public double SecondaryCoilHeight { get; set; }
        public double TotalCapacitance { get; set; }
        public double TopLoadCapacitance { get; set; }
        public double SecondaryResonanceNoTopLoad { get; set; }
    }
}
