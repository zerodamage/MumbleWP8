using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using MumbleWP8.ViewModels;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Navigation;

namespace MumbleWP8
{
    public partial class Servers : PhoneApplicationPage
    {
        ApplicationBarIconButton favAddButton;

        public Servers()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
           
            string edited;
            if (NavigationContext.QueryString.TryGetValue("edit", out edited))
            {
                NavigationService.RemoveBackEntry();
                NavigationService.RemoveBackEntry();
            }

            ApplicationBar = new ApplicationBar();

            favAddButton = new ApplicationBarIconButton();
            favAddButton.IconUri = new Uri("/Images/favs.addto.png", UriKind.Relative);
            favAddButton.Text = "add favorite";
            ApplicationBar.Buttons.Add(favAddButton);
            favAddButton.Click += new EventHandler(FavAddBarIconButton_Click);

            ApplicationBarMenuItem settingsMenuItem = new ApplicationBarMenuItem();
            settingsMenuItem.Text = "settings...";
            ApplicationBar.MenuItems.Add(settingsMenuItem);
            settingsMenuItem.Click += new EventHandler(SettingsBarMenuItem_Click);

            App.ViewModel.LoadPublicServers();
            DataContext = App.ViewModel;
        }

        private void ServerDelete(object sender, RoutedEventArgs e)
        {
            Server selectedServer = (sender as MenuItem).DataContext as Server;
            App.ViewModel.Favorites.Remove(selectedServer);
        }

        private void ServerEdit(object sender, RoutedEventArgs e)
        {
            Server selectedServer = (sender as MenuItem).DataContext as Server;

            NavigationService.Navigate(new Uri("/EditServer.xaml?existingindex=" + App.ViewModel.Favorites.IndexOf(selectedServer), UriKind.Relative));
        }

        private void FavList_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Server item = ((FrameworkElement)e.OriginalSource).DataContext as Server;
            if(item != null)
            {
                //TODO: Connect to server here
                NavigationService.Navigate(new Uri("/ConnectedPage.xaml", UriKind.Relative));
            }
        }

        private void FavAddBarIconButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/EditServer.xaml", UriKind.Relative));
        }

        private void SettingsBarMenuItem_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Settings.xaml", UriKind.Relative));
        }


        private void Pivot_LoadingPivotItem(object sender, PivotItemEventArgs e)
        {
            if (e.Item == favsPivotItem)
            {
                //Favorites tabs
                favAddButton.IsEnabled = true;
            }
            else
            {
                //Public tabs
                favAddButton.IsEnabled = false;
            }
        }
    }
}