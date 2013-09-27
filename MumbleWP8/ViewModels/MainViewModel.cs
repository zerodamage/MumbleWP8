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
            Favorites = new ObservableCollection<Server>();
            Channels = new ObservableCollection<Channel>();
            ChatMessages = new ObservableCollection<ChatMessage>();
            EUList = new List<PublicServer>();
            OCList = new List<PublicServer>();
            SAList = new List<PublicServer>();
            NAList = new List<PublicServer>();
            ASList = new List<PublicServer>();
        }

        public List<KeyedList<string, PublicServer>> GroupedCountries(List<PublicServer> publicServers)
        {
            var groupedServers =
                from server in publicServers
                orderby server.CountryCode
                group server by server.CountryCode into serversByCountry
                select new KeyedList<string, PublicServer>(serversByCountry);

            return new List<KeyedList<string, PublicServer>>(groupedServers);
        }


        public ObservableCollection<Server> Favorites
        {
            get
            {
                ObservableCollection<Server> searchedValue;
                if (IsolatedStorageSettings.ApplicationSettings.TryGetValue<ObservableCollection<Server>>("favorites", out searchedValue))
                {
                    return searchedValue;
                }
                else
                {
                    IsolatedStorageSettings.ApplicationSettings["favorites"] = new ObservableCollection<Server>();
                    return (ObservableCollection<Server>)IsolatedStorageSettings.ApplicationSettings["favorites"];
                }
            }
            set
            {
                ObservableCollection<Server> searchedValue;
                if (IsolatedStorageSettings.ApplicationSettings.TryGetValue<ObservableCollection<Server>>("favorites", out searchedValue))
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
        public ObservableCollection<ChannelItem> ChannelFlat
        {
            get
            {
                return FlattenChannels(Channels);
            }
        }

        public ObservableCollection<ChatMessage> ChatMessages { get; set; }

        

        public List<PublicServer> EUList { get; set; }
        public List<PublicServer> OCList { get; set; }
        public List<PublicServer> SAList { get; set; }
        public List<PublicServer> NAList { get; set; }
        public List<PublicServer> ASList { get; set; }

        public List<KeyedList<string, PublicServer>> EUListGrouped { get { return GroupedCountries(EUList); } }
        public List<KeyedList<string, PublicServer>> OCListGrouped { get { return GroupedCountries(OCList); } }
        public List<KeyedList<string, PublicServer>> SAListGrouped { get { return GroupedCountries(SAList); } }
        public List<KeyedList<string, PublicServer>> NAListGrouped { get { return GroupedCountries(NAList); } }
        public List<KeyedList<string, PublicServer>> ASListGrouped { get { return GroupedCountries(ASList); } }

        ObservableCollection<ChannelItem> _chanListFlat  = new ObservableCollection<ChannelItem>();
        //Show album pictures as a tree 
        ObservableCollection<ChannelItem> FlattenChannels(ObservableCollection<Channel> channels)
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

        public double SilenceThreshold { get { return _silenceSlider > _speechSlider ? _speechSlider : _silenceSlider; } }
        public double SpeechThreshold { get { return _speechSlider; } }

        private double _speechSlider = 0.8;
        public double SpeechSlider
        {
            get
            {
                return _speechSlider;
            }
            set
            {
                if (value != _speechSlider)
                {
                    _speechSlider = value;
                    NotifyPropertyChanged("SpeechSlider");
                }
            }
        }

        private double _silenceSlider = 0.6;
        public double SilenceSlider
        {
            get
            {
                return _silenceSlider;
            }
            set
            {
                if (value != _silenceSlider)
                {
                    _silenceSlider = value;
                    NotifyPropertyChanged("SilenceSlider");
                }
            }
        }

        private bool _microphoneOn = true;
        public bool MicrophoneOn
        {
            get
            {
                return _microphoneOn;
            }
            set
            {
                if (value != _microphoneOn)
                {
                    _microphoneOn = value;
                    NotifyPropertyChanged("MicrophoneOn");
                }
            }
        }
        
        private bool _speakerOn = true;
        public bool SpeakerOn
        {
            get
            {
                return _speakerOn;
            }
            set
            {
                if (value != _speakerOn)
                {
                    _speakerOn = value;
                    NotifyPropertyChanged("SpeakerOn");
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

    public abstract class ChatMessage
    {
        public ChannelItem Selected { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }

        public string ChatString;
        public TextAlignment Alignment;
        public HorizontalAlignment Alignment2;
        public int LabelRow;
        public ChatBubbleDirection Direction;

        public ChatMessage(string text, string image, ChannelItem selected)
        {
            Text = text;
            Image = image;
            Selected = selected;
        }
    }
    public class ChatMessageReceived : ChatMessage
    {
        public new string ChatString
        {
            get
            {
                return "From " + Selected.Label + ":";
            }
        }

        public new TextAlignment Alignment { get { return TextAlignment.Left; } }
        public new HorizontalAlignment Alignment2 { get { return HorizontalAlignment.Left; } }
        public new int LabelRow { get { return 0; } }
        public new ChatBubbleDirection Direction { get { return ChatBubbleDirection.UpperLeft; } }

        public ChatMessageReceived(string text, string image, ChannelItem selection) : base(text, image, selection) { }
        public ChatMessageReceived() : base("", "", new Channel()) {}
    }
    public class ChatMessageSent : ChatMessage
    {
        public new string ChatString
        {
            get
            {
                return "To " + Selected.Label + ":";
            }
        }

        public new TextAlignment Alignment { get { return TextAlignment.Right; } }
        public new HorizontalAlignment Alignment2 { get { return HorizontalAlignment.Right; } }
        public new int LabelRow { get { return 2; } }
        public new ChatBubbleDirection Direction { get { return ChatBubbleDirection.LowerRight; } }

        public ChatMessageSent(string text, string image, ChannelItem selection) : base(text, image, selection) { }
        public ChatMessageSent() : base("", "", new Channel()) { }
    }

    public class Server
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
    public class PublicServer : Server
    {
        public string ContinentCode;
        public string CountryCode;
        public string Country;
        public string Region;
        public string URL;
    }

    public abstract class ChannelItem
    {
        public string Label { get; set; }
        public string Icon { get; set; }
        public int Level { get; set; }
        public Thickness Indent { get { return new Thickness(Level * 20, 0, 0, 0); } }
        public List<MenuItem> ContextItems { get; set; }
    }
    public class Channel : ChannelItem
    {
        public ObservableCollection<Channel> subchannels;
        public ObservableCollection<User> users;

        public Channel()
        {
            Icon = "Icons/channel.png";
            ContextItems = new List<MenuItem>() { new MenuItem() { Header = "send message..." } };
        }

        public Channel(string name, int level)
        {
            Icon = "Icons/channel.png";
            Level = level;
            Label = name;
            ContextItems = new List<MenuItem>() { new MenuItem() { Header = "send message..." } };
        }

    }
    public class User : ChannelItem
    {
        public enum States { NotTalking, Talking };

        public User()
        {

        }

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
            ContextItems = new List<MenuItem>() { new MenuItem() { Header = "send message..." }, new MenuItem() { Header = "mute user" } };
        }
    }
    public class Self : ChannelItem
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