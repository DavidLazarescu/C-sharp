using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Model;

namespace WeatherApp.ViewModel.Helpers
{
    public class AccuWeatherHelper
    {
        public const string BASE_URL = "http://dataservice.accuweather.com/";
        public const string API_KEY = "iBc5KnlfrKg4xOlRYnDNyNXeLexyRudO";

        public const string AUTOCOMPLETE_ENDPOINT = "locations/v1/cities/autocomplete?apikey={0}&q={1}";
        public const string CURRENT_CONDITIONS_ENDPOINT = "currentconditions/v1/{0}?apikey={1}";
        


        public static async Task<List<City>> GetCitiesAsync(string query) 
        {
            List<City> cities = new List<City>();

            //string.Format() fills in the place holders "{0}", "{1}", etc...
            string url = BASE_URL + string.Format(AUTOCOMPLETE_ENDPOINT, API_KEY, query);

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                var jsonResponse = await response.Content.ReadAsStringAsync();

                cities = JsonConvert.DeserializeObject<List<City>>(jsonResponse);
            }

            return cities;
        }


        public static async Task<CurrentConditions> GetCurrentConditionsAsync(string cityKey)
        {
            CurrentConditions conditions = new CurrentConditions();


            string url = BASE_URL + string.Format(CURRENT_CONDITIONS_ENDPOINT, cityKey, API_KEY);

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                string jsonResponse = await response.Content.ReadAsStringAsync();

                conditions = (JsonConvert.DeserializeObject<List<CurrentConditions>>(jsonResponse)).FirstOrDefault();
            }

                return conditions;
        }
    }
}