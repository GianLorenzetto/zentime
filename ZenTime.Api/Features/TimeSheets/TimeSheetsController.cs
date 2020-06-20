using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZenTime.Api.Domain.TimeSheets;
using ZenTime.Api.Domain.TimeSheets.Actions;

namespace ZenTime.Api.Features.TimeSheets
{
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("[controller]")]
    public class TimeSheetsController : ControllerBase
    {
        private readonly ITimeSheetService _timeSheetService;

        public TimeSheetsController(ITimeSheetService timeSheetService)
        {
            _timeSheetService = timeSheetService;
        }
        
        [HttpGet]
        [Route("projects")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProjects()
        {
            var projects = await _timeSheetService.Projects();
            return Ok(new { Projects = projects });
        }
        
        [HttpGet]
        [Route("activities")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetActivities()
        {
            var activities = await _timeSheetService.Activities();
            return Ok(new { Activities = activities });
        }
        
        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetTimeSheetById([FromRoute] int id)
        {
            var ts = await _timeSheetService.GetTimeSheetById(id);
            return Ok(new { TimeSheet = ts });
        }
        
        [HttpPost]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateTimeSheet([FromBody] CreateTimeSheetAction actionRequest)
        {
            var id = _timeSheetService.CreateTimeSheet(actionRequest);
            return CreatedAtRoute(nameof(GetTimeSheetById), id, new { TimeSheetId = id });
        }
    }
}