using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnsNoteWriter.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string? Text { get; set; }
        public DateTime? CreationDateTime { get; set; } = DateTime.Now;
        public DateTime? ModifyDateTime { get; set; } = DateTime.Now;
        public bool? IsRead { get; set; } = false;
    }
}
