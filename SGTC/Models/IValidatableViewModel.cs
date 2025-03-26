using SGTC.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGTC.Models
{
    public interface IValidatableViewModel : IDataErrorInfo
    {
        string ErrorMessage { get; set; }
        bool IsFormValid { get; }
        void AddValidationRule(string propertyName, Func<string> validationFunc);
        void ClearValidationRules();
    }

    public abstract class ValidatableViewModel : ObservableObject, IValidatableViewModel
    {
        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        private Dictionary<string, Func<string>> _validationRules = new Dictionary<string, Func<string>>();

        public void AddValidationRule(string propertyName, Func<string> validationFunc)
        {
            _validationRules[propertyName] = validationFunc;
        }

        public string this[string columnName] => _validationRules.ContainsKey(columnName) ? _validationRules[columnName]() : null;

        public string Error => null;
        public bool IsFormValid => _validationRules.Values.All(v => v() == null);

        public void ClearValidationRules()
        {
            _validationRules.Clear();
        }
    }
}
