﻿using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Common.Configuration;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace _8tracksWin.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Shell : Page
    {
        public static Frame ContentFrame { get; private set; }

        public Shell(Frame frame)
        {
            this.InitializeComponent();
            this.shellView.Content = frame;
            ContentFrame = frame;

            if (GlobalConfigs.CurrentUser != null)
            {
                btnSignIn.IsChecked = true;
                GlobalConfigs.CurrentUser.RefreshData();
                userDetailsPanel.DataContext = new ViewModel.UserDetailsViewModel(GlobalConfigs.CurrentUser);
                userDetailsPanel.Visibility = Visibility.Visible;
            }
            else
                btnSignIn.IsChecked = false;
            
            GlobalConfigs.LoggedInUserExists += SignedInUserStatusChangeHandler;
        }

        private void btnHamburger_Click(object sender, RoutedEventArgs e)
        {
            shellView.IsPaneOpen = !shellView.IsPaneOpen;
        }

        private void SignedInUserStatusChangeHandler(object sender, bool signedInUserExists)
        {
            if(signedInUserExists)
            {
                btnSignIn.Visibility = Visibility.Collapsed;
                userDetailsPanel.DataContext = new ViewModel.UserDetailsViewModel(GlobalConfigs.CurrentUser);
                userDetailsPanel.Visibility = Visibility.Visible;
            }
            else
            {
                btnSignIn.Visibility = Visibility.Visible;
            }
        }

        private void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            shellView.IsPaneOpen = false;
            ContentFrame.Navigate(typeof(LoginPage));
        }
    }
}
