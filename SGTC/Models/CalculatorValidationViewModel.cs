using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGTC.Models
{
    public abstract class CalculatorValidationViewModel : ValidatableViewModel
    {
        protected bool IsPositive(double value) => value > 0;
        protected bool IsPositiveOrZero(double value) => value >= 0;

        protected abstract void SetupValidationRules();

        protected CalculatorValidationViewModel()
        {
            SetupValidationRules();
        }
    }
}
