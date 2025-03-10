using System.ComponentModel;
using System.Runtime.CompilerServices;
namespace SGTC.ViewModels.TopLoad
{
    public abstract class TopLoadTypeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
