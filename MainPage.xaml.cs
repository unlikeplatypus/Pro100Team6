using Microsoft.QueryStringDotNET;
using NotificationsExtensions.Toasts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TimerPractice.Model;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace TOM
{
    public sealed partial class MainPage : Page
    {
        private ObservableCollection<Assignment> _assignments = new ObservableCollection<Assignment>();
        private ObservableCollection<Budget> _budgets = new ObservableCollection<Budget>();
        static public User user = new User("1~EqBFIQ0fbgKL42cifKmZakSozFFgIVrXgBq57cuzB3jQtK1RomHcZDJ37Pc8Nspx");
        private string courseOutput = "";
        private string assignmentOutput = "";
        private Clock clock = new Clock();
        private DispatcherTimer timer;

        public MainPage()
        {
            this.InitializeComponent();
            ClockSetUp();
        }

        private ObservableCollection<Assignment> Assignments
        {
            get { return this._assignments; }
        }

        public ObservableCollection<Budget> Budgets
        {
            get { return this._budgets; }
        }

        // This method should be defined within your main page class.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            SetAssignments();

            Budgets.Add(new Budget("Groceries", 200, 50));
            Budgets.Add(new Budget("Entertainment", 75, 20));
            Budgets.Add(new Budget("Mortgage", 750, 2000));
        }

        public int BudgetMaxTotal()
        {
            int max = 0;
            foreach (Budget budget in Budgets)
            {
                max += budget.Max;
            }

            return max;
        }

        public int BudgetUsedTotal()
        {
            int amount = 0;
            foreach (Budget budget in Budgets)
            {
                amount += budget.Current;
            }

            return amount;
        }

        public void Idk()
        {
            Course[] courseTest = user.Courses;

            courseOutput = courseTest[0].Name;

            foreach (Assignment assgn in courseTest[0].Assignments)
            {
                assignmentOutput += assgn.Name + "\n";
            }
        }

        #region PFM, do not touch
        public void SetAssignments()
        {
            Course[] courses = user.Courses;
            Assignment[] newArray;

            foreach (Course course in courses)
                for (int i = 0; i < course.Assignments.Length; i++)
                {
                    Assignments.Add(course.Assignments[i]);
                }

            newArray = SortAssignments(Assignments.ToArray<Assignment>());

            Assignments.Clear();

            for (int i = 0; i < newArray.Length; i++)
            {
                Assignments.Add(newArray[i]);
            }
        }

        private Assignment[] SortAssignments(Assignment[] asgns)
        {
            List<Assignment> sorter = new List<Assignment>();
            Assignment[] newArray = new Assignment[asgns.Length];

            foreach (Assignment asgn in asgns)
            {
                sorter.Add(asgn);
            }

            sorter.Sort((x, y) => DateTime.Compare(x.Due_at, y.Due_at));

            for (int i = 0; i < asgns.Length; i++)
            {
                newArray[i] = sorter[i];
            }

            return newArray;
        }
        #endregion

        private void ClockSetUp()
        {
            Binding seconds = new Binding();
            seconds.Source = clock;
            seconds.Path = new PropertyPath("Seconds");
            seconds.Mode = BindingMode.OneWay;
            timerBox.SetBinding(TextBlock.TextProperty, seconds);


            Binding minutes = new Binding();
            minutes.Source = clock;
            minutes.Path = new PropertyPath("Minutes");
            minutes.Mode = BindingMode.OneWay;
            timerBox.SetBinding(TextBlock.TextProperty, minutes);


            Binding hours = new Binding();
            hours.Source = clock;
            hours.Path = new PropertyPath("Hours");
            hours.Mode = BindingMode.OneWay;
            timerBox.SetBinding(TextBlock.TextProperty, hours);

            timerBox.FontSize = 40;
            timerBox.Text = "00:00:00";
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
        }
        private void Timer_Tick(object sender, object e)
        {
            clock.Seconds += 1;
            if (clock.Seconds == 10)
            {
                Notify($"You have spent 30 Minutes working. Take a Break 😴😴😴");
            }
            if (clock.Seconds == 60)
            {
                clock.Minutes += 1;
                clock.Seconds = 0;
            }
            if (clock.Minutes == 60)
            {
                clock.Hours += 1;
                clock.Minutes = 0;
            }
            timerBox.FontSize = 40;
            timerBox.Text = string.Format("{0}:{1}:{2}", clock.Hours.ToString().PadLeft(2, '0'), clock.Minutes.ToString().PadLeft(2, '0'), clock.Seconds.ToString().PadLeft(2, '0'));
        }
        private void Notify(String s)
        {
            string title = "TOM Notification";
            string s_content = s;

            ToastVisual visual = new ToastVisual()
            {
                TitleText = new ToastText() { Text = title },
                BodyTextLine1 = new ToastText() { Text = s_content }
            };
            ToastContent content = new ToastContent() { Visual = visual };
            ToastNotification notification = new ToastNotification(content.GetXml());
            ToastNotificationManager.CreateToastNotifier().Show(notification);
        }
        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            timer.Start();
        }

        private void stopButton_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            Notify($"{clock.Hours} hours, {clock.Minutes} minutes, and {clock.Seconds} seconds has been spent working on Homework ✍✍✍");
        }

        private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var url = new Uri(((sender as Grid).DataContext as Assignment).Html_url);
            LaunchSite(url);
        }
        private async void LaunchSite(Uri uri)
        {
            await Windows.System.Launcher.LaunchUriAsync(uri);
        }

        private void resetButton_Click(object sender, RoutedEventArgs e)
        {
            clock.Seconds = 0;
            clock.Minutes = 0;
            clock.Hours = 0;
            timerBox.Text = string.Format("{0}:{1}:{2}", clock.Hours.ToString().PadLeft(2, '0'), clock.Minutes.ToString().PadLeft(2, '0'), clock.Seconds.ToString().PadLeft(2, '0'));
        }
    }
}