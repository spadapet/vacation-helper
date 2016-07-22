using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using Newtonsoft.Json;

namespace VacationHelper
{
    [JsonObject(MemberSerialization.OptIn)]
    public class VacationData : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string name1;
        private string name2;
        private DateTime vacationStart1;
        private DateTime vacationStart2;
        private DateTime vacationStart3;
        private DateTime leaveStart1;
        private TimeSpan vacationSpan1;
        private TimeSpan vacationSpan2;
        private TimeSpan vacationSpan3;
        private TimeSpan leaveSpan1;
        private SolidColorBrush brush1;
        private SolidColorBrush brush2;
        private SolidColorBrush brush3;
        private SolidColorBrush brush4;
        private HashSet<DateTime> holidays;

        public VacationData()
        {
            this.name1 = "Peter";
            this.name2 = "Lila";
            this.vacationStart1 = DateTime.Parse("2016/10/10");
            this.vacationStart2 = DateTime.Parse("2016/10/10"); ;
            this.vacationStart3 = DateTime.Parse("2016/10/10"); ;
            this.leaveStart1 = DateTime.Parse("2016/10/10"); ;
            this.vacationSpan1 = TimeSpan.FromDays(35);
            this.vacationSpan2 = TimeSpan.FromDays(20);
            this.vacationSpan3 = TimeSpan.Zero;
            this.leaveSpan1 = TimeSpan.FromDays(8 * 7);
            this.brush1 = new SolidColorBrush(Color.FromArgb(96, 255, 0, 0));
            this.brush2 = new SolidColorBrush(Color.FromArgb(96, 0, 255, 0));
            this.brush3 = new SolidColorBrush(Color.FromArgb(96, 255, 255, 0));
            this.brush4 = new SolidColorBrush(Color.FromArgb(96, 0, 0, 255));

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

        public static VacationData Load()
        {
            try
            {
                using (FileStream stream = File.OpenRead(VacationData.SaveFileName))
                using (StreamReader streamReader = new StreamReader(stream))
                using (JsonTextReader reader = new JsonTextReader(streamReader))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    VacationData data = serializer.Deserialize<VacationData>(reader);

                    if (data != null)
                    {
                        return data;
                    }
                }
            }
            catch
            {
            }

            return new VacationData();
        }

        public void Save()
        {
            try
            {
                using (FileStream stream = File.OpenWrite(VacationData.SaveFileName))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(writer, this);
                }
            }
            catch
            {
            }
        }

        private static string SaveFileName
        {
            get
            {
                return Environment.ExpandEnvironmentVariables(@"%appdata%\VacationData.json");
            }
        }

        [JsonProperty]
        public string Name1
        {
            get { return this.name1; }
            set { this.UpdateValue(ref this.name1, value); }
        }

        [JsonProperty]
        public string Name2
        {
            get { return this.name2; }
            set { this.UpdateValue(ref this.name2, value); }
        }

        [JsonProperty]
        public DateTime VacationStart1
        {
            get { return this.vacationStart1; }
            set { this.UpdateValue(ref this.vacationStart1, value); }
        }

        public DateTime VacationEnd1
        {
            get { return this.VacationStart1 + this.AdjustVacationTimeSpan(this.VacationStart1, this.VacationSpan1); }
        }

        [JsonProperty]
        public DateTime VacationStart2
        {
            get { return this.vacationStart2; }
            set { this.UpdateValue(ref this.vacationStart2, value); }
        }

        public DateTime VacationEnd2
        {
            get { return this.VacationStart2 + this.AdjustVacationTimeSpan(this.VacationStart2, this.VacationSpan2); }
        }

        [JsonProperty]
        public DateTime VacationStart3
        {
            get { return this.vacationStart3; }
            set { this.UpdateValue(ref this.vacationStart3, value); }
        }

        public DateTime VacationEnd3
        {
            get { return this.VacationStart3 + this.AdjustVacationTimeSpan(this.VacationStart3, this.VacationSpan3); }
        }

        [JsonProperty]
        public DateTime LeaveStart1
        {
            get { return this.leaveStart1; }
            set { this.UpdateValue(ref this.leaveStart1, value); }
        }

        public DateTime LeaveEnd1
        {
            get { return this.LeaveStart1 + this.LeaveSpan1; }
        }

        [JsonProperty]
        public TimeSpan VacationSpan1
        {
            get { return this.vacationSpan1; }
            set { this.UpdateValue(ref this.vacationSpan1, value); }
        }

        [JsonProperty]
        public TimeSpan VacationSpan2
        {
            get { return this.vacationSpan2; }
            set { this.UpdateValue(ref this.vacationSpan2, value); }
        }

        [JsonProperty]
        public TimeSpan VacationSpan3
        {
            get { return this.vacationSpan3; }
            set { this.UpdateValue(ref this.vacationSpan3, value); }
        }

        [JsonProperty]
        public TimeSpan LeaveSpan1
        {
            get { return this.leaveSpan1; }
            set { this.UpdateValue(ref this.leaveSpan1, value); }
        }

        public string AnyDate
        {
            get { return string.Empty; }
        }

        public Brush GetBackgroundBrush(DateTime dt)
        {
            List<SolidColorBrush> brushes = new List<SolidColorBrush>(8);

            if (dt >= this.VacationStart1 && dt < this.VacationStart1 + this.AdjustVacationTimeSpan(this.VacationStart1, this.VacationSpan1))
            {
                brushes.Add(this.brush1);
            }

            if (dt >= this.VacationStart2 && dt < this.VacationStart2 + this.AdjustVacationTimeSpan(this.VacationStart2, this.VacationSpan2))
            {
                brushes.Add(this.brush2);
            }

            if (dt >= this.VacationStart3 && dt < this.VacationStart3 + this.AdjustVacationTimeSpan(this.VacationStart3, this.VacationSpan3))
            {
                brushes.Add(this.brush3);
            }

            if (dt >= this.LeaveStart1 && dt < this.LeaveStart1 + this.LeaveSpan1)
            {
                brushes.Add(this.brush4);
            }

            if (brushes.Count == 0)
            {
                return null;
            }
            else if (brushes.Count == 1)
            {
                return brushes[0];
            }

            List<GradientStop> stops = new List<GradientStop>(brushes.Count);
            for (int i = 0; i < brushes.Count; i++)
            {
                stops.Add(new GradientStop(brushes[i].Color, i / (double)brushes.Count));
                stops.Add(new GradientStop(brushes[i].Color, (i + 1) / (double)brushes.Count));
            }

            return new LinearGradientBrush(new GradientStopCollection(stops));
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

                if (!string.IsNullOrEmpty(name) &&
                    (name.Contains("Vacation") || name.Contains("Leave")) &&
                    (name.Contains("Start") || name.Contains("Span")))
                {
                    string endName = name.Replace("Start", "End").Replace("Span", "End");
                    this.NotifyPropertyChanged(endName);
                    this.NotifyPropertyChanged(nameof(this.AnyDate));
                }

                return true;
            }

            return false;
        }
    }
}
