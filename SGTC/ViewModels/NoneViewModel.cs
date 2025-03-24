using SGTC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGTC.ViewModels
{
    public class NoneViewModel
    {
        private readonly ICoilDataService _dataService;
        public NoneViewModel(ICoilDataService dataService)
        {
            _dataService = dataService;
        }
    }
}
