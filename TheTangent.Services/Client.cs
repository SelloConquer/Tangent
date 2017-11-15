using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Collections;
using TheTagent.Data;
using TheTagent.Shared;
using TheTagent.Services;

namespace TheTagent.Services
{
    public static class Client
    {
        public static async Task<TheTagent.Data.User> Login(string Username, string Password)
        {
           
            TheTagent.Data.User user = null;
            try
            {
                List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
                parameters.Add(new KeyValuePair<string, string>("username", Username));
                parameters.Add(new KeyValuePair<string, string>("password", Password));

                string response = await RestBase.Request("http://staging.tangent.tngnt.co/api-token-auth/",parameters);
                TheTagent.Data.RestResult result = JsonConvert.DeserializeObject<TheTagent.Data.RestResult>(response);

                if (result != null)
                {
                    if (!result.success)
                    {
                        TheTagent.Shared.Runtime.LastErrorMessage = result.message;
                    }
                    else
                    {
                        user = JsonConvert.DeserializeObject<TheTagent.Data.User>(result.data);
                    }
                }
            }
            catch (Exception ex)
            {
                TheTagent.Shared.Runtime.LastErrorMessage = TheTagent.Shared.Const.MSG_ERROR_PREFIX + ex.Message;
            }
            return user;
        }
    }
}
