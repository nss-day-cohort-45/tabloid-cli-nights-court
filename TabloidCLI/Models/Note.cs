using System;

namespace TabloidCLI.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int PostId { get; set; }
        public Note()
        {
            CreateDateTime = DateTime.Now;
        }
        public override string ToString()
        {
            return $"{Title} ({Content} {CreateDateTime})";
        }
    }
}
