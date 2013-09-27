using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                }
            }
        }

        public enum Activation { Pushtospeak, Voiceactivated, Continous }

        public IEnumerable<Activation> ActivationTypes { get { return Enum.GetValues(typeof(Activation)).Cast<Activation>(); } }

        public Activation ActivationType
        {
            get
            {
                return GetSetting<Activation>("ActivationType");
            }
            set
            {
                if (value != GetSetting<Activation>("ActivationType"))
                {
                    SetSetting<int>("ActivationType", value);
                    NotifyPropertyChanged("ActivationType");
                }
            }
        }

        public int VoiceActivationMethod
        {
            get
            {
                return GetSetting<int>("VoiceActivationMethod");
            }
            set
            {
                if (value != GetSetting<int>("VoiceActivationMethod"))
                {
                    SetSetting<int>("VoiceActivationMethod", value);
                    NotifyPropertyChanged("SilenceSlider");
                }
            }
        }

        public T GetSetting<T>(string setting) where T : new()
        {
            T searchedValue;
            if (IsolatedStorageSettings.ApplicationSettings.TryGetValue<T>(setting, out searchedValue))
            {
                return searchedValue;
            }
            else
            {
                IsolatedStorageSettings.ApplicationSettings[setting] = new T();
                return (T)IsolatedStorageSettings.ApplicationSettings[setting];
            }
        }
        public void SetSetting<T>(string setting, dynamic value) where T : new()
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
