using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ZenTime.Api.Controllers;

namespace ZenTime.Api.Features
{
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("[controller]")]
    public class TimeSheetsController : ControllerBase
    {
        private readonly ILogger<TimeSheetsController> _logger;

        public TimeSheetsController(ILogger<TimeSheetsController> logger)
        {
            _logger = logger;
        }
        
        [HttpGet]
        [Route("/projects")]
        public IActionResult GetProjects()
        {
            _logger.LogInformation("Hi there!!");
            return Ok();
        }
        
        [HttpGet]
        [Route("/{id:int}")]
        public IActionResult GetTimesheet([FromRoute] int id)
        {
            _logger.LogInformation("Hi there!!");
            return Ok();
        }
        
        [HttpPost]
        [Route("")]
        public IActionResult CreateTimesheet()
        {
            _logger.LogInformation("Hi there!!");
            return Ok();
        }
    }
}