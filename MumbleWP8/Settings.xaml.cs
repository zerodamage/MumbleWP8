using Microsoft.Phone.Controls;
using MumbleWP8.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace MumbleWP8
{
    public partial class Settings : PhoneApplicationPage
    {
        public Settings()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            DataContext = App.ViewModel;
            //ActivationType.SelectedIndex = App.ViewModel.ActivationTypeIndex;
            //VoiceActivationMethod.SelectedIndex = App.ViewModel.VoiceActivationMethodIndex;
        }

        private void ListPicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 1 && VoiceActivated != null && PushActivated != null && ContActivated != null)
            {
                switch(((ListPickerItem)e.AddedItems[0]).Content.ToString())
                {
                    case "Voice activation":
                        App.ViewModel.ActivationTypeIndex = 0;
                        break;
                    case "Continously":
                        App.ViewModel.ActivationTypeIndex = 1;
                        break;

                    case "Push-to-speak":
                        App.ViewModel.ActivationTypeIndex = 2;
                        break;
                }
                
                VoiceActivated.Visibility = App.ViewModel.ActivationTypeIndex == 0 ? Visibility.Visible : Visibility.Collapsed;
                ContActivated.Visibility = App.ViewModel.ActivationTypeIndex == 1 ? Visibility.Visible : Visibility.Collapsed;
                PushActivated.Visibility = App.ViewModel.ActivationTypeIndex == 2 ? Visibility.Visible : Visibility.Collapsed;
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

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Silence1.Offset = App.ViewModel.SilenceThreshold;
            Silence2.Offset = App.ViewModel.SilenceThreshold;
            
            Speech1.Offset = App.ViewModel.SpeechThreshold;
            Speech2.Offset = App.ViewModel.SpeechThreshold;
        }

        private void ListPicker2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 1 && VoiceActivated != null)
            {
                switch (((ListPickerItem)e.AddedItems[0]).Content.ToString())
                {
                    case "Signal-to-noise":
                        App.ViewModel.VoiceActivationMethodIndex = 0;
                        break;
                    case "Amplitude":
                        App.ViewModel.VoiceActivationMethodIndex = 1;
                        break;
                }
            }
        }
    }
}