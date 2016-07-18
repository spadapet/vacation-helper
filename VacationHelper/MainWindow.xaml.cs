using System.Windows;

namespace VacationHelper
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.DataContext = App.MyCurrent.VacationData;
            InitializeComponent();
        }
    }
}
