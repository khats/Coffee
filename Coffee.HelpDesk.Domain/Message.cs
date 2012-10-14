using System;

namespace Coffee.HelpDesk.Domain
{
    public class Message
    {
        public string Author { get; set; }

        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}