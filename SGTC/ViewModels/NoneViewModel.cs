﻿using SGTC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGTC.ViewModels
{
    public class NoneViewModel : IValidatable
    {
        public NoneViewModel()
        {
        }

        public bool IsFormValid => true;
    }
}
