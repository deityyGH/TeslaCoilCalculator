using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGTC.Models
{
    public interface ICoilDataService
    {
        CoilParameters Parameters { get; set; }
        CoilResults Results { get; set; }
    }

    public class CoilDataService : ICoilDataService
    {
        public CoilParameters Parameters { get; set; } = new CoilParameters();
        public CoilResults Results { get; set; } = new CoilResults();
    }
}
