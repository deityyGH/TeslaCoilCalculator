using SGTC.Models;

namespace SGTC.ViewModels.TopLoad
{
    public class TorusViewModel : TopLoadTypeViewModel
    {
        private readonly CoilCalculatorData _data;
        private double _innerDiameter;
        public double InnerDiameter
        {
            
            get => _innerDiameter;
            set
            {
                _innerDiameter = value;
                _data.TopLoadTorusInDiameter = value;
                OnPropertyChanged();
            }
        }

        private double _outerDiameter;
        public double OuterDiameter
        {
            get => _outerDiameter;
            set
            {
                _outerDiameter = value;
                
                OnPropertyChanged();
            }
        }
        public override string ToString() => "Torus";
    }
}
