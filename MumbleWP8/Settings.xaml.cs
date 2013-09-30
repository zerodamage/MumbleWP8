using Microsoft.Phone.Controls;
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

            DataContext = App.ViewModel;
        }




        private void ListPicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 1 && VoiceActivated != null && PushActivated != null && ContActivated != null)
            {
                App.ViewModel.ActivationType = (string)e.AddedItems[0];

                VoiceActivated.Visibility = App.ViewModel.ActivationType.Equals("Voice activation") ? Visibility.Visible : Visibility.Collapsed;
                PushActivated.Visibility = App.ViewModel.ActivationType.Equals("Push-to-talk") ? Visibility.Visible : Visibility.Collapsed;
                ContActivated.Visibility = App.ViewModel.ActivationType.Equals("Continously") ? Visibility.Visible : Visibility.Collapsed;
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
            if (e.AddedItems.Count == 1)
            {
                App.ViewModel.VoiceActivationMethod = (string)e.AddedItems[0];
            }
        }
    }
}