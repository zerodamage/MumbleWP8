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

            DataContext = App.ViewModel;
        }



        private void SliderChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
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
            if (e.AddedItems.Count == 1 && VoiceActivated != null && PushActivated != null && ContActivated != null)
            {
                App.ViewModel.ActivationType = (MumbleWP8.ViewModels.MainViewModel.Activation)e.AddedItems[0];

                VoiceActivated.Visibility = (App.ViewModel.ActivationType == MumbleWP8.ViewModels.MainViewModel.Activation.voice) ? Visibility.Visible : Visibility.Collapsed; 
                PushActivated.Visibility = (App.ViewModel.ActivationType == MumbleWP8.ViewModels.MainViewModel.Activation.Pushtospeak) ? Visibility.Visible : Visibility.Collapsed;
                ContActivated.Visibility = (App.ViewModel.ActivationType == MumbleWP8.ViewModels.MainViewModel.Activation.continous) ? Visibility.Visible : Visibility.Collapsed;
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