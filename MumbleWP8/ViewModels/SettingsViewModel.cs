using System.Collections.Generic;
using System.IO.IsolatedStorage;

namespace MumbleWP8.ViewModels
{
    partial class MainViewModel
    {
        public double SilenceThreshold { get { return SilenceSlider > 1.0 - SpeechSlider ? 1.0 - SpeechSlider : SilenceSlider; } }
        public double SpeechThreshold { get { return 1.0 - SpeechSlider; } }

        public double SpeechSlider
        {
            get
            {
                return GetSetting<double>("speechslider");
            }
            set
            {
                if (value != GetSetting<double>("speechslider"))
                {
                    SetSetting<double>("speechslider", value);
                    
                    NotifyPropertyChanged("SpeechSlider");
                    NotifyPropertyChanged("SilenceThreshold");
                    NotifyPropertyChanged("SpeechThreshold");
                }
            }
        }

        public double SilenceSlider
        {
            get
            {
                return GetSetting<double>("silenceslider");
            }
            set
            {
                if (value != GetSetting<double>("silenceslider"))
                {
                    SetSetting<double>("silenceslider", value);
                    NotifyPropertyChanged("SilenceSlider");
                    NotifyPropertyChanged("SilenceThreshold");
                    NotifyPropertyChanged("SpeechThreshold");
                }
            }
        }

        public List<string> ActivationTypes { get { return new List<string>() { "Voice activation", "Continously", "Push-to-talk" }; } }

        public string ActivationType
        {
            get
            {
                return GetSetting<string>("ActivationType");
            }
            set
            {
                if (value != GetSetting<string>("ActivationType"))
                {
                    SetSetting<string>("ActivationType", value);
                    NotifyPropertyChanged("ActivationType");
                }
            }
        }

        public List<string> VoiceActivationMethods { get { return new List<string>() { "Signal-to-noise", "Amplitude" }; } }

        public string VoiceActivationMethod
        {
            get
            {
                return GetSetting<string>("VoiceActivationMethod");
            }
            set
            {
                if (value != GetSetting<string>("VoiceActivationMethod"))
                {
                    SetSetting<int>("VoiceActivationMethod", value);
                    NotifyPropertyChanged("VoiceActivationMethod");
                }
            }
        }

        public T GetSetting<T>(string setting)
        {
            T searchedValue;
            if (IsolatedStorageSettings.ApplicationSettings.TryGetValue<T>(setting, out searchedValue))
            {
                return searchedValue;
            }
            else
            {
                IsolatedStorageSettings.ApplicationSettings[setting] = default(T);
                return (T)IsolatedStorageSettings.ApplicationSettings[setting];
            }
        }
        public void SetSetting<T>(string setting, dynamic value)
        {
            T searchedValue;
            if (IsolatedStorageSettings.ApplicationSettings.TryGetValue<T>(setting, out searchedValue))
            {
                IsolatedStorageSettings.ApplicationSettings[setting] = value;
            }
            else
            {
                IsolatedStorageSettings.ApplicationSettings.Add(setting, value);
            }

            IsolatedStorageSettings.ApplicationSettings.Save();
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
    }
}
