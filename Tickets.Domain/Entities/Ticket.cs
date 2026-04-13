using System;
using System.Collections.Generic;
using System.Text;

namespace Tickets.Domain.Entities
{
    public class Ticket : BaseEntity
    {
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        /// <summary>
        /// Cuanto tiempo se estima que va a durar
        /// </summary>
        public decimal Hours { get; set; }

        public string UserId { get; set; }

       

        public TicketStatus Status { get; set; }

        public string Description { get; set; }

    }

    public enum TicketStatus
    {
        Open,
        InProgress,
        Closed
    }
}
