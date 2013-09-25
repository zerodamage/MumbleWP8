using MumbleWP8.ViewModels;
using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MumbleWP8
{
    static class SettingsAccess
    {
        public static List<ServerViewItem> Favorites
        {
            get
            {
                List<ServerViewItem> searchedValue;
                if (IsolatedStorageSettings.ApplicationSettings.TryGetValue<List<ServerViewItem>>("favorites", out searchedValue))
                {
                    return searchedValue;
                }
                else
                {
                    IsolatedStorageSettings.ApplicationSettings.Add("favorites", new List<ServerViewItem>());
                    return (List<ServerViewItem>)IsolatedStorageSettings.ApplicationSettings["favorites"];
                }
            }
            set
            {
                List<ServerViewItem> searchedValue;
                if (IsolatedStorageSettings.ApplicationSettings.TryGetValue<List<ServerViewItem>>("favorites", out searchedValue))
                {
                    IsolatedStorageSettings.ApplicationSettings["favorites"] = value;
                }
                else
                {
                    IsolatedStorageSettings.ApplicationSettings.Add("favorites", value);
                }

                IsolatedStorageSettings.ApplicationSettings.Save();
            }
        }
    }
}
