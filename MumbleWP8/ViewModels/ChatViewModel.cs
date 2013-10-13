using Coding4Fun.Toolkit.Controls;
using System.Collections.ObjectModel;
using System.Windows;

namespace MumbleWP8.ViewModels
{
    public partial class MainViewModel
    {
        public ObservableCollection<ChatMessage> ChatMessages { get; set; }
    }    
}
