using DnsNoteWriter.Models;
using DnsNoteWriter.Services.Interfaces;
using DnsNoteWriter.Transport;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace DnsNoteWriter.Services
{
    public class NoteReader : INoteReader
    {
        private readonly GatewayClient client;
        private readonly IConfiguration configuration;
        private readonly ILogger<NoteReader> logger;

        public NoteReader(GatewayClient client,
            IConfiguration configuration,
            ILogger<NoteReader> logger)
        {
            this.client = client;
            this.configuration = configuration;
            this.logger = logger;
        }

        public async Task ReadNotes()
        {
            var request = new NoteRequest()
            {
                ApiUrl = configuration["NoteServerSettings:ApiUrls:Read"],
                Data = null
            };

            try
            {
                Type resultType = typeof(IEnumerable<Note>);

                var result = await client.MakeRequest<NotesResponse>(resultType, request);

                if (result.Code != HttpStatusCode.OK)
                    throw new InvalidOperationException("Произошла ошибка:", result.Data as Exception);

                var resultData = result.Data as IEnumerable<Note>;

                if (resultData is null || !resultData.Any())
                {
                    logger.LogInformation("Заметок для чтения нет", result.Data);
                    return;
                }

                foreach (var item in resultData)
                    logger.LogInformation("Заметка c номером {0} и текстом {1} прочитана", item.Id, item.Text);
            }
            catch (Exception ex)
            {
                logger.LogError($"{ex.Message} / {ex.InnerException?.Message}");
            }
        }
    }
}
