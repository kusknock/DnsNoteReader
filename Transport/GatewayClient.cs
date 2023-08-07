using DnsNoteWriter.Transport.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Http.Json;

namespace DnsNoteWriter.Transport
{
    public class GatewayClient
    {
        private readonly IConfiguration configuration;

        public GatewayClient(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<TResponse> MakeRequest<TResponse>(Type resultType, IRequest request = null)
            where TResponse : class, IResponse
        {
            return await SendRequest(request, Activator.CreateInstance<TResponse>(), resultType);
        }

        private async Task<TResponse> SendRequest<TResponse>(IRequest request,
            TResponse response, Type resultType) where TResponse : class, IResponse
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(configuration["NoteServerSettings:BaseUrl"]);

                    try
                    {
                        var result = await client.GetAsync(request.ApiUrl);

                        var resultData = await result.Content.ReadFromJsonAsync(resultType);

                        return response.InitializeResponse(result.StatusCode, resultData) as TResponse;
                    }
                    catch (HttpRequestException ex) when (ex.StatusCode != HttpStatusCode.OK)
                    {
                        throw;
                    }
                }
                catch (Exception ex)
                {
                    return response.InitializeResponse(HttpStatusCode.InternalServerError, ex) as TResponse;
                }
            }
        }
    }
}
