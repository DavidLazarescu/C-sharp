using Newtonsoft.Json;
using QuizzGame.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace QuizzGame.ViewModel.Helper
{
    class DatabaseHelper
    {
        private static string DBPATH = "https://quizzgame-7cbab-default-rtdb.europe-west1.firebasedatabase.app/";


        public static async Task<bool> Insert<T>(T item)
        {
            string jsonBody = JsonConvert.SerializeObject(item);

            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            using(var client = new HttpClient())
            {
                var result = await client.PostAsync($"{DBPATH}{item.GetType().Name.ToLower()}.json", content);

                if (result.IsSuccessStatusCode)
                    return true;
                else
                    return false;
            }
        }

        public static async Task<bool> Update<T>(T item) where T : HasUserIds
        {
            string jsonBody = JsonConvert.SerializeObject(item);

            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                var result = await client.PatchAsync($"{DBPATH}{item.GetType().Name.ToLower()}/{item.UniqueId}.json", content);

                if (result.IsSuccessStatusCode)
                    return true;
                else
                    return false;
            }
        }

        public static async Task<bool> Delete<T>(T item) where T : HasUserIds
        {
            string jsonBody = JsonConvert.SerializeObject(item);

            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                var result = await client.DeleteAsync($"{DBPATH}{item.GetType().Name.ToLower()}/{item.UniqueId}.json");

                if (result.IsSuccessStatusCode)
                    return true;
                else
                    return false;
            }
        }

        public static async Task<UserData> ReadUserData<T>(UserData userData) where T : HasUserIds
        {
            using (HttpClient client = new HttpClient())
            {
                var result = await client.GetAsync($"{DBPATH}{typeof(T).Name.ToLower()}.json");

                if (result.IsSuccessStatusCode)
                {
                    var jsonResult = await result.Content.ReadAsStringAsync();

                    var objects = JsonConvert.DeserializeObject<Dictionary<string, T>>(jsonResult);

                    List<T> list = new List<T>();

                    if (objects != null)
                    {
                        foreach (var o in objects)
                        {
                            if (o.Value.ParentUserId == App.UserId)
                            {
                                o.Value.UniqueId = o.Key;
                                return o.Value as UserData;
                            }
                        }
                    }

                    return null;
                }
                else
                {
                    return null;
                }
            }
        }
    }



    public interface HasUserIds
    {
        public string UniqueId { get; set; }
        public string ParentUserId { get; set; }
    }
}
