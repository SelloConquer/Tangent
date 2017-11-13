using System;
using System.IO;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Xml.Schema;
using ModernHttpClient;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Collections;
using RestSharp;
using Newtonsoft.Json;
using TheTagent.Shared;
using TheTagent.Services;



namespace TheTagent.Services
{
    public class RestBase
    {
        /// <param name="URL">URL.</param>
        /// <param name="Parameters">Parameters.</param>
        public static async Task<string> Request(string URL, List<KeyValuePair<string, string>> Parameters)
        {
            try
            {
                using (HttpClient client = new HttpClient(new NativeMessageHandler()))
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(TheTagent.Shared.Const.REQUEST_HEADER_ACCEPT));

                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, URL + BuildParamString(Parameters));


                    using (HttpResponseMessage response = await client.SendAsync(request))
                    {
                        if ((int)response.StatusCode == 200)
                        {
                            using (HttpContent content = response.Content)
                            {
                                string result = await content.ReadAsStringAsync();
                                if (result != null)
                                    return result;
                                else
                                    
                                    TheTagent.Shared.Runtime.LastErrorMessage = TheTagent.Shared.Const.MSG_ERROR_PREFIX + " Unable to communicate with the server.";
                            }
                        }
                        else
                        {
                            TheTagent.Shared.Runtime.LastErrorMessage = TheTagent.Shared.Const.MSG_ERROR_PREFIX + " Unable to communicate with the server.";
                        }
                    }
                }
                return "";
            }
            catch (Exception ex)
            {
                TheTagent.Shared.Runtime.LastErrorMessage = TheTagent.Shared.Const.MSG_ERROR_PREFIX + ex.Message;
                return "";
            }
        }
        private static string BuildParamString(List<KeyValuePair<string, string>> Parameters)
        {
            if (Parameters == null || Parameters.Count == 0)
                return "";

            List<string> queries = new List<string>();

            foreach (var param in Parameters)
            {
                queries.Add(param.Key + "=" + param.Value);
            }

            var queryString = "?" + string.Join("&", queries.ToArray());

            return queryString;

        }

    }
}
