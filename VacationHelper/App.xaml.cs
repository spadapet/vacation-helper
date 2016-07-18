using System.Windows;

namespace VacationHelper
{
    public partial class App : Application
    {
        internal static new App Current { get { return (App)Application.Current; } }
        internal VacationData VacationData { get; } = new VacationData();
    }
}
