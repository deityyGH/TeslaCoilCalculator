using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace SGTC.ViewModels.TopLoad
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private TopLoadTypeViewModel _selectedTopLoadType;



        public ObservableCollection<TopLoadTypeViewModel> TopLoadTypes { get; }
        public MainViewModel()
        {
            TopLoadTypes = new ObservableCollection<TopLoadTypeViewModel>
                {
                    new NoneViewModel(),
                    new TorusViewModel(),
                    new BallViewModel()
                };

            SelectedTopLoadType = TopLoadTypes[0];

        }

        public TopLoadTypeViewModel SelectedTopLoadType
        {
            get => _selectedTopLoadType;
            set
            {
                _selectedTopLoadType = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(RequiredRows));
            }
        }

        public int RequiredRows => SelectedTopLoadType is TorusViewModel ? 2 : 1;




        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
