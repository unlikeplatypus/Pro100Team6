using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TOM
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private ObservableCollection<Assignment> _assignments = new ObservableCollection<Assignment>();
        private ObservableCollection<Budget> _budgets = new ObservableCollection<Budget>();

        public ObservableCollection<Assignment> Assignments
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

            // Instead of hard coded items, the data could be pulled 
            // asynchronously from a database or the internet.
            Assignments.Add(new Assignment("Math Homework", "11/8/2019"));
            Assignments.Add(new Assignment("Math Homework 2", "11/8/2019"));
            Assignments.Add(new Assignment("Math Homework 3", "11/8/2019"));
            Assignments.Add(new Assignment("Math Homework 4", "11/8/2019"));
            Assignments.Add(new Assignment("Math Homework 5", "11/8/2019"));
            Assignments.Add(new Assignment("Reading Assignment", "11/9/2019"));
            Assignments.Add(new Assignment("Reading Assignment 2", "11/9/2019"));
            Assignments.Add(new Assignment("Reading Assignment 3", "11/9/2019"));
            Assignments.Add(new Assignment("Reading Assignment 4", "11/9/2019"));
            Assignments.Add(new Assignment("Reading Assignment 5", "11/9/2019"));
            Assignments.Add(new Assignment("Reading Assignment 6", "11/9/2019"));
            Assignments.Add(new Assignment("Reading Assignment 7", "11/9/2019"));
            Assignments.Add(new Assignment("Reading Assignment 8", "11/9/2019"));
            Assignments.Add(new Assignment("Reading Assignment 9", "11/9/2019"));
            Assignments.Add(new Assignment("Reading Assignment 10", "11/9/2019"));
            Assignments.Add(new Assignment("Final", "11/20/2019"));

            Budgets.Add(new Budget("Groceries", 200, 50));
            Budgets.Add(new Budget("Entertainment", 75, 20));
            Budgets.Add(new Budget("Mortgage", 750, 2000));
        }
    }
}
