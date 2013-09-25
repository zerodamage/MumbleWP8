using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Xna.Framework.Media;
using System.Windows.Media;
using MumbleWP8.ViewModels;
using System.Windows.Input;
using System.Threading;


namespace MumbleWP8
{
    public partial class ConnectedPage : PhoneApplicationPage
    {
        // Constructor
        public ConnectedPage()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;
        }


        // Load data for the ViewModel Items
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
            foreach (ChannelViewItem usr in App.ViewModel.ChannelFlat)
            {
                usr.ContextItems[0].Click += sndmsg_Tap;
            }
            chatTarget.DataContext = App.ViewModel.ChannelFlat[0];
        }

        private void sndmsg_Tap(object sender, RoutedEventArgs e)
        {
            ChannelViewItem item = (sender as MenuItem).DataContext as ChannelViewItem;
            if (item.GetType().Equals(typeof(User)))
            {
                chatTarget.DataContext = null;
                chatTarget.DataContext = item;
            }
            MainView.SelectedIndex = 1;
        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            //Server browser
            NavigationService.Navigate(new Uri("/Servers.xaml", UriKind.Relative));
        }

        private void ApplicationBarIconButton_Click_2(object sender, EventArgs e)
        {
            //TODO: mute mic here
        }

        private void ApplicationBarIconButton_Click_3(object sender, EventArgs e)
        {
            //TODO: mute speakers here
        }

        private void ApplicationBarMenuItem_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Settings.xaml", UriKind.Relative));
        }

        private void TextBox_TextInputUpdate(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            MessageBox.Show("text done");
        }

        private void TextBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(e.Key.Equals(Key.Enter))
            {
                //setting the focus to different control
                ChatList.Focus();
                App.ViewModel.ChatMessages.Add(new ChatMessage(chatbox1.Text, null, chatTarget.DataContext as ChannelViewItem, new Self("ost", 1)));
                chatbox1.Text = "";
                ChatList.ScrollTo(App.ViewModel.ChatMessages.Last());
            }
        }

        private void MainView_LoadedPivotItem(object sender, PivotItemEventArgs e)
        {
            if (e.Item == ChatPivot)
            {
                chatbox1.Text = "Type message";
                chatbox1.SelectAll();
            }
        }
    }
}