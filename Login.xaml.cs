using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TOM
{
    public sealed partial class Login : Page
    {
        private StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
        public Login()
        {
            this.InitializeComponent();
        }

        private void HyperlinkButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(SignUp));
        }
        
        private void SignIn_Tapped(object sender, TappedRoutedEventArgs e)
        {
            SignIn();
        }

        private async void CheckPassword(StorageFile file)
        {
            string text = await Windows.Storage.FileIO.ReadTextAsync(file);
            string[] strings = text.Split(',');

            if (strings[0] == pass.Password)
            {
                
                MainPage.user = new User(strings[1]);
                this.Frame.Navigate(typeof(MainPage));
            }
            else
            {
                passwordStatus.Visibility = Visibility.Visible;
                progressRing.IsActive = false;
            }
        }

        private void pass_PasswordChanged(object sender, RoutedEventArgs e)
        {
            passwordStatus.Visibility = Visibility.Collapsed;
        }

        private void Grid_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                SignIn();
            }
        }


        private async void SignIn()
        {
            progressRing.IsActive = true;

            await Task.Delay(TimeSpan.FromSeconds(0.5));

            try
            {
                StorageFile file = await storageFolder.GetFileAsync($"{username.Text}.txt");
                MainPage.username = username.Text;
                CheckPassword(file);
            }
            catch
            {
                progressRing.IsActive = false;
            }
        }
    }
}
