using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGTC.Models
{
    public class CoilCalculatorData
    {
        // Primary tab
        public double PrimaryTurns { get; set; } = 0;
        public double PrimaryCoreDiameter { get; set; } = 0;
        public double PrimaryWireDiameter { get; set; } = 0;
        public double PrimaryWireInsulationDiameter { get; set; } = 0;
        public double PrimaryWireSpacing { get; set; } = 0;
        public double PrimaryWindingType { get; set; } = 0;
        public string PrimaryCapacitorConnectionType { get; set; } = "Series";
        public double PrimaryCapacitance { get; set; } = 0;
        public int PrimaryCapacitorAmount { get; set; } = 0;


        // Secondary tab
        public double SecondaryTurns { get; set; } = 0;
        public double SecondaryCoreDiameter { get; set; } = 0;
        public double SecondaryWireDiameter { get; set; } = 0;
        public double SecondaryWireInsulationDiameter { get; set; } = 0;

        // Top Load tab
        public string TopLoadType { get; set; } = "Torus";
        public double TopLoadTorusInDiameter { get; set; } = 0;
        public double TopLoadTorusOutDiameter { get; set; } = 0;
        public double TopLoadBallDiameter { get; set; } = 0;

    }
}
