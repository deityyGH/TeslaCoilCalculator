using System;
using System.Collections.ObjectModel;
using SGTC.Models;
using SGTC.Core;

namespace SGTC.ViewModels
{
    public class PrimaryCircuitViewModel : ObservableObject
    {
        private readonly ICoilDataService _dataService;
        public PrimaryCircuitViewModel(ICoilDataService dataService)
        {
            _dataService = dataService;

            PrimaryWindingTypes = new ObservableCollection<PrimaryWindingType>
            {
                PrimaryWindingType.Solenoid,
                PrimaryWindingType.FlatSpiral,
                PrimaryWindingType.Conical
            };

            PrimaryCapacitorConnectionTypes = new ObservableCollection<PrimaryCapacitorConnectionType>
            {
                PrimaryCapacitorConnectionType.Series,
                PrimaryCapacitorConnectionType.Parallel
            };
        }

        // Combo box
        private ObservableCollection<PrimaryWindingType> _primaryWindingTypes;
        public ObservableCollection<PrimaryWindingType> PrimaryWindingTypes
        {
            get => _primaryWindingTypes;
            set
            {
                _primaryWindingTypes = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<PrimaryCapacitorConnectionType> _primaryCapacitorConnectionTypes;
        public ObservableCollection<PrimaryCapacitorConnectionType> PrimaryCapacitorConnectionTypes
        {
            get => _primaryCapacitorConnectionTypes;
            set
            {
                _primaryCapacitorConnectionTypes = value;
                OnPropertyChanged();
            }
        }


        public double PrimaryTurns
        {
            get => _dataService.Parameters.PrimaryTurns;
            set
            {
                if (_dataService.Parameters.PrimaryTurns != value)
                {
                    _dataService.Parameters.PrimaryTurns = value;
                    OnPropertyChanged();
                }
            }
        }

        public double PrimaryCoreDiameter
        {
            get => _dataService.Parameters.PrimaryCoreDiameter;
            set
            {
                if (_dataService.Parameters.PrimaryCoreDiameter != value)
                {
                    _dataService.Parameters.PrimaryCoreDiameter = value;
                    OnPropertyChanged();
                }
            }
        }

        public double PrimaryWireDiameter
        {
            get => _dataService.Parameters.PrimaryWireDiameter;
            set
            {
                if (_dataService.Parameters.PrimaryWireDiameter != value)
                {
                    _dataService.Parameters.PrimaryWireDiameter = value;
                    OnPropertyChanged();
                }
            }
        }

        

        public double PrimaryWireInsulationDiameter
        {
            get => _dataService.Parameters.PrimaryWireInsulationDiameter;
            set
            {
                if (_dataService.Parameters.PrimaryWireInsulationDiameter != value)
                {
                    _dataService.Parameters.PrimaryWireInsulationDiameter = value;
                    OnPropertyChanged();
                }
            }
        }

        public double PrimaryWireSpacing
        {
            get => _dataService.Parameters.PrimaryWireSpacing;
            set
            {
                if (_dataService.Parameters.PrimaryWireSpacing != value)
                {
                    _dataService.Parameters.PrimaryWireSpacing = value;
                    OnPropertyChanged();
                }
            }
        }

        public PrimaryWindingType SelectedPrimaryWindingType
        {
            get => _dataService.Parameters.PrimaryWindingType;
            set
            {
                if (_dataService.Parameters.PrimaryWindingType != value)
                {
                    _dataService.Parameters.PrimaryWindingType = value;
                    OnPropertyChanged();
                }
            }
        }


        public PrimaryCapacitorConnectionType SelectedPrimaryCapacitorConnectionType
        {
            get => _dataService.Parameters.PrimaryCapacitorConnectionType;
            set
            {
                Console.WriteLine(_dataService.Parameters.PrimaryCapacitorConnectionType);
                if (_dataService.Parameters.PrimaryCapacitorConnectionType != value)
                {
                    _dataService.Parameters.PrimaryCapacitorConnectionType = value;
                    OnPropertyChanged();
                }
            }
        }


        public double PrimaryCapacitance
        {
            get => _dataService.Parameters.PrimaryCapacitance;
            set
            {
                if (_dataService.Parameters.PrimaryCapacitance != value)
                {
                    _dataService.Parameters.PrimaryCapacitance = value;
                    OnPropertyChanged();
                }
            }
        }


        public int PrimaryCapacitorAmount
        {
            get => _dataService.Parameters.PrimaryCapacitorAmount;
            set
            {
                if (_dataService.Parameters.PrimaryCapacitorAmount != value)
                {
                    _dataService.Parameters.PrimaryCapacitorAmount = value;
                    OnPropertyChanged();
                }
            }
        }








        // ====== Units ====== //



    }
}
