using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Xml.Linq;
using System.IO.IsolatedStorage;
using MumbleWP8.ViewModels;
using System.Threading;

namespace MumbleWP8
{
    public partial class Servers : PhoneApplicationPage
    {

        public Servers()
        {
            InitializeComponent();
        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/EditServer.xaml", UriKind.Relative));
        }

        private void ApplicationBarIconButton_Click_2(object sender, EventArgs e)
        {

        }

        ApplicationBarIconButton button1;

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

            button1 = new ApplicationBarIconButton();
            button1.IconUri = new Uri("/Images/favs.addto.png", UriKind.Relative);
            button1.Text = "connect";
            ApplicationBar.Buttons.Add(button1);
            button1.Click += new EventHandler(ApplicationBarIconButton_Click);

            ApplicationBarMenuItem menuItem1 = new ApplicationBarMenuItem();
            menuItem1.Text = "settings...";
            ApplicationBar.MenuItems.Add(menuItem1);
            menuItem1.Click += new EventHandler(ApplicationBarMenuItem_Click);

            App.ViewModel.LoadPublicServers();
            Thread.Sleep(1000);
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

        private void favList_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Server item = ((FrameworkElement)e.OriginalSource).DataContext as Server;
            if(item != null)
            {
                //TODO: Connect to server here
                NavigationService.Navigate(new Uri("/ConnectedPage.xaml", UriKind.Relative));
            }
        }

        private void ApplicationBarMenuItem_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Settings.xaml", UriKind.Relative));
        }


        private void Pivot_LoadingPivotItem_1(object sender, PivotItemEventArgs e)
        {
            if (e.Item == pivotItem1)
            {
                button1.IsEnabled = true;
            }
            else
            {
                button1.IsEnabled = false;
                //Public tabs
            }
        }
    }
}