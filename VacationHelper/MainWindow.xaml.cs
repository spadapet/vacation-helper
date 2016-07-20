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

        private void OnClickDay(object sender, RoutedEventArgs e)
        {
            if (this.lastFocus == 1)
            {
            }

            if (this.lastFocus == 2)
            {
            }
        }

        private void VacationStart1_IsKeyboardFocusWithinChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.lastFocus = 1;
        }

        private void VacationStart2_IsKeyboardFocusWithinChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.lastFocus = 2;
        }

        private void LeaveStart1_IsKeyboardFocusWithinChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.lastFocus = 3;
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

                    if (this.lastFocus == 2)
                    {
                        App.VacationData.VacationStart2 = dt;
                    }

                    if (this.lastFocus == 3)
                    {
                        App.VacationData.LeaveStart1 = dt;
                    }

                    break;
                }
            }
        }
    }
}
