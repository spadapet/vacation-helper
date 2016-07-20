using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace VacationHelper
{
    public class VacationData : INotifyPropertyChanged
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
        private SolidColorBrush brush1;
        private SolidColorBrush brush2;
        private SolidColorBrush brush3;
        private HashSet<DateTime> holidays;

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
            this.brush1 = new SolidColorBrush(Colors.Red);
            this.brush2 = new SolidColorBrush(Colors.Blue);
            this.brush3 = new SolidColorBrush(Colors.Green);
            this.holidays = new HashSet<DateTime>()
            {
                new DateTime(2016, 11, 24),
                new DateTime(2016, 11, 25),
                new DateTime(2016, 12, 23),
                new DateTime(2016, 12, 26),
                new DateTime(2017, 01, 02),
                new DateTime(2017, 01, 16),
            };
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
            set
            {
                if (this.UpdateValue(ref this.vacationStart1, value))
                {
                    this.NotifyPropertyChanged(nameof(this.VacationEnd1));
                }
            }
        }

        public DateTime VacationEnd1
        {
            get { return this.vacationStart1 + this.AdjustVacationTimeSpan(this.vacationStart1, this.vacationSpan1); }
        }

        public DateTime VacationStart2
        {
            get { return this.vacationStart2; }
            set
            {
                if (this.UpdateValue(ref this.vacationStart2, value))
                {
                    this.NotifyPropertyChanged(nameof(this.VacationEnd2));
                }
            }
        }

        public DateTime VacationEnd2
        {
            get { return this.vacationStart1 + this.AdjustVacationTimeSpan(this.vacationStart2, this.vacationSpan2); }
        }

        public DateTime LeaveStart1
        {
            get { return this.leaveStart1; }
            set { this.UpdateValue(ref this.leaveStart1, value); }
        }

        public DateTime LeaveEnd1
        {
            get { return this.leaveStart1 + this.leaveSpan1; }
        }

        public TimeSpan VacationSpan1
        {
            get { return this.vacationSpan1; }
            set
            {
                if (this.UpdateValue(ref this.vacationSpan1, value))
                {
                    this.NotifyPropertyChanged(nameof(this.VacationEnd1));
                }
            }
        }

        public TimeSpan VacationSpan2
        {
            get { return this.vacationSpan2; }
            set
            {
                if (this.UpdateValue(ref this.vacationSpan2, value))
                {
                    this.NotifyPropertyChanged(nameof(this.VacationEnd2));
                }
            }
        }

        public TimeSpan LeaveSpan1
        {
            get { return this.leaveSpan1; }
            set { this.UpdateValue(ref this.leaveSpan1, value); }
        }

        public string AnyDate
        {
            get { return string.Empty; }
        }

        public SolidColorBrush GetBackgroundBrush(DateTime dt)
        {
            bool use = false;
            Color color = Color.FromArgb(64, 0, 0, 0);

            if (dt >= this.VacationStart1 && dt < this.VacationStart1 + this.AdjustVacationTimeSpan(this.VacationStart1, this.VacationSpan1))
            {
                use = true;
                color.R = 128;
            }

            if (dt >= this.VacationStart2 && dt < this.VacationStart2 + this.AdjustVacationTimeSpan(this.VacationStart2, this.VacationSpan2))
            {
                use = true;
                color.G = 128;
            }

            if (dt >= this.LeaveStart1 && dt < this.LeaveStart1 + this.LeaveSpan1)
            {
                use = true;
                color.B = 128;
            }

            return use ? new SolidColorBrush(color) : null;
        }

        private TimeSpan AdjustVacationTimeSpan(DateTime start, TimeSpan span)
        {
            DateTime end = this.SkipNonWorkDays(start);
            for (double i = 0; i < span.TotalDays; i++)
            {
                end = this.SkipNonWorkDays(end.AddDays(1));
            }

            return end - start;
        }

        private DateTime SkipNonWorkDays(DateTime date)
        {
            while (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday || this.holidays.Contains(date))
            {
                date = date.AddDays(1);
            }

            return date;
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

                if (name != null && (name.Contains("Start") || name.Contains("Span")))
                {
                    this.NotifyPropertyChanged("AnyDate");
                }

                return true;
            }

            return false;
        }
    }
}
