using RestSharp;

namespace KantorClient.DAL.ServerCommunication
{
    public class RequestContext
    {
        public RequestContext(string url)
        {
            this.Url = url;
            this.Method = Method.Post;
        }

        public RequestContext(string url, Method method)
        {
            this.Url = url;
            this.Method = method;
        }

        public Method Method { get; set; }

        public string SynchronizationKey { get; set; }

        public string Url { get; set; }
    }
}
