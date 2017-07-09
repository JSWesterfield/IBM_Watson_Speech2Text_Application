using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ATTeamE.web.Services.Watson
{
    public class WatsonService : BaseService
    {
        public WatsonService(string domain, string apiKey)
        {
            Domain = domain;
            Headers.Add("authentication", apiKey);
            Headers.Add("contentType", "JSON");
        }
        private string Domain { get; set; }
        private Dictionary<string, string> Headers { get; set; }


        private IRestResponse<T> GenericRestSharp<T>(string url, string method = null, object data = null) where T : new()
        {
            #region Client

            RestClient rest = null;
            if (url != null)
            {
                rest = new RestClient(url);
            }
            else { throw new Exception("URL is not defined!"); }

            #endregion


            #region Request

            RestRequest request = new RestRequest();
            switch (method)
            {
                case "GET":
                    request.Method = Method.GET;
                    break;

                case "POST":
                    request.Method = Method.POST;
                    break;

                case "PATCH":
                    request.Method = Method.PATCH;
                    break;

                case "PUT":
                    request.Method = Method.PUT;
                    break;

                case "DELETE":
                    request.Method = Method.DELETE;
                    break;
                default:
                    request.Method = Method.GET;
                    break;
            };
            request.JsonSerializer = CustomSerializer.CamelCaseIngoreDictionaryKeys;
            request.RequestFormat = DataFormat.Json;
            request.AddBody(data);
            if (Headers != null)
            {
                foreach (var item in Headers)
                {
                    if (item.Key == "authentication")
                    {
                        rest.Authenticator = new HttpBasicAuthenticator("username", item.Key);
                    }
                    else if (item.Key == "contentType")
                    {
                        request.AddParameter(new Parameter { ContentType = item.Value });
                    }
                    else
                    {
                        request.AddParameter(new Parameter { Name = item.Key, Value = item.Value });
                    }
                }
            }
            #endregion


            return rest.Execute<T>(request);

        }

        //public string GetResponseMessage() {  }

        public void ConvertSpeechToText()
        {
              
        }


        public string ConvertTextToSpeech(string text) {

            string concatString = null;
            var arrayString = text.Split(' ');

            foreach(string word in arrayString)
            {
                if (string.IsNullOrEmpty(concatString)){
                    concatString = concatString + "%20" + word;
                }
            }

            return concatString;
        }
    }
}