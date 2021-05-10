using EvernoteClone.Model;
using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EvernoteClone.ViewModel.Helpers
{
    public class DatabaseHelper
    {
        private static string dbFile = Path.Combine(Environment.CurrentDirectory, "notesDb.db3");
        private static string dbPath = "https://evernoteclone-project-default-rtdb.europe-west1.firebasedatabase.app/";


        public static async Task<bool> Insert<T>(T item)
        {
            /*Inserts a object into the databank and returns an bool with the insertion status*/
            
            //Convert the object you want to Post to a json string format
            string jsonBody = JsonConvert.SerializeObject(item);

            //After make it to be a StringContent, so you can set it the content of the Post
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            //Create a new HttpClient
            using(var client = new HttpClient())
            {
                /*Post over the client, the uri should be: BasicPath (you can get it from the website), and after you can put whatever
                you want, in this case, i choosed for the table location (this is like the folder where it gets created in the db),
                the name of the type of my object.ToLower(), for example:  https://basic_domain/notebook.json cause u need to append .json*/
                var result = await client.PostAsync($"{dbPath}{item.GetType().Name.ToLower()}.json", content);

                if (result.IsSuccessStatusCode)
                    return true;
                else
                    return false;
            }
        }

        public static async Task<bool> Update<T>(T item) where T : HasId
        {

            string jsonBody = JsonConvert.SerializeObject(item);

            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                //You need to give the ID so its specified with item to patch
                var result = await client.PatchAsync($"{dbPath}{item.GetType().Name.ToLower()}/{item.Id}.json", content);

                if (result.IsSuccessStatusCode)
                    return true;
                else
                    return false;
            }
        }

        public static async Task<bool> Delete<T>(T item) where T : HasId
        {
            /*Deletes a object from the databank and returns an bool with the insertion status*/

            string jsonBody = JsonConvert.SerializeObject(item);

            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                //You need to give the ID so its specified with item to patch
                var result = await client.DeleteAsync($"{dbPath}{item.GetType().Name.ToLower()}/{item.Id}.json");

                if (result.IsSuccessStatusCode)
                    return true;
                else
                    return false;
            }
        }

        public static async Task<List<T>> Read<T>() where T : HasId
        {
            using(HttpClient client = new HttpClient())
            {
                var result = await client.GetAsync($"{dbPath}{typeof(T).Name.ToLower()}.json");

                var jsonResult = await result.Content.ReadAsStringAsync();

                if (result.IsSuccessStatusCode)
                {
                    //jsonResult is not a List! but a dictionary
                    var objects = JsonConvert.DeserializeObject<Dictionary<string, T>>(jsonResult);

                    List<T> list = new List<T>();

                    if (objects != null)
                    {
                        foreach (var o in objects)
                        {
                            o.Value.Id = o.Key;   //Sets the normal ID, to the unique Firebase generated ID
                            list.Add(o.Value);
                        }
                    }

                    return list;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
