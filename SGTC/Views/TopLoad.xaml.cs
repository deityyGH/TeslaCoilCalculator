using System.Windows.Controls;
using SGTC.ViewModels.TopLoad;

namespace SGTC.Views
{
    public partial class TopLoad : UserControl
    {
        public TopLoad()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }

    }
}
