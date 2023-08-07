using DnsNoteWriter.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DnsNoteWriter.Services
{
    public class NoteReaderService
    {
        private readonly INoteReader noteWriter;
        private readonly IConfiguration configuration;
        private readonly ILogger<NoteReaderService> logger;

        public NoteReaderService(INoteReader noteWriter,
            IConfiguration configuration, 
            ILogger<NoteReaderService> logger)
        {
            this.noteWriter = noteWriter;
            this.configuration = configuration;
            this.logger = logger;
        }

        public async Task Process()
        {
            while (true)
            {
                var text = Guid.NewGuid().ToString();

                await noteWriter.ReadNotes();

                Thread.Sleep(new Random(Guid.NewGuid().GetHashCode()).Next(1,3) * 1000);
            }
        }
    }
}