﻿using System;
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
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SignUp : Page
    {
        private StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
        public SignUp()
        {
            this.InitializeComponent();
        }

        private void HyperlinkButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Login));
        }
        
        
        private void SignIn_Tapped(object sender, TappedRoutedEventArgs e)
        {
            SignIn();
        }

        private bool CheckValidPassword()
        {
            bool passIsValid = false;
            if (pass.Password == confirm.Password)
            {
                passIsValid = true;
            }
            foreach (Char c in pass.Password)
            {
                if (c == ',')
                {
                    passIsValid = false;
                }
            }
            return passIsValid;
        }
        private bool CheckForEmpties()
        {
            if (username.Text.Trim() == "" || pass.Password.Trim() == "" || token.Text.Trim() == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private async void SaveInfo()
        {
            string name = username.Text;
            string password = pass.Password;
            string toke = token.Text;

            StorageFile settingsFile = await storageFolder.CreateFileAsync($"{name}.txt", Windows.Storage.CreationCollisionOption.FailIfExists);

            File.AppendAllText(settingsFile.Path, $"{password},{toke}");
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

            if (CheckForEmpties() && CheckValidPassword())
            {
                try
                {
                    StorageFile file = await storageFolder.GetFileAsync($"{username.Text}.txt");

                    passwordStatus.Visibility = Visibility.Visible;
                    progressRing.IsActive = false;

                }
                catch
                {
                    SaveInfo();
                    MainPage.username = username.Text;
                    MainPage.user = new User(token.Text);
                    this.Frame.Navigate(typeof(MainPage));

                }
            }
        }

        private void username_TextChanged(object sender, TextChangedEventArgs e)
        {
            passwordStatus.Visibility = Visibility.Collapsed;
        }
    }
}
