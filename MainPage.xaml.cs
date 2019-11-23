using Microsoft.QueryStringDotNET;
using NotificationsExtensions.Toasts;
using System;
using System.Collections.Generic;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TOM
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Clock clock = new Clock();
        private DispatcherTimer timer;

        public MainPage()
        {
            this.InitializeComponent();
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

            timerBox.Text = "00:00:00";
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, object e)
        {
            clock.Seconds += 1;
            if (clock.Seconds == 5)
            {
                Notify();
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
            timerBox.Text = string.Format("{0}:{1}:{2}", clock.Hours.ToString().PadLeft(2, '0'), clock.Minutes.ToString().PadLeft(2, '0'), clock.Seconds.ToString().PadLeft(2, '0'));
        }

        private void Notify()
        {
            string title = "Notification Title";
            string s_content = "Notification Content";


            ToastVisual visual = new ToastVisual()
            {
                TitleText = new ToastText() { Text = title },
                BodyTextLine1 = new ToastText() { Text = s_content }

            };

            ToastActionsCustom action = new ToastActionsCustom()
            {
                Inputs = { new ToastTextBox("txt") { PlaceholderContent = "Place Holder Content" } },

                Buttons = { new ToastButton("Reply", new QueryString() { "action", "Reply" }.ToString()) }
            };

            ToastContent content = new ToastContent() { Visual = visual, Actions = action };
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
        }
    }
}
