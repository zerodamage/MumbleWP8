using MumbleWP8.Resources;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MumbleWP8.ViewModels
{
    public partial class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            Favorites = new ObservableCollection<Server>();
            Channels = new ObservableCollection<Channel>();
            ChatMessages = new ObservableCollection<ChatMessage>();

            EUListGrouped = new ObservableCollection<KeyedList<string, PublicServer>>();
            OCListGrouped = new ObservableCollection<KeyedList<string, PublicServer>>();
            SAListGrouped = new ObservableCollection<KeyedList<string, PublicServer>>();
            NAListGrouped = new ObservableCollection<KeyedList<string, PublicServer>>();
            ASListGrouped = new ObservableCollection<KeyedList<string, PublicServer>>();
        }




        /// <summary>
        /// Sample property that returns a localized string
        /// </summary>
        public string LocalizedSampleProperty
        {
            get
            {
                return AppResources.SampleProperty;
            }
        }

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        public void LoadData()
        {
            App.ViewModel.Channels = new ObservableCollection<Channel>();
            App.ViewModel.Channels.Add(new Channel("testchannel", 0));
            App.ViewModel.Channels.Add(new Channel("testchannel2", 0));

            App.ViewModel.Channels[0].subchannels = new ObservableCollection<Channel>();
            ObservableCollection<Channel> subchanneltest = App.ViewModel.Channels[0].subchannels;
            subchanneltest.Add(new Channel("subchannel1", 1));
            subchanneltest.Add(new Channel("subchannel2", 1));

            subchanneltest[0].users = new ObservableCollection<User>();
            subchanneltest[0].users.Add(new User("testuser", 2));
            subchanneltest[0].users.Add(new User("testuser2", 2));
            subchanneltest[0].users.Add(new User("testuser3", 2));
            subchanneltest[0].users.Add(new User("testuser4", 2) { state = User.States.Talking });

            App.ViewModel.ChatMessages = new ObservableCollection<ChatMessage>();
            App.ViewModel.ChatMessages.Add(new ChatMessageReceived("test msg1", null, subchanneltest[0].users[0]));
            App.ViewModel.ChatMessages.Add(new ChatMessageSent("test msg2", null, subchanneltest[0]));
            App.ViewModel.ChatMessages.Add(new ChatMessageSent("test msg3", null, App.ViewModel.Channels[0]));

            this.IsDataLoaded = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }    
}