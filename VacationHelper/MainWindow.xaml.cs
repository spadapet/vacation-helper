using System.Windows;

namespace VacationHelper
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.DataContext = App.Current.VacationData;
            InitializeComponent();
        }
    }
}
