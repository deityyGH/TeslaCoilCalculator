using SGTC.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGTC.Core;
using SGTC.Models;

namespace SGTC.ViewModels
{
    public class TopLoadViewModel : ObservableObject
    {
        private readonly CoilCalculatorData _data = CoilCalculatorData.Instance;

        private ObservableObject _currentTopLoadContentView;
        public ObservableObject CurrentTopLoadContentView
        {
            get => _currentTopLoadContentView;
            set
            {
                _currentTopLoadContentView = value;
                OnPropertyChanged();
            }
        }

        // Combobox
        private ObservableCollection<TopLoadType> _topLoadTypes;
        public ObservableCollection<TopLoadType> TopLoadTypes
        {
            get => _topLoadTypes;
            set
            {
                _topLoadTypes = value;
                OnPropertyChanged();
            }
        }

        public TopLoadType SelectedTopLoadType
        {
            get => _data.TopLoadType;
            set
            {
                if (_data.TopLoadType != value)
                {
                    _data.TopLoadType = value;
                    OnPropertyChanged();
                }
            }
        }

        //private readonly NoneViewModel _noneViewModel;
        private readonly TorusViewModel _torusViewModel;
        private readonly SphereViewModel _sphereViewModel;

        public TopLoadViewModel()
        {
            TopLoadTypes = new ObservableCollection<TopLoadType>
            {
                TopLoadType.None,
                TopLoadType.Torus,
                TopLoadType.Sphere
            };

            _torusViewModel = new TorusViewModel();
            _sphereViewModel = new SphereViewModel();
        }

        private void UpdateTopContentView()
        {
            CurrentTopLoadContentView = SelectedTopLoadType switch
            {
                TopLoadType.Torus => _torusViewModel,
                TopLoadType.Sphere => _sphereViewModel
            };
        }
    }
}
