﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGTC.Models
{
    public interface IValidatable
    {
        bool IsFormValid { get; }
    }
}
