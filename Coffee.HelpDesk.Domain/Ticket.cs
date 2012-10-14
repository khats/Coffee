using System.Collections.Generic;

namespace Coffee.HelpDesk.Domain
{
    public class Ticket : TicketDescription
    {
        public IEnumerable<Message> Messages { get; set; }
    }
}