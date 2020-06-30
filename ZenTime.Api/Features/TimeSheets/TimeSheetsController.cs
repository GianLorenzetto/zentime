using System.Net.Mime;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZenTime.Application.Commands;
using ZenTime.Application.Queries;

namespace ZenTime.Api.Features.TimeSheets
{
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("[controller]")]
    public class TimeSheetsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TimeSheetsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet]
        [Route("projects")]
        [ProducesResponseType(typeof(GetAllProjects.Response), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProjects()
        {
            var projects = await _mediator.Send(new GetAllProjects.Query());
            return Ok(projects);
        }
        
        [HttpGet]
        [Route("activities")]
        [ProducesResponseType(typeof(GetAllActivities.Response), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetActivities()
        {
            var activities = await _mediator.Send(new GetAllActivities.Query());
            return Ok(activities);
        }
        
        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(typeof(GetTimeSheetById.Response), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetTimeSheetById([FromRoute] int id)
        {
            var response = await _mediator.Send(new GetTimeSheetById.Query { TimeSheetId = id });
            return Ok(response);
        }
        
        [HttpPost]
        [Route("")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(CreateTimeSheet.Response), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateTimeSheet([FromBody] CreateTimeSheet.Command request)
        {
            var response = await _mediator.Send(request);
            return CreatedAtRoute(nameof(GetTimeSheetById), response.TimeSheetId, response);
        }
    }
}