using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Tickets.Application.Services;
using Tickets.Domain.Entities;
using Tickets.DTOs;

namespace Tickets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TicketController : ControllerBase
    {
        private readonly TicketsCase _ticketsCase;
        public TicketController(TicketsCase ticketsCase)
        {
            _ticketsCase = ticketsCase;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(TicketDto ticket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Datos insuficientes" });
            }

            var userId = this.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            return Ok(await _ticketsCase.Add(new Ticket()
            {
                CreatedBy = userId,
                CreaAt = DateTime.UtcNow,
                Description = ticket.Description,
                EndDate = ticket.End,
                StartDate = ticket.Start,
                Hours = ticket.Hour,
                Title = ticket.Title,
                UserId = ticket.UserId,
                IsDeleted = false,
                Status = TicketStatus.Open
            }));
        }

        [HttpGet("{ticketId:int}")]
        public async Task<IActionResult> GetById(int ticketId)
        {
            var result = await _ticketsCase.FindByIdAsync(ticketId);
            return Ok(result);
        }

        [HttpDelete("{ticketId:int}")]
        public async Task<IActionResult> Delete(int ticketId)
        {
            var result = await _ticketsCase.Delete(ticketId);


            return Ok(new { Success = result });
        }

        [HttpGet("GetAll/{page:int}/{take:int}/{search?}")]
        public async Task<IActionResult> GetAll(int page, int take, string? search)
        {

            return Ok(await _ticketsCase.GetAll(take, page, search));

        }
    }
}
