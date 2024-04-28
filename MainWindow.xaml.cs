using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;

namespace KALENDAR
{
    public partial class MainWindow : Window
    {
        private DateTime currentDate;

        public MainWindow()
        {
            InitializeComponent();
            currentDate = DateTime.Today;
            UpdateCalendar();
        }

        private void UpdateCalendar()
        {
            
            calendarGrid.Children.Clear();

            
            int daysInMonth = DateTime.DaysInMonth(currentDate.Year, currentDate.Month);
            DayOfWeek firstDayOfWeek = new DateTime(currentDate.Year, currentDate.Month, 1).DayOfWeek;

            
            calendarGrid.ColumnDefinitions.Clear();
            for (int i = 0; i < 7; i++)
            {
                calendarGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            }

            
            string[] dayNames = { "Пн", "Вт", "Ср", "Чт", "Пт", "Сб", "Вс" };
            for (int i = 0; i < dayNames.Length; i++)
            {
                Button dayButton = new Button() { Content = dayNames[i], IsEnabled = false, Background = Brushes.LightGray };
                Grid.SetColumn(dayButton, (int)firstDayOfWeek + i >= 7 ? (int)firstDayOfWeek + i - 7 : (int)firstDayOfWeek + i);
                calendarGrid.Children.Add(dayButton);
            }

            
            for (int i = 1; i <= daysInMonth; i++)
            {
                Button dayButton = new Button() { Content = i.ToString(), Margin = new Thickness(5) };
                int column = (int)(new DateTime(currentDate.Year, currentDate.Month, i).DayOfWeek);
                int row = (i - 1 + (int)firstDayOfWeek) / 7 + 1;
                if (row > calendarGrid.RowDefinitions.Count)
                {
                    calendarGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
                }
                Grid.SetColumn(dayButton, column);
                Grid.SetRow(dayButton, row);
                dayButton.Click += DayButton_Click;
                calendarGrid.Children.Add(dayButton);
            }

            
            txtMonthYear.Text = currentDate.ToString("MMMM yyyy");
        }

        private void DayButton_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;
            int day = int.Parse(clickedButton.Content.ToString());
            DateTime selectedDate = new DateTime(currentDate.Year, currentDate.Month, day);

            
            ActivitySelectionWindow activityWindow = new ActivitySelectionWindow(selectedDate);
            activityWindow.ShowDialog();
        }

        private void NavigateBack_Click(object sender, RoutedEventArgs e)
        {
            currentDate = currentDate.AddMonths(-1);
            UpdateCalendar();
        }

        private void NavigateForward_Click(object sender, RoutedEventArgs e)
        {
            currentDate = currentDate.AddMonths(1);
            UpdateCalendar();
        }
    }
}