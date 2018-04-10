using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SharedSheep.Card;

namespace SharedSheep.RequestHandler
{
    class HttpClient<T> : IRequestHandler<T>
    {
        public HttpResponseMessage Post(string url, T obj)
        {
            var content = JsonConvert.SerializeObject(obj);
            Console.WriteLine("content from POST: " + content);
            var input = new StringContent(content, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var result = httpClient.PostAsync(url, input).Result;
                return result;
            }
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
