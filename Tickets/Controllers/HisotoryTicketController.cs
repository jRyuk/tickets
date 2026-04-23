using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Tickets.Application.Services;
using Tickets.Application.Services.HistoryTicketCase;
using Tickets.Domain.Entities;
using Tickets.DTOs;

namespace Tickets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HisotoryTicketController : ControllerBase
    {
        private readonly HistoryService _historyService;
        public HisotoryTicketController(HistoryService historyService)
        {
            _historyService = historyService;
        }
        public async Task<IActionResult> AddHistoryTicket(HistoryTicketDto historyTicket)
        {
            await _historyService.Add(new HistoryTicket()
            {
                CreatedBy = User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                CreaAt = DateTime.UtcNow,
                Comment = historyTicket.Comment,
                UserId = historyTicket.UserId,
                TicketId = historyTicket.TicketId
            });
            return Ok();
        }

        [HttpGet("{ticketId:int}")]
        public async Task<IActionResult> GetById(int ticketId)
        {
            var result = await _historyService.FindByIdAsync(ticketId);
            return Ok(result);
        }

        [HttpDelete("{ticketId:int}")]
        public async Task<IActionResult> Delete(int ticketId)
        {
            var result = await _historyService.Delete(ticketId);


            return Ok(new { Success = result });
        }

        [HttpGet("GetAll/{page:int}/{take:int}/{search?}")]
        public async Task<IActionResult> GetAll(int page, int take, string? search)
        {

            return Ok(await _historyService.GetAll(take, page, search));

        }
    }
}
