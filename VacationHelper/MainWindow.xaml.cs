using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace VacationHelper
{
    public partial class MainWindow : Window
    {
        private int lastFocus;

        public MainWindow()
        {
            this.DataContext = App.VacationData;
            InitializeComponent();
        }

        private void VacationStart1_IsKeyboardFocusWithinChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.lastFocus = 1;
        }

        private void VacationStart2_IsKeyboardFocusWithinChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.lastFocus = 2;
        }

        private void VacationStart3_IsKeyboardFocusWithinChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.lastFocus = 3;
        }

        private void LeaveStart1_IsKeyboardFocusWithinChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.lastFocus = 4;
        }

        private void Button_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            for (DependencyObject obj = e.OriginalSource as DependencyObject; obj != null; obj = VisualTreeHelper.GetParent(obj))
            {
                CalendarDayButton cdb = obj as CalendarDayButton;
                if (cdb != null && cdb.DataContext is DateTime)
                {
                    DateTime dt = (DateTime)cdb.DataContext;

                    if (this.lastFocus == 1)
                    {
                        App.VacationData.VacationStart1 = dt;
                    }
                    else if (this.lastFocus == 2)
                    {
                        App.VacationData.VacationStart2 = dt;
                    }
                    else if (this.lastFocus == 3)
                    {
                        App.VacationData.VacationStart3 = dt;
                    }
                    else if (this.lastFocus == 4)
                    {
                        App.VacationData.LeaveStart1 = dt;
                    }

                    break;
                }
            }
        }
    }
}
