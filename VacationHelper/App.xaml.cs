using System.Windows;

namespace VacationHelper
{
    public partial class App : Application
    {
        public static VacationData VacationData { get; } = new VacationData();
    }
}
