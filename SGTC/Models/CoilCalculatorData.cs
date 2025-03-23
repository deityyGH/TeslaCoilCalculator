using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGTC.Models
{
    public class CoilCalculatorData
    {

        private static CoilCalculatorData _instance;
        public static CoilCalculatorData Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CoilCalculatorData();
                }
                return _instance;
            }
        }
        // Primary tab
        public double PrimaryTurns { get; set; } = 10.0;
        public double PrimaryCoreDiameter { get; set; } = 87.6;
        public double PrimaryWireDiameter { get; set; } = 1.75;
        public double PrimaryWireInsulationDiameter { get; set; } = 2.4;
        public double PrimaryWireSpacing { get; set; } = 0;
        public PrimaryWindingType PrimaryWindingType { get; set; } = PrimaryWindingType.Solenoid;
        public PrimaryCapacitorConnectionType PrimaryCapacitorConnectionType { get; set; } = PrimaryCapacitorConnectionType.Parallel;
        public double PrimaryCapacitance { get; set; } = 6;
        public int PrimaryCapacitorAmount { get; set; } = 4;

        

        // Secondary tab
        public double SecondaryTurns { get; set; } = 900;
        public double SecondaryCoreDiameter { get; set; } = 75;
        public double SecondaryWireDiameter { get; set; } = 0.18;
        public double SecondaryWireInsulationDiameter { get; set; } = 0.2;

        // Top Load tab
        public TopLoadType TopLoadType { get; set; } = TopLoadType.Torus;
        public double TopLoadTorusInDiameter { get; set; } = 56;
        public double TopLoadTorusOutDiameter { get; set; } = 166;
        public double TopLoadSphereDiameter { get; set; } = 0;


        private CoilCalculatorData() { }
    }
}
