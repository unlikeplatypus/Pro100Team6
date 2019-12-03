using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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

        public MainPage()
        {
            this.InitializeComponent();
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

        private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var uri = new Uri(((sender as Grid).DataContext as Assignment).Html_url);
            LaunchSite(uri);
        }
        private async void LaunchSite(Uri uri)
        {
            await Windows.System.Launcher.LaunchUriAsync(uri);
        }
    }
}