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

        public async Task<bool> Delete(int ticketId)
        {
            return await _ticketRepository.Delete(ticketId);
        }

        public async Task<Ticket> FindByIdAsync(int id)
        {
            return await _ticketRepository.FindFirstOrDefaultAsync(t => t.Id == id, t => t.HistoryTickets);
        }

        public async Task<List<Ticket>> GetAll(int take, int page, string search)
        {
            var result = await _ticketRepository.GetAll(c=> !c.IsDeleted, take, page, search);
            return result.ToList();
        }
    }
}
