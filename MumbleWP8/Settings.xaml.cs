using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace MumbleWP8
{
    

    public partial class Settings : PhoneApplicationPage
    {
        public Settings()
        {
            InitializeComponent();
        }

        double SilenceThreshold;
        double SilenceThresReal;
        double SpeechThreshold;

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SilenceThresReal = (e.NewValue) / 100;
            UpdateSlider();
        }

        private void UpdateSlider()
        {
            if (Silence1 != null && Silence2 != null && Speech1 != null && Speech2 != null)
            {
                if (SilenceThresReal > SpeechThreshold)
                {
                    SilenceThreshold = SpeechThreshold;
                }
                else
                {
                    SilenceThreshold = SilenceThresReal;
                }

                Silence1.Offset = SilenceThreshold;
                Silence2.Offset = SilenceThreshold;
                Speech1.Offset = SpeechThreshold;
                Speech2.Offset = SpeechThreshold;
            }
        }
        
        private void Slider_ValueChanged_1(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SpeechThreshold = (100 - e.NewValue) / 100;
            UpdateSlider();
        }

        private void ListPicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (VoiceActivated != null && e.AddedItems.Count == 1)
            {
                VoiceActivated.Visibility = ((e.AddedItems[0] as ListPickerItem).Content.ToString() == "Voice activated") ? Visibility.Visible : Visibility.Collapsed;
            }
            if (PushActivated != null && e.AddedItems.Count == 1)
            {
                PushActivated.Visibility = ((e.AddedItems[0] as ListPickerItem).Content.ToString() == "Push-to-talk") ? Visibility.Visible : Visibility.Collapsed;
            }
            if (ContActivated != null && e.AddedItems.Count == 1)
            {
                ContActivated.Visibility = ((e.AddedItems[0] as ListPickerItem).Content.ToString() == "Continuous") ? Visibility.Visible : Visibility.Collapsed;
            }

            
            
            //.AddedItems[0] == "Push-to-talk"
        }

        private void ToggleSwitch_Checked(object sender, RoutedEventArgs e)
        {
            Sidetone.Visibility = System.Windows.Visibility.Visible;
        }

        private void ToggleSwitch_Unchecked(object sender, RoutedEventArgs e)
        {
            Sidetone.Visibility = System.Windows.Visibility.Collapsed;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Certificates.xaml", UriKind.Relative));
        }
    }
}