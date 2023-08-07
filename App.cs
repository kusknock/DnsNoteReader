using DnsNoteWriter.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DnsNoteWriter
{
    public class App
    {
        private readonly NoteReaderService noteWriterService;

        public App(NoteReaderService noteWriterService)
        {
            this.noteWriterService = noteWriterService;
        }

        public async Task Run(string[] args)
        {
            await noteWriterService.Process();

            Console.ReadLine();
        }
    }
}