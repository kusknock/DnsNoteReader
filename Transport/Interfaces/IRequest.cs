namespace DnsNoteWriter.Transport.Interfaces
{
    public interface IRequest
    {
        string ApiUrl { get; set; }
        object Data { get; set; }
    }
}