using System.Windows;

namespace VacationHelper
{
    public partial class App : Application
    {
        public static App MyCurrent { get { return (App)Application.Current; } }
        public VacationData VacationData { get; } = new VacationData();
    }
}
