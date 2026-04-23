using System;
using System.Collections.Generic;
using System.Text;

namespace Tickets.Domain.Entities
{
    public class HistoryTicket : BaseEntity
    {
        public string UserId { get; set; }

        public int TicketId { get; set; }

        public string Comment { get; set; }

        public virtual Ticket Ticket { get; set; }
    }
}
