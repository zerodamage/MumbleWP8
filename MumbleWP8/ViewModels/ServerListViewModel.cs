using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net;
using System.Windows;
using System.Xml.Linq;

namespace MumbleWP8.ViewModels
{
    public partial class MainViewModel
    {
        public ObservableCollection<Server> Favorites
        {
            get
            {
                ObservableCollection<Server> searchedValue;
                if (IsolatedStorageSettings.ApplicationSettings.TryGetValue<ObservableCollection<Server>>("favorites", out searchedValue))
                {
                    return searchedValue;
                }
                else
                {
                    IsolatedStorageSettings.ApplicationSettings["favorites"] = new ObservableCollection<Server>();
                    return (ObservableCollection<Server>)IsolatedStorageSettings.ApplicationSettings["favorites"];
                }
            }
            set
            {
                ObservableCollection<Server> searchedValue;
                if (IsolatedStorageSettings.ApplicationSettings.TryGetValue<ObservableCollection<Server>>("favorites", out searchedValue))
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

        public ObservableCollection<KeyedList<string, PublicServer>> EUListGrouped { get; set; }
        public ObservableCollection<KeyedList<string, PublicServer>> OCListGrouped { get; set; }
        public ObservableCollection<KeyedList<string, PublicServer>> SAListGrouped { get; set; }
        public ObservableCollection<KeyedList<string, PublicServer>> NAListGrouped { get; set; }
        public ObservableCollection<KeyedList<string, PublicServer>> ASListGrouped { get; set; }

        public void LoadPublicServers()
        {
            try
            {
                WebClient wc = new WebClient();
                wc.DownloadStringCompleted += PublicServerListDownloadComplete;
                wc.DownloadStringAsync(new Uri("http://mumble.info/list2.cgi"));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void PublicServerListDownloadComplete(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                XDocument xdoc = XDocument.Parse(e.Result, LoadOptions.None);

                List<PublicServer> vList = (from XElement xele in xdoc.Root.Elements()
                                            select new PublicServer
                                            {
                                                Name = xele.Attribute("name").Value.Trim(" ".ToCharArray()),
                                                Port = int.Parse(xele.Attribute("port").Value),
                                                Address = xele.Attribute("ip").Value.ToString(),
                                                ContinentCode = xele.Attribute("continent_code") == null ? null : xele.Attribute("continent_code").Value,
                                                CountryCode = xele.Attribute("country_code") == null ? null : xele.Attribute("country_code").Value,
                                                Country = xele.Attribute("country") == null ? null : xele.Attribute("country").Value,
                                                Region = xele.Attribute("region") == null ? null : xele.Attribute("region").Value
                                            }).ToList();

                App.ViewModel.EUListGrouped = GroupedCountries(vList.FindAll(test => test.ContinentCode == "EU").ToList());
                App.ViewModel.OCListGrouped = GroupedCountries(vList.FindAll(test => test.ContinentCode == "OC").ToList());
                App.ViewModel.SAListGrouped = GroupedCountries(vList.FindAll(test => test.ContinentCode == "SA").ToList());
                App.ViewModel.NAListGrouped = GroupedCountries(vList.FindAll(test => test.ContinentCode == "NA").ToList());
                App.ViewModel.ASListGrouped = GroupedCountries(vList.FindAll(test => test.ContinentCode == "AS").ToList());

                NotifyPropertyChanged("EUListGrouped");
                NotifyPropertyChanged("OCListGrouped");
                NotifyPropertyChanged("SAListGrouped");
                NotifyPropertyChanged("NAListGrouped");
                NotifyPropertyChanged("ASListGrouped");
            }
        }

        private ObservableCollection<KeyedList<string, PublicServer>> GroupedCountries(List<PublicServer> publicServers)
        {
            var groupedServers =
                from server in publicServers
                orderby server.CountryCode
                group server by server.CountryCode into serversByCountry
                select new KeyedList<string, PublicServer>(serversByCountry);

            return new ObservableCollection<KeyedList<string, PublicServer>>(groupedServers);
        }
    }
}
