using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace SGTC.ViewModels.Tabs
{
    class MainViewModel : INotifyPropertyChanged
    {
        private object _selectedTabType;
        public object SelectedTabType
        {
            get => _selectedTabType;
            set
            {
                if (_selectedTabType != value)
                {
                    _selectedTabType = value;
                    OnPropertyChanged();
                }

            }
        }

        public ICommand SelectedTabCommand { get; private set; }

        public MainViewModel()
        {
            SelectedTabType = new PrimaryCircuitViewModel();
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        

    }
}
