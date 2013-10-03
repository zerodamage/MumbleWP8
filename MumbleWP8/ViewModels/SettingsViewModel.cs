using System.Collections.Generic;
using System.IO.IsolatedStorage;

namespace MumbleWP8.ViewModels
{
    partial class MainViewModel
    {
        public double SilenceThreshold { get { return SilenceSlider > 1.0 - SpeechSlider ? 1.0 - SpeechSlider : SilenceSlider; } }
        public double SilenceSlider
        {
            get
            {
                return GetSetting<double>("SilenceSlider");
            }
            set
            {
                if (value != GetSetting<double>("SilenceSlider"))
                {
                    SetSetting<double>("SilenceSlider", value);
                    NotifyPropertyChanged("SilenceSlider");
                    NotifyPropertyChanged("SilenceThreshold");
                    NotifyPropertyChanged("SpeechThreshold");
                }
            }
        }

        public double SpeechThreshold { get { return 1.0 - SpeechSlider; } }
        public double SpeechSlider
        {
            get
            {
                return GetSetting<double>("SpeechSlider");
            }
            set
            {
                if (value != GetSetting<double>("SpeechSlider"))
                {
                    SetSetting<double>("SpeechSlider", value);
                    NotifyPropertyChanged("SpeechSlider");
                    NotifyPropertyChanged("SilenceThreshold");
                    NotifyPropertyChanged("SpeechThreshold");
                }
            }
        }
        
        public enum ActivationTypes { Voiceactivation, Continously, Pushtospeak };
        public int ActivationTypeIndex
        {
            get
            {
                return GetSetting<int>("ActivationTypeIndex");
            }
            set
            {
                SetSetting<int>("ActivationTypeIndex", value);
                NotifyPropertyChanged("ActivationTypeIndex");
            }
        }

        public enum VoiceActivationMethods { Signaltonoise, Amplitude };
        public int VoiceActivationMethodIndex
        {
            get
            {
                return GetSetting<int>("VoiceActivationMethodIndex");
            }
            set
            {
                if (value != GetSetting<int>("VoiceActivationMethodIndex"))
                {
                    SetSetting<int>("VoiceActivationMethodIndex", value);
                    NotifyPropertyChanged("VoiceActivationMethodIndex");
                }
            }
        }

        public bool SpeakerOn
        {
            get
            {
                return GetSetting<bool>("SpeakerOn");
            }
            set
            {
                if (value != GetSetting<bool>("SpeakerOn"))
                {
                    SetSetting<double>("SpeakerOn", value);
                    NotifyPropertyChanged("SpeakerOn");
                }
            }
        }
        public bool MicrophoneOn
        {
            get
            {
                return GetSetting<bool>("MicrophoneOn");
            }
            set
            {
                if (value != GetSetting<bool>("MicrophoneOn"))
                {
                    SetSetting<double>("MicrophoneOn", value);
                    NotifyPropertyChanged("MicrophoneOn");
                }
            }
        }

        public int TransmissionQualityIndex
        {
            get
            {
                return GetSetting<int>("TransmissionQualityIndex");
            }
            set
            {
                if (value != GetSetting<int>("TransmissionQualityIndex"))
                {
                    SetSetting<int>("TransmissionQualityIndex", value);
                    NotifyPropertyChanged("TransmissionQualityIndex");
                }
            }
        }
        public bool InputPreprocess
        {
            get
            {
                return GetSetting<bool>("InputPreprocess");
            }
            set
            {
                if (value != GetSetting<bool>("InputPreprocess"))
                {
                    SetSetting<bool>("InputPreprocess", value);
                    NotifyPropertyChanged("InputPreprocess");
                }
            }
        }
        public bool EchoCancellation
        {
            get
            {
                return GetSetting<bool>("EchoCancellation");
            }
            set
            {
                if (value != GetSetting<bool>("EchoCancellation"))
                {
                    SetSetting<bool>("EchoCancellation", value);
                    NotifyPropertyChanged("EchoCancellation");
                }
            }
        }
        public bool SpeakerphoneMode
        {
            get
            {
                return GetSetting<bool>("SpeakerphoneMode");
            }
            set
            {
                if (value != GetSetting<bool>("SpeakerphoneMode"))
                {
                    SetSetting<bool>("SpeakerphoneMode", value);
                    NotifyPropertyChanged("SpeakerphoneMode");
                }
            }
        }
        public bool OutputSidetone
        {
            get
            {
                return GetSetting<bool>("OutputSidetone");
            }
            set
            {
                if (value != GetSetting<bool>("OutputSidetone"))
                {
                    SetSetting<bool>("OutputSidetone", value);
                    NotifyPropertyChanged("OutputSidetone");
                }
            }
        }
        public double SidetoneAmount
        {
            get
            {
                return GetSetting<double>("SidetoneAmount");
            }
            set
            {
                if (value != GetSetting<double>("SidetoneAmount"))
                {
                    SetSetting<double>("SidetoneAmount", value);
                    NotifyPropertyChanged("SidetoneAmount");
                }
            }
        }
        public bool OpusForceCelt
        {
            get
            {
                return GetSetting<bool>("OpusForceCelt");
            }
            set
            {
                if (value != GetSetting<bool>("OpusForceCelt"))
                {
                    SetSetting<bool>("OpusForceCelt", value);
                    NotifyPropertyChanged("OpusForceCelt");
                }
            }
        }

        public bool ForceTCP
        {
            get
            {
                return GetSetting<bool>("ForceTCP");
            }
            set
            {
                if (value != GetSetting<bool>("ForceTCP"))
                {
                    SetSetting<bool>("ForceTCP", value);
                    NotifyPropertyChanged("ForceTCP");
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

       
    }
}
