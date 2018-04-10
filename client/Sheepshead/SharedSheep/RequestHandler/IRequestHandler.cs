using Newtonsoft.Json.Linq;
using SharedSheep.Card;
using System.Net.Http;

namespace SharedSheep.RequestHandler
{
    interface IRequestHandler<T>
    {
        JToken Get(string url);

        HttpResponseMessage Post(string url, T obj);
        HttpResponseMessage Put(string url, T obj, int id);
    }
}
