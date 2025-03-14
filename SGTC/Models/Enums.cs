using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGTC.Models
{
    public enum PrimaryWindingType
    {
        Solenoid, 
        FlatSpiral,
        Conical
    }

    public enum PrimaryCapacitorConnectionType
    {
        Series,
        Parallel
    }

    public enum TopLoadType
    {
        None,
        Torus,
        Sphere
    }

    //public enum Unit
    //{
    //    Pico = -12,
    //    Nano = -9,
    //    Micro = -6,
    //    Mili = -3,
    //    Base = 0,
    //    Kilo = 3,
    //    Mega = 6
    //}


}
