using System.Windows.Controls;

namespace ColorSpace
{
    public partial class ColorGeneratorView : UserControl
    {
        public ColorGeneratorView()
        {
            InitializeComponent();
            DataContext = new ColorGeneratorViewModel();
        }
    }
}
