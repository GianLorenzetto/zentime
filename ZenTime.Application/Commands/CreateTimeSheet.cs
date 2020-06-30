using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using ZenTime.Application.Services;
using ZenTime.Domain.TimeSheets;

namespace ZenTime.Application.Commands
{
    public class CreateTimeSheet
    {
        public class Command : IRequest<Response>
        {
            public int ProjectId { get; set; }
            public int ActivityId { get; set; }
            public int DurationInMins { get; set; }
            public DateTimeOffset StartedAt { get; set; }
            public string? Details { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(c => c.ProjectId).GreaterThan(0);
                RuleFor(c => c.ActivityId).GreaterThan(0);
                RuleFor(c => c.DurationInMins).GreaterThan(0);
            }
        }
        
        public class Handler : IRequestHandler<Command, Response>
        {
            private readonly ITimeSheetService _timeSheetService;

            public Handler(ITimeSheetService timeSheetService)
            {
                _timeSheetService = timeSheetService;
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                var validator = new CommandValidator();
                await validator.ValidateAndThrowAsync(request, cancellationToken: cancellationToken);
                    
                var timeSheet = new TimeSheet(
                    request.ProjectId,
                    request.ActivityId,
                    request.DurationInMins,
                    request.StartedAt,
                    request.Details
                );
                var id = await _timeSheetService.CreateTimeSheet(timeSheet);
                return new Response {TimeSheetId = id};
            }
        }
        
        public class Response
        {
            public int? TimeSheetId { get; set; }
        }
    }
}