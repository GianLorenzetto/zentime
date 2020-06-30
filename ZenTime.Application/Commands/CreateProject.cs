using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using ZenTime.Application.Services;
using ZenTime.Domain.TimeSheets;

namespace ZenTime.Application.Commands
{
    public class CreateProject
    {
        public class Command : IRequest<Response>
        {
            public string? Name { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(c => c.Name).NotEmpty();
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

                var project = new Project(request.Name!);
                var id = await _timeSheetService.CreatProject(project);
                return new Response {Id = id};
            }
        }
        
        public class Response
        {
            public int? Id { get; set; }
        }
    }
}