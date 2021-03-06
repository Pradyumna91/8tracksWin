﻿using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common
{
    public static class ApiClient
    {
        const string devApiKey = "1814ff5a6d53aac74fc28827f0602d26ffc5d191";
        const string baseApiUri = "https://8tracks.com/";

        private static HttpRequestMessage CreateRequestObj(string methodPath, HttpMethod method, Dictionary<string, string> headers = null, Dictionary<string, string> queryParams =  null)
        {
            //Always adding the api_version to the url
            if (queryParams != null)
                queryParams.Add("api_version", "3");
            else
                queryParams = new Dictionary<string, string>() { { "api_version", "3" } };

            UriBuilder baseApiUriBuilder = new UriBuilder(baseApiUri);
            baseApiUriBuilder.Query = CreateQueryStringFromParameters(queryParams);
            baseApiUriBuilder.Path += methodPath;
            HttpRequestMessage request = new HttpRequestMessage(method, baseApiUriBuilder.Uri);
            request.Headers.Add("X-Api-Key", devApiKey);
            if (Configuration.GlobalConfigs.CurrentUser != null)
                request.Headers.Add("X-User-Token", Configuration.GlobalConfigs.CurrentUser.UserToken);

            if(headers != null)
                foreach (string key in headers.Keys)
                    request.Headers.Add(key, headers[key]);

            return request;
        }

        private static string CreateQueryStringFromParameters(Dictionary<string,string> queryParams)
        {
            System.Text.StringBuilder queryString = new System.Text.StringBuilder();

            foreach (string key in queryParams.Keys)
                queryString.Append("&" + key + "=" + queryParams[key]);
            queryString.Remove(0, 1);                  //removing the first '&'

            return queryString.ToString();
        }

        private static HttpResponseMessage PerformHttpOperationSync(string methodPath, string data, HttpMethod method, Dictionary<string, string> headers = null, Dictionary<string, string> queryParams = null)
        {
            HttpRequestMessage req = CreateRequestObj(methodPath, method, headers, queryParams);
            if(data != null)
                req.Content = new StringContent(data, System.Text.Encoding.UTF8, "application/x-www-form-urlencoded");

            using (HttpClient client = new HttpClient())
            {
                return client.SendAsync(req).Result;
            }
        }

        private static async Task<HttpResponseMessage> PerformHttpOperationAsync(string methodPath, string data, HttpMethod method, Dictionary<string, string> headers = null, Dictionary<string, string> queryParams = null)
        {
            HttpRequestMessage req = CreateRequestObj(methodPath, method, headers, queryParams);
            if(data != null)
                req.Content = new StringContent(data, System.Text.Encoding.UTF8, "application/x-www-form-urlencoded");

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.SendAsync(req);
                return response;
            }
        }

        public static HttpResponseMessage Post(string methodPath, string data, Dictionary<string, string> headers = null, Dictionary<string, string> queryParams = null)
        {
            return PerformHttpOperationSync(methodPath, data, HttpMethod.Post, headers, queryParams);
        }

        public static async Task<HttpResponseMessage> PostAsync(string methodPath, string data, Dictionary<string, string> headers = null, Dictionary<string, string> queryParams = null)
        {
            return await PerformHttpOperationAsync(methodPath, data, HttpMethod.Post, headers, queryParams);
        }

        public static HttpResponseMessage Get(string methodPath, Dictionary<string,string> headers = null, Dictionary<string,string> queryParams = null)
        {

            return PerformHttpOperationSync(methodPath, null, HttpMethod.Get, headers, queryParams);
        }

        public static async Task<HttpResponseMessage> GetAsync(string methodPath, Dictionary<string, string> headers = null, Dictionary<string, string> queryParams = null)
        {
            return await PerformHttpOperationAsync(methodPath, null, HttpMethod.Get, headers, queryParams);
        }

        public static HttpResponseMessage Delete(string methodPath, string data, Dictionary<string, string> headers = null, Dictionary<string, string> queryParams = null)
        {
            return PerformHttpOperationSync(methodPath, data, HttpMethod.Delete, headers, queryParams);
        }

        public static async Task<HttpResponseMessage> DeleteAsync(string methodPath, string data, Dictionary<string, string> headers = null, Dictionary<string, string> queryParams = null)
        {
            return await PerformHttpOperationAsync(methodPath, data, HttpMethod.Delete, headers, queryParams);
        }
    }
}
