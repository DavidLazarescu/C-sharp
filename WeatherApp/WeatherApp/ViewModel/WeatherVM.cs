using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using WeatherApp.Model;
using WeatherApp.ViewModel.Commands;
using WeatherApp.ViewModel.Helpers;

namespace WeatherApp.ViewModel
{
    public class WeatherVM : INotifyPropertyChanged
    {
        private string query;
        public string Query
        {
            get { return query; }
            set
            {
                query = value;
                OnPropertyChanged("Query");
            }
        }

        public ObservableCollection<City> Cities { get; set; }

        private CurrentConditions currentConditions;
        public CurrentConditions CurrentConditions
        {
            get { return currentConditions; }
            set
            {
                currentConditions = value;
                OnPropertyChanged("CurrentConditions");
            }
        }

        private City selectedCity;
        public City SelectedCity
        {
            get { return selectedCity; }
            set
            {
                selectedCity = value;
                if (selectedCity != null)
                {
                    OnPropertyChanged("SelectedCity");
                    getCurrentConditions();
                }
            }
        }

        private string icon;

        public string Icon
        {
            get { return icon; }
            set 
            { 
                icon = value;
                OnPropertyChanged("Icon");
            }
        }



        public SearchCommand SearchCommand { get; set; }


        public WeatherVM()
        {
            SearchCommand = new SearchCommand(this);
            Cities = new ObservableCollection<City>();
        }


        private async void getCurrentConditions()
        {
            Query = string.Empty;
            CurrentConditions = await AccuWeatherHelper.GetCurrentConditionsAsync(SelectedCity.Key);
            Cities.Clear();


            getWeatherIcon();
        }

        private void getWeatherIcon()
        {

            switch (CurrentConditions.WeatherIcon)
            {
                case 1:
                    Icon = "/Resources/WeatherIcons/01-s.png";
                    break;
                case 2:
                    Icon = "/Resources/WeatherIcons/02-s.png";
                    break;
                case 3:
                    Icon = "/Resources/WeatherIcons/03-s.png";
                    break;
                case 4:
                    Icon = "/Resources/WeatherIcons/04-s.png";
                    break;
                case 5:
                    Icon = "/Resources/WeatherIcons/05-s.png";
                    break;
                case 6:
                    Icon = "/Resources/WeatherIcons/06-s.png";
                    break;
                case 7:
                    Icon = "/Resources/WeatherIcons/07-s.png";
                    break;
                case 8:
                    Icon = "/Resources/WeatherIcons/08-s.png";
                    break;
                case 11:
                    Icon = "/Resources/WeatherIcons/11-s.png";
                    break;
                case 12:
                    Icon = "/Resources/WeatherIcons/12-s.png";
                    break;
                case 13:
                    Icon = "/Resources/WeatherIcons/13-s.png";
                    break;
                case 14:
                    Icon = "/Resources/WeatherIcons/14-s.png";
                    break;
                case 15:
                    Icon = "/Resources/WeatherIcons/15-s.png";
                    break;
                case 16:
                    Icon = "/Resources/WeatherIcons/16-s.png";
                    break;
                case 17:
                    Icon = "/Resources/WeatherIcons/17-s.png";
                    break;
                case 18:
                    Icon = "/Resources/WeatherIcons/18-s.png";
                    break;
                case 19:
                    Icon = "/Resources/WeatherIcons/19-s.png";
                    break;
                case 20:
                    Icon = "/Resources/WeatherIcons/20-s.png";
                    break;
                case 21:
                    Icon = "/Resources/WeatherIcons/21-s.png";
                    break;
                case 22:
                    Icon = "/Resources/WeatherIcons/22-s.png";
                    break;
                case 23:
                    Icon = "/Resources/WeatherIcons/23-s.png";
                    break;
                case 24:
                    Icon = "/Resources/WeatherIcons/24-s.png";
                    break;
                case 25:
                    Icon = "/Resources/WeatherIcons/25-s.png";
                    break;
                case 26:
                    Icon = "/Resources/WeatherIcons/26-s.png";
                    break;
                case 29:
                    Icon = "/Resources/WeatherIcons/29-s.png";
                    break;
                case 30:
                    Icon = "/Resources/WeatherIcons/30-s.png";
                    break;
                case 31:
                    Icon = "/Resources/WeatherIcons/31-s.png";
                    break;
                case 32:
                    Icon = "/Resources/WeatherIcons/32-s.png";
                    break;
                case 33:
                    Icon = "/Resources/WeatherIcons/33-s.png";
                    break;
                case 34:
                    Icon = "/Resources/WeatherIcons/34-s.png";
                    break;
                case 35:
                    Icon = "/Resources/WeatherIcons/35-s.png";
                    break;
                case 36:
                    Icon = "/Resources/WeatherIcons/36-s.png";
                    break;
                case 37:
                    Icon = "/Resources/WeatherIcons/37-s.png";
                    break;
                case 38:
                    Icon = "/Resources/WeatherIcons/38-s.png";
                    break;
                case 39:
                    Icon = "/Resources/WeatherIcons/39-s.png";
                    break;
                case 40:
                    Icon = "/Resources/WeatherIcons/40-s.png";
                    break;
                case 41:
                    Icon = "/Resources/WeatherIcons/41-s.png";
                    break;
                case 42:
                    Icon = "/Resources/WeatherIcons/42-s.png";
                    break;

                default:
                    break;
            }
        }

        public async void makeQuery()
        {
            Cities.Clear();
            var cities = await AccuWeatherHelper.GetCitiesAsync(Query);

            foreach(var city in cities)
            {
                Cities.Add(city);
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));  //The "?" is to check if the event exists
        }
    }
}
