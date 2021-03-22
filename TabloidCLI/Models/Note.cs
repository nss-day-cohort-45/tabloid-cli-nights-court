using System.Collections.Generic;
using System;

namespace TabloidCLI.Models
{
    class Note
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime NoteDate { get; set; }
        public int PostId { get; set; }
        public Note()
        {
            NoteDate = DateTime.Now;
        }
        public override string ToString()
        {
            return $"{Title} ({Content} {NoteDate})";
        }
    }
}
