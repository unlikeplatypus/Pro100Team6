using System;
using System.Collections.Generic;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TOM
{
    public sealed partial class Login : Page
    {
        public Login()
        {
            this.InitializeComponent();
        }

        private void HyperlinkButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(SignUp));
        }
        
        private async void SignIn_Tapped(object sender, TappedRoutedEventArgs e)
        {
            try
            {
                StorageFile file = await storageFolder.GetFileAsync($"{username.Text}.txt");
                CheckPassword(file);
            }
            catch
            {
            }
        }

        private async void CheckPassword(StorageFile file)
        {
            string text = await Windows.Storage.FileIO.ReadTextAsync(file);
            string[] strings = text.Split(',');

            if (strings[0] == pass.Password)
            {
                this.Frame.Navigate(typeof(MainPage), strings[1]);
            }
        }
    }
}
