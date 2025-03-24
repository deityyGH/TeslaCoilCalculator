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
        private readonly ICoilDataService _dataService;

        public TorusViewModel TorusViewModel { get; set; }
        public SphereViewModel SphereViewModel { get; set; }
        public NoneViewModel NoneViewModel { get; set; }

        public TopLoadViewModel(
            TorusViewModel torusViewModel,
            SphereViewModel sphereViewModel,
            NoneViewModel noneViewModel,
            ICoilDataService dataService)
        {
            TorusViewModel = torusViewModel;
            SphereViewModel = sphereViewModel;
            NoneViewModel = noneViewModel;

            _dataService = dataService;

            TopLoadTypes = new ObservableCollection<TopLoadType>
            {
                TopLoadType.None,
                TopLoadType.Torus,
                TopLoadType.Sphere
            };


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
        //private ObservableCollection<TopLoadType> _topLoadTypes;
        //public ObservableCollection<TopLoadType> TopLoadTypes
        //{
        //    get => _topLoadTypes;
        //    set
        //    {
        //        _topLoadTypes = value;
        //        OnPropertyChanged();
        //    }
        //}
        public ObservableCollection<TopLoadType> TopLoadTypes { get; }


        public TopLoadType SelectedTopLoadType
        {
            get => _dataService.Parameters.TopLoadType;
            set
            {
                if (_dataService.Parameters.TopLoadType != value)
                {
                    _dataService.Parameters.TopLoadType = value;
                    OnPropertyChanged();
                    UpdateTopLoadContentView();
                }
            }
        }


        private void UpdateTopLoadContentView()
        {
            CurrentTopLoadContentView = SelectedTopLoadType switch
            {
                TopLoadType.Torus => TorusViewModel,
                TopLoadType.Sphere => SphereViewModel,
                _ => NoneViewModel
            };
        }
    }
}
