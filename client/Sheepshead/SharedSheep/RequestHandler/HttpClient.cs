using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json.Linq;
using SharedSheep.Card;

namespace SharedSheep.RequestHandler
{
    class HttpClient<T> : IRequestHandler<T>
    {
        public HttpResponseMessage Post(string url, T obj)
        {
            throw new NotImplementedException();
        }

        public HttpResponseMessage Put(string url, T obj, int id)
        {
            throw new NotImplementedException();
        }

        public JToken Get(string url)
        {
            using (var httpClient = new HttpClient())
            {
                var response = httpClient.GetStringAsync(new Uri(url)).Result;
                return JArray.Parse(response);
            }
        }
    }
}
