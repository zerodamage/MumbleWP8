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

            preview.DataContext = App.ViewModel;
        }



        private void Slider_SlienceThres(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            App.ViewModel.SilenceSlider = (e.NewValue) / 100;
            UpdatePreview();

        }
        
        private void Slider_SpeechThres(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            App.ViewModel.SpeechSlider = (100 - e.NewValue) / 100;
            UpdatePreview();
        }

        private void UpdatePreview()
        {
            double ost = App.ViewModel.SpeechThreshold;
            double ost2 = App.ViewModel.SilenceThreshold;
            if (preview != null)
            {
                Silence1.Offset = ost2;
                Silence2.Offset = ost2;
                Speech1.Offset = ost;
                Speech2.Offset = ost;
            }
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
        }

        private void ToggleSwitch_Checked(object sender, RoutedEventArgs e)
        {
            Sidetone.Visibility = System.Windows.Visibility.Visible;
        }

        private void ToggleSwitch_Unchecked(object sender, RoutedEventArgs e)
        {
            Sidetone.Visibility = System.Windows.Visibility.Collapsed;

        }

        private void Button_Certificates_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Certificates.xaml", UriKind.Relative));
        }
    }
}