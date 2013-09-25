using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using MumbleWP8.Resources;
using Coding4Fun.Toolkit.Controls;
using System.Windows;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Phone.Controls;

namespace MumbleWP8.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            Favorites = new ObservableCollection<ServerViewItem>();
            Channels = new ObservableCollection<Channel>();
            ChatMessages = new ObservableCollection<ChatMessage>();
            EUList = new List<PublicServerViewItem>();
            OCList = new List<PublicServerViewItem>();
            SAList = new List<PublicServerViewItem>();
            NAList = new List<PublicServerViewItem>();
            ASList = new List<PublicServerViewItem>();
        }

        public List<KeyedList<string, PublicServerViewItem>> GroupedCountries(List<PublicServerViewItem> list22)
        {

            var photos = list22;

            var groupedPhotos =
                from photo in photos
                orderby photo.Country_code
                group photo by photo.Country_code into photosByMonth
                select new KeyedList<string, PublicServerViewItem>(photosByMonth);

            return new List<KeyedList<string, PublicServerViewItem>>(groupedPhotos);
        }

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        public ObservableCollection<ServerViewItem> Favorites
        {
            get
            {
                ObservableCollection<ServerViewItem> searchedValue;
                if (IsolatedStorageSettings.ApplicationSettings.TryGetValue<ObservableCollection<ServerViewItem>>("favorites", out searchedValue))
                {
                    return searchedValue;
                }
                else
                {
                    IsolatedStorageSettings.ApplicationSettings["favorites"] = new ObservableCollection<ServerViewItem>();
                    return (ObservableCollection<ServerViewItem>)IsolatedStorageSettings.ApplicationSettings["favorites"];
                }
            }
            set
            {
                ObservableCollection<ServerViewItem> searchedValue;
                if (IsolatedStorageSettings.ApplicationSettings.TryGetValue<ObservableCollection<ServerViewItem>>("favorites", out searchedValue))
                {
                    IsolatedStorageSettings.ApplicationSettings["favorites"] = value;
                }
                else
                {
                    IsolatedStorageSettings.ApplicationSettings.Add("favorites", value);
                }

                IsolatedStorageSettings.ApplicationSettings.Save();
            }
        }
        
        public ObservableCollection<Channel> Channels { get; set; }

        public ObservableCollection<ChatMessage> ChatMessages { get; set; }

        public ObservableCollection<ChannelViewItem> ChannelFlat
        {
            get
            {
                return FlattenChannels(Channels);
            }
        }

        public List<PublicServerViewItem> EUList { get; set; }
        public List<PublicServerViewItem> OCList { get; set; }
        public List<PublicServerViewItem> SAList { get; set; }
        public List<PublicServerViewItem> NAList { get; set; }
        public List<PublicServerViewItem> ASList { get; set; }

        public List<KeyedList<string, PublicServerViewItem>> EUListGrouped { get { return GroupedCountries(EUList); } }
        public List<KeyedList<string, PublicServerViewItem>> OCListGrouped { get { return GroupedCountries(OCList); } }
        public List<KeyedList<string, PublicServerViewItem>> SAListGrouped { get { return GroupedCountries(SAList); } }
        public List<KeyedList<string, PublicServerViewItem>> NAListGrouped { get { return GroupedCountries(NAList); } }
        public List<KeyedList<string, PublicServerViewItem>> ASListGrouped { get { return GroupedCountries(ASList); } }

        ObservableCollection<ChannelViewItem> _chanListFlat  = new ObservableCollection<ChannelViewItem>();
        //Show album pictures as a tree 
        ObservableCollection<ChannelViewItem> FlattenChannels(ObservableCollection<Channel> channels)
        {
            
            foreach(Channel chan in channels)
            {
                _chanListFlat.Add(chan);
                if(chan.subchannels != null)
                {
                    FlattenChannels(chan.subchannels);
                }
                if (chan.users != null)
                {
                    foreach (User usr in chan.users)
                    {
                        _chanListFlat.Add(usr);
                    }
                }
            }
            return _chanListFlat;
        } 

        private string _sampleProperty = "Sample Runtime Property Value";
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding
        /// </summary>
        /// <returns></returns>
        public string SampleProperty
        {
            get
            {
                return _sampleProperty;
            }
            set
            {
                if (value != _sampleProperty)
                {
                    _sampleProperty = value;
                    NotifyPropertyChanged("SampleProperty");
                }
            }
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

        /// <summary>
        /// Creates and adds a few ItemViewModel objects into the Items collection.
        /// </summary>
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
            App.ViewModel.ChatMessages.Add(new ChatMessage("test msg1", null, subchanneltest[0].users[0], subchanneltest[0].users[1]));
            App.ViewModel.ChatMessages.Add(new ChatMessage("test msg2", null, subchanneltest[0], subchanneltest[0].users[2]));
            App.ViewModel.ChatMessages.Add(new ChatMessage("test msg3", null, App.ViewModel.Channels[0], subchanneltest[0]));

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



    public class ChatMessage
    {
        public ChannelViewItem Target { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }
        public ChannelViewItem Orig { get; set; }

        public string ChatString
        {
            get
            {
                //Outgoing message
                if (Orig.GetType().Equals(typeof(MumbleWP8.ViewModels.Self)))
                {
                    return "To " + Target.Label + ":";
                }
                else
                {
                    return "From " + Orig.Label + ":";
                }
                
                
            }
        }

        public TextAlignment Alignment
        {
            get
            {
                return Orig.GetType().Equals(typeof(MumbleWP8.ViewModels.Self)) ? TextAlignment.Left : TextAlignment.Right;
            }
        }

        public HorizontalAlignment Alignment2
        {
            get
            {
                return Orig.GetType().Equals(typeof(MumbleWP8.ViewModels.Self)) ? HorizontalAlignment.Right : HorizontalAlignment.Left;
            }
        }

        public int LabelRow
        {
            get
            {
                return Orig.GetType().Equals(typeof(MumbleWP8.ViewModels.Self)) ? 2 : 0;
            }
        }

        public ChatBubbleDirection Direction
        {
            get
            {
                return Orig.GetType().Equals(typeof(MumbleWP8.ViewModels.Self)) ? ChatBubbleDirection.LowerRight : ChatBubbleDirection.UpperLeft ;
            }
        }

        public ChatMessage(string text, string image, ChannelViewItem target, ChannelViewItem org)
        {
            Text = text;
            Image = image;
            Orig = org;
            Target = target;
        }
    }

    public class ServerViewItem
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public int Ping { get; set; }
        public int MaxUsers { get; set; }
        public int CurrentUsers { get; set; }
        public string Users
        {
            get { return string.Concat(CurrentUsers, "/", MaxUsers); }
        }
    }

    public class PublicServerViewItem : ServerViewItem
    {
        public string Continent_code;
        public string Country_code;
        public string Country;
        public string Region;
        public string URL;
    }

    public abstract class ChannelViewItem
    {
        public string Label { get; set; }
        public string Icon { get; set; }
        public int Level { get; set; }
        public Thickness Indent { get { return new Thickness(Level * 20, 0, 0, 0); } }
        public List<MenuItem> ContextItems { get; set; }
    }

    public class Channel : ChannelViewItem
    {
        public ObservableCollection<Channel> subchannels;
        public ObservableCollection<User> users;
        

        public Channel(string name, int level)
        {
            Icon = "Icons/channel.png";
            Level = level;
            Label = name;
            ContextItems = new List<MenuItem>() { new MenuItem() { Header = "send message..." } };
        }

    }

    public class User : ChannelViewItem
    {
        public enum States { NotTalking, Talking };

        

        public States state;
        public new string Icon
        {
            get
            {
                switch (state)
                {
                    case States.Talking:
                        return "Icons/talking_on.png";
                    case States.NotTalking:
                        return "Icons/talking_off.png";
                    default:
                        return "";
                }
            }
        }

        public User(string name, int level)
        {
            Level = level;
            Label = name;
            MenuItem sndmsg = new MenuItem() { Header = "send message..." };
            ContextItems = new List<MenuItem>() { sndmsg, new MenuItem() { Header = "mute user" } };
        }
    }

    public class Self : ChannelViewItem
    {
        public enum States { NotTalking, Talking };
        public States state;
        public new string Icon
        {
            get
            {
                switch (state)
                {
                    case States.Talking:
                        return "Icons/talking_on.png";
                    case States.NotTalking:
                        return "Icons/talking_off.png";
                    default:
                        return "";
                }
            }
        }

        public Self(string name, int level)
        {
            Level = level;
            Label = name;
            MenuItem sndmsg = new MenuItem() { Header = "send message..." };
            ContextItems = new List<MenuItem>() { sndmsg, new MenuItem() { Header = "mute user" } };
        }
    }
}