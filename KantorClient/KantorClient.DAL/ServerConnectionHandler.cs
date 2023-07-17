using KantorClient.DAL.ServerCommunication;
using KantorServer.Application.Requests;
using KantorServer.Application.Responses;
using RestSharp;
using RestSharp.Authenticators;
using System.Net;
using System.Net.Security;
using System.Text.Json;
using System.Threading;

namespace KantorClient.DAL
{
    public class ServerConnectionHandler
    {
        public static async Task<Tres> ExecuteFunction<Treq, Tres>(RequestContext reqContext, Treq request)
            where Treq : BaseServerRequest where Tres : BaseServerResponse
        {
            var options = new RestClientOptions(reqContext.Url)
            {
                //Proxy = WebRequest.DefaultWebProxy,transfer
                //RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };

            var client = new RestClient(options);
            
            //Workaround for: "The remote certificate is invalid according to the validation procedure."
            //ServicePointManager.SecurityProtocol = ServicePointManager.SecurityProtocol | (SecurityProtocolType)3072;
            //ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) =>
            //{ return true; };

            var restRequest = new RestRequest()
            {
                RequestFormat = DataFormat.Json,
                Method = reqContext.Method,
            };
            if(restRequest.Method == Method.Get)
            {
                restRequest.AddHeader("Content-Length", "0");
                restRequest.AddHeader("User-Agent", "PostmanRuntime/7.32.3");
                restRequest.RequestFormat = DataFormat.None;
            }
            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
                IncludeFields = true,
            };
            var jsonRequest = JsonSerializer.Serialize(request,serializeOptions);
            restRequest.AddParameter("application/json", jsonRequest, ParameterType.RequestBody);
            var restResponse = await client.ExecuteAsync(restRequest);
            if (restResponse.Content != null)
            {
                var response = JsonSerializer.Deserialize<Tres>(restResponse.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true});
                return response;
            }

            return null;
        }
    }
}
