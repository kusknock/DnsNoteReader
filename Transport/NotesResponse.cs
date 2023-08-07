using DnsNoteWriter.Transport.Interfaces;
using System.Net;

namespace DnsNoteWriter.Transport
{
    public class NotesResponse : IResponse
    {
        public object? Data { get; set; }

        public HttpStatusCode Code { get; set; }

        public IResponse InitializeResponse(HttpStatusCode code, object data)
        {
            Code = code;
            Data = data;

            return this;
        }
    }
}
