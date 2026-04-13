using System;
using System.Collections.Generic;
using System.Text;
using Tickets.Domain.Entities;
using Tickets.Domain.Interfaces.Repositories;

namespace Tickets.Application.Services
{
    public class TicketsCase
    {
        private readonly ITickesRepository _ticketRepository;
        public TicketsCase(ITickesRepository tickesRepository)
        {
            _ticketRepository = tickesRepository;
        }

        public async Task<Ticket> Add(Ticket ticket)
        {
            return await _ticketRepository.AddAsync(ticket);
        }

        public async Task<List<Ticket>> FindByIdAsync(int id)
        {
            return (await _ticketRepository.FindAsync(t => t.Id == id)).ToList();
        }

      
    }
}
