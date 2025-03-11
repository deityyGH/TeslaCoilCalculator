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
}
