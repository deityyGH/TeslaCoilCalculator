namespace SGTC.ViewModels.TopLoad
{
    public class BallViewModel : TopLoadTypeViewModel
    {
        private double _diameter;
        public double Diameter
        {
            get => _diameter;
            set
            {
                _diameter = value;
                OnPropertyChanged();
            }
        }
        public override string ToString() => "Ball";
    }
}
