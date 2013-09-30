using Microsoft.Phone.Controls;
using MumbleWP8.ViewModels;
using System;
using System.Windows;
using System.Windows.Navigation;

namespace MumbleWP8
{
    public partial class EditServer : PhoneApplicationPage
    {
        bool editing;
        Server item = new Server();

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