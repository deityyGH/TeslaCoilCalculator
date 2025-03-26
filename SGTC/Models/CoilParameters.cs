using SGTC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGTC.Models
{
    public class CoilParameters
    {
        public double PrimaryTurns { get; set; } = 10.0;
        public double PrimaryCoreDiameter { get; set; } = 87.6 * 1e-3;
        public double PrimaryWireDiameter { get; set; } = 1.75 * 1e-3;
        public double PrimaryWireInsulationDiameter { get; set; } = 2.4 * 1e-3;
        public double PrimaryWireSpacing { get; set; } = 0 * 1e-3;
        public PrimaryWindingType PrimaryWindingType { get; set; } = PrimaryWindingType.Solenoid;
        //public PrimaryCapacitorConnectionType PrimaryCapacitorConnectionType { get; set; } = PrimaryCapacitorConnectionType.Parallel;
        public double PrimaryCapacitance { get; set; } = 22 * 1e-9;
        public Unit CapacitanceUnit { get; set; } = Unit.Nano;
        //public int PrimaryCapacitorAmount { get; set; } = 4;


        // Secondary tab
        public double SecondaryTurns { get; set; } = 900.0;
        public double SecondaryCoreDiameter { get; set; } = 75 * 1e-3;
        public double SecondaryWireDiameter { get; set; } = 0.18 * 1e-3;
        public double SecondaryWireInsulationDiameter { get; set; } = 0.2 * 1e-3;

        // Top Load tab
        public TopLoadType TopLoadType { get; set; } = TopLoadType.Torus;
        public double TopLoadTorusInDiameter { get; set; } = 56 * 1e-3;
        public double TopLoadTorusOutDiameter { get; set; } = 166 * 1e-3;
        public double TopLoadSphereDiameter { get; set; } = 0 * 1e-3;
    }
}
