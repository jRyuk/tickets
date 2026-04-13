using System;
using System.Collections.Generic;
using System.Text;

namespace Tickets.Domain.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }

        public DateTime CreaAt { get; set; }

        public bool IsDeleted { get; set; }

        public string CreatedBy { get; set; }

       
    }
}
