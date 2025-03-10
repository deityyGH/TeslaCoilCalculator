using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace SGTC.ViewModels.Tabs
{
    class PrimaryCircuitViewModel : INotifyPropertyChanged
    {
        private double _primaryTurns;
        public double PrimaryTurns
        {
            get => _primaryTurns;
            set
            {
                if (_primaryTurns != value)
                {
                    _primaryTurns = value;
                    OnPropertyChanged();
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
