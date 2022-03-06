using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using RestSharp;

namespace APICommunication
{
    public interface IAPIClient
    {
        public Task<string> GetAbsoluteShortURLAsync(string originURL);
        public Task<string> GetOriginURLByAbsoluteShortURL(string absoluteShortURL);
    }

    class APIClient : IAPIClient
    {
        private static readonly string apiBaseURL = "http://localhost:7750";
        private static readonly string shortenURL = "/short";
        private static readonly string getLongURL = "/get/{shorturl}";

        private RestClient client;

        public APIClient()
        {
            client = new RestClient(apiBaseURL);
        }

        public async Task<string> GetAbsoluteShortURLAsync(string originURL)
        {
            var request = BuildPostRequest(shortenURL, new Dictionary<string, object>() { { "url", originURL } });
            var result = await ExecuteRequestAsync<string>(request);
            return result;
        }

        public async Task<string> GetOriginURLByAbsoluteShortURL(string absoluteShortURL)
        {
            var request = BuildPostRequest(getLongURL, new Dictionary<string, object>() { { "shorturl", absoluteShortURL } });
            var result = await ExecuteRequestAsync<string>(request);
            return result;
        }

        private RestRequest BuildPostRequest(string resource, Dictionary<string, object> parameters)
        {
            var request = new RestRequest(resource, Method.Post);
            foreach(var parameter in parameters)
            {
                request.AddParameter(parameter.Key, parameter.Value, ParameterType.RequestBody);
            }

            return request;
        }

        private async Task<T> ExecuteRequestAsync<T>(RestRequest request)
        {
            var response = await client.ExecuteAsync<T>(request);
            return response.Data;
        }
    }
}