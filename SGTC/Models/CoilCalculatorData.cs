using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGTC.Models
{
    class CoilCalculatorData
    {
        // Primary tab
        public double PrimaryTurns { get; set; }
        public double PrimaryWireDiameter { get; set; }
        public double PrimaryWireInsulationDiameter { get; set; }
        public double PrimaryWireSpacing { get; set; }
        public double PrimaryWindingType { get; set; }
        public string PrimaryCapacitorConnectionType { get; set; } = "Series";
        public double PrimaryCapacitance { get; set; }
        public int PrimaryCapacitorAmount { get; set; }


        // Secondary tab


        

    }
}
