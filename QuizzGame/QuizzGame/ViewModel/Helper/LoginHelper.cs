using Newtonsoft.Json;
using QuizzGame.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuizzGame.ViewModel.Helper
{
    public class LoginHelper
    {
        private static string API_KEY = "AIzaSyBhNJ04G8CgZV8geDfzv_B5nOgIl1m_dPc";

        public static async Task<bool> Register(User user)
        {
            /*Registers the user, if sucessfull, go to login, if not show a MessageBox with errors*/
            using (HttpClient client = new HttpClient()) { 
                var body = new
                {
                    email = user.Email,
                    password = user.Password,
                    returnSecureToken = true
                };

                string bodyJson = JsonConvert.SerializeObject(body);
                var data = new StringContent(bodyJson, Encoding.UTF8, "application/json");

                var response = await client.PostAsync($"https://identitytoolkit.googleapis.com/v1/accounts:signUp?key={API_KEY}", data);

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

        public static async Task<bool> Login(User user)
        {
            /*Logs the user in, if the loggin was successfull, set the local UserID to the general UserID
              If the login wasn't successfull, show a window with the error message*/
            using (HttpClient client = new HttpClient())
            {
                var body = new
                {
                    email = user.Email,
                    password = user.Password,
                    returnSecureToken = true
                };

                string bodyJson = JsonConvert.SerializeObject(body);
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

        public static async Task<bool> ResetPassword(User user)
        {
            /*Resets the password, if successfull, send a email to the user with a password reset, if not show a MessageBox with the error*/
            using (HttpClient client = new HttpClient())
            {
                var body = new
                {
                    requestType = "PASSWORD_RESET",
                    email = user.Email
                };

                string bodyJson = JsonConvert.SerializeObject(body);
                var data = new StringContent(bodyJson, Encoding.UTF8, "application/json");

                var response = await client.PostAsync($"https://identitytoolkit.googleapis.com/v1/accounts:sendOobCode?key={API_KEY}", data);

                if (response.IsSuccessStatusCode) 
                {
                    MessageBox.Show("Password reset was successfull, check your emails!");

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
}
