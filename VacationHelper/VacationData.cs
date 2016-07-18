using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace VacationHelper
{
    internal class VacationData : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string name1;
        private string name2;
        private DateTime vacationStart1;
        private DateTime vacationStart2;
        private DateTime leaveStart1;
        private TimeSpan vacationSpan1;
        private TimeSpan vacationSpan2;
        private TimeSpan leaveSpan1;

        public VacationData()
        {
            this.name1 = "Peter";
            this.name2 = "Lila";
            this.vacationStart1 = DateTime.Parse("2016/10/10");
            this.vacationStart2 = DateTime.Parse("2016/10/10"); ;
            this.leaveStart1 = DateTime.Parse("2016/10/10"); ;
            this.vacationSpan1 = TimeSpan.FromDays(35);
            this.vacationSpan2 = TimeSpan.FromDays(20);
            this.leaveSpan1 = TimeSpan.FromDays(8 * 7);
        }

        public string Name1
        {
            get { return this.name1; }
            set { this.UpdateValue(ref this.name1, value); }
        }

        public string Name2
        {
            get { return this.name2; }
            set { this.UpdateValue(ref this.name2, value); }
        }

        public DateTime VacationStart1
        {
            get { return this.vacationStart1; }
            set { this.UpdateValue(ref this.vacationStart1, value); }
        }

        public DateTime VacationStart2
        {
            get { return this.vacationStart2; }
            set { this.UpdateValue(ref this.vacationStart2, value); }
        }

        public DateTime LeaveStart1
        {
            get { return this.leaveStart1; }
            set { this.UpdateValue(ref this.leaveStart1, value); }
        }

        public TimeSpan VacationSpan1
        {
            get { return this.vacationSpan1; }
            set { this.UpdateValue(ref this.vacationSpan1, value); }
        }

        public TimeSpan VacationSpan2
        {
            get { return this.vacationSpan2; }
            set { this.UpdateValue(ref this.vacationSpan2, value); }
        }

        public TimeSpan LeaveSpan1
        {
            get { return this.leaveSpan1; }
            set { this.UpdateValue(ref this.leaveSpan1, value); }
        }

        private void NotifyPropertyChanged(string name = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private bool UpdateValue<T>(ref T oldValue, T newValue, [CallerMemberName] string name = null)
        {
            if (!EqualityComparer<T>.Default.Equals(oldValue, newValue))
            {
                oldValue = newValue;
                this.NotifyPropertyChanged(name);
                return true;
            }

            return false;
        }
    }
}
