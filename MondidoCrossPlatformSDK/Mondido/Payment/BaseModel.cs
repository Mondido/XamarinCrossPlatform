using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Mondido.Configuration;
using Mondido.Exceptions;
using Newtonsoft.Json;
using System.Text;
using System.IO;
using MondidoCrossPlatformSDK.Request;

namespace Mondido.Payment
{
    public enum HttpMethod
    {
        POST,
        PUT
    }

    [DataContract]
    public class BaseModel
    {
        private static string _apiBaseUrl = "";
        private static string _apiUsername = "";
        private static string _apiPassword = "";

        public static string ApiBaseUrl
        {
            get
            {
                if (string.IsNullOrEmpty(_apiBaseUrl))
                {
                    _apiBaseUrl = Settings.ApiBaseUrl;
                }
                return _apiBaseUrl;
            }
        }


        public static string ApiUsername
        {
            get
            {
                if (string.IsNullOrEmpty(_apiUsername))
                {
                    _apiUsername = Settings.ApiUsername;
                }
                return _apiUsername;
            }
        }

        public static string ApiPassword
        {
            get
            {
                if (string.IsNullOrEmpty(_apiPassword))
                {
                    _apiPassword = Settings.ApiPassword;
                }
                return _apiPassword;
            }
        }

        public static async Task<string> HttpGet(string url)
        {
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(ApiBaseUrl + url));
			request.Method = "GET";
			request.Credentials = new NetworkCredential(ApiUsername, ApiPassword);

			try
			{
				// Send the request to the server and wait for the response:
				using (WebResponse response = await request.GetResponseAsync())
				{
					// Get a stream representation of the HTTP web response:
					using (Stream stream = response.GetResponseStream())
					{
						// Use this stream to build a JSON document object:
						StreamReader reader = new StreamReader(stream, Encoding.UTF8);
						return reader.ReadToEnd();
					}
				}
			}
			catch (System.Net.WebException ex)
			{
				StreamReader reader = new StreamReader(ex.Response.GetResponseStream(), Encoding.UTF8);
				var err = reader.ReadToEnd();
				Settings.Errors.Add(err);
				return err;
			}        
		}

        public static async Task<string> HttpDelete(string url)
        {
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(ApiBaseUrl + url));
			request.ContentType = "multipart/form-data";
			request.Method = "DELETE";
			request.Credentials = new NetworkCredential(ApiUsername, ApiPassword);

			try
			{
				using (var rstream = await request.GetRequestStreamAsync())
				{
					// Send the request to the server and wait for the response:
					using (WebResponse response = await request.GetResponseAsync())
					{
						// Get a stream representation of the HTTP web response:
						using (Stream stream = response.GetResponseStream())
						{
							// Use this stream to build a JSON document object:
							StreamReader reader = new StreamReader(stream, Encoding.UTF8);
							return reader.ReadToEnd();
						}
					}
				}
			}
			catch (System.Net.WebException ex)
			{
				StreamReader reader = new StreamReader(ex.Response.GetResponseStream(), Encoding.UTF8);
				var err = reader.ReadToEnd();
				Settings.Errors.Add(err);
				return err;
			}
        }

		public static async Task<string> HttpPost(string url, HttpParams postData, HttpMethod method = HttpMethod.POST)
		{
			// Create an HTTP web request using the URL:
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(ApiBaseUrl + url));
			request.ContentType = "multipart/form-data";
			if (method == HttpMethod.POST)
			{
				request.Method = "POST";
			}
			else
			{
				request.Method = "PUT";
			}
			request.Credentials = new NetworkCredential(ApiUsername, ApiPassword);
			HttpContent content = new FormUrlEncodedContent(postData);

			try
			{
				using (var rstream = await request.GetRequestStreamAsync())
				{
					await content.CopyToAsync(rstream);
					// Send the request to the server and wait for the response:
					using (WebResponse response = await request.GetResponseAsync())
					{
						// Get a stream representation of the HTTP web response:
						using (Stream stream = response.GetResponseStream())
						{
							// Use this stream to build a JSON document object:
							StreamReader reader = new StreamReader(stream, Encoding.UTF8);
							return reader.ReadToEnd();
						}
					}
				}
			}
			catch (System.Net.WebException ex)
			{
				StreamReader reader = new StreamReader(ex.Response.GetResponseStream(), Encoding.UTF8);
				var err = reader.ReadToEnd();
				Settings.Errors.Add(err);
				return err;
			}
		}
			
		public static async Task<string> HttpPut(string url, HttpParams postData)
        {
            return await HttpPost(url, postData, HttpMethod.PUT);
        }

        public static HttpMessageHandler GetHandler()
        {
            if (MessageHandlerProvider.Handler == null)
            {
                var credentials = new NetworkCredential(ApiUsername, ApiPassword);
                var handler = new HttpClientHandler { Credentials = credentials };
                return handler;
            }
            return MessageHandlerProvider.Handler;
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

		protected static string ParseFilters(Dictionary<string,string> filters)
        {
            if (filters == null)
            {
                return string.Empty;
            }

            StringBuilder sb = new StringBuilder();
			foreach (KeyValuePair<string, string> kvp in filters)
			{
                sb.Append(string.Format("&filter[{0}]={1}", kvp.Key, kvp.Value));

			}

            return sb.ToString();
        }
    }
}
