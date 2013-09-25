using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Input;
using System.IO.IsolatedStorage;
using MumbleWP8.ViewModels;

namespace MumbleWP8
{
    public partial class EditServer : PhoneApplicationPage
    {
        bool editing;


        ServerViewItem item = new ServerViewItem();

        public EditServer()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!editing)
            {
                App.ViewModel.Favorites.Add(item);
            }
            NavigationService.Navigate(new Uri("/Servers.xaml?edit=true", UriKind.Relative));
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            string indexo;
            if (NavigationContext.QueryString.TryGetValue("existingindex", out indexo))
            {
                item = App.ViewModel.Favorites[int.Parse(indexo)];
                TitleText.Text = "Edit server";
                editing = true;
            }
            else
            {
                TitleText.Text = "Add server";
                editing = false;
            }

            ContentPanel.DataContext = item;
        }

    }
}