using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ZenTime.Application.Services;
using ZenTime.Domain.TimeSheets;

namespace ZenTime.Application.Queries
{
    public class GetAllProjects
    {
        public class Query: IRequest<Response> { }

        public class Handler : IRequestHandler<Query, Response>
        {
            private readonly ITimeSheetService _timeSheetService;

            public Handler(ITimeSheetService timeSheetService)
            {
                _timeSheetService = timeSheetService;
            }
                
            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                return new Response(await _timeSheetService.AllProjects());
            }
        }
        
        public class Response
        {
            public IEnumerable<Project> Projects { get; }

            public Response(IEnumerable<Project> projects)
            {
                Projects = projects;
            }
        }
    }
}