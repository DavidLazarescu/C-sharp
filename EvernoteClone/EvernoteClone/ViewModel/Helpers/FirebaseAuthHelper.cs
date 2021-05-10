using EvernoteClone.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EvernoteClone.ViewModel.Helpers
{
    public class FirebaseAuthHelper
    {
        private static string API_KEY = "AIzaSyC8nsJz1Vi27m1Obu-Wml6UaUPElTC3eds";

        public static async Task<bool> Register(User user)
        {
            using(HttpClient client = new HttpClient())
            {
                var body = new  //Unknown object, could also be a user
                {
                    email = user.Email,
                    password = user.Password,
                    returnSecureToken = true
                };

                string bodyJson = JsonConvert.SerializeObject(body);   //The object as Json
                var data = new StringContent(bodyJson, Encoding.UTF8, "application/json");

                var response = await client.PostAsync($"https://identitytoolkit.googleapis.com/v1/accounts:signUp?key={API_KEY}", data);

                if (response.IsSuccessStatusCode)
                {
                    string resultJson = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<FirebaseResult>(resultJson);

                    App.UserId = result.localId;  //Sets the App.User ID (current User)

                    return true;
                }
                else
                {
                    string errorJson = await response.Content.ReadAsStringAsync();
                    var error = JsonConvert.DeserializeObject<Error>(errorJson);
                    MessageBox.Show(error.error.message);

                    return false;
                }
            }
        }


        public static async Task<bool> Login(User user)
        {
            using (HttpClient client = new HttpClient())
            {
                var body = new  //Unknown object, could also be a user
                {
                    email = user.Email,
                    password = user.Password,
                    returnSecureToken = true
                };

                string bodyJson = JsonConvert.SerializeObject(body);   //The object as Json
                var data = new StringContent(bodyJson, Encoding.UTF8, "application/json");

                var response = await client.PostAsync($"https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key={API_KEY}", data);

                if (response.IsSuccessStatusCode)
                {
                    string resultJson = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<FirebaseResult>(resultJson);
                    App.UserId = result.localId;

                    return true;
                }
                else
                {
                    string errorJson = await response.Content.ReadAsStringAsync();
                    var error = JsonConvert.DeserializeObject<Error>(errorJson);
                    MessageBox.Show(error.error.message);

                    return false;
                }
            }
        }
    }




    public class FirebaseResult
    {
        public string kind { get; set; }
        public string idToken { get; set; }
        public string email { get; set; }
        public string refreshToken { get; set; }
        public string expiresIn { get; set; }
        public string localId { get; set; }
    }

    public class ErrorDetails
    {
        public int code { get; set; }
        public string message { get; set; }
    }

    public class Error
    {
        public ErrorDetails error { get; set; }
    }
}
