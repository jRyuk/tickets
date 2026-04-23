using System;
using System.Collections.Generic;
using System.Text;
using Tickets.Domain.Entities;
using Tickets.Domain.Interfaces.Repositories;

namespace Tickets.Application.Services.HistoryTicketCase
{
    public class HistoryService
    {
        private readonly IHistoryTicketRepository _historyTicketRepository;

        public HistoryService(IHistoryTicketRepository historyTicketRepository)
        {
            _historyTicketRepository = historyTicketRepository;
        }
        public async Task<HistoryTicket> Add(HistoryTicket ticket)
        {
            return await _historyTicketRepository.AddAsync(ticket);
        }

        public async Task<bool> Delete(int ticketId)
        {
            var result = await _historyTicketRepository.Delete(ticketId);

            return result;
        }

        public async Task<HistoryTicket> FindByIdAsync(int ticketId)
        {
          return await _historyTicketRepository.FindFirstOrDefaultAsync(t => t.Id == ticketId, t=> t.Ticket);
        }

        public async Task<List<HistoryTicket>> GetAll(int take, int page, string? search)
        {
            var result = await _historyTicketRepository.GetAll(c => !c.IsDeleted, take, page, search);
            return result.ToList();
        }
    }
}
