using Coding4Fun.Toolkit.Controls;
using System.Collections.ObjectModel;
using System.Windows;

namespace MumbleWP8.ViewModels
{
    public partial class MainViewModel
    {
        public ObservableCollection<ChatMessage> ChatMessages { get; set; }
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
        public ChatMessageReceived() : base("", "", new Channel()) { }
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
    
}
