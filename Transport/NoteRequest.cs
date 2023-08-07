using DnsNoteWriter.Transport.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnsNoteWriter.Transport
{
    public class NoteRequest : IRequest
    {
        public string ApiUrl { get; set; }
        public object? Data { get; set; }
    }
}
