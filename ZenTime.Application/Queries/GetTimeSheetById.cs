using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using ZenTime.Application.Services;
using ZenTime.Domain.TimeSheets;

namespace ZenTime.Application.Queries
{
    public class GetTimeSheetById
    {
        public class Query : IRequest<Response>
        {
            public int TimeSheetId { get; set; }
        }

        public class QueryValidator : AbstractValidator<GetTimeSheetById.Query>
        {
            public QueryValidator()
            {
                RuleFor(c => c.TimeSheetId).GreaterThan(0);
            }
        }
        
        public class Handler : IRequestHandler<Query, Response>
        {
            private readonly ITimeSheetService _timeSheetService;

            public Handler(ITimeSheetService timeSheetService)
            {
                _timeSheetService = timeSheetService;
            }
                
            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                var validator = new QueryValidator();
                await validator.ValidateAndThrowAsync(request, cancellationToken: cancellationToken);
                
                return new Response {
                    TimeSheet = await _timeSheetService.GetTimeSheetById(request.TimeSheetId)
                };
            }
        }
        
        public class Response
        {
            public TimeSheet TimeSheet { get; set; }
        }
    }
}