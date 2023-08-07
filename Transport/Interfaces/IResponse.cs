using System.Net;

namespace DnsNoteWriter.Transport.Interfaces
{
    public interface IResponse
    {
        object? Data { get; set; }

        HttpStatusCode Code { get; set; }

        IResponse InitializeResponse(HttpStatusCode statusCode, object data);
    }
}