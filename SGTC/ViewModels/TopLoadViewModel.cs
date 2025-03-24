using SGTC.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGTC.Core;

namespace SGTC.ViewModels
{
    public class TopLoadViewModel : ObservableObject
    {
        private readonly CoilCalculatorData _data = CoilCalculatorData.Instance;
        private readonly ICoilDataService _dataService;

        public TopLoadViewModel(ICoilDataService dataService)
        {
            _dataService = dataService;
            TopLoadTypes = new ObservableCollection<TopLoadType>
            {
                TopLoadType.None,
                TopLoadType.Torus,
                TopLoadType.Sphere
            };

            _torusViewModel = new TorusViewModel();
            _sphereViewModel = new SphereViewModel();
            _noneViewModel = new NoneViewModel();
            UpdateTopLoadContentView();
        }

        private object _currentTopLoadContentView;
        public object CurrentTopLoadContentView
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
                    UpdateTopLoadContentView();
                }
            }
        }

        //private readonly NoneViewModel _noneViewModel;
        public TorusViewModel _torusViewModel { get; set; }
        public SphereViewModel _sphereViewModel { get; set; }
        public NoneViewModel _noneViewModel { get; set; }



        private void UpdateTopLoadContentView()
        {
            CurrentTopLoadContentView = SelectedTopLoadType switch
            {
                TopLoadType.Torus => _torusViewModel,
                TopLoadType.Sphere => _sphereViewModel,
                TopLoadType.None => _noneViewModel,
                _ => _noneViewModel
            };
        }
    }
}
