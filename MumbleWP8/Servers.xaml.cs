﻿using System;
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

namespace MumbleWP8
{
    public partial class Servers : PhoneApplicationPage
    {
        

        public Servers()
        {

            InitializeComponent();

            WebClient wc = new WebClient();
            wc.DownloadStringCompleted += HttpCompleted;
            wc.DownloadStringAsync(new Uri("http://mumble.info/list2.cgi"));

            DataContext = App.ViewModel;
        }

        private void HttpCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                XDocument xdoc = XDocument.Parse(e.Result, LoadOptions.None);

                List<XElement> ost = xdoc.Root.Elements().ToList();

                List<PublicServerViewItem> vList = (from XElement xele in xdoc.Root.Elements()
                         select new PublicServerViewItem
                         {
                             Name = xele.Attribute("name").Value.Trim(" ".ToCharArray()),
                             Port = int.Parse(xele.Attribute("port").Value),
                             Continent_code = xele.Attribute("continent_code") == null ? null : xele.Attribute("continent_code").Value,
                             Country_code = xele.Attribute("country_code") == null ? null : xele.Attribute("country_code").Value,
                             Address = xele.Attribute("ip").Value.ToString()
                         }).ToList();

                App.ViewModel.EUList = vList.FindAll(test => test.Continent_code == "EU").ToList();
                App.ViewModel.OCList = vList.FindAll(test => test.Continent_code == "OC").ToList();
                App.ViewModel.SAList = vList.FindAll(test => test.Continent_code == "SA").ToList();
                App.ViewModel.NAList = vList.FindAll(test => test.Continent_code == "NA").ToList();
                App.ViewModel.ASList = vList.FindAll(test => test.Continent_code == "AS").ToList();
            }

            DataContext = null;
            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;
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
        }

        private void ServerDelete(object sender, RoutedEventArgs e)
        {
            ServerViewItem selectedServer = (sender as MenuItem).DataContext as ServerViewItem;
            App.ViewModel.Favorites.Remove(selectedServer);
        }

        private void ServerEdit(object sender, RoutedEventArgs e)
        {
            ServerViewItem selectedServer = (sender as MenuItem).DataContext as ServerViewItem;

            NavigationService.Navigate(new Uri("/EditServer.xaml?existingindex=" + App.ViewModel.Favorites.IndexOf(selectedServer), UriKind.Relative));
        }

        private void favList_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            ServerViewItem item = ((FrameworkElement)e.OriginalSource).DataContext as ServerViewItem;
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