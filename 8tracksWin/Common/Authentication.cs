﻿using System.Net.Http;
using Newtonsoft.Json.Linq;
using Common.Configuration;

namespace Common
{
    public static class Authentication
    {
        /// <summary>
        /// Login with username and password.
        /// </summary>
        /// <returns>True on successful login and false otherwise</returns>
        public static async System.Threading.Tasks.Task<bool> Login(string username, string password)
        {
            string data = System.Uri.EscapeDataString(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "login={0}&password={1}", username, password));

            HttpResponseMessage response = ApiClient.Post("sessions.json", data);
            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                JObject responseObj = JObject.Parse(responseContent);
                if (responseObj["logged_in"].Value<bool>())
                {
                    GlobalConfigs.UserToken = responseObj.SelectToken("$.user.user_token").Value<string>();
                    return true;
                }
            }

            return false;
        }

        public static bool LoginFacebook()
        {
            return true;
        }
    }
}
