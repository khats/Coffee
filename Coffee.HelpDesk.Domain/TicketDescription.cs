using System;

namespace Coffee.HelpDesk.Domain
{
    public class TicketDescription
    {
        public string Subject { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public string AuthorOfLastMessage { get; set; }
    }
}