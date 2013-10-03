using Microsoft.Phone.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MumbleWP8.ViewModels
{
    public partial class MainViewModel
    {
        public ObservableCollection<Channel> Channels { get; set; }
        public ObservableCollection<ChannelItem> ChannelFlat
        {
            get
            {
                return FlattenChannels(Channels);
            }
        }

        ObservableCollection<ChannelItem> _chanListFlat = new ObservableCollection<ChannelItem>();
        //Show album pictures as a tree 
        ObservableCollection<ChannelItem> FlattenChannels(ObservableCollection<Channel> channels)
        {
            foreach (Channel chan in channels)
            {
                _chanListFlat.Add(chan);
                if (chan.subchannels != null)
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
    }

    public abstract class ChannelItem : INotifyPropertyChanged
    {
        public string Label { get; set; }
        public string Icon { get; set; }
        public int Level { get; set; }
        public Thickness Indent { get { return new Thickness(Level * 20, 0, 0, 0); } }
        public List<MenuItem> ContextItems { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public class Channel : ChannelItem
    {
        public ObservableCollection<Channel> subchannels;
        public ObservableCollection<User> users;

        public Channel()
        {
            Icon = "MumbleIcons/channel.png";
            ContextItems = new List<MenuItem>() { new MenuItem() { Header = "send message..." } };
        }

        public Channel(string name, int level)
        {
            Icon = "MumbleIcons/channel.png";
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
                        return "MumbleIcons/talking_on.png";
                    case States.NotTalking:
                        return "MumbleIcons/talking_off.png";
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
        private States _state;
        public States State { get { return _state; } set { _state = value; NotifyPropertyChanged("State"); NotifyPropertyChanged("Icon"); } } 
        public new string Icon
        {
            get
            {
                switch (_state)
                {
                    case States.Talking:
                        return "MumbleIcons/talking_on.png";
                    case States.NotTalking:
                        return "MumbleIcons/talking_off.png";
                    default:
                        return "";
                }
            }
        }

        public Self(string name, int level)
        {
            Level = level;
            Label = name;
            ContextItems = new List<MenuItem>() { new MenuItem() { Header = "send message..." }, new MenuItem() { Header = "mute user" } };
        }
    }
}
