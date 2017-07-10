using ATTeamE.web.Domain.Convo;
using ATTeamE.web.Domain.TMDB;
using ATTeamE.web.Interfaces;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace ATTeamE.web.Services.Watson
{
    public class WatsonService : BaseService, IWatsonService
    {
        public WatsonService(string username, string password)
        {
            if (Headers == null) { Headers = new Dictionary<string, string>(); }
            Headers.Add("SpeechToTextAuth", System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes("988558a8-ef47-4a26-8f2c-561581034c37:RJQL403LfsXG")));
            Headers.Add("TextToSpeechAuth", System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes("be81879e-381c-47de-a44e-60f7650dc9e8:zkrJ6PqpsXdg")));
            Headers.Add("ConvoAuth", System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes("fe5c2f9b-3654-454b-9852-125c23b09853:M3FOtVH0o1nw")));
            Headers.Add("ContentType", "application/JSON");
        }
        private Dictionary<string, string> Headers { get; set; }
        private IRestResponse<T> GenericRestSharp<T>(string url, string method = null, object data = null, HttpBasicAuthenticator auth = null) where T : new()
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
                    request.AddParameter(new Parameter { Name = item.Key, Value = item.Value, Type = ParameterType.HttpHeader });
                }
            }

            if (auth != null)
            {
                request.Parameters.Remove(request.Parameters.FirstOrDefault(a => a.Name == "Authorization"));
                rest.Authenticator = auth;
            }
            #endregion


            return rest.Execute<T>(request);

        }

        public Message GetResponseMessage(string request)
        {
            var obj = new
            {
                Input = new
                {
                    Text = request
                },
                AlternateIntents = true
            };

            IRestResponse<Message> response = GenericRestSharp<Message>("https://watson-api-explorer.mybluemix.net/conversation/api/v1/workspaces/9bcb964c-7d33-45c4-ad3a-02d7efe5f667/message?version=2017-07-08", "POST", obj);

            return response.Data;
        }

        public string GetToken(string apiName)
        {
            HttpBasicAuthenticator auth = null;
            switch (apiName)
            {
                case "SpeechToText":
                    auth = new HttpBasicAuthenticator("BASIC", Headers["SpeechToTextAuth"]);
                    break;

                case "TextToSpeech":
                    auth = new HttpBasicAuthenticator("BASIC", Headers["TextToSpeechAuth"]);
                    break;

                case "Convo":
                    auth = new HttpBasicAuthenticator("BASIC", Headers["ConvoAuth"]);
                    break;
            }
            IRestResponse<object> response = GenericRestSharp<object>("https://stream.watsonplatform.net/authorization/api/v1/token?url=https://stream.watsonplatform.net/speech-to-text/api", null, null, auth);
            if (response.Data == null) { throw new Exception(response.Content); }

            return response.Content;
        }

        private string PrepareText(string text)
        {

            string concatString = null;

            var arrayString = text.Split(' ');

            foreach (string word in arrayString)
            {
                if (!string.IsNullOrEmpty(concatString))
                {
                    concatString += "%20" + word;
                }
                else
                {
                    concatString = word;
                }
            }

            return concatString;
        }



        public byte[] ConvertTextToSpeech(string text)
        {
            byte[] response = null;

            if (!string.IsNullOrEmpty(text))
            {

                var concatText = PrepareText(text);
                var url = "https://watson-api-explorer.mybluemix.net/text-to-speech/api/v1/synthesize?accept=audio%2Fwav&voice=en-US_MichaelVoice&text=" + concatText;

                Dictionary<string, string> headers = new Dictionary<string, string>();
               
                response = GetFileRest(url, null, null, headers);

            }
            return response;
        }

        public static byte[] GetFileRest(string url, string method = null, object data = null, Dictionary<string, string> Headers = null)
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
                        rest.Authenticator = new RestSharp.Authenticators.HttpBasicAuthenticator("username", item.Key);
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



            return rest.DownloadData(request);

        }
        public MovieResults DiscoverContent(Dictionary<string, string> filters)
        {
            string url = "https://api.themoviedb.org/3/discover/movie?api_key=d4aae4641cb455dcc2208ffb053f6efe";
            IEnumerable<KeyValuePair<string, string>> validFilters = filters.Where(a =>
            {
                if (a.Key == "certification") { return true; }
                else if (a.Key == "vote_average") { return true; }
                else if (a.Key == "vote_count") { return true; }
                else if (a.Key == "with_people") { return true; }
                else if (a.Key == "with_genres") { return true; }
                else { return false; }
            });

            foreach (var item in validFilters)
            {
                url += "&" + item.Key + "=" + item.Value;
            }

            IRestResponse<MovieResults> response = GenericRestSharp<MovieResults>(url);

            return response.Data;
        }

        public MovieResults SearchContent(string filter)
        {
            string url = "https://api.themoviedb.org/3/discover/movie?api_key=d4aae4641cb455dcc2208ffb053f6efe&query=" + filter;

            IRestResponse<MovieResults> response = GenericRestSharp<MovieResults>(url);

            return response.Data;
        }

    }
}