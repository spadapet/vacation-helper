using System;
using System.Windows;

namespace VacationHelper
{
    public partial class App : Application
    {
        public static VacationData VacationData { get; }

        static App()
        {
            App.VacationData = VacationData.Load();
        }

        protected override void OnDeactivated(EventArgs args)
        {
            App.VacationData.Save();
            base.OnDeactivated(args);
        }
    }
}
