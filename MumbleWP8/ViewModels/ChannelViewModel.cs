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
}
