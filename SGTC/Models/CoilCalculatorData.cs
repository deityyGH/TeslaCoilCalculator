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
        public double PrimaryTurns { get; set; } = 0;
        public double PrimaryCoreDiameter { get; set; } = 0;
        public double PrimaryWireDiameter { get; set; } = 0;
        public double PrimaryWireInsulationDiameter { get; set; } = 0;
        public double PrimaryWireSpacing { get; set; } = 0;
        public PrimaryWindingType PrimaryWindingType { get; set; } = PrimaryWindingType.Solenoid;
        public PrimaryCapacitorConnectionType PrimaryCapacitorConnectionType { get; set; } = PrimaryCapacitorConnectionType.Series;
        public double PrimaryCapacitance { get; set; } = 0;
        public int PrimaryCapacitorAmount { get; set; } = 0;


        // Secondary tab
        public double SecondaryTurns { get; set; } = 0;
        public double SecondaryCoreDiameter { get; set; } = 0;
        public double SecondaryWireDiameter { get; set; } = 0;
        public double SecondaryWireInsulationDiameter { get; set; } = 0;

        // Top Load tab
        public TopLoadType TopLoadType { get; set; } = TopLoadType.None;
        public double TopLoadTorusInDiameter { get; set; } = 0;
        public double TopLoadTorusOutDiameter { get; set; } = 0;
        public double TopLoadSphereDiameter { get; set; } = 0;


        private CoilCalculatorData() { }
    }
}
