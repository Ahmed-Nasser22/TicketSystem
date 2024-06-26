﻿using MediatR;
using TicketSystem.Application.Commands;
using TicketSystem.Application.Interfaces;
using TicketSystem.Domain.Enums;
using TicketSystem.Domain.Models;

namespace TicketSystem.Application.CommandHandlers
{
    public class CreateTicketCommandHandler : IRequestHandler<CreateTicketCommand, Ticket>
    {
        private readonly ITicketRepository ticketRepository;

        public CreateTicketCommandHandler(ITicketRepository ticketRepository)
        {
            this.ticketRepository = ticketRepository;
        }

        public async Task<Ticket> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
        {
            var ticket = new Ticket
            {
                Id = new Guid(),
                CreationDateTime = DateTime.UtcNow,
                PhoneNumber = request.PhoneNumber,
                Governorate = request.Governorate,
                City = request.City,
                District = request.District,
                Status = TicketStatus.New,
            };
           return await ticketRepository.AddTicketAsync(ticket);
        }
    }
}
